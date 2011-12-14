namespace Pit.Dom

open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomOption =
    inherit DomElement
    val opt:ScriptObject

    internal new (opt) =
        { inherit DomElement(opt); opt=opt }

    static member Of(el:DomElement) =
        new DomOption(el.InternalScriptObject)

    member x.DefaultSelected
        with get() = x.opt.GetProperty<bool>("defaultSelected")

    member x.Form
        with get() =
           let form = x.opt.GetProperty<ScriptObject>("form")
           new DomForm(form)

    member x.Index
        with get() = x.opt.GetProperty<float>("index") |> int
        and set(v: int) = x.opt.SetProperty("index", v)

    member x.Selected
        with get() = x.opt.GetProperty<bool>("selected")
        and set(v: bool) = x.opt.SetProperty("selected", v)

    member x.Text
        with get() = x.opt.GetProperty<string>("text")
        and set(v: string) = x.opt.SetProperty("text", v)

    member x.Value
        with get() = x.opt.GetProperty<string>("value")
        and set(v: string) = x.opt.SetProperty("value", v)