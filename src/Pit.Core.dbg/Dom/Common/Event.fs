namespace Pit.Dom
open System
open System.Windows.Browser
open Pit
open Pit.FSharp.Control

module Event =

    [<Js;CompiledName("Click")>]
    let click (el:DomObject) =
        let evt = new UIEvent("onclick", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Change")>]
    let change (el:DomObject) =
        let evt = new UIEvent("onchange", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Blur")>]
    let blur (el:DomObject) =
        let evt = new UIEvent("onblur", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish


    [<Js;CompiledName("DoubleClick")>]
    let dblclick(el:DomObject) =
        let evt = new UIEvent("ondblclick", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Error")>]
    let error (el:DomObject) =
        let evt = new UIEvent("onerror", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Focus")>]
    let focus (el:DomObject) =
        let evt = new UIEvent("onfocus", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish


    [<Js;CompiledName("KeyDown")>]
    let keydown (el:DomObject) =
        let evt = new UIEvent("onkeydown", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("KeyPress")>]
    let keypress (el:DomObject) =
        let evt = new UIEvent("onkeypress", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("KeyUp")>]
    let keyup (el:DomObject) =
        let evt = new UIEvent("onkeyup", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Load")>]
    let load (el:DomObject) =
        let evt = new UIEvent("onload", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseDown")>]
    let mousedown (el:DomObject) =
        let evt = new UIEvent("onmousedown", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseMove")>]
    let mousemove (el:DomObject) =
        let evt = new UIEvent("onmousemove", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseOut")>]
    let mouseout (el:DomObject) =
        let evt = new UIEvent("onmouseout", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseOver")>]
    let mouseover (el:DomObject) =
        let evt = new UIEvent("onmouseover", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("MouseUp")>]
    let mouseup (el:DomObject) =
        let evt = new UIEvent("onmouseup", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Resize")>]
    let resize (el:DomObject) =
        let evt = new UIEvent("onresize", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Select")>]
    let select (el:DomObject) =
        let evt = new UIEvent("onselect", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish

    [<Js;CompiledName("Unload")>]
    let unload (el:DomObject) =
        let evt = new UIEvent("onunload", (el.InternalScriptObject :?> HtmlObject))
        evt.Publish