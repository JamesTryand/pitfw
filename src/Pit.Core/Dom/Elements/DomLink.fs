namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomLink() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomLink()

    [<CompileTo("charset")>]
    member x.Charset
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("href")>]
    member x.Href
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("hrefLang")>]
    member x.HrefLang
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("media")>]
    member x.Media
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("rel")>]
    member x.Rel
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("rev")>]
    member x.Rev
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""
        and set(v: string) = ()