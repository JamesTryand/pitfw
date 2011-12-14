namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type DomArticle =
    inherit DomElement

    val mutable article: ScriptObject

    internal new (_article) =
        { inherit DomElement(_article); article=_article }

    static member Of(el:DomElement) =
        new DomArticle(el.InternalScriptObject)


[<AllowNullLiteral>]
type DomAside =
    inherit DomElement

    val mutable aside: ScriptObject

    internal new (_aside) =
        { inherit DomElement(_aside); aside=_aside }

    static member Of(el:DomElement) =
        new DomAside(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomFigure =
    inherit DomElement

    val mutable figure: ScriptObject

    internal new (_figure) =
        { inherit DomElement(_figure); figure=_figure }

    static member Of(el:DomElement) =
        new DomFigure(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomFigCaption =
    inherit DomElement

    val mutable figcaption: ScriptObject

    internal new (_figcaption) =
        { inherit DomElement(_figcaption); figcaption=_figcaption }

    static member Of(el:DomElement) =
        new DomFigCaption(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomFooter =
    inherit DomElement

    val mutable footer: ScriptObject

    internal new (_footer) =
        { inherit DomElement(_footer); footer=_footer }

    static member Of(el:DomElement) =
        new DomFooter(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomHeader =
    inherit DomElement

    val mutable header: ScriptObject

    internal new (_header) =
        { inherit DomElement(_header); header=_header }

    static member Of(el:DomElement) =
        new DomHeader(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomHGroup =
    inherit DomElement

    val mutable hgroup: ScriptObject

    internal new (_hgroup) =
        { inherit DomElement(_hgroup); hgroup=_hgroup }

    static member Of(el:DomElement) =
        new DomHGroup(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomMark =
    inherit DomElement

    val mutable mark: ScriptObject

    internal new (_mark) =
        { inherit DomElement(_mark); mark=_mark }

    static member Of(el:DomElement) =
        new DomMark(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomNav =
    inherit DomElement

    val mutable nav: ScriptObject

    internal new (_nav) =
        { inherit DomElement(_nav); nav=_nav }

    static member Of(el:DomElement) =
        new DomNav(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomProgress =
    inherit DomElement

    val mutable progress: ScriptObject

    internal new (_progress) =
        { inherit DomElement(_progress); progress=_progress }

    static member Of(el:DomElement) =
        new DomProgress(el.InternalScriptObject)

    member x.Max
        with get() = x.progress.GetProperty<float>("max") |> int
        and set(v: int) = x.progress.SetProperty("max", box(v))

    member x.Value
        with get() = x.progress.GetProperty<float>("value") |> int
        and set(v: int) = x.progress.SetProperty("value", box(v))

[<AllowNullLiteral>]
type DomRuby =
    inherit DomElement

    val mutable ruby: ScriptObject

    internal new (_ruby) =
        { inherit DomElement(_ruby); ruby=_ruby }

    static member Of(el:DomElement) =
        new DomRuby(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomRT =
    inherit DomElement

    val mutable rt: ScriptObject

    internal new (_rt) =
        { inherit DomElement(_rt); rt=_rt }

    static member Of(el:DomElement) =
        new DomRT(el.InternalScriptObject)


[<AllowNullLiteral>]
type DomRP =
    inherit DomElement

    val mutable rp: ScriptObject

    internal new (_rp) =
        { inherit DomElement(_rp); rp=_rp }

    static member Of(el:DomElement) =
        new DomRP(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomSection =
    inherit DomElement

    val mutable section: ScriptObject

    internal new (_section) =
        { inherit DomElement(_section); section=_section }

    static member Of(el:DomElement) =
        new DomSection(el.InternalScriptObject)


[<AllowNullLiteral>]
type DomEmbed =
    inherit DomElement

    val mutable embed: ScriptObject

    internal new (_embed) =
        { inherit DomElement(_embed); embed=_embed }

    static member Of(el:DomElement) =
        new DomEmbed(el.InternalScriptObject)

    member x.Height
        with get() = x.embed.GetProperty<string>("height")
        and set(v: string) = x.embed.SetProperty("height", v)

    member x.Src
        with get() = x.embed.GetProperty<string>("src")
        and set(v: string) = x.embed.SetProperty("src", v)

    member x.Type
        with get() = x.embed.GetProperty<string>("type")
        and set(v: string) = x.embed.SetProperty("type", v)    

    member x.Width
        with get() = x.embed.GetProperty<string>("width")
        and set(v: string) = x.embed.SetProperty("width", v)

[<AllowNullLiteral>]
type DomKeygen =
    inherit DomElement

    val mutable keygen: ScriptObject

    internal new (_keygen) =
        { inherit DomElement(_keygen); keygen=_keygen }

    static member Of(el:DomElement) =
        new DomKeygen(el.InternalScriptObject)

    member x.AutoFocus
        with get() = x.keygen.GetProperty<string>("autofocus")
        and set(v: string) = x.keygen.SetProperty("autofocus", v)

    member x.Challenge
        with get() = x.keygen.GetProperty<string>("challenge")
        and set(v: string) = x.keygen.SetProperty("challenge", v)

    member x.Disabled
        with get() = x.keygen.GetProperty<string>("disabled")
        and set(v: string) = x.keygen.SetProperty("disabled", v)

    member x.Form
        with get() = x.keygen.GetProperty<string>("form")
        and set(v: string) = x.keygen.SetProperty("form", v)

    member x.KeyType
        with get() = x.keygen.GetProperty<string>("keytype")
        and set(v: string) = x.keygen.SetProperty("keytype", v)

    member x.Name
        with get() = x.keygen.GetProperty<string>("name")
        and set(v: string) = x.keygen.SetProperty("name", v)

[<AllowNullLiteral>]
type DomOutput =
    inherit DomElement

    val mutable output: ScriptObject

    internal new (_output) =
        { inherit DomElement(_output); output=_output }

    static member Of(el:DomElement) =
        new DomOutput(el.InternalScriptObject)

    member x.For
        with get() = x.output.GetProperty<string>("for")
        and set(v: string) = x.output.SetProperty("for", v)

    member x.Form
        with get() = x.output.GetProperty<string>("form")
        and set(v: string) = x.output.SetProperty("form", v)

    member x.Name
        with get() = x.output.GetProperty<string>("name")
        and set(v: string) = x.output.SetProperty("name", v)


