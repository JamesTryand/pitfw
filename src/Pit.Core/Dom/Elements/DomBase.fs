namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomBase() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomBase()

    [<CompileTo("href")>]
    member x.Href
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("target")>]
    member x.Target
        with get() = ""
        and set(v: string) = ()