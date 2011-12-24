namespace Pit.Dom.Html5
open Pit
open System.Windows.Browser

    [<ScriptableType>]
    type WebSocketMessageEventArgs(data:string) =
        [<ScriptableMember>]
        member x.Data
            with get() = data

    type WebSocket(url:string, protocol:string array) =
        static do
            // websocket initialization thru string
            "function createSocket() {
                window.WebSocket = window.WebSocket || window.MozWebSocket;
             }" |> eval

            "WebSocketWrapper.onOpenDebug = function(socket, xso) {
                socket.onopen = function() {
                    xso.Invoke();
                }
             }" |> eval

            "WebSocketWrapper.onMessageDebug = function(socket,xso) {
                socket.onmessage = function(e) {
                    xso.Invoke(e);
                }
            }" |> eval

            "WebSocketWrapper.onCloseDebug = function(socket,xso) {
                socket.onclose = function() {
                    xso.Invoke();
                }
            }" |> eval

        let socket =
            HtmlPage.Window.Invoke("createSocket", [||]) |> ignore
            if protocol.Length = 0 then
                HtmlPage.Window.Invoke("WebSocket", [|box(url)|]) :?> ScriptObject
            else
                HtmlPage.Window.Invoke("WebSocket", [|box(url);box(protocol)|]) :?> ScriptObject

        new(url:string) = new WebSocket(url, [||])

        member x.ReadyState =
            socket.GetProperty("readyState") :?> string

        member x.BufferedAmount =
            socket.GetProperty("bufferedAmount") |> toInt

        member x.Send(data:string) =
            socket.Invoke("send", [|data|]) |> ignore

        member x.Close() =
            socket.Invoke("close", [||]) |> ignore

        member x.OnOpen
            with set(v:unit->unit) =
                let so = new ScriptableInvoker(v)
                HtmlPage.RegisterScriptableObject("socket",so)
                let wrap = HtmlPage.Window.GetProperty("WebSocketWrapper") :?> ScriptObject
                wrap.Invoke("onOpenDebug", [|box(socket);box(so)|]) |> ignore

        member x.OnMessage
            with set(v:WebSocketMessageEventArgs->unit) =
                let so = new ScriptableInvokerArgs<WebSocketMessageEventArgs>(v)
                HtmlPage.RegisterScriptableObject("socket",so)
                let wrap = HtmlPage.Window.GetProperty("WebSocketWrapper") :?> ScriptObject
                wrap.Invoke("onMessageDebug", [|box(socket);box(so)|]) |> ignore

        member x.OnClose
            with set(v:unit->unit) =
                let so = new ScriptableInvoker(v)
                HtmlPage.RegisterScriptableObject("socket",so)
                let wrap = HtmlPage.Window.GetProperty("WebSocketWrapper") :?> ScriptObject
                wrap.Invoke("onCloseDebug", [|box(socket);box(so)|]) |> ignore