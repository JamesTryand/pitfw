namespace Pit.Runtime

open System
open System.Windows
open System.Net
open System.Xml
open System.Xml.Linq
open Pit
open Microsoft.FSharp.Reflection
open System.IO
open System.Windows.Resources

module Deployment =

    let processManifest(xapResource) =
        use sr = new StreamReader(Application.GetResourceStream(xapResource, new Uri("AppManifest.xaml", UriKind.Relative)).Stream)
        let manifestStr = sr.ReadToEnd()
        let deploymentRoot = XDocument.Parse(manifestStr).Root
        deploymentRoot.Elements().Elements()
        |> Seq.map(fun p -> p.Attribute(XName.Get("Source")).Value)
        |> Seq.iter(fun src ->
            let srcInfo = Application.GetResourceStream(xapResource, new Uri(src, UriKind.Relative))
            let assemblyPart = new AssemblyPart()
            assemblyPart.Source <- src
            let asm = assemblyPart.Load(srcInfo.Stream)
            srcInfo.Stream.Close()
        )

    let getEntryPoint (exportedTypes: Type[]) =
        let e =
            exportedTypes
            |> Array.filter(fun t ->
                let z = t.GetMethods() |> Array.filter (fun l -> l.GetCustomAttributes(typeof<DomEntryPointAttribute>, false).Length = 1)
                if z.Length > 1 then raise(new Exception("Two or more Dom Entry points found"))
                z.Length = 1
            )
        match e with
        | [| ty |] -> Some(ty)
        | _        -> None

type App() as app =
  inherit Application()

  do
    app.Startup.Add(fun e ->
      if e.InitParams.ContainsKey("clientXaps") then
        let xapArray = e.InitParams.["clientXaps"].Split(';')
        xapArray |>
        Array.iter (fun (x:string) ->
            let webClient = new WebClient()
            webClient.OpenReadCompleted.Add( fun e ->
                    let xapResourceInfo = new StreamResourceInfo(e.Result, null)
                    // load all assemblies to the current app domain
                    xapResourceInfo |> Deployment.processManifest
                    let xmls = Application.GetResourceStream(xapResourceInfo, new Uri("AppManifest.xaml", UriKind.Relative))
                    let reader = XDocument.Load(xmls.Stream)
                    let appDllName = reader.Root.Attribute(XName.Get("EntryPointAssembly")).Value.ToString() + ".dll"
                    let appDll = Application.GetResourceStream(new System.Windows.Resources.StreamResourceInfo(e.Result, null), new Uri(appDllName, UriKind.Relative))
                    let assemblyPart = new System.Windows.AssemblyPart()
                    let assembly = assemblyPart.Load(appDll.Stream)
                    match Deployment.getEntryPoint (assembly.GetExportedTypes() |>  Array.filter (fun g -> FSharpType.IsModule g)) with
                    | Some(entryPointType) ->
                        let entryPointMethod = entryPointType.GetMethods() |> Array.filter (fun l -> l.GetCustomAttributes(typeof<DomEntryPointAttribute>, false).Length = 1) |> Seq.head
                        entryPointMethod.Invoke(null, null) |> ignore
                    | _ -> System.Diagnostics.Debug.WriteLine("Entry Point Module not found in " + assembly.FullName)
            )
            webClient.OpenReadAsync(new Uri(x, UriKind.Relative))
     )
  )