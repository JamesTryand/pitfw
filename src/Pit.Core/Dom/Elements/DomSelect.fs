namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomSelect() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomSelect()

    [<CompileTo("form")>]
    member x.Form
        with get() = DomForm()

    [<CompileTo("length")>]
    member x.Length
        with get() = 0.

    [<CompileTo("multiple")>]
    member x.Multiple
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("selectedIndex")>]
    member x.SelectedIndex
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("size")>]
    member x.Size
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""

    [<CompileTo("options")>]
    member x.Options
        with get() =
            let items : DomOption[] = Array.zeroCreate(0)
            items

    [<CompileTo("add")>]
    member x.Add(addItem : DomOption, beforeItem : DomOption) =
        ()

    [<CompileTo("remove")>]
    member x.Remove(index : int) =
        ()