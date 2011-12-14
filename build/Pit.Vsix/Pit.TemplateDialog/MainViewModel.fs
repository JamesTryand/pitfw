namespace Pit.TemplateDialog
open System
open System.Collections.Generic
open System.ComponentModel
open System.Collections.ObjectModel


type SupportedWebDataEntry() =
    let mutable ptPath = String.Empty
    let mutable aspnet = String.Empty    
    member this.projectTemplatePath 
        with get() = ptPath
        and set(v) = ptPath <- v
    member this.aspnetLanguage 
        with get() = aspnet
        and set(v) = aspnet <- v


type SupportedWebData(n, sortIndex, supportsAspNetIntegration) =
    let mutable name = n    
    let mutable entries = new List<SupportedWebDataEntry>()
    let mutable aspNetInteg = supportsAspNetIntegration
    let mutable sortIndex = sortIndex            
    member this.Name 
        with get() = name
        and set(v) = name <- v
    member this.SortIndex 
        with get() = sortIndex
        and set(v) = sortIndex <- v
    member this.Entries 
        with get() = entries
        and set(v) = entries <- v
    member this.SupportsAspNetIntegration 
        with get() = aspNetInteg
        and set(v) = aspNetInteg <- v

    static member CompareBySortIndex (a:SupportedWebData) (b:SupportedWebData) =
        let mutable index = 1
        if (a.SortIndex < b.SortIndex) then index <- -1
        if (a.SortIndex = b.SortIndex) then index <- 0
        index

[<AllowNullLiteral>]
type MainViewModel(selected:SupportedWebData) =
    inherit ViewModelBase()

    let mutable shouldAddNewWebApp = true   

    let mutable webProjectName = String.Empty

    let mutable selectedWebProjectName = String.Empty

    let mutable selectedWebProjectType:SupportedWebData = selected

    let mutable webProjectTypes = new List<SupportedWebData>()

    let mutable webProjects = new ObservableCollection<String>()

    member x.ShouldAddNewWebApp 
        with get() = shouldAddNewWebApp
        and set(v) =
            shouldAddNewWebApp <- v
            x.OnPropertyChanged("ShouldAddNewWebApp")

    member x.WebProjName 
        with get() = webProjectName
        and set(v) =
            webProjectName <- v
            x.OnPropertyChanged("WebProjName")

    member x.WebProjectTypes 
        with get() = webProjectTypes
        and set(v) =
            webProjectTypes <- v
            x.OnPropertyChanged("WebProjectTypes")

    member x.WebProjects
        with get() = webProjects
        and set(v) =
            webProjects <- v
            x.OnPropertyChanged("WebProjects")

    member x.SelectedWebProjectType 
        with get() = selectedWebProjectType
        and set(v) =
            selectedWebProjectType <- v
            x.OnPropertyChanged("SelectedWebProjectType")

    member x.SelectedWebProjectName
        with get() = selectedWebProjectName
        and set(v) =
            selectedWebProjectName <- v
            x.OnPropertyChanged("SelectedWebProjectName")
    

