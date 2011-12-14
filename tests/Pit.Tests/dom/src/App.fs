namespace Pit.Tests.Dom.dbg

open System.Windows
open Pit.Dom
open Pit.Dom.Tests

type App() as app =
    inherit Application()

    do
        app.Startup.Add(fun e ->
            match e.InitParams.["test"] with
            | "literals" -> (fun _ -> LoadLiterals()) |> hook ("btn")
            | "canvas"   -> LoadCanvas()
            | "dom"      -> LoadDOMTests()
            | "svg"      -> LoadSVGTests()
            | "html5"    -> LoadHTML5Test()
            | "test"     -> LoadSampleTest()
            | _          -> ()
        )