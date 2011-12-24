namespace Pit.Dom
open System
open Pit
open Pit.FSharp.Control

module Event =

    [<Js>]
    let click (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("click", el)
        evt.Publish

    [<Js>]
    let change (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("change", el)
        evt.Publish

    [<Js>]
    let blur (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("blur", el)
        evt.Publish


    [<Js>]
    let dblclick (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("dblclick", el)
        evt.Publish

    [<Js>]
    let error (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("error", el)
        evt.Publish

    [<Js>]
    let focus (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("focus", el)
        evt.Publish


    [<Js>]
    let keydown (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("keydown", el)
        evt.Publish

    [<Js>]
    let keypress (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("keypress", el)
        evt.Publish

    [<Js>]
    let keyup (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("keyup", el)
        evt.Publish

    [<Js>]
    let load (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("load", el)
        evt.Publish


    [<Js>]
    let mousedown (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("mousedown", el)
        evt.Publish

    [<Js>]
    let mousemove (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("mousemove", el)
        evt.Publish

    [<Js>]
    let mouseout (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("mouseout", el)
        evt.Publish

    [<Js>]
    let mouseover (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("mouseover", el)
        evt.Publish

    [<Js>]
    let mouseup (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("mouseup", el)
        evt.Publish

    [<Js>]
    let resize (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("resize", el)
        evt.Publish

    [<Js>]
    let select (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("select", el)
        evt.Publish

    [<Js>]
    let unload (el:DomObject) =
        let evt = new UIEvent<HtmlEventArgs>("unload", el)
        evt.Publish
