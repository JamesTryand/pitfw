namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomSource() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomSource()

    [<CompileTo("media")>]
    member x.Media
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("src")>]
    member x.Src
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""
        and set(v: string) = ()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomAudio() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomAudio()

    [<CompileTo("autoplay")>]
    member x.AutoPlay
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("controls")>]
    member x.Controls
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("loop")>]
    member x.Loop
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("preload")>]
    member x.PreLoad
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("src")>]
    member x.Src
        with get() = ""
        and set(v: string) = ()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomVideo() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomVideo()

    [<CompileTo("autoplay")>]
    member x.AutoPlay
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("controls")>]
    member x.Controls
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("height")>]
    member x.Height
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("loop")>]
    member x.Loop
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("muted")>]
    member x.Muted
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("poster")>]
    member x.Poster
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("preload")>]
    member x.PreLoad
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("src")>]
    member x.Src
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("width")>]
    member x.Width
        with get() = ""
        and set(v: string) = ()