namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral;JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomElement() =
    inherit DomObject()
    let style = DomStyle()

    static member Of(obj : DomObject) =
        new DomElement()

    [<CompileTo("accessKey")>]
    member x.AccessKey
        with get() = String.Empty
        and set(v : String) = ()

    [<CompileTo("className")>]
    member x.ClassName
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("clientHeight")>]
    member x.ClientHeight
        with get() = 0.

    [<CompileTo("clientWidth")>]
    member x.ClientWidth
        with get() = 0.

    [<CompileTo("dir")>]
    member x.Dir
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("disabled")>]
    member x.Disabled
        with get() = false
        and set(v : bool) = ()

    [<CompileTo("firstChild")>]
    member x.FirstChild
        with get() = new DomElement()

    [<CompileTo("height")>]
    member x.Height
        with get() = 0.
        and set(v : float) = ()

    [<CompileTo("id")>]
    member x.Id
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("innerHTML")>]
    member x.InnerHTML
        with get() = String.Empty
        and set(v : String) = ()

    [<CompileTo("lang")>]
    member x.Lang
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("cssClass")>]
    member x.CssClass
        with get() = String.Empty
        and set(v : string) = ()
        
    [<CompileTo("lastChild")>]
    member x.LastChild
        with get() = new DomElement()

    [<CompileTo("length")>]
    member x.Length
        with get() = 0

    [<CompileTo("localName")>]
    member x.LocalName
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("namespaceURI")>]
    member x.NamespaceURI
        with get() = String.Empty

    [<CompileTo("nextSibling")>]
    member x.NextSibling
        with get() = new DomElement()
        
    [<CompileTo("nodeName")>]
    member x.NodeName
        with get() = String.Empty

    [<CompileTo("nodeType")>]
    member x.NodeType
        with get() = 0.

    [<CompileTo("nodeValue")>]
    member x.NodeValue
        with get() = String.Empty

    [<CompileTo("offsetHeight")>]
    member x.OffsetHeight
        with get() = 0.

    [<CompileTo("offsetLeft")>]
    member x.OffsetLeft
        with get() = 0.

    [<CompileTo("offsetParent")>]
    member x.OffsetParent
        with get() = new DomElement()

    [<CompileTo("offsetTop")>]
    member x.OffsetTop
        with get() = 0.

    [<CompileTo("offsetWidth")>]
    member x.OffsetWidth
        with get() = 0.

    [<CompileTo("parentNode")>]
    member x.ParentNode
        with get() = new DomElement()

    [<CompileTo("prefix")>]
    member x.Prefix
        with get() = String.Empty
        and set(v : String) = ()

    [<CompileTo("previousSibling")>]
    member x.PreviousSibling
        with get() = new DomElement()

    [<CompileTo("scrollHeight")>]
    member x.ScrollHeight
        with get() = 0.

    [<CompileTo("scrollLeft")>]
    member x.ScrollLeft
        with get() = 0.

    [<CompileTo("scrollTop")>]
    member x.ScrollTop
        with get() = 0.

    [<CompileTo("scrollWidth")>]
    member x.ScrollWidth
        with get() = 0.

    [<CompileTo("style")>]
    member x.Style
        with get() = style
        and set(v:string) = () 

    [<CompileTo("tabIndex")>]
    member x.TabIndex
        with get() = 0
        and set(v : int) = ()

    [<CompileTo("tagName")>]
    member x.TagName with get() = String.Empty

    [<CompileTo("title")>]
    member x.Title
        with get() = String.Empty
        and set(v : string) = ()

    [<CompileTo("width")>]
    member x.Width
        with get() = 0.
        and set(v : float) = ()

    [<CompileTo("appendChild")>]
    member x.AppendChild(element : DomElement) =
        ()

    [<CompileTo("appendChild")>]
    member x.AppendChild (element : DomElement, referenceElement : DomElement) =
        ()

    [<CompileTo("blur")>]
    member x.Blur() =
        ()

    [<CompileTo("click")>]
    member x.Click() =
        ()

    [<CompileTo("removeChild")>]
    member x.RemoveChild (element : DomElement) =
        ()

    [<CompileTo("replaceChild")>]
    member x.ReplaceChild(newNode : DomElement, oldNode : DomElement) =
        ()

    [<CompileTo("focus")>]
    member x.Focus() =
        ()

    [<CompileTo("getElementsByTagName")>]
    member x.GetElementsByTagName(tagName : string) =
        let p : DomElement[] = Array.zeroCreate 0
        p

    [<CompileTo("hasChildNodes")>]
    member x.HasChildNodes() =
        false

    [<CompileTo("insertBefore")>]
    member x.InsertBefore(newNode : DomElement, existingNode : DomElement) =
        ()

    [<CompileTo("item")>]
    member x.Item(idx : int) =
        new DomElement()

    [<CompileTo("getAttribute")>]
    member x.GetAttribute(name : string) =
        String.Empty

    [<CompileTo("setAttribute")>]
    member x.SetAttribute(name : string, value : string) =
        ()

    [<CompileTo("removeAttribute")>]
    member x.RemoveAttribute(name : string) =
        ()

    [<CompileTo("cloneNode")>]
    member x.CloneNode() =
        new DomElement()

    [<CompileTo("toString")>]
    override x.ToString() =
        String.Empty