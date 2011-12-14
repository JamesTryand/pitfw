namespace Pit.Dom
open System
open System.Windows.Browser
open Utils

[<AllowNullLiteral>]
type DomBody(scriptObj : ScriptObject) =

    static member Of(scriptObj : ScriptObject) =
        new DomBody(scriptObj)

    member x.ALink
        with get() = scriptObj.GetProperty<string>("aLink")
        and set(v : string) = scriptObj.SetProperty("aLink", box(v))

    member x.Background
        with get() = scriptObj.GetProperty<string>("background")
        and set(v : string) = scriptObj.SetProperty("background", box(v))

    member x.BgColor
        with get() = scriptObj.GetProperty<string>("bgColor")
        and set(v : string) = scriptObj.SetProperty("bgColor", box(v))

    member x.Link
        with get() = scriptObj.GetProperty<string>("link")
        and set(v : string) = scriptObj.SetProperty("link", box(v))

    member x.Text
        with get() = scriptObj.GetProperty<string>("text")
        and set(v : string) = scriptObj.SetProperty("text", box(v))

    member x.VLink
        with get() = scriptObj.GetProperty<string>("vLink")
        and set(v : string) = scriptObj.SetProperty("vLink", box(v))


[<AllowNullLiteral>]
type DomDocument =
    inherit DomObject
    val htmlDoc: HtmlObject

    internal new (htmlDoc) =
        { inherit DomObject(htmlDoc); htmlDoc = htmlDoc }

    static member Of(htmlDoc : HtmlObject) =
        new DomBody(htmlDoc)

    member x.Body
        with get() =
            let o = x.htmlDoc.GetProperty<ScriptObject>("body")
            new DomBody(o)

    member x.Cookie
        with get() = x.htmlDoc.GetProperty<string>("cookie")
        and set(v : string) = x.htmlDoc.SetProperty("cookie", box(v))

    member x.DocumentMode
        with get() = x.htmlDoc.GetProperty<float>("documentMode") |> int

    member x.DocumentUri
        with get() = x.htmlDoc.GetProperty<string>("documentURI")

    member x.Domain
        with get() = x.htmlDoc.GetProperty<string>("domain")

    member x.LastModified
        with get() =
            let lastModified = x.htmlDoc.GetProperty<string>("lastModified")
            DateTime.Parse(lastModified)

    member x.ReadyState
        with get() =
            x.htmlDoc.GetProperty<string>("readyState")

    member x.Title
        with get() = x.htmlDoc.GetProperty<string>("title")

    member x.Referrer
        with get() = x.htmlDoc.GetProperty<string>("referrer")

    member x.Url
        with get() = x.htmlDoc.GetProperty<string>("URL")

    member x.GetElementById(id : string) =
        let el = x.htmlDoc.Invoke("getElementById", [| box(id) |]) :?> ScriptObject
        new DomElement(el)

    member x.GetElementsByName(name : string) =
        let els = x.htmlDoc.Invoke("getElementsByName", [|box(name)|]) :?> ScriptObjectCollection
        els |> Seq.map(fun e -> new DomElement(e)) |> Seq.toArray

    member x.GetElementsByTagName(tagName : string) =
        let els = x.htmlDoc.Invoke("getElementsByTagName", [| box(tagName) |]) :?> ScriptObjectCollection
        els |> Seq.map(fun so -> new DomElement(so)) |> Seq.toArray

    member x.Anchors
        with get() =
            let p = x.htmlDoc.GetProperty("anchors") :?> ScriptObjectCollection
            p |> Seq.map( fun s -> new DomAnchor(s)) |> Seq.toArray

    member x.Forms
        with get() =
            let p = x.htmlDoc.GetProperty("forms") :?> ScriptObjectCollection
            p |> Seq.map( fun s -> new DomForm(s)) |> Seq.toArray

    member x.Images
        with get() =
            let p = x.htmlDoc.GetProperty("images") :?> ScriptObjectCollection
            p |> Seq.map( fun s -> new DomImage(s)) |> Seq.toArray

    member x.Links
        with get() =
            let p = x.htmlDoc.GetProperty("links") :?> ScriptObjectCollection
            p |> Seq.map( fun s -> new DomLink(s)) |> Seq.toArray

    member x.CreateElement(tagName : string) =
        let el = x.htmlDoc.Invoke("createElement", [| box(tagName) |]) :?> ScriptObject
        new DomElement(el)

    member x.CreateElementNS(ns:String, tagName : string) =
        let el = x.htmlDoc.Invoke("createElementNS", box(ns) , box(tagName)) :?> ScriptObject        
        new DomElement(el)

    member x.CreateTextNode(text : string) =
        let el = x.htmlDoc.Invoke("createTextNode", [|box(text) |]) :?> ScriptObject
        new DomElement(el)

    member x.Submit() =
        x.htmlDoc.Invoke("submit", null) |> ignore

    member x.Submit(formId : string) =
        x.htmlDoc.Invoke("submit", [| box(formId) |]) |> ignore

    member x.Open(mimeType : string) =
        x.htmlDoc.Invoke("open", [|box(mimeType)|]) |> ignore

    member x.Open(mimeType : string, replace : string) =
        x.htmlDoc.Invoke("open", [|box(mimeType);box(replace)|]) |> ignore

    member x.Close() =
        x.htmlDoc.Invoke("close", null) |> ignore

    member x.Write(str : string) =
        x.htmlDoc.Invoke("write", [| box(str) |]) |> ignore

    member x.WriteLn(str : string) =
        x.htmlDoc.Invoke("writeln", [| box(str) |]) |> ignore

    member x.QuerySelector(query:string) =
        x.htmlDoc.Invoke("querySelector", [| box(query) |]) |> ignore

    member x.QuerySelectorAll(query:string) =
        let els = x.htmlDoc.Invoke("querySelectorAll", [|box(query)|]) :?> ScriptObjectCollection
        els |> Seq.map(fun e -> new DomElement(e)) |> Seq.toArray