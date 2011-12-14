open System
open System.IO
open System.Xml
open System.Xml.Linq
open System.Linq
open Pit
open Pit.Compiler

let xname name = XName.Get name
let private (++) v1 v2   = Path.Combine(v1, v2)

[<EntryPoint>]
let main args =
    if args.Length = 0 || args.Length < 1 then
        let s  = "Usage of Pit Compiler should be as below,

pfc.exe test.fsproj /o:output.js /ft:true

/o - Output file
/ft - Format JS true / false
/ast - Show AST true / false"
        printfn "%s" s
    else
        let parseArg(cmp) =
            let bitString = args.FirstOrDefault((fun s -> s.Contains(cmp)))            
            if bitString <> null && bitString <> String.Empty then
                let b = bitString.Split([|':'|])
                Some(b.[0], b.[1])
            else
                None

        let opfile = args.[1].Replace("/o:","")

        let mutable formatJsArg = parseArg("/ft:")            
        let mutable formatJs = 
            match formatJsArg with
            | Some(j, j1) -> Boolean.Parse(j1)
            | _ -> false

        let printAstArg = parseArg("/ast:")
        let printAst =
            match printAstArg with
            | Some(j, j1) -> Boolean.Parse(j1)
            | _ -> false

        if args.[0].EndsWith("fsproj") then 
            let projName = args.[0]
            let projFolderLoc = projName.Substring(0, projName.LastIndexOf("\\"))
            let outputFolderLoc = Path.Combine(Environment.GetEnvironmentVariable("PitLocation", EnvironmentVariableTarget.User) , "bin")
            //let outputFolderLoc = projFolderLoc + @"\bin\Release\"        
      
        
            let xdoc = XDocument.Load(projName)
            let rootEl = xdoc.Root
            let els = rootEl.Elements().Where((fun (s : XElement) -> s.Name.LocalName = "ItemGroup"))
            let asmEls =
                els
                |> Seq.map(fun (x : XElement) ->
                    let c = x.Elements().ToArray() |> Array.filter(fun (xe : XElement) -> xe.Name.LocalName = "Reference" || xe.Name.LocalName = "ProjectReference" )
                    c
                )
                |> Seq.filter(fun (x : XElement[])-> x.Length > 0)
            let assemblies = 
                seq {
                    for x in asmEls do
                        for xe in x do                        
                            let asmName = xe.Attribute(xname "Include").Value
                            //if hint path                
                            let hint = xe.Elements().Where((fun (s : XElement) -> s.Name.LocalName = "HintPath")).ToArray()
                            if hint.Length > 0 then
                                let hintPath = hint.First().Value
                                yield hintPath
                            else
                                if xe.Name.LocalName = "ProjectReference" then 
                                    let el = xe.Elements().First()                                
                                    yield el.Value + ".dll"
                                else yield asmName + ".dll"
                }
                |> Seq.filter(fun x -> x <> String.Empty && x.Contains("Pit"))
                |> Seq.map(fun x ->
                    let dll = 
                        if x.Contains("\\") then 
                            let xi = x.LastIndexOf("\\") + 1
                            outputFolderLoc ++ x.Substring(xi, x.Length - xi) 
                        else x
                    dll
                )
                |> Seq.toArray

            printfn "%A" assemblies 

            let compileEls =
                els
                |> Seq.map(fun (x : XElement) -> 
                    let c = x.Elements().ToArray() |> Array.filter(fun (xe : XElement) -> xe.Name.LocalName = "Compile" )
                    c
                )
                |> Seq.filter(fun (x : XElement[])-> x.Length > 0)
            let srcFiles = 
                seq {
                    for x in compileEls do
                        for xe in x do
                            let fileName = xe.Attribute(xname "Include").Value
                            yield projFolderLoc ++ fileName
                }
                |> Seq.toArray
            printfn "%A" srcFiles

            if srcFiles.Length > 0 then            
                PitCodeCompiler.Compile srcFiles assemblies opfile projFolderLoc formatJs printAst

        else 
            let dllPath = args.[0]
            PitCodeCompiler.GenAst dllPath opfile formatJs printAst

    //Console.ReadKey(true) |> ignore
    0