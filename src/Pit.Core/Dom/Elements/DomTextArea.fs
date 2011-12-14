namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomTextArea() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomTextArea()

    [<CompileTo("cols")>]
    member x.Cols
        with get() = 0
        and set(v : int) = ()

    [<CompileTo("defaultValue")>]
    member x.DefaultValue
        with get() = ""

    [<CompileTo("forms")>]
    member x.Form
        with get() = DomForm()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("readOnly")>]
    member x.ReadOnly
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("rows")>]
    member x.Rows
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""

    [<CompileTo("value")>]
    member x.Value
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("select")>]
    member x.Select() =
        ()