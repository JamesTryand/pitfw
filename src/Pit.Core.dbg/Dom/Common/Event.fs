namespace Pit.Dom
open System
open System.Windows.Browser
open Pit
open Pit.FSharp.Control

module Event =

    [<Js;CompiledName("Click")>]
    let click (el:DomElement) =
        let evt = new UIEvent("onclick", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Change")>]
    let change (el:DomElement) =
        let evt = new UIEvent("onchange", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Blur")>]
    let blur (el:DomElement) =
        let evt = new UIEvent("onblur", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish


    [<Js;CompiledName("DoubleClick")>]
    let dblclick(el:DomElement) =
        let evt = new UIEvent("ondblclick", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Error")>]
    let error (el:DomElement) =
        let evt = new UIEvent("onerror", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Focus")>]
    let focus (el:DomElement) =
        let evt = new UIEvent("onfocus", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish


    [<Js;CompiledName("KeyDown")>]
    let keydown (el:DomElement) =
        let evt = new UIEvent("onkeydown", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("KeyPress")>]
    let keypress (el:DomElement) =
        let evt = new UIEvent("onkeypress", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("KeyUp")>]
    let keyup (el:DomElement) =
        let evt = new UIEvent("onkeyup", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Load")>]
    let load (el:DomElement) =
        let evt = new UIEvent("onload", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseDown")>]
    let mousedown (el:DomElement) =
        let evt = new UIEvent("onmousedown", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseMove")>]
    let mousemove (el:DomElement) =
        let evt = new UIEvent("onmousemove", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseOut")>]
    let mouseout (el:DomElement) =
        let evt = new UIEvent("onmouseout", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseOver")>]
    let mouseover (el:DomElement) =
        let evt = new UIEvent("onmouseover", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseUp")>]
    let mouseup (el:DomElement) =
        let evt = new UIEvent("onmouseup", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Resize")>]
    let resize (el:DomElement) =
        let evt = new UIEvent("onresize", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Select")>]
    let select (el:DomElement) =
        let evt = new UIEvent("onselect", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Unload")>]
    let unload (el:DomElement) =
        let evt = new UIEvent("onunload", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish