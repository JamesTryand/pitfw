namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom
    // LINK Tags is used to load up the files, no need to test this, just ensure the js code generation
    (*module DomLinkTests =

        [<Js>]
        let Clear() =
            let div = document.GetElementById("check")
            div.InnerHTML <- ""

        [<Js>]
        let DomLinkSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<link id='lin'></link>"

        [<Js>]
        let link =
            document.GetElementById("lin") |> DomLink.Of

        [<Js>]
        let Charset() =
            link.Charset <- "ISO-8859-1"
            Assert.AreEqual "CharSet" link.Charset "ISO-8859-1"

        [<Js>]
        let Href() =
            link.Href <- "http://www.google.com"
            Assert.AreEqual "Href" link.Href "http://www.google.com"

        [<Js>]
        let HrefLang() =
            link.HrefLang <- "us-en"
            Assert.AreEqual "HrefLang" link.Href "us-en"

        [<Js>]
        let Rel() =
            link.Rel <- "friend"
            Assert.AreEqual "Rel" link.Rel "friend"

        [<Js>]
        let Rev() =
            link.Rev <- "friend"
            Assert.AreEqual "Rev" link.Rev "friend"

        [<Js>]
        let Target() =
            link.Media <- "screen"
            Assert.AreEqual "Target" link.Media "screen"

        [<Js>]
        let Type() =
            link.Type <- "text\html"
            Assert.AreEqual "Type" link.Type "text\html"*)