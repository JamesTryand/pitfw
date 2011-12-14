namespace Pit.Rx

    module RxSample =
        open Pit
        open Pit.Dom
        open Pit.Javascript
        open Pit.Dom.Html5
        #if DEBUG
        type HtmlEventArgs = System.Windows.Browser.HtmlEventArgs
        #endif

        /// Wireup function that hooks to the DOM elements mouse events using Observable module
        [<Js>]
        let wireup (i:int) (el:DomElement) (mouseMove:IEvent<HtmlEventArgs>) =
            mouseMove
            |> Observable.map(fun e -> e.ClientX, e.ClientY)
            |> Observable.delay(i * 100)
            |> Observable.subscribe(fun (x,y) ->
                el.Style.Left <- x.ToString() + "px"
                el.Style.Top  <- y.ToString() + "px"
            )
            |> ignore

        /// Create a list of string elements based on the given string and wires up the DOM mouse events
        [<Js>]
        let trackMouse (str:string) (div: DomElement) =
            div.InnerHTML <- ""
            let msg = new JsString(str)
            let mouseMove = div |> Event.mousemove
            for i = 0 to msg.Length - 1 do
                let c = msg.CharAt(i)
                let closure = i + 1
                let el = document.CreateElement("span")
                el.InnerHTML <- c
                el.Style.Position <- "absolute"
                div.AppendChild(el)
                wireup i el mouseMove