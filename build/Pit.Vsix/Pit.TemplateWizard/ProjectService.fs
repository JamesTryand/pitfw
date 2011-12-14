namespace Pit.TemplateWizard
open EnvDTE
open VSLangProj
open System.Reflection
open System.IO
open System

[<AutoOpen>]
module Helpers =
    let pitEnvVarialbe = "PitLocation"

    let binPath = Path.Combine(Environment.GetEnvironmentVariable(pitEnvVarialbe, EnvironmentVariableTarget.User) , "bin")
    
    let pfcPath = Path.Combine(binPath,"pfc.exe")

    let pitJsPath = Path.Combine(binPath,"Pit.js")
    
    let runtimeXapPath = Path.Combine(binPath, "Pit.RunTime.xap")

    let getFileString fileName = 
        let execAssembly = System.Reflection.Assembly.GetExecutingAssembly()
        using(execAssembly.GetManifestResourceStream(fileName))(fun ms ->
            let stream = new System.IO.StreamReader(ms)
            let text = stream.ReadToEnd()
            stream.Close() 
            text
        )

    let writeFile (path: String) (textToWrite: String) = 
        let sw = new StreamWriter(path)
        sw.Write(textToWrite)
        sw.Close()
        

[<AutoOpen>]
module ProjectService = 
    let BuildProjectMap (projects:Projects) =
        projects 
        |> Seq.cast<Project> 
        |> Seq.map(fun project -> project.Name, project)
        |> Map.ofSeq

    let BuildAssemblyReferencesMap (references:References) =
        references
        |> Seq.cast<Reference>
        |> Seq.filter (fun reference ->reference.Name.EndsWith(".dbg"))
        |> Seq.map (fun reference -> 
            if reference.Name.EndsWith(".dbg") then
                reference.Name.Replace("dbg", ""), reference.Path.Replace("dbg.dll", "dll")
            else reference.Name, reference.Path 
            )           