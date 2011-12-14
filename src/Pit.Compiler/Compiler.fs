namespace Pit.Compiler

open System
open System.IO
open System.Linq
open System.CodeDom.Compiler
open Microsoft.FSharp.Compiler.CodeDom
open System.Reflection
open System.Text
open Pit.Compiler.JsBeautify
open Pit

module PitCodeCompiler =

    let private CompileFSharpString(srcFiles, depAssemblies : string[]) =
        use pro = new FSharpCodeProvider()
        let opt = CompilerParameters()
        opt.GenerateInMemory <- true
        depAssemblies
        |> Seq.iter(fun asm ->
            opt.ReferencedAssemblies.Add(asm) |> ignore
        )
        srcFiles |> Seq.iter(fun s -> opt.TempFiles.AddFile(s, true) |> ignore)
        let res = pro.CompileAssemblyFromFile( opt, srcFiles )
        let errors = [for a in res.Errors do
                            if not(a.IsWarning) then
                                yield a
                         ]
        if errors.Length = 0 then
                (errors, Some(res.CompiledAssembly))
        else (errors, None)

    let private (++) v1 v2   = Path.Combine(v1, v2)
    let private assemblyDirectory = Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase).Remove(0, 6)
    let private randomFile directory = directory ++ Path.GetRandomFileName() + ".dll"

    let private compile1 (srcFiles : string[]) (assemblies : string[]) (directory : string) (printAst : bool) =
        let result = CompileFSharpString(srcFiles, assemblies)
        let errors = fst result
        let genAsm = snd result
        if genAsm.IsSome then
            let asm = genAsm.Value
            let types = asm.GetExportedTypes()
            let js = TypeParser.getAst types |> JavaScriptWriter.getJS
            (*let js = seq {
                for a in ast do
                    if printAst then
                        printfn "%A" a
                    let jscript = JavaScriptWriter.getJS a
                    yield jscript
            }*)
            (errors, js)
        else
            (errors, "")

    let Compile (srcFiles : string[]) (assemblies : string[]) (outputfile : string) (directory : string) (formatJs : bool) (printAst : bool)=
        let er, js = compile1 srcFiles assemblies directory printAst
        if er.Length = 0 then
            use fs = File.Create(outputfile)
            use sw = new StreamWriter(fs)
            //sw.Write(js)
            if formatJs then
                let bjs = new JsBeautify(js, new JsBeautifyOptions())
                let b = bjs.GetResult()
                sw.Write(b)
            else
                sw.Write(js)
            
            printfn "Generated Output File %s" outputfile
        else
            eprintfn "%A" er

    let GenAst (dllPath:String) (outputfile : string) (formatJs : bool) (printAst : bool) =
        let asm =  Assembly.LoadFrom(dllPath)
        if asm.GetCustomAttributes(typeof<PitAssemblyAttribute>, true).Length = 1 then
            let types = asm.GetExportedTypes()
            let ast = TypeParser.getAst types
            let js = ast |> JavaScriptWriter.getJS (*seq {
                for a in ast do
                    if printAst then
                        printfn "%A" a
                    let jscript = JavaScriptWriter.getJS a
                    yield jscript
            }*)
            //let res =  Seq.toArray(js)
            use fs = File.Create(outputfile)
            use sw = new StreamWriter(fs)
            if formatJs then
                let bjs = new JsBeautify(js, new JsBeautifyOptions())
                let b = bjs.GetResult()
                sw.Write(b)
            else
                sw.Write(js)
            printfn "Generated Output File %s" outputfile