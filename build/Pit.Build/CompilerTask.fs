namespace Pit.Build

open Microsoft.Build.Utilities
open Microsoft.Build.Framework
open System.Diagnostics.CodeAnalysis
open System
open System.Diagnostics
open System.Text.RegularExpressions
open System.IO
open System.Xml
open System.Xml.Linq
open System.Linq

module Helper = 

    let pitEnvVarialbe = "PitLocation"

    let slAppListString = "SilverlightApplicationList"

    let slnRegex = @"=\s*""(?<ProjectName>.+?)""\s*,\s*""(?<ProjectFile>.+?)""\s*,\s*""(?<ProjectGUID>.+?)"""

    let binPath = Path.Combine(Environment.GetEnvironmentVariable(pitEnvVarialbe, EnvironmentVariableTarget.User), "bin")

    let pfcPath = Path.Combine(binPath, "pfc.exe")

    let runtimeXapPath = Path.Combine(binPath, "Pit.RunTime.xap")

    let pitJsPath = Path.Combine(binPath, "Pit.js")

    let getGuid (guidString: string) = 
        new Guid(guidString)

    let xname1 name nameSpace =  
        XName.Get(name , nameSpace)   

    let xname2 name = XName.Get(name)

    let getRegexMatches text = 
        Regex.Matches(text, slnRegex)    

    let getFileString fileName = 
        let execAssembly = System.Reflection.Assembly.GetExecutingAssembly()
        using(execAssembly.GetManifestResourceStream(fileName))(fun ms ->
            let stream = new System.IO.StreamReader(ms)
            let text = stream.ReadToEnd()
            stream.Close() 
            text
        )

    let getXDoc fileName =
        let execAssembly = System.Reflection.Assembly.GetExecutingAssembly()
        using(execAssembly.GetManifestResourceStream(fileName))(fun ms ->
            XDocument.Load(ms))

    let writeFile (path: String) (textToWrite: String) = 
        let sw = new StreamWriter(path)
        sw.Write(textToWrite)
        sw.Close()
        
    let getFsFilePathsInProject (xDoc:XDocument) =
        xDoc.Descendants(xname1 "Compile" xDoc.Root.Name.NamespaceName)
            .Attributes(xname2 "Include")
            |> Seq.filter (fun k -> k.Value.EndsWith(".fs"))
            |> Seq.map (fun l -> 
                    let p = new DirectoryInfo(l.Value)
                    p.FullName
                    ) |> Seq.toArray
    
    let copyAndOverWriteFile sourcePath destPath =
        File.Copy(sourcePath, destPath, true)

    let copyAndAvoidOverWriteFile sourcePath destPath =
        if not(File.Exists(destPath)) then
            File.Copy(sourcePath, destPath, false)

    let createDirectory (directoryPath:String) (directoryName:String) =  
        let dirs = Directory.GetDirectories(directoryPath, directoryName, SearchOption.TopDirectoryOnly)
        if dirs.Count() = 0 then
            Directory.CreateDirectory(Path.Combine(directoryPath, directoryName)) |> ignore

    let getSLGuids (xDoc:XDocument) = 
        let slDescendants = xDoc.Descendants(xname1 slAppListString xDoc.Root.Name.NamespaceName)
        if slDescendants.Count() > 0 then
            slDescendants.First().Value.Split(',') 
            |> Array.map (fun kk -> kk.Split('|').[0])
        else [||]

    let getProjGuids (xDoc:XDocument) = 
        let slDescendants = xDoc.Descendants(xname1 "ProjectReference" xDoc.Root.Name.NamespaceName)
        slDescendants |> Seq.map(fun l -> getGuid(l.Descendants(xname1 "Project" xDoc.Root.Name.NamespaceName).First().Value)) |> Seq.toArray

    let getFilesNotPresentInProject (files:String[]) (xDoc:XDocument) (elementString:String) =
        let cFiles = xDoc.Descendants(xname1 elementString xDoc.Root.Name.NamespaceName)
                        .Attributes(xname2 "Include")    
                        |> Seq.map (fun l -> 
                        let p = new DirectoryInfo(l.Value)
                        p.FullName) |> Seq.toArray
        let filesToAdd = files |> Array.filter (fun fl ->
            if cFiles.Contains(fl) then false
            else true)
        filesToAdd


    let addFilesToProject (elementString:String) (xDoc:XDocument) (files:String[])=
        let itemGroupElement = new XElement(xname1 "ItemGroup" xDoc.Root.Name.NamespaceName)
        xDoc.Root.Add(itemGroupElement)
        getFilesNotPresentInProject files xDoc elementString |> Array.iter (fun l -> itemGroupElement.Add(new XElement(xname1 elementString xDoc.Root.Name.NamespaceName, new XAttribute(xname2 "Include", l ))))

    let addReferencesToProject (elementString:String) (xDoc:XDocument) (asms)=
        let itemGroupElement = new XElement(xname1 "ItemGroup" xDoc.Root.Name.NamespaceName)
        xDoc.Root.Add(itemGroupElement)
        asms |> Array.iter (fun (k:String, l) -> 
                                    let getEl = 
                                        let el = new XElement(xname1 (elementString) xDoc.Root.Name.NamespaceName, new XAttribute(xname2 "Include", (k.Replace(".dll", ""))))
                                        let r = new XElement(xname1 "HintPath" xDoc.Root.Name.NamespaceName)
                                        r.Value <- l
                                        el.Add(r)
                                        el
                                    itemGroupElement.Add(getEl)
                                    )

        
    let assemblies (projName:String) = 
        let xdoc = XDocument.Load(projName)
        let rootEl = xdoc.Root
        let els = rootEl.Elements().Where((fun (s : XElement) -> s.Name.LocalName = "ItemGroup"))
        let asmEls =
            els
            |> Seq.map(fun (x : XElement) ->
                let c = x.Elements().ToArray() |> Array.filter(fun (xe : XElement) -> xe.Name.LocalName = "Reference" )
                c
            )
            |> Seq.filter(fun (x : XElement[])-> x.Length > 0)
                 
        seq {
                for x in asmEls do
                    for xe in x do                        
                        let asmName = xe.Attribute(xname2 "Include").Value
                        yield  asmName + ".dll", Path.Combine(binPath, asmName + ".dll")
            }
            |> Seq.filter(fun (x, y) -> x <> String.Empty && x.EndsWith(".dbg.dll"))
            |> Seq.map (fun (x, y) -> x.Replace(".dbg", ""), y.Replace(".dbg", ""))
            |> Seq.toArray
               

   
