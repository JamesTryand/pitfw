namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomArea() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomArea()

    [<CompileTo("alt")>]
    member x.Alt
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("coords")>]
    member x.Coords
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("hash")>]
    member x.Hash
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("host")>]
    member x.Host
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("hostname")>]
    member x.HostName
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("href")>]
    member x.Href
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("noHref")>]
    member x.NoHref
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("pathname")>]
    member x.PathName
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("port")>]
    member x.Port
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("protocol")>]
    member x.Protocol
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("search")>]
    member x.Search
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("shape")>]
    member x.Shape
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("target")>]
    member x.Target
        with get() = ""
        and set(v: string) = ()