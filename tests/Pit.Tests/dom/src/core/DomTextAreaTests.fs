namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomTextAreaTests =

        [<Js>]
        let Setup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<textarea id='mytextarea'></textarea>"

        [<Js>]
        let get() =
            document.GetElementById("mytextarea") |> DomTextArea.Of

        [<Js>]
        let ColsRows() =
            let t = get()
            t.Cols <- 5
            t.Rows <- 10
            Assert.AreEqual "TextArea Cols" 5 t.Cols
            Assert.AreEqual "TextArea Rows" 10 t.Rows

        [<Js>]
        let DefaultValue() =
            let t = get()
            Assert.IsNotNull "TextArea DefaultValue" t.DefaultValue

        [<Js>]
        let IsReadOnly() =
            let t = get()
            Assert.AreNotEqual "TextArea ReadOnly" false t.ReadOnly

        [<Js>]
        let Type() =
            let t = get()
            Assert.IsNotNull "TextArea Type" t.Type

        [<Js>]
        let Value() =
            let t = get()
            t.Value <- "Hello World! Hello World!!"
            Assert.AreEqual "TextArea Value" "Hello World! Hello World!!" t.Value