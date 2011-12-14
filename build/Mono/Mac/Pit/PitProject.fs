namespace MonoDevelop.Pit

open MonoDevelop.Core
open MonoDevelop.Projects
open MonoDevelop.Core.Serialization
open System.Xml
open System
open System.IO

type Command() =
    
    [<field:ItemProperty("type")>]
    let mutable typeProp :string = "AfterBuild"

    let pfcPath = Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("PitLocation"), "bin") , "pfc.exe")

    [<field:ItemProperty("command")>]
    let mutable com :string = "\"" + pfcPath + "\"" + " ${TargetDir}/${TargetName} /o:${TargetDir}/${TargetName}.js /ft:false"

    
    member this.TypeProp
        with get() = typeProp
        and set(v:string) = typeProp <- v

    member this.Command
        with get() = com
        and set(v) = com <- v
    

type PitProject =
    inherit DotNetProject

    override this.ProjectType 
        with get() = "Pit"
        
    override this.IsLibraryBasedProjectType with get() = true

    [<field:ItemProperty("CustomCommands")>]
    val mutable private customCommands : Command[] 

    member x.CustomCommands 
        with get() = x.customCommands
        and set(value) = x.customCommands <- value
            
    new() = { inherit DotNetProject(); customCommands = [|new Command()|] }

    new(languageName : string) = {  inherit DotNetProject(languageName);customCommands = [|new Command()|]  }

    new(languageName : string, info : ProjectCreateInformation , projectOptions:XmlElement) = { inherit DotNetProject(languageName, info, projectOptions);customCommands = [|new Command()|] }