namespace Pit.Dom

open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomInput =
    inherit DomElement
    val input: ScriptObject

    internal new(input) =
        { inherit DomElement(input); input=input }

    static member Of(el:DomElement) =
        new DomInput(el.InternalScriptObject)

    member x.Form
        with get() =
           let form = x.input.GetProperty<ScriptObject>("form")
           new DomForm(form)

    member x.Name
        with get() = x.input.GetProperty<string>("name")
        and set(v: string) = x.input.SetProperty("name", v)

    member x.Type
        with get() =
            x.input.GetProperty<string>("type")
        and set(v:string) = x.input.SetProperty("type", v.ToLower())

    member x.Value
        with get() = x.input.GetProperty<string>("value")
        and set(v: string) = x.input.SetProperty("value", v)

type DomInputButton(input) =
    inherit DomInput(input)

type DomInputCheckbox =
    inherit DomInput
    val checkbox: ScriptObject

    internal new (checkbox) =
        { inherit DomInput(checkbox); checkbox=checkbox }

    static member Of(el:DomElement) =
        new DomInputCheckbox(el.InternalScriptObject)

    member x.Checked
        with get() = x.checkbox.GetProperty<bool>("checked")
        and set(v: bool) = x.checkbox.SetProperty("checked", v)

    member x.DefaultChecked
        with get() = x.checkbox.GetProperty<bool>("defaultChecked")

type DomInputFile =
    inherit DomInput
    val file: ScriptObject

    internal new (file) =
        { inherit DomInput(file); file=file }

    static member Of(el:DomElement) =
        new DomInputFile(el.InternalScriptObject)

    member x.Accept
        with get() = x.file.GetProperty<string>("accept")
        and set(v: string) = x.file.SetProperty("accept", v)

type DomInputHidden =
    inherit DomInput
    val hidden: ScriptObject
    internal new (hidden) =
        { inherit DomInput(hidden); hidden=hidden }

    static member Of(el: DomElement) =
        new DomInputHidden(el.InternalScriptObject)

type DomInputPassword =
    inherit DomInput
    val password: ScriptObject
    internal new (password) =
        { inherit DomInput(password); password=password }

    static member Of(el:DomElement) =
        new DomInputPassword(el.InternalScriptObject)

    member x.DefaultValue
        with get() = x.password.GetProperty<string>("defaultValue")
        and set(v: string) = x.password.SetProperty("defaultValue", v)

    member x.MaxLength
        with get() = x.password.GetProperty<float>("maxLength") |> int
        and set(v: int) = x.password.SetProperty("maxLength", v)

    member x.ReadOnly
        with get() = x.password.GetProperty<bool>("readOnly")
        and set(v: bool) = x.password.SetProperty("readOnly", v)

    member x.Size
        with get() = x.password.GetProperty<float>("size") |> int
        and set(v: int) = x.password.SetProperty("size", v)

    member x.Select() =
        x.password.Invoke("select", [||]) |> ignore

type DomInputRadioButton =
    inherit DomInput
    val radio : ScriptObject

    internal new(radio) =
        { inherit DomInput(radio); radio=radio }

    static member Of(el:DomElement) =
        new DomInputRadioButton(el.InternalScriptObject)

    member x.Checked
        with get() = x.radio.GetProperty<bool>("checked")
        and set(v: bool) = x.radio.SetProperty("checked", v)

    member x.DefaultChecked
        with get() = x.radio.GetProperty<bool>("defaultChecked")

type DomInputReset =
    inherit DomInput
    val reset: ScriptObject

    internal new(reset) =
        { inherit DomInput(reset); reset=reset }

    static member Of (el: DomElement) =
        new DomInputReset(el.InternalScriptObject)

type DomInputSubmit =
    inherit DomInput
    val submit: ScriptObject

    internal new(submit) =
        { inherit DomInput(submit); submit=submit }

    static member Of (el: DomElement) =
        new DomInputReset(el.InternalScriptObject)

type DomInputText =
    inherit DomInput
    val text: ScriptObject

    internal new(text) =
        { inherit DomInput(text); text=text }

    static member Of(el:DomElement) =
        new DomInputText(el.InternalScriptObject)

    member x.DefaultValue
        with get() = x.text.GetProperty<string>("defaultValue")
        and set(v: string) = x.text.SetProperty("defaultValue", v)

    member x.MaxLength
        with get() = x.text.GetProperty<float>("maxLength") |> int

    member x.ReadOnly
        with get() = x.text.GetProperty<bool>("readOnly")
        and set(v: bool) = x.text.SetProperty("readOnly", v)

    member x.Size
        with get() = x.text.GetProperty<float>("size")
        and set(v: float) = x.text.SetProperty("size", v)

    member x.Select() =
        x.text.Invoke("select", [||]) |> ignore