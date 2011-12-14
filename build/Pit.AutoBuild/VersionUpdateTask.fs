namespace Pit.AutoBuild

open Microsoft.Build.Utilities
open Microsoft.Build.Framework
open System.Diagnostics.CodeAnalysis
open System
open System.Diagnostics
open System.Text.RegularExpressions
open System.IO
open System.Linq
  
type VersionUpdateTask() =
    inherit Task()      

    let mutable version = String.Empty
    let mutable filePath = String.Empty

    member x.FilePath
        with get() = filePath
        and set(v) =
            filePath <- v
    
    member x.Version
        with get() = version
        and set(v) =
            version <- v

    override this.Execute() =
        try 
            let regex s = new Regex(s)
            let assembly_regex = "[0-9]{1,4}\.[0-9]{1,4}\.[0-9]{1,4}\.[0-9]{1,4}"
            let fileStream = new StreamReader(this.FilePath)
            let fileData = fileStream.ReadToEnd()
            fileStream.Close()
            if fileData.Length > 0 then
                let replacedData = (regex assembly_regex).Replace(fileData, this.Version)
                let writeStream = new StreamWriter(this.FilePath)
                writeStream.Write(replacedData)
                writeStream.Close()
                Console.WriteLine("Replaced version for -> {0}",this.FilePath)
        with ex -> this.Log.LogErrorFromException(ex)                
        true 
     

