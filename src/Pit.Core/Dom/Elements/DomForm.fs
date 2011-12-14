namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral>]
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomForm() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomForm()

    [<CompileTo("elements")>]
    member x.Elements
        with get() =
            let items : DomElement[] = Array.zeroCreate(0)
            items.[0]

    [<CompileTo("acceptCharset")>]
    member x.AcceptCharset
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("action")>]
    member x.Action
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("enctype")>]
    member x.EncType
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("length")>]
    member x.Length
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("method")>]
    member x.Method
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("target")>]
    member x.Target
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("reset")>]
    member x.Reset() =
        ()

    [<CompileTo("submit")>]
    member x.Submit() =
        ()

    [<CompileTo("onreset")>]
    member x.OnReset(scriptMethod : string) =
        ()

    [<CompileTo("onsubmit")>]
    member x.OnSubmit(scriptMethod : string) =
        ()

