namespace Pit.Dom.Tests

    open Pit
    open Pit.Dom

    module DomEventsTest =

        [<Js>]
        let OneButtonSetUp() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<input type='button' value='Button 1' id='but1' />"
            ()

        [<Js>]
        let TwoButtonSetUp() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<input type='button' value='Button 1' id='but1' /><input type='button' value='Button 2' id='but2' />"
            ()

        [<Js>]
        let OneTextSetUp() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<input type='text' value='Text 1' id='txt1' />"
            ()

        [<Js>]
        let TwoTextSetUp() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<input type='text' value='Text 1' id='txt1' /><input type='text' value='Text 2' id='txt2' />"
            ()

        [<Js>]
        let Click() =
            OneButtonSetUp()
            let but = document.GetElementById("but1") |> DomInputButton.Of
            but
            |> Event.click
            |> Event.add (fun k -> (Assert.IsNotNull "Click Test" k))
            but.Click() |> ignore

        [<Js>]
        let Change() =
            OneTextSetUp()
            let txt = document.GetElementById("txt1") |> DomInputText.Of
            txt
            |> Event.change
            |> Event.add (fun k ->
                (Assert.IsNotNull "Change Test" k)
                Click())
            txt.Value <- "NewText"

        [<Js>]
        let Blur() =
            TwoTextSetUp()
            let txt = document.GetElementById("txt1") |> DomInputText.Of
            txt
            |> Event.blur
            |> Event.add (fun k ->
                (Assert.IsNotNull "Blur Test" k)
                Change())
            let t2 = document.GetElementById("txt2") |> DomInputText.Of
            t2.Focus()

        [<Js>]
        let DBLClick() =
            OneButtonSetUp()
            let but = document.GetElementById("but1") |> DomInputButton.Of
            but
            |> Event.dblclick
            |> Event.add (fun k ->
                (Assert.IsNotNull "DblClick Test" k)
                Blur())
            alert("Double click button to test dbl click event")

        [<Js>]
        let Error() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='empty.gif' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.error
            |> Event.add (fun k ->
                (Assert.IsNotNull "Error Test" k)
                DBLClick())

        [<Js>]
        let Focus() =
            OneButtonSetUp()
            let but = document.GetElementById("but1") |> DomInputButton.Of
            but
            |> Event.focus
            |> Event.add (fun k ->
                                (Assert.IsNotNull "Focus Test" k)
                                Error())
            but.Focus()

        [<Js>]
        let KeyDown() =
            OneTextSetUp()
            let txt = document.GetElementById("txt1") |> DomInputText.Of
            txt
            |> Event.keydown
            |> Event.add (fun k ->
                (Assert.IsNotNull "KeyDown Test" k)
                Focus())
            alert("Type in text box to chk")

        [<Js>]
        let KeyUp() =
            OneTextSetUp()
            let txt = document.GetElementById("txt1") |> DomInputText.Of
            txt
            |> Event.keyup
            |> Event.add (fun k ->
                (Assert.IsNotNull "KeyUpDown Test" k)
                KeyDown())
            alert("Type in text box to chk")

        [<Js>]
        let KeyPress() =
            OneTextSetUp()
            let txt = document.GetElementById("txt1") |> DomInputText.Of
            txt
            |> Event.keypress
            |> Event.add (fun k ->
                (Assert.IsNotNull "KeyPress Test" k)
                KeyUp())
            alert("Type in text box to chk")

        [<Js>]
        let Load() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='black.png' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.load
            |> Event.add (fun k ->
                (Assert.IsNotNull "Load Test" k)
                KeyPress())

        [<Js>]
        let MouseDown() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='empty.gif' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.mousedown
            |> Event.add (fun k ->
                (Assert.IsNotNull "MouseDown Test" k)
                Load())
            alert("MouseDown in image location")

        [<Js>]
        let MouseMove() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='empty.gif' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.mousemove
            |> Event.add (fun k ->
                (Assert.IsNotNull "MouseMove Test" k)
                MouseDown())
            alert("MouseMove in image location")

        [<Js>]
        let MouseUp() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='empty.gif' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.mouseup
            |> Event.add (fun k ->
                (Assert.IsNotNull "MouseUp Test" k)
                MouseMove())
            alert("MouseUp in image location")

        [<Js>]
        let MouseOver() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='empty.gif' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.mouseover
            |> Event.add (fun k ->
                (Assert.IsNotNull "MouseOver Test" k)
                MouseUp())
            alert("MouseOver in image location")

        [<Js>]
        let MouseOut() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img src='empty.gif' id='img1' />"
            let img = document.GetElementById("img1") |> DomImage.Of
            img
            |> Event.mouseout
            |> Event.add (fun k ->
                (Assert.IsNotNull "MouseOut Test" k)
                MouseOver())
            alert("MouseOut in image location")

        [<Js>]
        let Select() =
            OneTextSetUp()
            let txt = document.GetElementById("txt1") |> DomInputText.Of
            txt
            |> Event.select
            |> Event.add (fun k ->
                (Assert.IsNotNull "Select Test" k)
                MouseOut())
            alert("Select some text in textbox")