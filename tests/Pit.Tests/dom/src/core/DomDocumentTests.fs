namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomDocument =

        [<Js>]
        let Body() =
            Assert.IsNotNull "Body Test" document.Body

        [<Js>]
        let Cookie() =
            Assert.IsNotNull "Cookie Test" document.Cookie

        [<Js>]
        let DocumentMode() =
            Assert.IsNotNull "DocumentMode Test" document.DocumentMode

        [<Js>]
        let DocumentUri() =
            Assert.IsNotNull "DocumentUri Test" document.DocumentUri

        [<Js>]
        let Domain() =
            Assert.IsNotNull "Domain Test" document.Domain

        [<Js>]
        let LastModified() =
            Assert.IsNotNull "LastModified Test" document.LastModified

        [<Js>]
        let ReadyState() =
            Assert.IsNotNull "ReadyState Test" document.ReadyState

        [<Js>]
        let Title() =
            Assert.IsNotNull "Title Test" document.Title

        [<Js>]
        let Referrer() =
            Assert.IsNotNull "Referrer Test" document.Referrer

        [<Js>]
        let Url() =
            Assert.IsNotNull "Url Test" document.Url

        [<Js>]
        let GetElementById() =
            Assert.AreEqual "GetElementById Test" (document.GetElementById("check").Id) "check"

        [<Js>]
        let GetElementsByName() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<input name='x'></input>"
            let els = document.GetElementsByName("x")
            Assert.AreEqual "GetElementsByName Test" els.Length 1

        [<Js>]
        let GetElementsByTagName() =
            let els = document.GetElementsByName("input")
            Assert.AreEqual "GetElementsByTagName Test" els.Length 1