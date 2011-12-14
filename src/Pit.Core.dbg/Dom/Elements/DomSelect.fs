namespace Pit.Dom

open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomSelect =
    inherit DomElement
    val select: ScriptObject

    internal new (select) =
        { inherit DomElement(select); select=select }

    static member Of(el:DomElement) =
        new DomSelect(el.InternalScriptObject)

    member x.Form
        with get() =
           let form = x.select.GetProperty<ScriptObject>("form")
           new DomForm(form)

    member x.Length
        with get() = x.select.GetProperty<float>("length")

    member x.Multiple
        with get() = x.select.GetProperty<bool>("multiple")
        and set(v: bool) = x.select.SetProperty("multiple", v)

    member x.Name
        with get() = x.select.GetProperty<string>("name")
        and set(v: string) = x.select.SetProperty("name", v)

    member x.SelectedIndex
        with get() = x.select.GetProperty<float>("selectedIndex") |> int
        and set(v: int) = x.select.SetProperty("selectedIndex", v)

    member x.Size
        with get() = x.select.GetProperty<float>("size") |> int
        and set(v: int) = x.select.SetProperty("size", v)

    member x.Type
        with get() = x.select.GetProperty<string>("type")

    member x.Options
        with get() =
            let t = x.select.GetProperty<HtmlElement>("options")
            t.Children |> Seq.map(fun i -> new DomOption(i)) |> Seq.toArray

    member x.Add(addItem: DomOption, beforeItem: DomOption) =
        x.select.Invoke("add", [| box(addItem.opt); box(beforeItem.opt) |]) |> ignore

    member x.Remove(index: int) =
        x.select.Invoke("remove", [| box(index) |]) |> ignore