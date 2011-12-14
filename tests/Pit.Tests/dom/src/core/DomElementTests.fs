namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomElementTests =

        [<Js>]
        let private divElement = document.GetElementById("check")

        [<Js>]
        let AccessKey() =
            divElement.AccessKey <- "w"
            Assert.AreEqual "Access key test" divElement.AccessKey "w"

        [<Js>]
        let ClassName() =
            divElement.ClassName <- "cName"
            Assert.AreEqual "Class Name test" divElement.AccessKey "cName"


        [<Js>]
        let ClientHeight() =
            Assert.AreNotEqual "Client Height test" divElement.ClientHeight 20.0

        [<Js>]
        let ClientWidth() =
            Assert.AreNotEqual "Client Width test" divElement.Width 20.0

        [<Js>]
        let Dir() =
            divElement.Dir <- "ltr"
            Assert.AreEqual "Direction(dir) test" divElement.Dir "ltr"

        [<Js>]
        let Disabled() =
            Assert.AreEqual "Disabled test" divElement.Disabled false

        [<Js>]
        let Height() =
            Assert.AreNotEqual "Height test" divElement.Height 20.0

        [<Js>]
        let TagName() =
            Assert.AreEqual "Tag Name test" divElement.TagName ""

        [<Js>]
        let Id() =
            Assert.AreEqual "Id test" divElement.Id "check"

        [<Js>]
        let InnerHTML() =
            divElement.InnerHTML <- "Some"
            Assert.AreEqual "Inner HTML test" divElement.InnerHTML "Some"


        [<Js>]
        let Lang() =
            Assert.AreEqual "Language(Lang) test" divElement.Lang ""


        [<Js>]
        let CssClass() =
            Assert.AreEqual "Css Class test" divElement.CssClass ""

        [<Js>]
        let FirstChild() =
            divElement.InnerHTML <- "<div id='first'></div><div id='last'></div>"
            Assert.AreEqual "First Child Test" divElement.FirstChild.Id "first"

        [<Js>]
        let LastChild() =
            Assert.AreEqual "Last Child Test" divElement.LastChild.Id "last"

        [<Js>]
        let Length() =
            Assert.IsNull "Length Test" divElement.Length


        [<Js>]
        let LocalName() =
            Assert.IsNull "Local Name Test" divElement.LocalName


        [<Js>]
        let NamespaceURI() =
            Assert.IsNull "NamespaceURI Test" divElement.NamespaceURI


        [<Js>]
        let NextSibling() =
            Assert.IsNull "NextSibling Test" divElement.NextSibling

        (*[<Js>]
        let OffsetHeight() =
            divElement.OffsetHeight <- 40.
            Assert.AreEqual "OffsetHeight Test" divElement.OffsetHeight 40.

        [<Js>]
        let OffsetLeft() =
            divElement.OffsetLeft <- 40.
            Assert.AreEqual "OffsetLeft Test" divElement.OffsetLeft 40.

        [<Js>]
        let OffsetTop() =
            divElement.OffsetTop <- 40.
            Assert.AreEqual "OffsetTopTest Test" divElement.OffsetTop 40.

        [<Js>]
        let OffsetWidth() =
            divElement.OffsetWidth <- 40.
            Assert.AreEqual "OffsetTopTest Test" divElement.OffsetWidth 40.*)

        [<Js>]
        let ParentNode() =
            Assert.IsNull "ParentNode Test" divElement.ParentNode

        [<Js>]
        let Prefix() =
            divElement.Prefix <- "prefix"
            Assert.AreEqual "Prefix Test" divElement.Prefix "prefix"


        [<Js>]
        let PreviousSibling() =
            Assert.IsNull "PreviousSibling Test" divElement.PreviousSibling


        (*[<Js>]
        let NodeName() =
            divElement.NodeName <- "nodename"
            Assert.AreEqual "NodeName Test" divElement.NodeName "nodename"

        [<Js>]
        let NodeValue() =
            divElement.NodeName <- "nodevalue"
            Assert.AreEqual "NodeValue Test" divElement.NodeName "nodevalue"

        [<Js>]
        let NodeType() =
            divElement.NodeType <- 1.
            Assert.AreEqual "NodeType Test" divElement.NodeType 1.


        [<Js>]
        let ScrollHeight() =
            divElement.ScrollHeight <- 10.
            Assert.AreEqual "ScrollHeight Test" divElement.ScrollHeight 10.

        [<Js>]
        let ScrollWidth() =
            divElement.ScrollWidth <- 10.
            Assert.AreEqual "ScrollWidth Test" divElement.ScrollWidth 10.

        [<Js>]
        let ScrollLeft() =
            divElement.ScrollLeft <- 10.
            Assert.AreEqual "ScrollLeft Test" divElement.ScrollLeft 10.

        [<Js>]
        let ScrollTop() =
            divElement.ScrollTop <- 10.
            Assert.AreEqual "ScrollTop Test" divElement.ScrollTop 10.*)

        [<Js>]
        let TabIndex() =
            divElement.TabIndex <- 3
            Assert.AreEqual "TabIndex Test" divElement.Style 3

        [<Js>]
        let Title() =
            divElement.Title <- "title"
            Assert.AreEqual "Title Test" divElement.Title "title"

        [<Js>]
        let Width() =
            divElement.Title <- "width"
            Assert.AreEqual "Width Test" divElement.Width "width"

        [<Js>]
        let AppendChild() =
            let im = document.CreateElement("div")
            im.LocalName <- "lName"
            divElement.AppendChild(im)
            Assert.AreEqual "Width Test" divElement.FirstChild.LocalName "lName"

