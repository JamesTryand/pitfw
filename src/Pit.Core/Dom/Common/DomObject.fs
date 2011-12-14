namespace Pit.Dom

open System
open Pit
open Pit.FSharp.Control

[<JsIgnore>]
[<AbstractClass>]
[<AllowNullLiteral>]
type DomObject()  =

    [<CompileTo("attachEvent")>]
    member x.AttachEvent(evtName : string, handler : EventHandler) =
        ()

    [<CompileTo("detachEvent")>]
    member x.DetachEvent(evtName : string, handler : EventHandler) =
        ()