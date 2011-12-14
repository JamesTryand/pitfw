namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomAnchor() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomAnchor()

    [<CompileTo("charset")>]
    member x.Charset
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("href")>]
    member x.Href
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("hreflang")>]
    member x.HrefLang
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("rel")>]
    member x.Rel
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("rev")>]
    member x.Rev
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("target")>]
    member x.Target
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""
        and set(v : string) = ()