type CompilerTask() =
    inherit Task()      

    let processInfo = 
        new ProcessStartInfo(UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true, WorkingDirectory = Directory.GetCurrentDirectory(), FileName = Helper.pfcPath)

    let mutable config = String.Empty          
    let mutable solution = String.Empty
    let mutable slGuid = String.Empty
    let mutable xapName = String.Empty
    let mutable pFilePath = String.Empty
    let mutable isTest = String.Empty
    let mutable outputPath = String.Empty
    let mutable projType = String.Empty

    member x.Configuration 
        with get() = config
        and set(v) =
            config <- v

    member x.OutputPath
        with get() = outputPath
        and set(v) =
            outputPath <- v

    member x.ProjType
        with get() = projType
        and set(v) =
            projType <- v

    member x.SlnPath
        with get() = solution
        and set(v) =
            solution <- v

    member x.ProjGuid
        with get() = slGuid
        and set(v) =
            slGuid <- v

    member x.ProjPath
        with get() = pFilePath
        and set(v) =
            pFilePath <- v
    
    member x.XapName
        with get() = xapName
        and set(v) =
            xapName <- v

    override this.Execute() =
        try 
            let solutionFolder = Path.GetDirectoryName(this.SlnPath)
            let matches =  File.ReadAllText(this.SlnPath) |> Helper.getRegexMatches
            for i in 0..matches.Count-1 do
                let projectPathRelativeToSolution = matches.[i].Groups.["ProjectFile"].Value;
                let projectPathOnDisk = Path.GetFullPath(Path.Combine(solutionFolder, projectPathRelativeToSolution))
                let projectFile = projectPathRelativeToSolution
                let projectGUID = matches.[i].Groups.["ProjectGUID"].Value |> Helper.getGuid
                let projectDirectory = Path.GetDirectoryName(projectPathOnDisk)                           
                let pd = Path.GetDirectoryName(this.ProjPath )                
                if not(Directory.Exists(Path.Combine(pd, "obj\\PitPackage"))) then Directory.CreateDirectory(Path.Combine(pd, "obj\\PitPackage")) |> ignore
                if projectGUID <> Helper.getGuid(this.ProjGuid) then
                    let xDoc = XDocument.Load(projectPathOnDisk)                    
                    let scriptsPath = Path.Combine(projectDirectory, "Scripts")
                    let projName = this.XapName.Replace(".xap", "")
                    Helper.getSLGuids xDoc |> Array.iter (fun guid -> 
                        if not(String.IsNullOrEmpty(guid)) && Helper.getGuid(guid) = Helper.getGuid(this.ProjGuid) then                          
                                match this.Configuration with
                                | "Debug" ->                                    
                                    let jsDestPath =  Path.Combine(scriptsPath, projName + ".Silverlight.js")      
                                    if not(File.Exists(jsDestPath)) then
                                        Helper.getFileString("Pit.Silverlight.js.template").Replace("<%clientxap%>", "clientXaps=" + this.XapName) |> Helper.writeFile jsDestPath  
                                    let slnDestPath =  Path.Combine(Path.Combine(projectDirectory, "ClientBin"), this.XapName)    
                                    slnDestPath |> Helper.copyAndOverWriteFile (Path.Combine(this.OutputPath, this.XapName))
                                    let runTimeDestPath =  Path.Combine(Path.Combine(projectDirectory, "ClientBin"), "Pit.RunTime.xap")
                                    if not(File.Exists(runTimeDestPath)) then
                                        runTimeDestPath |> Helper.copyAndAvoidOverWriteFile Helper.runtimeXapPath           
                                | "Release" -> 
                                    let pitDestPath =  Path.Combine(scriptsPath, "Pit.js")      
                                    if not(File.Exists(pitDestPath)) then
                                        Helper.copyAndOverWriteFile Helper.pitJsPath pitDestPath
                                    let asms = Helper.assemblies this.ProjPath
                                    asms 
                                    |> Seq.iter (fun (a, b) ->
                                        let pRes = Path.Combine(scriptsPath, a.Replace(".dll",".js"))
                                        if not(File.Exists(pRes)) then 
                                            try
                                                let args =  "\"" + b + "\"" + " /o:" + "\"" + pRes + "\"" + " /ft:true"                                  
                                                processInfo.Arguments <- args
                                                use pfbuild = Process.Start(processInfo)
                                                let stdout = pfbuild.StandardOutput.ReadToEnd()
                                                pfbuild.WaitForExit()
                                                let err = pfbuild.StandardError.ReadToEnd()
                                                if err.Length = 0 then
                                                    this.Log.LogMessage(MessageImportance.High, stdout)
                                                else this.Log.LogError err                                  
                                                pfbuild.Close()
                                            with e -> this.Log.LogErrorFromException(e)          
                                        )

                                    let tProjPath = Path.Combine(Path.Combine(pd, "obj\\PitPackage"), "Pit.Library.fsproj")
                                    let txDoc = Helper.getXDoc "Pit.Library.template" 
                                    let srcProj = XDocument.Load(this.ProjPath)
                                    srcProj |> Helper.getFsFilePathsInProject |> Helper.addFilesToProject "Compile" txDoc  
                                    txDoc.Save(tProjPath)
                                    asms |> Helper.addReferencesToProject "Reference" txDoc  
                                    txDoc.Save(tProjPath)
                                    let jsOutPath = Path.Combine(Path.Combine(projectDirectory, "Scripts"), this.XapName.Replace(".xap", ".js"))   
                                    try                                  
                                        processInfo.Arguments <- "\"" + tProjPath + "\"" + " /o:" + "\"" + jsOutPath + "\"" + " /ft:true"
                                        let pfbuild = Process.Start(processInfo)
                                        let stdout = pfbuild.StandardOutput.ReadToEnd()
                                        pfbuild.WaitForExit()
                                        let err = pfbuild.StandardError.ReadToEnd()
                                        if err.Length = 0 then                                        
                                            this.Log.LogMessage(MessageImportance.High, stdout)
                                        else 
                                            this.Log.LogError err    
                                        pfbuild.Close()
                                    with e -> this.Log.LogErrorFromException(e)                                            
                                | _ -> () 
                    )            
            with ex -> this.Log.LogErrorFromException(ex)                
        true 
     

