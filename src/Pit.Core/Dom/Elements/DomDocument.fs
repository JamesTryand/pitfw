namespace Pit.Dom

open System
open Pit

[<JsIgnore(IgnoreGetSet=true)>]
[<Alias("body")>]
[<AllowNullLiteral>]
type DomBody() =
    inherit DomObject()

    static member Of(obj : DomObject) =
        new DomBody()


    [<CompileTo("aLink")>]
    member x.ALink
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("background")>]
    member x.Background
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("bgColor")>]
    member x.BgColor
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("link")>]
    member x.Link
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("text")>]
    member x.Text
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("vLink")>]
    member x.VLink
        with get() = ""
        and set(v : string) = ()


[<Alias("document");JsIgnore(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomDocument() =
    inherit DomObject()

    static member Of(obj : DomObject) =
        new DomDocument()

    [<CompileTo("body")>]
    member x.Body
        with get() = new DomBody()

    [<CompileTo("cookie")>]
    member x.Cookie
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("documentMode")>]
    member x.DocumentMode
        with get() = 0

    [<CompileTo("domain")>]
    member x.Domain
        with get() = ""

    [<CompileTo("documentURI")>]
    member x.DocumentUri
        with get() = String.Empty

    [<CompileTo("lastModified")>]
    member x.LastModified
        with get() = DateTime.Now

    [<CompileTo("readyState")>]
    member x.ReadyState
        with get() = ""

    [<CompileTo("title")>]
    member x.Title
        with get() = ""
        and set(v:string) = ()

    [<CompileTo("referrer")>]
    member x.Referrer
        with get() = ""

    [<CompileTo("URL")>]
    member x.Url
        with get() = ""

    [<CompileTo("getElementById")>]
    member x.GetElementById (id : string)  =
        new DomElement()

    [<CompileTo("getElementsByName")>]
    member x.GetElementsByName(name : string) =
        let p : DomElement[] = Array.zeroCreate 0
        p

    [<CompileTo("getElementsByTagName")>]
    member x.GetElementsByTagName(tagName : string) =
        let p : DomElement[] = Array.zeroCreate 0
        p

    [<CompileTo("anchors")>]
    member x.Anchors
        with get() =
            let p : DomAnchor[] = Array.zeroCreate 0
            p

    [<CompileTo("forms")>]
    member x.Forms
        with get() =
            let p : DomForm[] = Array.zeroCreate 0
            p

    [<CompileTo("images")>]
    member x.Images
        with get() =
            let p : DomImage[] = Array.zeroCreate 0
            p

    [<CompileTo("links")>]
    member x.Links
        with get() =
            let p : DomLink[] = Array.zeroCreate 0
            p

    [<CompileTo("createElement")>]
    member x.CreateElement (tagName:String) =
        new DomElement()

    [<CompileTo("createElementNS")>]
    member x.CreateElementNS(ns:String,tagName:String) =
        new DomElement()

    [<CompileTo("createTextNode")>]
    member x.CreateTextNode(text : string) =
        new DomElement()

    [<CompileTo("submit")>]
    member x.Submit() =
        ()

    [<CompileTo("submit")>]
    member x.Submit(formId : string) =
        ()

    [<CompileTo("open")>]
    member x.Open(mimeType : string) =
        ()

    [<CompileTo("open")>]
    member x.Open(mimeType : string, replace : string) =
        ()

    [<CompileTo("close")>]
    member x.Close() =
        ()

    [<CompileTo("write")>]
    member x.Write(str : String) =
        ()

    [<CompileTo("writeln")>]
    member x.WriteLn(str : String) =
        ()

    [<CompileTo("querySelector")>]
    member x.QuerySelector(str:String) =
        new DomElement()

    [<CompileTo("querySelectorAll")>]
    member x.QuerySelectorAll(query:String) =
        [|DomElement()|]