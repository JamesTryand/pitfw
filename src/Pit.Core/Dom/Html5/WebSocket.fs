namespace Pit.Dom.Html5
open Pit

    [<JsIgnore(IgnoreNamespace=true,IgnoreGetSet=true)>]
    type WebSocketMessageEventArgs() =
        [<CompileTo("data")>]
        member x.Data
            with get() = ""

    [<JsIgnore(IgnoreNamespace=true,IgnoreGetSet=true)>]
    type WebSocket(url:string, protocol:string array) =

        new(url:string) = new WebSocket(url, [||])

        [<CompileTo("readyState")>]
        member x.ReadyState = ""

        [<CompileTo("bufferedAmount")>]
        member x.BufferedAmount = 0

        [<CompileTo("send")>]
        member x.Send(data:string) = ()

        [<CompileTo("close")>]
        member x.Close() = ()

        [<CompileTo("onopen")>]
        member x.OnOpen
            with set(v:unit->unit) = ()

        [<CompileTo("onmessage")>]
        member x.OnMessage
            with set(v:WebSocketMessageEventArgs->unit) = ()

        [<CompileTo("onclose")>]
        member x.OnClose
            with set(v:unit->unit) = ()