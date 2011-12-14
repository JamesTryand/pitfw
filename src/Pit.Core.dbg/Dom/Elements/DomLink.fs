namespace Pit.Dom

open System.Windows.Browser
open Pit
open Utils

[<AllowNullLiteral>]
type DomLink =
    inherit DomElement
    val link: ScriptObject

    internal new (link) =
        { inherit DomElement(link); link=link }

    static member Of(el:DomElement) =
        new DomLink(el.InternalScriptObject)

    member x.Charset
        with get() = x.link.GetProperty<string>("charset")
        and set(v: string) = x.link.SetProperty("charset", v)

    member x.Href
        with get() = x.link.GetProperty<string>("href")
        and set(v: string) = x.link.SetProperty("href", v)

    member x.HrefLang
        with get() = x.link.GetProperty<string>("hrefLang")
        and set(v: string) = x.link.SetProperty("hrefLang", v)

    member x.Media
        with get() =
            x.link.GetProperty<string>("media")
        and set(v) = x.link.SetProperty("media", v.ToString().ToLower())

    member x.Rel
        with get() =
            x.link.GetProperty<string>("rel")
        and set(v) = x.link.SetProperty("rel", v.ToString().ToLower())

    member x.Rev
        with get() =
            x.link.GetProperty<string>("rev")
        and set(v) = x.link.SetProperty("rev", v.ToString().ToLower())

    member x.Type
        with get() = x.link.GetProperty<string>("type")
        and set(v: string) = x.link.SetProperty("type", v)