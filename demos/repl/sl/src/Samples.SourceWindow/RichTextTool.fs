//----------------------------------------------------------------------------
//
// Copyright (c) 2002-2011 Microsoft Corporation. 
//
// This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// By using this source code in any fashion, you are agreeing to be bound 
// by the terms of the Apache License, Version 2.0.
//
// You must not remove this notice, or any other, from this software.
//----------------------------------------------------------------------------

namespace Samples.SourceWindow

open System.Collections.Generic
open System.IO
open System.Text
open System.Windows
open System.Windows.Controls
open System.Windows.Documents
open System.Windows.Media
open Microsoft.FSharp.Compiler.Interactive
open Microsoft.FSharp.Compiler.SourceCodeServices

// TODO: none of this should need to be mutable
type TokenDefinition = 
    { mutable Text : string; 
      mutable Foreground: SolidColorBrush; 
      mutable Token: TokenInformation option; 
      mutable Line : int; 
      mutable ErrorInfo:ErrorInfo option }

type RichTextTool() =
    // Create color palette used for syntax highlighting
    static let BlackBrush = SolidColorBrush(Colors.Black)
    static let BlueBrush = SolidColorBrush(Colors.Blue)
    static let DarkRedBrush = SolidColorBrush(Color.FromArgb(255uy, 128uy, 0uy, 0uy))
    static let GrayBrush = SolidColorBrush(Colors.Gray)
    static let GreenBrush = SolidColorBrush(Colors.Green)

    /// Returns the entire text in the specified RichTextBox.
    static member GetTextFromRichText(rtb: RichTextBox) = 
        let start = rtb.Selection.Start
        let fin = rtb.Selection.End

        rtb.SelectAll()
        let text = rtb.Selection.Text

        rtb.Selection.Select(start, fin)

        text

    /// Returns the currently selected text in the specified RichTextBox.
    /// If no text is selected, the entire content is returned.
    static member GetSelectedTextFromRichText(rtb: RichTextBox) = 
        if (rtb.Selection.Text.Length < 1) then
            RichTextTool.GetTextFromRichText(rtb)
        else 
            rtb.Selection.Text

    /// Sets the specified text as the content of the given RichTextBox control.
    /// The current text is overwritten.
    static member SetTextToRichText( rtb: RichTextBox, text: string ) = 
        rtb.SelectAll()
        rtb.Selection.Text <- text

    /// Appends the specified text to the content of the given RichTextBox control.
    static member AppendTextToRichText(rtb: RichTextBox, text: string) = 
        rtb.SelectAll()
        let mutable current = rtb.Selection.Text
        if (current = null) then 
            current <- System.String.Empty

        rtb.Selection.Text <- current + text

    /// Returns a Point describing the current cursor insertion point in 
    /// the specified RichTextBox.
    static member GetCursorPoint(rtb: RichTextBox) = 
        let selEnd = rtb.Selection.End
        let fwdRect = selEnd.GetCharacterRect(LogicalDirection.Forward)
        let bkdRect = selEnd.GetCharacterRect(LogicalDirection.Backward)
        fwdRect.Union(bkdRect)
        let ret = Point(fwdRect.X + fwdRect.Width / 2.0, fwdRect.Y + fwdRect.Height / 2.0)
        ret

    /// <summary>
    /// Breaks the specified source code into a list of (string, color) pairs. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// A TokenDefinition is a (string, color) pair. It will become a Run in the RichTextBox model.
    /// A list of TokenDefinition instances will become a Paragraph. The overall content of the
    /// RichTextBox is a list of paragraphs, hence the return type of this method.
    /// </para>
    /// <para>
    /// This method does not have to execute on the UI thread.
    /// </para>
    /// </remarks>
    static member  ComputeColorization(srcCodeServices: Runner.SimpleSourceCodeServices, source: string, currentErrors: ErrorInfo[]) : List<List<TokenDefinition>> = 
        let blocks = new List<List<TokenDefinition>>()
        use reader = new StringReader(source) 
        let mutable line = reader.ReadLine()

        // Tokenizer state
        let mutable state = 0L
        let mutable lineCount = 0

        // TODO: this search repeatedly reiterates the error list at each token
        let getErrorInfoForToken (lineCount, token: TokenInformation) = 
            let lineCount = lineCount
            currentErrors |> Array.tryFind (fun e -> 
                e.StartLine <= lineCount && lineCount <= e.EndLine && 
                (e.StartLine < lineCount || e.StartColumn <= token.LeftColumn)  &&
                (lineCount < e.EndLine  ||  token.RightColumn < e.EndColumn))

        while (line <> null) do
            let (tokens, st) = srcCodeServices.TokenizeLine(line, state)
            state <- st

            let p = new List<TokenDefinition>()

            let mutable rdef = { Text = line; Foreground = BlackBrush; Token = None; Line=lineCount; ErrorInfo=None }
            p.Add(rdef)
            //// Variable 'left' keeps track of where 'rdef.Text' begins in the current 'line'
            let mutable left = 0
            for token in tokens do
                let brush = 
                   match token.ColorClass with 
                    | TokenColorKind.Comment -> GreenBrush
                    | TokenColorKind.InactiveCode -> GrayBrush
                    | TokenColorKind.Keyword 
                    | TokenColorKind.PreprocessorKeyword -> BlueBrush
                    | TokenColorKind.String -> DarkRedBrush
                    | _ -> BlackBrush

                // The first run in the line is an exception. We can just change its color.
                if (token.LeftColumn = 0) then 
                    rdef.Foreground <- brush
                    rdef.Token <- Some token
                    rdef.ErrorInfo <- getErrorInfoForToken (lineCount, token)
                else
                    rdef.Text <- line.Substring(left, token.LeftColumn - left)
                    left <- left + rdef.Text.Length
                    let err =  getErrorInfoForToken (lineCount, token)
                    rdef <- { Text = line.Substring(token.LeftColumn); Foreground = brush; Token=Some token; Line=lineCount; ErrorInfo=err }
                    p.Add(rdef)

            blocks.Add(p)
            line <- reader.ReadLine()
            lineCount <- lineCount + 1

        // If source ends with a line break, add a paragraph with an empty run
        if (source.EndsWith("\n") || source.EndsWith("\r")) then
            let p = new List<TokenDefinition>()
            let rdef = { Text = System.String.Empty; Foreground = BlackBrush; Token=None; Line=lineCount; ErrorInfo=None }
            p.Add(rdef)
            blocks.Add(p)

        blocks

    /// <summary>
    /// Update the content of the specified RichTextBox to match the content defined by
    /// the given list of paragraph definitions.
    /// </summary>
    /// <remarks>
    /// Caller must invoke this method on the UI thread.
    /// </remarks>
    static member ParagraphDefinitionsToRichText(rtb:RichTextBox, lineDefs: List<List<TokenDefinition>>) =
        // Count number of paragraphs that we can keep unmodified (rtb.Blocks[0..numToKeep]).
        let mapping = new System.Collections.Generic.Dictionary<_,_>()
        let numToKeep = 
            (rtb.Blocks,lineDefs) 
            ||> Seq.zip 
            |> Seq.takeWhile (fun (b,defs) -> 
                match b with 
                | :? Paragraph as p -> 
                        (p.Inlines.Count = defs.Count) &&
                        (p.Inlines, defs) ||> Seq.forall2 (fun r d -> 
                            match r with 
                            | :? Run as r -> 
                                if (r <> null) && (r.Foreground.Equals d.Foreground) && (r.Text = d.Text) && ((r.TextDecorations = TextDecorations.Underline) = d.ErrorInfo.IsSome )  then 
                                    mapping.[r] <- d
                                    true // keep
                                else
                                    false // discard
                            | _ -> false)
                | _ -> false)
            |> Seq.length


        // Remove blocks that are obsolete
        let numToRemove = rtb.Blocks.Count - numToKeep
        for i in 0 .. numToRemove - 1  do
            rtb.Blocks.RemoveAt(numToKeep)    

        // Add new blocks
        let numToAdd = lineDefs.Count - numToKeep
        for i in 0 .. numToAdd - 1  do
            let lineDef = lineDefs.[numToKeep + i]
            let paragraph = Paragraph()
            for tokenDef in lineDef do
                let r = Run(Text = tokenDef.Text, Foreground = tokenDef.Foreground)
                if tokenDef.ErrorInfo.IsSome then 
                    r.TextDecorations <- TextDecorations.Underline
                mapping.[r] <- tokenDef
                
                paragraph.Inlines.Add(r)

            rtb.Blocks.Add(paragraph)
        mapping
