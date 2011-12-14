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


type LibWizard() as this =  
    [<DefaultValue>] val mutable solution : Solution2
    [<DefaultValue>] val mutable dte : DTE
    [<DefaultValue>] val mutable dte2 : DTE2
    [<DefaultValue>] val mutable serviceProvider : IServiceProvider
    [<DefaultValue>] val mutable destinationPath : string
    [<DefaultValue>] val mutable safeProjectName : string    
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
        member this.ProjectFinishedGenerating project = "Not Implemented" |> ignore
        member this.ProjectItemFinishedGenerating projectItem = "Not Implemented" |> ignore
        member this.ShouldAddProjectItem filePath = true
        member this.BeforeOpeningFile projectItem = "Not Implemented" |> ignore
        member this.RunFinished() = 
            let currentCursor = Cursor.Current
            Cursor.Current <- Cursors.WaitCursor
            try
                let templatePath = this.solution.GetProjectTemplate("PitLibrary.zip", "FSharp")
                let dName = Path.GetDirectoryName(this.destinationPath)
                let soln = this.serviceProvider.GetService(typeof<IVsSolution>) :?> IVsSolution
                let AddProject status projectVsTemplateName projectName destPath =
                        this.dte2.StatusBar.Text <- status
                        let path = templatePath.Replace("PitLibrary.vstemplate", projectVsTemplateName)
                        this.solution.AddFromTemplate(path, destPath, projectName, false) |> ignore
                AddProject "Adding the F# Pit Debug project..." 
                        (Path.Combine("PitLibrary.dbg", "PitLibrary.dbg.vstemplate")) 
                        (this.safeProjectName + ".dbg")
                        (Path.Combine(dName, (this.safeProjectName + ".dbg")))
                AddProject "Adding the F# Pit project..." 
                        (Path.Combine("PitLibrary", "PitLibrary.vstemplate")) 
                        this.safeProjectName 
                        (Path.Combine(dName, this.safeProjectName))
            finally
                Cursor.Current <- currentCursor
            


