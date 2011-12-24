namespace Pit.Dom

open System
open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomElement =
    inherit DomObject

    val htmlEl: ScriptObject
    val style:  DomStyle

    new (htmlEl : ScriptObject) =
            {   inherit DomObject(htmlEl);
                htmlEl = htmlEl
                style  = new DomStyle(htmlEl.GetProperty<ScriptObject>("style"))
            }

    member x.AccessKey
        with get() = x.htmlEl.GetProperty<string>("accessKey")
        and set(v : string) = x.htmlEl.SetProperty("accessKey", box(v))

    member x.ClassName
        with get() = x.htmlEl.GetProperty<string>("className")
        and set(v : string) = x.htmlEl.SetProperty("className", box(v))

    member x.ClientHeight
        with get() = x.htmlEl.GetProperty<float>("clientHeight")

    member x.ClientWidth
        with get() = x.htmlEl.GetProperty<float>("clientWidth")

    member x.Dir
        with get() = x.htmlEl.GetProperty<string>("dir")
        and set(v : string) = x.htmlEl.SetProperty("dir", box(v))

    member x.Disabled
        with get() = x.htmlEl.GetProperty<bool>("disabled")
        and set(v : bool) = x.htmlEl.SetProperty("disabled", box(v))

    member x.FirstChild
        with get() =
            let firstChild = x.htmlEl.GetProperty<ScriptObject>("firstChild")
            new DomElement(firstChild)

    member x.Height
        with get() = x.htmlEl.GetProperty<float>("height")
        and set(v : float) = x.htmlEl.SetProperty("height", box(v))

    member x.Id
        with get() = x.htmlEl.GetProperty<string>("id")
        and set(v:string) = x.htmlEl.SetProperty("id", box(v))

    member x.InnerHTML
        with get() = x.htmlEl.GetProperty<string>("innerHTML")
        and set(v : string) = x.htmlEl.SetProperty("innerHTML", box(v))

    member x.Lang
        with get() = x.htmlEl.GetProperty<string>("lang")
        and set(v : string) = x.htmlEl.SetProperty("lang", box(v))

    member x.CssClass
        with get() = x.htmlEl.GetProperty<string>("cssClass")
        and set(v : string) = x.htmlEl.SetProperty("cssClass", box(v))


    member x.LastChild
        with get() =
            let lastChild = x.htmlEl.GetProperty<ScriptObject>("lastChild")
            new DomElement(lastChild)

    member x.Length
        with get() = x.htmlEl.GetProperty<float>("length") |> int

    member x.LocalName
        with get() = x.htmlEl.GetProperty<string>("localName")
        and set(v : string) = x.htmlEl.SetProperty("localName", box(v))

    member x.NamespaceURI
        with get() = x.htmlEl.GetProperty<string>("namespaceURI")

    member x.NextSibling
        with get() =
            let nextSib = x.htmlEl.GetProperty<ScriptObject>("nextSibling")
            new DomElement(nextSib)

    member x.NodeName
        with get() = x.htmlEl.GetProperty<string>("nodeName")

    member x.NodeType
        with get() = x.htmlEl.GetProperty<float>("nodeType")

    member x.NodeValue
        with get() = x.htmlEl.GetProperty<string>("nodeValue")

    member x.OffsetHeight
        with get() = x.htmlEl.GetProperty<float>("offsetHeight")

    member x.OffsetLeft
        with get() = x.htmlEl.GetProperty<float>("offsetLeft")

    member x.OffsetTop
        with get() = x.htmlEl.GetProperty<float>("offsetTop")

    member x.OffsetWidth
        with get() = x.htmlEl.GetProperty<float>("offsetWidth")

    member x.ParentNode
        with get() =
            let parent = x.htmlEl.GetProperty<ScriptObject>("parentNode")
            new DomElement(parent)

    member x.Prefix
        with get() = x.htmlEl.GetProperty<string>("prefix")
        and set(v : string) = x.htmlEl.SetProperty("prefix", box(v))

    member x.PreviousSibling
        with get() =
            let prevSib = x.htmlEl.GetProperty<ScriptObject>("previousSibling")
            new DomElement(prevSib)

    member x.ScrollHeight
        with get() = x.htmlEl.GetProperty<float>("scrollHeight")

    member x.ScrollLeft
        with get() = x.htmlEl.GetProperty<float>("scrollLeft")

    member x.ScrollTop
        with get() = x.htmlEl.GetProperty<float>("scrollTop")

    member x.ScrollWidth
        with get() = x.htmlEl.GetProperty<float>("scrollWidth")

    member x.Style
        with get() = x.style
        and set(v:string) = x.htmlEl.SetProperty("tabIndex", box(v))

    member x.TabIndex
        with get() = x.htmlEl.GetProperty<float>("tabIndex") |> int
        and set(v : int)  = x.htmlEl.SetProperty("tabIndex", box(v))

    member x.TagName
        with get() = x.htmlEl.GetProperty<string>("tagName")

    member x.Title
        with get() = x.htmlEl.GetProperty<string>("title")
        and set(v : string) = x.htmlEl.SetProperty("title", box(v))

    member x.Width
        with get() = x.htmlEl.GetProperty<float>("width")
        and set(v : float) = x.htmlEl.SetProperty("width", box(v))

    member internal x.GetInternalScriptObject() =
        x.htmlEl

    member x.AppendChild(element : DomElement) =
        let so = element.GetInternalScriptObject()
        x.htmlEl.Invoke("appendChild", [| box(so) |]) |> ignore

    member x.AppendChild(element : DomElement, referenceElement : DomElement) =
        let so1 = element.GetInternalScriptObject()
        let so2 = referenceElement.GetInternalScriptObject()
        x.htmlEl.Invoke("appendChild", [| box(so1); box(so2) |]) |> ignore

    member x.Blur() =
        x.htmlEl.Invoke("blur", null) |> ignore

    member x.Click() =
        x.htmlEl.Invoke("click", null) |> ignore

    member x.RemoveChild(element : DomElement) =
        let so = element.GetInternalScriptObject()
        x.htmlEl.Invoke("removeChild", [| box(so) |]) |> ignore

    member x.ReplaceChild(newNode : DomElement, oldNode : DomElement) =
        let so1 = newNode.GetInternalScriptObject()
        let so2 = oldNode.GetInternalScriptObject()
        x.htmlEl.Invoke("replaceChild", [| box(so1); box(so2) |]) |> ignore

    member x.Focus() =
        x.htmlEl.Invoke("focus", null) |> ignore

    member x.GetElementsByTagName(tagName : string) =
        let els = x.htmlEl.Invoke("getElementsByTagName", [| box(tagName) |]) :?> ScriptObjectCollection
        els |> Seq.map(fun so -> new DomElement(so)) |> Seq.toArray

    member x.HasChildNodes() =
        let res = x.htmlEl.Invoke("hasChildNodes", null) :?> Boolean
        res

    member x.InsertBefore(newNode : DomElement, existingNode : DomElement) =
        let so1 = newNode.GetInternalScriptObject()
        let so2 = existingNode.GetInternalScriptObject()
        x.htmlEl.Invoke("insertBefore", [| box(so1); box(so2) |]) |> ignore

    member x.Item(idx : int) =
        let el = x.htmlEl.Invoke("item", [| box(idx) |]) :?> ScriptObject
        new DomElement(el)

    member x.GetAttribute(name : string) =
        x.htmlEl.Invoke("getAttribute", [| box(name) |]) :?> string

    member x.SetAttribute(name : string, value : string) =
        x.htmlEl.Invoke("setAttribute", [| box(name); box(value) |]) |> ignore

    member x.RemoveAttribute(name : string) =
        x.htmlEl.Invoke("removeAttribute", [| box(name) |]) |> ignore

    member x.CloneNode() =
        let el = x.htmlEl.Invoke("cloneNode", null) :?> ScriptObject
        new DomElement(el)

    override x.ToString() =
        let str = x.htmlEl.Invoke("toString", null) :?> string
        str