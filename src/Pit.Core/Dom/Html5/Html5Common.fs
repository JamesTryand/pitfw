namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomArticle() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomArticle()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomAside() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomAside()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomFigure() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomFigure()
        
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomFigCaption() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomFigCaption()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomFooter() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomFooter()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomHeader() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomHeader()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomHGroup() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomHGroup()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomMark() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomMark()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomNav() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomNav()

    [<CompileTo("max")>]
    member x.Max
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("value")>]
    member x.Value
        with get() = 0
        and set(v: int) = ()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomRuby() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomRuby()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomRT() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomRT()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomRP() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomRP()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomSection() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomSection()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomEmbed() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomEmbed()

    [<CompileTo("height")>]
    member x.Height
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("src")>]
    member x.Src
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("type")>]
    member x.Type
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("width")>]
    member x.Width
        with get() = ""
        and set(v: string) = ()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomKeygen() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomKeygen()

    [<CompileTo("autofocus")>]
    member x.AutoFocus
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("challenge")>]
    member x.Challenge
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("disabled")>]
    member x.Disabled
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("form")>]
    member x.Form
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("keytype")>]
    member x.KeyType
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomOutput() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new DomOutput()

    [<CompileTo("for")>]
    member x.For
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("form")>]
    member x.Form
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()



