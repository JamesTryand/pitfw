namespace Pit.Dom

open System.Windows.Browser
open Utils
open Pit

[<AllowNullLiteral>]
type DomArea =
    inherit DomElement
    val area: ScriptObject

    internal new (area) =
        { inherit DomElement(area); area=area }

    static member Of(el:DomElement) =
        new DomArea(el.InternalScriptObject)

    member x.Alt
        with get() = x.area.GetProperty<string>("alt")
        and set(v: string) = x.area.SetProperty("alt", v)

    member x.Shape
        with get() = x.area.GetProperty<string>("shape")
        and set(v: string) = x.area.SetProperty("shape", v)

    member x.Coords
        with get() = x.area.GetProperty<string>("coords")
        and set(v: string) = x.area.SetProperty("coords", v)

    member x.Hash
        with get() = x.area.GetProperty<string>("hash")
        and set(v: string) = x.area.SetProperty("hash", v)

    member x.Host
        with get() = x.area.GetProperty<string>("host")
        and set(v: string) = x.area.SetProperty("host", v)

    member x.HostName
        with get() = x.area.GetProperty<string>("hostname")
        and set(v: string) = x.area.SetProperty("hostname", v)

    member x.Href
        with get() = x.area.GetProperty<string>("href")
        and set(v: string) = x.area.SetProperty("href", v)

    member x.NoHref
        with get() = x.area.GetProperty<bool>("noHref")
        and set(v:string) = x.area.SetProperty("noHref", v.ToString())

    member x.PathName
        with get() = x.area.GetProperty<string>("pathName")
        and set(v: string) = x.area.SetProperty("pathname", v)

    member x.Port
        with get() = x.area.GetProperty<string>("port")
        and set(v: string) = x.area.SetProperty("port", v)

    member x.Protocol
        with get() = x.area.GetProperty<string>("protocol")
        and set(v: string) = x.area.SetProperty("protocol", v)

    member x.Search
        with get() = x.area.GetProperty<string>("search")
        and set(v: string) = x.area.SetProperty("search", v)

    member x.Target
        with get() = x.area.GetProperty<string>("target")
        and set(v: string) = x.area.SetProperty("target", v)