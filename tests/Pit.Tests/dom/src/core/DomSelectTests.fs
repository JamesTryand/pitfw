namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomSelectTests  =
        [<Js>]
        let DomSelectSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<select id='mySelect'><option id='op1'>Apple</option></select>"

        [<Js>]
        let select() =
            document.GetElementById("mySelect") |> DomSelect.Of

        [<Js>]
        let Form() =
            let select = select()
            Assert.IsNull "Form" select.Form

        [<Js>]
        let Length() =
            let select = select()
            Assert.AreEqual "Length" select.Length  1

        [<Js>]
        let Multiple() =
            let select = select()
            Assert.AreEqual "Multiple" select.Multiple  false

        [<Js>]
        let Name() =
            let select = select()
            select.Name <- "name"
            Assert.AreEqual "Name" select.Name  "name"

        [<Js>]
        let SelectedIndex() =
            let select = select()
            Assert.AreEqual "SelectedIndex" select.SelectedIndex  0

        [<Js>]
        let Size() =
            let select = select()
            Assert.AreEqual "Size" select.Size  0

        [<Js>]
        let Type() =
            let select = select()
            Assert.AreEqual "Type" select.Type  "select-one"

        [<Js>]
        let Options() =
            let select = select()
            Assert.AreEqual "Options" select.Options.[0].Text "Apple"