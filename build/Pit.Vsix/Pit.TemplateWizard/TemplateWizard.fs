namespace Pit.TemplateWizard
open System
open System.IO
open System.Windows.Forms
open System.Collections.Generic
open EnvDTE
open EnvDTE80
open Microsoft.VisualStudio.Shell
open Microsoft.VisualStudio.Shell.Interop
open Microsoft.VisualStudio.Web.Silverlight
open Microsoft.VisualStudio.TemplateWizard
open VSLangProj
open Pit.TemplateDialog
open System.Reflection
open Microsoft.Win32
open Pit.TemplateDialog
open System.Collections.ObjectModel
open System.Diagnostics

type Wizard() as this =  
    [<DefaultValue>] val mutable solution : Solution2
    [<DefaultValue>] val mutable dte : DTE
    [<DefaultValue>] val mutable dte2 : DTE2
    [<DefaultValue>] val mutable serviceProvider : IServiceProvider
    [<DefaultValue>] val mutable destinationPath : string
    [<DefaultValue>] val mutable safeProjectName : string
    [<DefaultValue(false)>] val mutable includePitTestProject : bool
    [<DefaultValue>] val mutable supportedWebInfo : List<SupportedWebData>
    [<DefaultValue>] val mutable webProjs : ObservableCollection<String>
    [<DefaultValue>] val mutable viewModel : MainViewModel
    let processInfo = 
        new ProcessStartInfo(UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true, WorkingDirectory = Directory.GetCurrentDirectory(), FileName = Helpers.pfcPath)

    do
        this.webProjs <- new ObservableCollection<String>()
        this.supportedWebInfo <- new List<SupportedWebData>()
    interface IWizard with
        member this.RunStarted (automationObject:Object, 
                                replacementsDictionary:Dictionary<string,string>, 
                                runKind:WizardRunKind, customParams:Object[]) =
            this.dte <- automationObject :?> DTE
            this.dte2 <- automationObject :?> DTE2
            this.solution <- this.dte2.Solution :?> Solution2
            this.serviceProvider <- new ServiceProvider(automationObject :?> 
                                     Microsoft.VisualStudio.OLE.Interop.IServiceProvider)
            this.destinationPath <- replacementsDictionary.["$destinationdirectory$"]
            this.safeProjectName <- replacementsDictionary.["$safeprojectname$"]
            let mutable text = ref String.Empty
            replacementsDictionary.TryGetValue("$exclusiveproject$", text) |> ignore
            let addingToSolution = !text = null || String.Compare(!text, "True", true) <> 0
            let soln = this.serviceProvider.GetService(typeof<IVsSolution>) :?> IVsSolution
            
            let mutable empty = ref Guid.Empty
            let mutable numHierarchies = ref null
            this.webProjs.Add("<New Web Project>")
            let p = soln.GetProjectEnum(1u, empty, numHierarchies) 
            if !numHierarchies <> null then
                let mutable array = System.Array.CreateInstance(typeof<IVsHierarchy>, 1) :?> IVsHierarchy[]
                let mutable num = ref 0u              
                let k =  !numHierarchies              
                while (k.Next(1u, array, num) = 0 && !num > 0u) do
                    if (array).[0] :? IVsSilverlightProjectConsumer then
                        let projectName = (array).[0].GetProperty(4294967294u, -2012)
                        this.webProjs.Add((snd projectName).ToString())
            
            try                
                let service = this.serviceProvider.GetService(typeof<ILocalRegistry>) :?> ILocalRegistry2
                let mutable root = ref String.Empty
                service.GetLocalRegistryRoot(root) |> ignore
                let lCIDAsString = "1033"
                let name = !root + "\\Packages\\{CB22EE0E-4072-4ae7-96E2-90FCCF879544}\\SupportedWebProjects";
                use registryKey = Registry.LocalMachine.OpenSubKey(name, false)
                if registryKey <> null then
                    let subKeyNames = registryKey.GetSubKeyNames()
                    let array = subKeyNames
                    for i = 0 to array.Length - 1 do
                        let name2 = array.[i]
                        use registryKey2 = registryKey.OpenSubKey(name2, false)
                        if registryKey2 <> null then
                            let res = registryKey2.GetValue(null) |> string
                            let mutable text = ref String.Empty
                            let t = this.serviceProvider.GetService(typeof<SVsResourceManager>) :?> IVsResourceManager
                            let guid = ref (Guid.Parse("{CB22EE0E-4072-4ae7-96E2-90FCCF879544}"))
                            t.LoadResourceString(guid, -1, res, text) |> ignore
                            if not(String.IsNullOrEmpty(!text)) then 
                                let subKeyNames2 = registryKey2.GetSubKeyNames()
                                if subKeyNames2.Length > 0 then
                                    let mutable sortIndex = 100u
                                    let mutable supportsAspNetIntegration = true
                                    let mutable value = registryKey2.GetValue("SortIndex")
                                    if value :? int then sortIndex <- value.ToString() |> int |> uint32
                                    value <- registryKey2.GetValue("SupportsAspNetIntegration")
                                    if value :? int then supportsAspNetIntegration <- ((value.ToString() |> int) <> 0)
                                    let supportedWebData = new SupportedWebData(!text, sortIndex, supportsAspNetIntegration)
                                    this.supportedWebInfo.Add(supportedWebData);
                                    let array2 = subKeyNames2
                                    for j = 0 to array2.Length - 1 do
                                        let text2 = array2.[j]
                                        use registryKey3 = registryKey2.OpenSubKey(text2, false)
                                        if registryKey3 <> null then 
                                            let text3 = registryKey3.GetValue(null) |> string
                                            if not(String.IsNullOrEmpty(text3)) then
                                                let supportedWebDataEntry = new SupportedWebDataEntry()
                                                supportedWebDataEntry.aspnetLanguage <- text2
                                                let i = text3.LastIndexOf("{lcid}")
                                                let p = text3.Substring(i + 7)
                                                supportedWebDataEntry.projectTemplatePath <-  p
                                                supportedWebData.Entries.Add(supportedWebDataEntry)
                    this.supportedWebInfo.Sort(new Comparison<SupportedWebData>(SupportedWebData.CompareBySortIndex))
            with | _ -> ()   
            
            this.viewModel <- new MainViewModel(this.supportedWebInfo.[0])
            this.viewModel.WebProjName <- this.safeProjectName + ".Web"
            this.viewModel.WebProjects <- this.webProjs
            this.viewModel.WebProjectTypes <- this.supportedWebInfo
            if this.webProjs.Count > 1 then 
                this.viewModel.SelectedWebProjectName <- this.webProjs.[1]
                this.viewModel.ShouldAddNewWebApp <- false
            else this.viewModel.SelectedWebProjectName <- this.webProjs.[0]
            let win = Main.getMainDialog(this.viewModel)             
            let dialog = win.ShowDialog()
            if dialog.HasValue then 
                match dialog.Value with
                | true -> 
                    ()
                | _ ->
                    raise (new WizardCancelledException())
        member this.ProjectFinishedGenerating project = "Not Implemented" |> ignore
        member this.ProjectItemFinishedGenerating projectItem = "Not Implemented" |> ignore
        member this.ShouldAddProjectItem filePath = true
        member this.BeforeOpeningFile projectItem = "Not Implemented" |> ignore
        member this.RunFinished() = 
            let currentCursor = Cursor.Current
            Cursor.Current <- Cursors.WaitCursor
            try
                let pitApp = this.safeProjectName 
                let templatePath = this.solution.GetProjectTemplate("PitApp.zip", "FSharp")
                try
                    let AddProject status projectVsTemplateName projectName =
                        this.dte2.StatusBar.Text <- status
                        let path = templatePath.Replace("PitApp.vstemplate", projectVsTemplateName)
                        this.solution.AddFromTemplate(path, this.destinationPath, projectName, false) |> ignore
                    let mutable text = Path.GetDirectoryName(this.destinationPath)
                    text <- System.IO.Path.Combine(text, this.viewModel.WebProjName)
                    if this.viewModel.SelectedWebProjectName = "<New Web Project>" then
                        let p1 = this.viewModel.SelectedWebProjectType.Entries.[0].projectTemplatePath.Split('\\')
                        let p2 = this.solution.GetProjectTemplate(p1.[0], "CSharp")
                        this.solution.AddFromTemplate(p2, text, this.viewModel.WebProjName, false) |> ignore  
                    else    this.viewModel.WebProjName <-  this.viewModel.SelectedWebProjectName                
                    AddProject "Adding the F# Pit Silverlight Application project..." 
                        (Path.Combine("PitApplication", "PitApplication.vstemplate")) pitApp                   
                    let soln = this.serviceProvider.GetService(typeof<IVsSolution>) :?> IVsSolution
                    let projects = BuildProjectMap this.dte.Solution.Projects
                    
                    let webProjH = ref null
                    soln.GetProjectOfUniqueName(projects.Item(this.viewModel.WebProjName).UniqueName, webProjH) |> ignore
                    let pAppH = ref null               
                    soln.GetProjectOfUniqueName(projects.Item(pitApp).UniqueName, pAppH) |> ignore
                    
                    this.dte2.StatusBar.Text <- "Updating silverlight project references..."
                    let consumer = !webProjH :?> IVsSilverlightProjectConsumer
                    consumer.LinkToSilverlightProject("ClientBin", true, false, !pAppH:?>IVsSilverlightProject)
                    let k = projects.Item(pitApp).Object :?> VSProject
                    let ki = projects.Item(this.viewModel.WebProjName).Object :?> VSProject                       
                    try 
                        ki.Project.ProjectItems.Item("Scripts") |> ignore
                    with 
                        | _ -> ki.Project.ProjectItems.AddFolder("Scripts") |> ignore                  
                    
                    let p = Path.GetDirectoryName(ki.Project.FileName)
                    let scriptsPath = System.IO.Path.Combine(p, "Scripts")

                    let pitJSDestPath = System.IO.Path.Combine(scriptsPath, "Pit.js")
                    File.Copy(Helpers.pitJsPath, pitJSDestPath, true)                    
                    ki.Project.ProjectItems.AddFromFile(pitJSDestPath) |> ignore

                    let emptyAppJsPath = System.IO.Path.Combine(scriptsPath, pitApp + ".js")
                    File.Create(emptyAppJsPath) |> ignore            
                    ki.Project.ProjectItems.AddFromFile(emptyAppJsPath) |> ignore

                    let clientBinPath = System.IO.Path.Combine(p, "ClientBin")
                    let runtimeDestPath = System.IO.Path.Combine(clientBinPath, "Pit.Runtime.xap")
                    File.Copy(Helpers.runtimeXapPath, runtimeDestPath, true)  
                    ki.Project.ProjectItems.AddFromFile(runtimeDestPath) |> ignore

                    let slDestPath = System.IO.Path.Combine(scriptsPath, pitApp + ".Silverlight.js")
                    Helpers.getFileString("Pit.Silverlight.js.template").Replace("<%clientxap%>", "clientXaps=" + pitApp + ".xap") |> Helpers.writeFile slDestPath  
                    ki.Project.ProjectItems.AddFromFile(slDestPath) |> ignore

                    let htmlDestPath = System.IO.Path.Combine(p, pitApp + ".htm")
                    Helpers.getFileString("Pit.Html.Template").Replace("<%replace%>", pitApp) |> Helpers.writeFile htmlDestPath  
                    ki.Project.ProjectItems.AddFromFile(htmlDestPath) |> ignore

                    let references = BuildAssemblyReferencesMap k.References
                    references 
                    |> Seq.iter ( fun (n, p) ->
                                    this.dte2.StatusBar.Text <- String.Format("Creating Js file for {0}..." ,  n)
                                    let pRes = System.IO.Path.Combine(scriptsPath, (n + "js")) 
                                    if not(File.Exists(pRes)) then
                                        let args =  "\"" + p + "\"" + " /o:" + "\"" + pRes + "\"" + " /ft:true"                                  
                                        processInfo.Arguments <- args
                                        use pfbuild = Process.Start(processInfo)
                                        let stdout = pfbuild.StandardOutput.ReadToEnd()
                                        pfbuild.WaitForExit()
                                        let err = pfbuild.StandardError.ReadToEnd()
                                        if err.Length = 0 then
                                            this.dte2.StatusBar.Text <- stdout
                                            if File.Exists(pRes) then ki.Project.ProjectItems.AddFromFile(pRes) |> ignore
                                        else  this.dte2.StatusBar.Text <- String.Format("Error occurred in creating Js file for {0}..." ,  n)                                 
                                        pfbuild.Close()
                        )                       

                with
                | ex -> failwith (sprintf "%s\n\r%s\n\r%s\n\r%s\n\r%s" 
                            "The project creation has failed."
                            "Ensure that you have installed Silverlight 4 and Pit Assemblies" 
                            "Send error to Pit team"
                            "The actual exception message is: "
                            ex.Message)
            finally
                Cursor.Current <- currentCursor
            


