namespace Pit.Dom

open System.Windows.Browser
open Pit
open Utils

[<AllowNullLiteral>]
type DomForm =
    inherit DomElement
    val form: ScriptObject

    internal new(form) =
        { inherit DomElement(form); form=form }

    static member Of(el:DomElement) =
        new DomForm(el.InternalScriptObject)

    member x.Elements
        with get() =
            let els = x.form.Invoke("elements", [||]) :?> ScriptObjectCollection
            els |> Seq.map(fun e -> new DomElement(e)) |> Seq.toArray

    member x.AcceptCharset
        with get() = x.form.GetProperty<string>("acceptCharset")
        and set(v: string) = x.form.SetProperty("acceptCharset", v)

    member x.Action
        with get() = x.form.GetProperty<string>("action")
        and set(v: string) = x.form.SetProperty("action", v)

    member x.EncType
        with get() = x.form.GetProperty<string>("enctype")
        and set(v: string) = x.form.SetProperty("enctype", v)

    member x.Length
        with get() = x.form.GetProperty<float>("length") |> int
        and set(v: int) = x.form.SetProperty("length", v)

    member x.Method
        with get() = x.form.GetProperty<string>("method")
        and set(v: string) = x.form.SetProperty("method", v)

    member x.Name
        with get() = x.form.GetProperty<string>("name")
        and set(v: string) = x.form.SetProperty("name", v)

    member x.Target
        with get() = x.form.GetProperty<string>("target")
        and set(v: string) = x.form.SetProperty("target", v)

    member x.Reset() =
        x.form.Invoke("reset", [||]) |> ignore

    member x.Submit() =
        x.form.Invoke("submit", [||]) |> ignore

    member x.OnReset(scriptMethod : string) =
        x.form.Invoke("onreset", [|box(scriptMethod)|]) |> ignore

    member x.OnSubmit(scriptMethod: string) =
        x.form.Invoke("onsubmit", [|box(scriptMethod)|]) |> ignore