namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomImage() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomImage()

    [<CompileTo("align")>]
    member x.Align
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("alt")>]
    member x.Alt
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("border")>]
    member x.Border
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("complete")>]
    member x.Complete
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("height")>]
    member x.Height
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("hspace")>]
    member x.HSpace
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("longDesc")>]
    member x.LongDesc
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("lowsrc")>]
    member x.LowSrc
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("src")>]
    member x.Src
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("useMap")>]
    member x.UseMap
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("vspace")>]
    member x.VSpace
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("width")>]
    member x.Width
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("onabort")>]
    member x.OnAbort(scriptMethod : string) =
        ()

    [<CompileTo("onerror")>]
    member x.OnError(scriptMethod : string) =
        ()

    [<CompileTo("onload")>]
    member x.OnLoad(scriptMethod : string) =
        ()
