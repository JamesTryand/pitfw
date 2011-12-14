namespace Pit.Dom

open System
open System.Windows.Browser
open Utils

[<AllowNullLiteral>]
type DomAnchor = 
    inherit DomElement
    val anchor: ScriptObject

    internal new (anchor) =
        { inherit DomElement(anchor); anchor=anchor }

    static member Of(el:DomElement) =
        new DomAnchor(el.InternalScriptObject)

    member x.Charset
        with get() = x.anchor.GetProperty<string>("charset")
        and set(v: string) = x.anchor.SetProperty("charset", v)

    member x.Href
        with get() = x.anchor.GetProperty<string>("href")
        and set(v: string) = x.anchor.SetProperty("href", v)

    member x.HrefLang
        with get() = x.anchor.GetProperty<string>("hreflang")
        and set(v: string) = x.anchor.SetProperty("hreflang", v)

    member x.Name
        with get() = x.anchor.GetProperty<string>("name")
        and set(v: string) = x.anchor.SetProperty("name", v)

    member x.Rel
        with get() = x.anchor.GetProperty<string>("rel")
        and set(v: string) = x.anchor.SetProperty("rel", v)

    member x.Rev
        with get() = x.anchor.GetProperty<string>("rev")
        and set(v: string) = x.anchor.SetProperty("rev", v)

    member x.Target
        with get() = x.anchor.GetProperty<string>("target")
        and set(v: string) = x.anchor.SetProperty("target", v)

    member x.Type
        with get() = x.anchor.GetProperty<string>("type")
        and set(v: string) = x.anchor.SetProperty("type", v)