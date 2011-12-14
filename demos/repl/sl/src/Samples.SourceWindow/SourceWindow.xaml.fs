//----------------------------------------------------------------------------
//
// Copyright (c) 2002-2011 Microsoft Corporation. 
//
// This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// By open this source code in any fashion, you are agreeing to be bound 
// by the terms of the Apache License, Version 2.0.
//
// You must not remove this notice, or any other, from this software.
//----------------------------------------------------------------------------

namespace Samples.SourceWindow

open System
open System.Collections.Generic
open System.IO
open System.Net
open System.Threading
open System.Windows
open System.Windows.Browser
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Documents
open System.Windows.Input
open System.Windows.Media
open Microsoft.FSharp.Compiler.Interactive
open Microsoft.FSharp.Compiler.SourceCodeServices

[<AutoOpen>]
module private Utilities = 
    /// Use this implementation of the dynamic binding operator
    /// to bind to Xaml components in code-behind, see example below
    let (?) (c:obj) (s:string) =
        match c with 
        | :? ResourceDictionary as r ->  r.[s] :?> 'T
        | :? Control as c -> c.FindName(s) :?> 'T
        | _ -> failwith "dynamic lookup failed"
               
    /// Find an element in a sequence, but also keep a reversed-list of the elements in the sequence prior to the discovered element
    let tryFindWithCtxt f (s:seq<'T>) = 
        use e = s.GetEnumerator()
        let mutable found = None
        let mutable ctxt = []
        while found.IsNone && e.MoveNext() do 
            let v = e.Current 
            if f v then 
                found <- Some v
            else
                ctxt <- v :: ctxt 
        ctxt, found
                

type BackgroundWorkerRequest = 
     /// A request which indicates the source code has changed
     | SourceCodeChange of int * string 
     /// A request to do some background work which access the current typecheck state
     | BackgroundWork of (Runner.TypeCheckResults option -> unit)

/// <summary>
/// Defines a composite control to interact with F# Interactive in Silverlight.
/// </summary>
/// <remarks>
/// The composite control is made up of a script editing window, a textual output window,
/// a graphical windows and a toolbar. The control embeds an instance of FSI, feeding it 
/// F# code from the editing text box and capturing output from FSI into the lower output
/// text box.
/// </remarks>
[<ScriptableType>]
type SourceWindowControl() as this = 
    inherit UserControl()
    do Application.LoadComponent(this, new System.Uri("/Samples.SourceWindow;component/SourceWindow.xaml", System.UriKind.Relative));
    let txtInput : RichTextBox = this?txtInput
    let txtOutput : RichTextBox = this?txtOutput
    let jsOutput : RichTextBox = this?jsOutput
    let btnCompile : Button = this?btnCompile
    let btnQuotations : Button = this?btnQuotations
    let btnExec : Button = this?btnExec
    //// Number of spaces inserted when Tab key is pressed.
    let tabSpace = "    "
    //// Delay between a source change and activation of the syntax colorization  
    let colorizationDelay = 100
    //// Delay between a successful colorization and the activation of the typecheck process. 
    let typeCheckDelay = 500
    
    /// The mapping from RichTextBox Run elements to information about them
    let mutable currentMappingToTokens = Dictionary<Run,TokenDefinition>()

    let getFileString fileName = 
        let execAssembly = System.Reflection.Assembly.GetExecutingAssembly()
        using(execAssembly.GetManifestResourceStream(fileName))(fun ms ->
            let stream = new System.IO.StreamReader(ms)
            let text = stream.ReadToEnd()
            stream.Close() 
            text
        )

    let welcomeMessage = getFileString("Source.tt")

    //// Active source code services.
    let srcCodeServices = new Runner.SimpleSourceCodeServices()

    let standardOpts =  [| "--noframework"; "-r:mscorlib.dll"; "-r:FSharp.Core.dll"; "-r:System.Core.dll"; "-r:System.Net.dll"; "-r:Pit.Common.Silverlight.dll"; "-r:Pit.Core.Silverlight.dll"; "-r:Pit.Compiler.Core.Silverlight.dll"; "-r:Pit.Compiler.JsBeautify.Silverlight.dll" |]


    let mutable outFileCount = 0 
    let nextOutFile ext = (outFileCount <- outFileCount + 1; sprintf "out%d.%s" outFileCount ext)

    let mutable sourceVersionStamp = 0 // incrementing sequence of version numbers for source

    /// This is the background worker that runs and reacts to source changes.
    /// The we split into a a number of states which can be reached w.r.t. colorization, type-checking
    /// and serving arbitrary queries of the known type-checking state.
    let codeAnalysisWorker = 

        new MailboxProcessor<_>(fun inbox -> 
            let rec waiting(info:Runner.TypeCheckResults option) = 
                async { let! reqData = inbox.Receive()
                        return! processRequest (reqData, info) (fun () -> waiting info) }

            and processRequest (reqData, info) state = 
                async { match reqData with 
                        | SourceCodeChange(reqStamp,sourceToAnalyze) -> 
                            // The source code changed do not colorize or typecheck, go back and process the message
                            return! colorizeAfterDelayThenTypeCheck (reqStamp,sourceToAnalyze, info) 
                        | BackgroundWork f -> 
                            // Do the requested work, giving access to the current typecheck info
                            f info
                            return! state()

                      }

            and colorizeAfterDelayThenTypeCheck(reqStamp, sourceToAnalyze, info) = 
                async { let! msg = inbox.TryReceive(colorizationDelay) 
                        match msg with 
                        | Some reqData -> 
                            // We got interrupted, so process the message. 
                            return! processRequest (reqData, info) (fun () -> colorizeAfterDelayThenTypeCheck(reqStamp, sourceToAnalyze, info))
                        | None -> 
                            colorize(reqStamp, sourceToAnalyze, info)
                            return! typeCheckAfterDelay(reqStamp, sourceToAnalyze, info) }

 
            /// Compute and apply colorization
            and colorize(reqStamp, sourceToAnalyze, info) = 
                let currentErrors = match info with None -> [| |] | Some info -> info.Errors
                let tokenDefs = RichTextTool.ComputeColorization(srcCodeServices, sourceToAnalyze, currentErrors)
                this.Dispatcher.BeginInvoke(fun () -> 
                    if reqStamp = sourceVersionStamp then 
                        this.ApplyColorization(tokenDefs)) |> ignore 

 
            and typeCheckAfterDelay(reqStamp, sourceToAnalyze, info) = 
                async { let! msg = inbox.TryReceive(typeCheckDelay) 
                        match msg with 
                        | Some reqData -> 
                            // We got interrupted, go process the message.
                            // If the message is a source code change we abandon the type check 
                            return! processRequest (reqData, info) (fun () -> typeCheckAfterDelay(reqStamp, sourceToAnalyze, info))
                        | None -> 

                            let info = srcCodeServices.TypeCheckSource(sourceToAnalyze, standardOpts)
                            let errorText = 
                                [ for x in info.Errors do  
                                      yield sprintf "(%d.%d-%d.%d): %s" x.StartLine x.StartColumn x.EndLine x.EndColumn x.Message ]

                            colorize(reqStamp, sourceToAnalyze, Some info)
                            this.Dispatcher.BeginInvoke(fun () -> 
                                if reqStamp = sourceVersionStamp then 
                                    RichTextTool.SetTextToRichText(txtOutput, String.concat "\n"  errorText)
                                    txtOutput.Selection.Select(txtOutput.ContentStart,txtOutput.ContentStart)) |> ignore 
                            return! waiting(Some info) }
            waiting(None))


    let onSourceChange() =  
        sourceVersionStamp <- sourceVersionStamp + 1; 
        let source = RichTextTool.GetTextFromRichText(txtInput)
        codeAnalysisWorker.Post (SourceCodeChange(sourceVersionStamp, source))
    do codeAnalysisWorker.Start()

    /// Handles initialization of the control.
    member private this.UserControl_Loaded(sender:obj, e:RoutedEventArgs) =
        btnCompile.Click.Add (fun e -> this.Compile())
        btnQuotations.Click.Add (fun e -> this.CompileToDynamicAssembly(exec=false))
        btnExec.Click.Add (fun e -> this.CompileToDynamicAssembly(exec=true))

        txtInput.MouseMove.Add (fun e -> 
            let pos = e.GetPosition(txtInput)
            this.ShowAllInfoAtPosition pos)
        txtInput.MouseRightButtonDown.Add (fun e -> e.Handled <- true)
        txtOutput.MouseRightButtonDown.Add (fun e -> e.Handled <- true)
        // Set up scripting text box
        txtInput.KeyDown.Add(fun e -> 
            match e.Key, Keyboard.Modifiers with 
            //| Key.Decimal, _ -> 
            //    this.ShowCompletionsAtPosition(txtInput.Selection.Start)
            | Key.Enter, ModifierKeys.Control ->
                this.Compile()
                e.Handled <- true
            | Key.Tab, ModifierKeys.None  ->
                txtInput.Selection.Text <- txtInput.Selection.Text + tabSpace
                e.Handled <- true
            | _ -> ())

        txtInput.ContentChanged.Add (fun _ -> onSourceChange())

        RichTextTool.SetTextToRichText(txtInput, welcomeMessage)
        txtInput.Selection.Select(txtInput.ContentStart,txtInput.ContentStart)

        // Set up output text box
        RichTextTool.SetTextToRichText(txtOutput, String.Empty) 


    /// Find the context of names at a given position
    member private this.GetNamesContextAtPosition(pos) = 

        /// A sequence of all the runs in the input text box.
        let allRunsOfInput = 
            seq { for b in txtInput.Blocks do
                      match b with 
                      | :? Paragraph as p -> 
                        for inl in p.Inlines do 
                            match inl with 
                            | :? Run as r -> yield r
                            | _ -> ()
                      | _ -> () }
        /// Find the run that corresponds to the current cursor position and the context of tokens to its left.
        /// TODO: PERFORMANCE: See GetAllRunsOfInput
        let runAtPosition = 
            allRunsOfInput |> tryFindWithCtxt (fun r -> 
                    let rect = r.ContentStart.GetCharacterRect(LogicalDirection.Forward)
                    rect.Union(r.ContentEnd.GetCharacterRect(LogicalDirection.Backward))
                    rect.Contains(pos)) 

        match runAtPosition with 
        | _, None -> None
        | ctxt, Some chosenRun -> 
          // Find the token information that corresponds to the current cursor position, as identified by the Run from the editor.
          let chosenTokenInfo = 
                if currentMappingToTokens.ContainsKey chosenRun then Some currentMappingToTokens.[chosenRun] else None

          // Compute the chain of names at the position and before by looking for "A.", "B." etc. chains in the prior tokens.
          // Note this is a bit inaccurate since it doesn't check the tokens are identifiers.
          let priorNames =
              let rec takeFromContext (l:Run list) acc = 
                  match l with 
                  | r1 :: r2 :: rest when r1.Text = "." -> takeFromContext rest (r2.Text :: acc)
                  | _ -> List.rev acc
              takeFromContext ctxt  []

          Some (chosenTokenInfo, priorNames, chosenRun.Text)


    member private this.ShowAllInfoAtPosition(pos) =
        let txtPos = txtInput.GetPositionFromPoint pos

        // Find the run that corresponds to the current cursor position
        match this.GetNamesContextAtPosition(pos) with 
        | None -> ()
        | Some (chosenTokenInfo, priorNames, chosenName) -> 

        codeAnalysisWorker.Post (BackgroundWork(fun currentTypeCheckResults ->

          // Compute the data tip that would be shown when hovering over this position
          let dataTip = 
              match currentTypeCheckResults, chosenTokenInfo with 
              | Some typeCheckResults, Some tokenDesc -> 
                  match tokenDesc.Token with 
                  | Some token when token.ColorClass = TokenColorKind.Identifier  || token.ColorClass = TokenColorKind.UpperIdentifier  -> 
                      let dataTip = 
                          try typeCheckResults.GetDataTipText(tokenDesc.Line, token.LeftColumn+1, List.append priorNames [ chosenName ])
                          with e -> e.Message
                      Some dataTip
                  | _ -> None
              | _ -> None

          // Compute the error tip that would be shown when hovering over this position
          let errorTip = 
              match currentTypeCheckResults, chosenTokenInfo with 
              | Some typeCheckResults, Some tokenDesc -> 
                  match tokenDesc.ErrorInfo with 
                  | Some err -> Some err.Message
                  | _ -> None
              | _ -> None

          // Compute the completions that would be shown when hovering over this position
          let completions = 
              match currentTypeCheckResults, chosenTokenInfo with 
              | Some typeCheckResults, Some tokenDesc -> 
                  match tokenDesc.Token with 
                  | Some token when token.ColorClass = TokenColorKind.Identifier  || token.ColorClass = TokenColorKind.UpperIdentifier  -> 
                      let dataTip = 
                          try typeCheckResults.GetDeclarations(tokenDesc.Line, token.LeftColumn+1, priorNames, chosenName)
                          with e -> [| |]
                      Some dataTip
                  | _ -> None
              | _ -> None
  
          // Compute the declarations that would be shown if you press "." after this position
          let decls = 
              match currentTypeCheckResults, chosenTokenInfo with 
              | Some typeCheckResults, Some tokenDesc -> 
                  match tokenDesc.Token with 
                  | Some token when token.ColorClass = TokenColorKind.Identifier  || token.ColorClass = TokenColorKind.UpperIdentifier  -> 
                      let dataTip = 
                          try typeCheckResults.GetDeclarations(tokenDesc.Line, token.LeftColumn+1, List.append priorNames [ chosenName ], "")
                          with e -> [| |]
                      Some dataTip
                  | _ -> None
              | _ -> None

          // Compute the declaration location if you to go-to-definition at this position
          let declLocation = 
              match currentTypeCheckResults, chosenTokenInfo with 
              | Some typeCheckResults, Some tokenDesc -> 
                  match tokenDesc.Token with 
                  | Some token when token.ColorClass = TokenColorKind.Identifier  || token.ColorClass = TokenColorKind.UpperIdentifier  -> 
                      try typeCheckResults.GetDeclarationLocation(tokenDesc.Line, token.LeftColumn+1, List.append priorNames [ chosenName ], false)
                      with e -> FindDeclResult.IdNotFound
                  | _ -> FindDeclResult.IdNotFound
              | _ -> FindDeclResult.IdNotFound

          // Compute the F1 help string used for MSDN documentation in Visual Studio if you press F1 at this position
          // This is also the right string for C# assemblies.
          let f1 = 
              match currentTypeCheckResults, chosenTokenInfo with 
              | Some typeCheckResults, Some tokenDesc -> 
                  match tokenDesc.Token with 
                  | Some token when token.ColorClass = TokenColorKind.Identifier  || token.ColorClass = TokenColorKind.UpperIdentifier  -> 
                      try typeCheckResults.GetF1Keyword(tokenDesc.Line, token.LeftColumn+1, List.append priorNames [ chosenName ])
                      with e -> None
                  | _ -> None
              | _ -> None

          // Dump it all out
          let text = 
                [ match errorTip with 
                  | Some err -> 
                      yield sprintf "-------------ERRORS--------------"
                      yield err; 
                      yield ""
                  | _ -> ()
                  match dataTip with 
                  | Some dataTip -> 
                      yield sprintf "-------------QUICK INFO--------------"
                      yield dataTip; 
                      yield ""
                  | _ -> ()
                  match decls with 
                  | Some [| |] ->  ()
                  | Some decls -> 
                      yield sprintf "-------------DOT-COMPLETION--------------"
                      yield sprintf "Decls if you pressed '.' after this token: " 
                      yield String.concat "," 
                             [ for decl in decls -> decl.Name ]
                  | _ -> ()
                  match completions with 
                  | Some [| |] ->  ()
                  | Some decls -> 
                      yield sprintf "-------------PARTIAL WORD COMPLETION--------------"
                      yield sprintf "Completions if you completed this token assuming it is incomplete." 
                      yield sprintf "  (note: if you implement a good filtering algorithm you can reduce this list based on the partial word)" 
                      yield String.concat "," 
                             [ for decl in decls -> decl.Name ]
                  | _ -> ()
                  match declLocation with 
                  | FindDeclResult.DeclFound ((line,col), file, _) -> 
                      yield sprintf "-------------GOTO DEFINITION--------------"
                      yield sprintf "Line %d, Column %d, File %s" line col file; 
                      yield ""
                  | _ -> ()
                  match f1 with 
                  | Some f1str -> 
                      yield sprintf "-------------F1 HELP--------------"
                      yield sprintf "The F1 Help Moniker for MSDN is '%s'" f1str; 
                      yield ""
                  | _ -> ()
                  yield ""
                  yield sprintf "---------------------DETAILS----------------"
                  yield sprintf "token text = '%s'" chosenName 
                  yield ""
                  yield sprintf "token = '%A'" chosenTokenInfo
                  yield ""
                  yield sprintf "chosenTokenInfo = '%A'" chosenTokenInfo ]
          this.Dispatcher.BeginInvoke(fun () -> 
            RichTextTool.SetTextToRichText(txtOutput, String.concat "\n" text)
            txtOutput.Selection.Select(txtOutput.ContentStart,txtOutput.ContentStart)) |> ignore)) 



    member private this.EnterBusyState() =
        btnCompile.IsEnabled <- false
        btnQuotations.IsEnabled <- false
        btnExec.IsEnabled <- false
        { new System.IDisposable with 
            member x.Dispose() = 
                btnCompile.IsEnabled <- true
                btnQuotations.IsEnabled <- true
                btnExec.IsEnabled <- true }

    member private this.Compile() =
        use _holder = this.EnterBusyState() 
        let scriptText = RichTextTool.GetSelectedTextFromRichText(txtInput)
        this.DoCompileToBinaryInIsolatedStorage(scriptText)

    member private this.CompileToDynamicAssembly(exec:bool) =
        use _holder = this.EnterBusyState() 
        let script = RichTextTool.GetSelectedTextFromRichText(txtInput)
        this.DoCompileToDynamicAssembly(script, exec)

    member private this.DoCompileToBinaryInIsolatedStorage(scriptText:string) = 
        try 
            let fileName = "script.fsx"
            // The hosted compiler looks for the input script in isolated storage
            begin 
                use file =  new System.IO.StreamWriter(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CreateFile(fileName))
                file.Write scriptText
            end;
            let outFile = nextOutFile "dll"
            let argv = [| yield "fsc.exe"; yield "-o"; yield outFile; yield "-a"; yield! standardOpts; yield fileName;  |]
            let errors, result = srcCodeServices.Compile argv
            let outFileSize = 
                if result = 0 then 
                    // The hosted compiler writes the output DLL to isolated storage
                    use stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(outFile, FileMode.Open, FileAccess.Read, FileShare.Read)
                    use reader = new System.IO.BinaryReader(stream)
                    reader.BaseStream.Length
                else -1L
            let text = 
                if result = 0 then 
                    [ yield sprintf "The code has been compiled to a DLL '%s' saved in isolated storage." outFile 
                      yield sprintf "Size = %d bytes" outFileSize 
                      yield ""
                      yield "You could develop this application further to analyze this binary or send it to"
                      yield "a hosted service for execution. The binaries produced can be Silverlight, .NET"
                      yield "or other CLI binaries, depending on which mscorlib.dll and FSharp.Core.dll is referenced in"
                      yield "the compilation arguments in the application. The default is that the compilation"
                      yield "references the mscorlib.dll and FSharp.Core.dll for Silverlight, but the application could"
                      yield "be adjusted to reference DLLs stored (in subdirectories) in isolated storage"
                      yield "or DLLs downloaded from trusted DLLs on web sites."
                      yield ""
                      yield sprintf "Any errors and warnings from compilation are shown below\n%A" errors ]
                else
                    [ yield sprintf "Errors and warnings from compilation are shown below:\n%A" errors ]
            RichTextTool.SetTextToRichText(txtOutput, String.concat "\n" text)
            txtOutput.Selection.Select(txtOutput.ContentStart,txtOutput.ContentStart)

        with e -> 
            RichTextTool.SetTextToRichText(txtOutput, e.ToString())
            txtOutput.Selection.Select(txtOutput.ContentStart,txtOutput.ContentStart)

    member private this.DoCompileToDynamicAssembly(scriptText:string, exec) = 
        try 
            let fileName = "script.fsx"
            // The hosted compiler looks for the input script in isolated storage
            begin 
                use file =  new System.IO.StreamWriter(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CreateFile(fileName))
                file.Write scriptText
            end;
            let outFile = nextOutFile (if exec then "exe" else "dll")
            let argv = [| yield "fsc.exe"; 
                          yield "-o"; yield outFile; 
                          if not exec then 
                              yield "-a"; 
                          yield! standardOpts; 
                          yield fileName;  |]

            let stdin, stdout = new Samples.ConsoleApp.CompilerInputStream(), new Samples.ConsoleApp.CompilerOutputStream()
            let stdins, stdouts = (new StreamReader(stdin)), (new StreamWriter(stdout))
            let streams = 
                if exec then 
                    Some ((stdins :> TextReader), (stdouts :> TextWriter), (stdouts :> TextWriter))
                else None
            let errors, result, assemblyOpt = srcCodeServices.CompileToDynamicAssembly (argv, streams)
            stdouts.Flush()
            let outs = stdout.Read()

            let js = match assemblyOpt with 
                     | None -> ""
                     | Some assembly -> 
                        let t = assemblyOpt.Value.GetTypes() |> Array.filter (fun t -> not(t.FullName.Contains("$Script$fsx"))) |>  Pit.Compiler.TypeParser.getAst |> Pit.Compiler.JavaScriptWriter.getJS
                        let bjs = new Pit.Compiler.JsBeautify.JsBeautify(t, new Pit.Compiler.JsBeautify.JsBeautifyOptions())
                        let b = bjs.GetResult()   
                        HtmlPage.Window.Invoke("addJs", b) |> ignore
                        b

            let text = 
                [ if result = 0 then 
                     yield "The code has been compiled to a dynamic Silverlight assembly and loaded."
                     yield ""
                     if exec then 
                         yield "The initialization code has been executed. The output is shown below."
                     else
                         yield "The initialization code has not been excecuted."
                     if exec then 
                         yield ""
                         yield outs
                     else
                         yield ""
                         yield "Because we can reflect on this DLL it is easy to get at its F# reflected definitions."
                         yield "This is useful if we want to translate to code, e.g. to Javascript or to a more exotic"
                         yield "target like a GPU. If any functions or values have been given the [<ReflectedDefinition>]"
                         yield "property then the stored definitions are shown below. You could adjust this application to"
                         yield "process these quotations further, e.g. to convert them to another target language."
                         yield ""
                         yield "(Note, this uses a compilation to generate a DLL targeting Silverlight, and we reflect on that DLL."
                         yield "At the moment it is not quite so simple to reflect over DLLs when"
                         yield "compiling for other target frameworks besides Silverlight, but if you needed to do that"
                         yield "you could consider adjusting the quotations.fs source code from the F# library distribution"
                         yield "to crack the stored tables of quotations data)"
                  match assemblyOpt with 
                  | None -> ()
                  | Some assembly -> 
                      if not exec then 
                          yield sprintf "assembly = %A" assembly
                          let rec reflect (ty:System.Type) = 
                              [ 
                                yield sprintf "type = %s" ty.FullName
                                for m in ty.GetMethods(System.Reflection.BindingFlags.DeclaredOnly ||| System.Reflection.BindingFlags.Public ||| System.Reflection.BindingFlags.NonPublic ||| System.Reflection.BindingFlags.Instance ||| System.Reflection.BindingFlags.Static) do 
                                  match Quotations.Expr.TryGetReflectedDefinition(m) with 
                                  | Some info -> yield sprintf "definition(%s.%s) = %A" m.DeclaringType.FullName m.Name info
                                  | None -> yield sprintf "definition(%s.%s) = (no ReflectedDefinition attribute)" m.DeclaringType.FullName m.Name 
                                for nty in ty.GetNestedTypes(System.Reflection.BindingFlags.DeclaredOnly ||| System.Reflection.BindingFlags.Public ||| System.Reflection.BindingFlags.NonPublic) do 
                                    yield! reflect nty ]

                          for ty in assembly.GetTypes() do 
                              yield! reflect ty   
                  if errors.Length > 0 then 
                      yield sprintf "errors and warnings = %A" errors ]

            RichTextTool.SetTextToRichText(txtOutput, String.concat "\n" text)
            txtOutput.Selection.Select(txtOutput.ContentStart,txtOutput.ContentStart)

            RichTextTool.SetTextToRichText(jsOutput, js)
            jsOutput.Selection.Select(jsOutput.ContentStart,jsOutput.ContentStart)

        with e -> 
            RichTextTool.SetTextToRichText(txtOutput, e.ToString())
            txtOutput.Selection.Select(txtOutput.ContentStart,txtOutput.ContentStart)

    /// Apply a new code colorization 
    member this.ApplyColorization(blockDef: List<List<TokenDefinition>>) =
        // Preserve cursor location
        let cursorPt = RichTextTool.GetCursorPoint(txtInput)

        // Apply the new formatting
        currentMappingToTokens <- RichTextTool.ParagraphDefinitionsToRichText(txtInput, blockDef)

        // Attempt to restore cursor position
        let currTP = txtInput.GetPositionFromPoint(cursorPt)
        if (currTP <> null) then 
            txtInput.Selection.Select(currTP, currTP)



