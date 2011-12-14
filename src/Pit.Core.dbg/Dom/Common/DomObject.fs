namespace Pit.Dom

open System
open System.Windows.Browser
open Pit
open Utils

type HtmlEventArgs = System.Windows.Browser.HtmlEventArgs

[<AbstractClass>]
[<AllowNullLiteral>]
type DomObject(scObj : ScriptObject) =

    member x.AttachEvent(evtName : string, handler : EventHandler) =
        match scObj with
        | :? HtmlObject ->
            let htmlObj = scObj :?> HtmlObject
            htmlObj.AttachEvent(evtName, handler) |> ignore
        | _ -> ()

    member x.DetachEvent(evtName : string, handler : EventHandler) =
        match scObj with
        | :? HtmlObject ->
            let htmlObj = scObj :?> HtmlObject
            htmlObj.DetachEvent(evtName, handler) |> ignore
        | _ -> ()
