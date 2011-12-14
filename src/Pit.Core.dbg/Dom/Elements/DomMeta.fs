namespace Pit.Dom

open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomMeta =
    inherit DomElement
    val meta:ScriptObject

    internal new (meta) =
        { inherit DomElement(meta); meta=meta }

    static member Of(el:DomElement) =
        new DomMeta(el.InternalScriptObject)

    member x.Content
        with get() = x.meta.GetProperty<string>("content")
        and set(v: string) = x.meta.SetProperty("content", v)

    member x.Name
        with get() = x.meta.GetProperty<string>("name")
        and set(v: string) = x.meta.SetProperty("name", v)

    member x.HttpEquiv
        with get() = x.meta.GetProperty<string>("httpEquiv")
        and set(v: string) = x.meta.SetProperty("httpEquiv", v)

    member x.Scheme
        with get() = x.meta.GetProperty<string>("scheme")
        and set(v: string) = x.meta.SetProperty("scheme", v)