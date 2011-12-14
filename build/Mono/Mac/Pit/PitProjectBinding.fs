namespace MonoDevelop.Pit

open MonoDevelop.Core
open MonoDevelop.Projects
open System.Xml

type PitProjectBinding() =
    inherit DotNetProjectBinding()
    
    override this.Name 
        with get() = "Pit"

    override this.CreateProject( language:string,  info:ProjectCreateInformation,  projectOptions:XmlElement) =
        let project = new PitProject(language, info, projectOptions)
        project :> DotNetProject
