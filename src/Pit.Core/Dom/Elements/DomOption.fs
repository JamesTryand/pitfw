namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomOption() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomOption()

    [<CompileTo("defaultSelected")>]
    member x.DefaultSelected
        with get() = true

    [<CompileTo("form")>]
    member x.Form
        with get() = DomForm()

    [<CompileTo("index")>]
    member x.Index
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("selected")>]
    member x.Selected
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("text")>]
    member x.Text
        with get() = ""
        and set(v: bool) = ()

    [<CompileTo("value")>]
    member x.Value
        with get() = ""
        and set(v: string) = ()