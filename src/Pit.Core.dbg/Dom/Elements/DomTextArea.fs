namespace Pit.Dom

open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomTextArea =
    inherit DomElement
    val textarea: ScriptObject

    internal new (textarea) =
        { inherit DomElement(textarea);textarea=textarea }

    static member Of(el:DomElement) =
        new DomTextArea(el.InternalScriptObject)

    member x.Cols
        with get() = x.textarea.GetProperty<float>("cols") |> int
        and set(v: int) = x.textarea.SetProperty("cols", v)

    member x.DefaultValue
        with get() = x.textarea.GetProperty<string>("defaultValue")

    member x.Form
        with get() =
            let form = x.textarea.GetProperty<ScriptObject>("form")
            new DomForm(form)

    member x.Name
        with get() = x.textarea.GetProperty<string>("name")
        and set(v: string) = x.textarea.SetProperty("name", v)

    member x.ReadOnly
        with get() = x.textarea.GetProperty<bool>("readOnly")
        and set(v: bool) = x.textarea.SetProperty("readOnly", v)

    member x.Rows
        with get() = x.textarea.GetProperty<float>("rows") |> int
        and set(v: int) = x.textarea.SetProperty("rows", v)

    member x.Type
        with get() = x.textarea.GetProperty<string>("type")

    member x.Value
        with get() = x.textarea.GetProperty<string>("value")
        and set(v: string) = x.textarea.SetProperty("value", v)

    member x.Select() =
        x.textarea.Invoke("select", [||]) |> ignore