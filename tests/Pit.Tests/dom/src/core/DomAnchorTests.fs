namespace Pit.Dom.Tests

    open Pit
    open Pit.Dom

    module DomAnchorTests =

        [<Js>]
        let DomAnchorSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<a id='anc'><a/>"

        [<Js>]
        let anc() =
            document.GetElementById("anc") |> DomAnchor.Of

        [<Js>]
        let Charset() =
            let anc = anc()
            anc.Charset <- "ISO-8859-1"
            Assert.AreEqual "CharSet" anc.Charset "ISO-8859-1"

        [<Js>]
        let Href() =
            let anc = anc()
            anc.Href <- "http://www.google.com"
            Assert.AreEqual "Href" anc.Href "http://www.google.com/"

        [<Js>]
        let HrefLang() =
            let anc = anc()
            anc.HrefLang <- "us-en"
            Assert.AreEqual "HrefLang" anc.HrefLang "us-en"

        [<Js>]
        let Name() =
            let anc = anc()
            anc.Name <- "name"
            Assert.AreEqual "Name" anc.Name "name"

        [<Js>]
        let Rel() =
            let anc = anc()
            anc.Rel <- "friend"
            Assert.AreEqual "Rel" anc.Rel "friend"

        [<Js>]
        let Rev() =
            let anc = anc()
            anc.Rev <- "friend"
            Assert.AreEqual "Rev" anc.Rev "friend"

        [<Js>]
        let Target() =
            let anc = anc()
            anc.Target <- "_blank"
            Assert.AreEqual "Target" anc.Target "_blank"

        [<Js>]
        let Type() =
            let anc = anc()
            anc.Type <- "text"
            Assert.AreEqual "Type" anc.Type "text"
