namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomInput() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInput()

    [<CompileTo("form")>]
    member x.Form
        with get() = new DomForm()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("value")>]
    member x.Value
        with get() = ""
        and set(v: string) = ()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputButton() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputButton()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputCheckbox() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputCheckbox()

    [<CompileTo("checked")>]
    member x.Checked
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("defaultChecked")>]
    member x.DefaultChecked
        with get() = true

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputFile() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputFile()

    [<CompileTo("accept")>]
    member x.Accept
        with get() = ""
        and set(v : string) = ()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputHidden() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputHidden()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputPassword() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputPassword()

    [<CompileTo("defaultValue")>]
    member x.DefaultValue
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("maxLength")>]
    member x.MaxLength
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("readOnly")>]
    member x.ReadOnly
        with get() = true
        and set(v : bool) = ()

    [<CompileTo("size")>]
    member x.Size
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("select")>]
    member x.Select() =
        ()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputRadioButton() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputRadioButton()

    [<CompileTo("checked")>]
    member x.Checked
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("defaultChecked")>]
    member x.DefaultChecked
        with get() = true

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputReset() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputReset()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputSubmit() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputSubmit()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomInputText() =
    inherit DomInput()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomInputText()

    [<CompileTo("defaultValue")>]
    member x.DefaultValue
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("maxLength")>]
    member x.MaxLength
        with get() = 0

    [<CompileTo("readOnly")>]
    member x.ReadOnly
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("size")>]
    member x.Size
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("select")>]
    member x.Select() =
        ()