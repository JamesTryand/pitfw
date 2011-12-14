namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomMetaTests =

        [<Js>]
        let DomLinkSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<meta  id='met'></meta>"

        [<Js>]
        let meta =
            document.GetElementById("met") |> DomMeta.Of

        [<Js>]
        let Content() =
            meta.Content <- "Some"
            Assert.AreEqual "Content" meta.Content "Some"

        [<Js>]
        let Name() =
            meta.Name <- "SomeName"
            Assert.AreEqual "Content" meta.Name "SomeName"

        [<Js>]
        let HttpEquiv() =
            meta.HttpEquiv <- "content-type"
            Assert.AreEqual "Content" meta.HttpEquiv "content-type"

        [<Js>]
        let Scheme() =
            meta.Scheme <- "YYYY-MM-DD"
            Assert.AreEqual "Content" meta.Scheme "YYYY-MM-DD"