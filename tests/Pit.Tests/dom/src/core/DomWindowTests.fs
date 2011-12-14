namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomWindow =

        (*[<Js>]
        let Closed() =
            let win = window.Open("http://www.google.com")
            win.Close()
            Assert.AreEqual "Window Closed" win.Closed true*)

        [<Js>]
        let DefaultStatus() =
            window.DefaultStatus <- "Some"
            Assert.AreEqual "Default Status Test" window.DefaultStatus "Some"

        [<Js>]
        let Document() =
            Assert.IsNotNull "Document Test" window.Document

        [<Js>]
        let History() =
            Assert.IsNotNull "History Test" window.History

        [<Js>]
        let Location() =
            Assert.IsNotNull "Location Test" window.Location

        [<Js>]
        let Navigator() =
            Assert.IsNotNull "Naivgator Test" window.Navigator

        (*[<Js>]
        let InnerHeight() =
            window.InnerHeight <- 40.
            Assert.AreEqual "Inner Height test" window.InnerHeight 40.

        [<Js>]
        let InnerWidth() =
            window.InnerWidth <- 40.
            Assert.AreEqual "Inner Height test" window.InnerWidth 40.

        [<Js>]
        let Length() =
            window.Length <- 5.
            Assert.AreEqual "Length Test" window.Length 5.*)

        [<Js>]
        let Name() =
            window.Name <- "WinName"
            Assert.AreEqual "Name Test" window.Name "WinName"


//
//    member x.Opener
//        with get() =
//            let el = htmlWin.GetProperty<ScriptObject>("opener")
//            if el <> null then
//                Some(new DomWindow(el))
//            else
//                None

        [<Js>]
        let OuterHeightTest() =
            window.OuterHeight <- 40.
            Assert.AreEqual "Outer Height test" window.OuterHeight 40.

        [<Js>]
        let OuterWidthTest() =
            window.OuterWidth <- 40.
            Assert.AreEqual "Outer Width test" window.OuterWidth 40.

        (*[<Js>]
        let PageXOffsetTest() =
            window.PageXOffset <- 40.
            Assert.AreEqual "PageXOffset test" window.PageXOffset 40.

        [<Js>]
        let PageYOffsetTest() =
            window.PageYOffset <- 40.
            Assert.AreEqual "PageYOffset test" window.PageYOffset 40.*)

        [<Js>]
        let Parent() =
            Assert.IsNotNull "Parent Test" window.Parent

        [<Js>]
        let Screen() =
            Assert.IsNotNull "Screen Test" window.Screen

        [<Js>]
        let ScreenLeft() =
            Assert.IsNotNull "ScreenLeft Test" window.ScreenLeft

        [<Js>]
        let ScreenTop() =
            Assert.IsNotNull "ScreenTop Test" window.ScreenTop

        [<Js>]
        let ScreenX() =
            Assert.IsNotNull "ScreenX Test" window.ScreenX

        [<Js>]
        let ScreenY() =
            Assert.IsNotNull "ScreenY Test" window.ScreenY

        [<Js>]
        let Self() =
            Assert.IsNotNull "Self Test" window.Self

        [<Js>]
        let Status() =
            Assert.IsNotNull "Status Test" window.Status

        [<Js>]
        let Top() =
            Assert.IsNotNull "Top Test" window.Top

        [<Js>]
        let SetInterval()=
            let id = ref 1
            let started = ref false
            let div = document.GetElementById("check")
            div.InnerHTML <- "<span>1</span>"
            let span = div.LastChild
            let intervalid = ref 0
            let btn = document.GetElementById("test")
            btn
            |> Event.click
            |> Event.add(fun _ ->
                if not(!started) then
                    intervalid:=
                        window.SetInterval(
                            (fun _ ->
                                id := !id + 1
                                span.InnerHTML <- (!id).ToString()), 1000)
                    started:=true
                else
                    window.ClearInterval(!intervalid)
            )