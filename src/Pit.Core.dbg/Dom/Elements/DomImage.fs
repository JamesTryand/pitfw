namespace Pit.Dom

open System.Windows.Browser
open Pit
open Utils

[<AllowNullLiteral>]
type DomImage =
    inherit DomElement
    val image: ScriptObject

    internal new(image) =
        { inherit DomElement(image); image=image }

    static member Of(el:DomElement) =
        new DomImage(el.InternalScriptObject)

    member x.Align
        with get() =
            x.image.GetProperty<string>("align")
        and set(v:string) = x.image.SetProperty("align", v.ToLower())

    member x.Alt
        with get() = x.image.GetProperty<string>("alt")
        and set(v: string) = x.image.SetProperty("alt", v)

    member x.Border
        with get() = x.image.GetProperty<string>("border")
        and set(v: string) = x.image.SetProperty("border", v)

    member x.Complete
        with get() = x.image.GetProperty<bool>("complete")
        and set(v: bool) = x.image.SetProperty("complete", v)

    member x.Height
        with get() = x.image.GetProperty<string>("height")
        and set(v: string) = x.image.SetProperty("height", v)

    member x.HSpace
        with get() = x.image.GetProperty<string>("hspace")
        and set(v: string) = x.image.SetProperty("hspace", v)

    member x.LongDesc
        with get() = x.image.GetProperty<string>("longDesc")
        and set(v: string) = x.image.SetProperty("longDesc", v)

    member x.LowSrc
        with get() = x.image.GetProperty<string>("lowsrc")
        and set(v: string) = x.image.SetProperty("lowsrc", v)

    member x.Name
        with get() = x.image.GetProperty<string>("name")
        and set(v: string) = x.image.SetProperty("name", v)

    member x.Src
        with get() = x.image.GetProperty<string>("src")
        and set(v: string) = x.image.SetProperty("src", v)

    member x.UseMap
        with get() = x.image.GetProperty<string>("useMap")
        and set(v: string) = x.image.SetProperty("useMap", v)

    member x.VSpace
        with get() = x.image.GetProperty<string>("vspace")
        and set(v: string) = x.image.SetProperty("vspace", v)

    member x.Width
        with get() = x.image.GetProperty<string>("width")
        and set(v: string) = x.image.SetProperty("width", v)

    member x.OnAbort(scriptMethod: string) =
        x.image.Invoke("onabort", [| box(scriptMethod) |]) |> ignore

    member x.OnError(scriptMethod: string) =
        x.image.Invoke("onerror", [| box(scriptMethod) |]) |> ignore

    member x.OnLoad(scriptMethod: string) =
        x.image.Invoke("onload", [| box(scriptMethod) |]) |> ignore