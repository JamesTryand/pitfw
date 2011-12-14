namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomMeta() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomMeta()

    [<CompileTo("content")>]
    member x.Content
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("httpEquiv")>]
    member x.HttpEquiv
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("scheme")>]
    member x.Scheme
        with get() = ""
        and set(v: string) = ()