//
//    member x.AppendChild(element : DomElement, referenceElement : DomElement) =
//        let so1 = element.GetInternalScriptObject()
//        let so2 = referenceElement.GetInternalScriptObject()
//        htmlEl.Invoke("appendChild", [| box(so1); box(so2) |]) |> ignore
//
//    member x.Blur() =
//        htmlEl.Invoke("blur", null)
//
//    member x.Click() =
//        htmlEl.Invoke("click", null)

        [<Js>]
        let RemoveChild() =
            let im = document.GetElementById("div")
            divElement.RemoveChild(im)
            let els = divElement.GetElementsByTagName("div")
            Assert.AreEqual "RemoveChild Test" els.Length 0


        [<Js>]
        let ReplaceChild() =
            let im = document.CreateElement("div")
            im.LocalName <- "lName"
            divElement.AppendChild(im)
            let nNode = document.CreateElement("img")
            divElement.ReplaceChild(nNode, im)
//
//    member x.Focus() =
//        htmlEl.Invoke("focus", null) |> ignore

        [<Js>]
        let GetElementsByTagName() =
            let im = document.GetElementsByTagName("div")
            Assert.AreEqual "GetElementsByTagName Test" (im.Length > 0)  true

        [<Js>]
        let HasChildNodes() =
            Assert.AreEqual "HasChildNodes Test" (divElement.HasChildNodes()) true
//
//        [<Js>]
//        let InsertBefore() =
//            let im = document.CreateElement("div")
//            im.LocalName <- "lName"
//            divElement.InsertBefore(im ,

//
//    member x.Item(idx : int) =
//        let el = htmlEl.Invoke("item", [| box(idx) |]) :?> ScriptObject
//        new DomElement(el)
//
//    member x.GetAttribute(name : string) =
//        htmlEl.Invoke("getAttribute", [| box(name) |]) :?> string
//
//    member x.SetAttribute(name : string, value : string) =
//        htmlEl.Invoke("setAttribute", [| box(name); box(value) |]) |> ignore
//
//    member x.RemoveAttribute(name : string) =
//        htmlEl.Invoke("removeAttribute", [| box(name) |]) |> ignore
//
//    member x.CloneNode() =
//        let el = htmlEl.Invoke("cloneNode", null) :?> ScriptObject
//        new DomElement(el)
//
//    override x.ToString() =
//        let str = htmlEl.Invoke("toString", null) :?> string
//        str
//