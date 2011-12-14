namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type DomSource =
    inherit DomElement

    val mutable source : ScriptObject

    internal new (_source) =
        { inherit DomElement(_source); source=_source }

    static member Of(el:DomElement) =
        new DomSource(el.InternalScriptObject)
            
    member x.Media
        with get() = x.source.GetProperty<string>("media")
        and set(v: string) = x.source.SetProperty("media", v)

    member x.Src
        with get() = x.source.GetProperty<string>("src")
        and set(v: string) = x.source.SetProperty("src", v)

    member x.Type
        with get() = x.source.GetProperty<string>("type")
        and set(v: string) = x.source.SetProperty("type", v)


[<AllowNullLiteral>]
type DomAudio =
    inherit DomElement

    val mutable audio : ScriptObject

    internal new (_audio) =
        { inherit DomElement(_audio); audio=_audio }

    static member Of(el:DomElement) =
        new DomAudio(el.InternalScriptObject)

    member x.AutoPlay
        with get() = x.audio.GetProperty<string>("autoplay")
        and set(v: string) = x.audio.SetProperty("autoplay", v)

    member x.Controls
        with get() = x.audio.GetProperty<string>("controls")
        and set(v: string) = x.audio.SetProperty("controls", v)

    member x.Loop
        with get() = x.audio.GetProperty<string>("loop")
        and set(v: string) = x.audio.SetProperty("loop", v)

    member x.PreLoad
        with get() = x.audio.GetProperty<string>("preload")
        and set(v: string) = x.audio.SetProperty("preload", v)
    
    member x.Src
        with get() = x.audio.GetProperty<string>("src")
        and set(v: string) = x.audio.SetProperty("src", v)

[<AllowNullLiteral>]
type DomVideo =
    inherit DomElement

    val mutable video : ScriptObject

    internal new (_video) =
        { inherit DomElement(_video); video=_video }

    static member Of(el:DomElement) =
        new DomVideo(el.InternalScriptObject)

    member x.AutoPlay
        with get() = x.video.GetProperty<string>("autoplay")
        and set(v: string) = x.video.SetProperty("autoplay", v)

    member x.Controls
        with get() = x.video.GetProperty<string>("controls")
        and set(v: string) = x.video.SetProperty("controls", v)

    member x.Height
        with get() = x.video.GetProperty<string>("height")
        and set(v: string) = x.video.SetProperty("height", v)

    member x.Loop
        with get() = x.video.GetProperty<string>("loop")
        and set(v: string) = x.video.SetProperty("loop", v)

    member x.Muted
        with get() = x.video.GetProperty<string>("muted")
        and set(v: string) = x.video.SetProperty("muted", v)

    member x.Poster
        with get() = x.video.GetProperty<string>("poster")
        and set(v: string) = x.video.SetProperty("poster", v)

    member x.PreLoad
        with get() = x.video.GetProperty<string>("preload")
        and set(v: string) = x.video.SetProperty("preload", v)
    
    member x.Src
        with get() = x.video.GetProperty<string>("src")
        and set(v: string) = x.video.SetProperty("src", v)

    member x.Width
        with get() = x.video.GetProperty<string>("width")
        and set(v: string) = x.video.SetProperty("width", v)