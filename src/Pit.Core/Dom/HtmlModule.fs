namespace Pit.Dom

open System
open Pit

[<JsIgnore(IgnoreNamespace=true)>]
[<AutoOpen>]
module HtmlModule =
    let window = new DomWindow()
    let document = new DomDocument()

    [<JsIgnore(IgnoreTypeName=true)>]
    let alert(str) = window.Alert(str)