namespace Pit.Javascript
open Pit
open System.Windows.Browser

    type XMLHttpRequest() =
        static do
            // some problem when creating ActiveXObject thru Silverlight, so we just use this below generic method to get the object
            "function createXMLHttp() {
                if (window.ActiveXObject) {
                    return new ActiveXObject(\"Msxml2.XMLHTTP\");
                }
                else {
                    return new XMLHttpRequest();
                }
            }" |> eval

            // registering a proxy method to invoke the function when the actual ajax event gets fired
            "XMLHttpRequest.onReadyStateChangeDebug = function (xhr, xso) {
                xhr.onreadystatechange = function () {
                    xso.Invoke();
                }
            }" |> eval

        let xmlhttp = HtmlPage.Window.Invoke("createXMLHttp", [||]) :?> ScriptObject

        member x.Abort() =
            xmlhttp.Invoke("abort", [||]) |> ignore

        member x.GetAllResponseHeaders() =
            xmlhttp.Invoke("getAllResponseHeaders", [||]) :?> string

        member x.GetResponseHeader() =
            xmlhttp.Invoke("getResponseHeader", [||]) :?> string

        member x.Open(methodType: string, url: string, async: bool) =
            xmlhttp.Invoke("open", [| box(methodType); box(url); box(async); |]) |> ignore

        member x.Send(data:string) =
            xmlhttp.Invoke("send", [|box(data)|]) |> ignore

        member x.SetRequestHeader(key:string, value:string) =
            xmlhttp.Invoke("setRequestHeader", [| box(key); box(value) |]) |> ignore

        member x.ReadyState
            with get() = xmlhttp.GetProperty("readyState") :?> float |> int

        member x.OnReadyStateChange
            with set(v:unit->unit) =
                let so = new ScriptableInvoker(v)
                HtmlPage.RegisterScriptableObject("xmlhttp", so)
                // uses a proxy method from JS to invoke a proxy object
                // when then delegates the call to the actual func
                let xhr = HtmlPage.Window.GetProperty("XMLHttpRequest") :?> ScriptObject
                xhr.Invoke("onReadyStateChangeDebug", [|box(xmlhttp);box(so)|]) |> ignore

        member x.ResponseText
            with get() = xmlhttp.GetProperty("responseText") :?> string

        member x.ResponseXML
            with get() = xmlhttp.GetProperty("responseXML") :?> ScriptObject

        member x.Status     = xmlhttp.GetProperty("status")
        member x.StatusText = xmlhttp.GetProperty("statusText") :?> string

    type XMLHttpRequestIE(name:string) =
        inherit XMLHttpRequest()