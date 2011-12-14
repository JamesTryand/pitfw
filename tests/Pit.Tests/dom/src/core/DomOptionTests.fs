namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomOptionTests =
        [<Js>]
        let DomOptionSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<select id='mySelect'><option id='op1'>Apple</option></select>"

        [<Js>]
        let op() =
            document.GetElementById("op1") |> DomOption.Of

        [<Js>]
        let DefaultSelected() =
            let op = op()
            Assert.AreEqual "DefaultSelected" op.DefaultSelected false

        [<Js>]
        let Form() =
            let op = op()
            Assert.IsNull "Form" op.Form

        [<Js>]
        let Index() =
            let op = op()
            Assert.AreEqual "Index" op.Index 0

        [<Js>]
        let Selected() =
            let op = op()
            Assert.AreEqual "Selected" op.Selected true

        [<Js>]
        let Text() =
            let op = op()
            Assert.AreEqual "Text" op.Text "Apple"

        [<Js>]
        let Value() =
            let op = op()
            Assert.AreEqual "Value" op.Value "Apple"