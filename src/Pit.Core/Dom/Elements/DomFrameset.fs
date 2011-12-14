namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomFrameset() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomFrameset()

    [<CompileTo("cols")>]
    member x.Cols
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("rows")>]
    member x.Rows
        with get() = ""
        and set(v : string) = ()