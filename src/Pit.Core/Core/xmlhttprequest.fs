namespace Pit.Javascript
open Pit

    [<JsIgnore(IgnoreNamespace=true,IgnoreGetSet=true)>]
    type XMLHttpRequest() =
        [<CompileTo("abort")>]
        member x.Abort() = ()

        [<CompileTo("getAllResponseHeaders")>]
        member x.GetAllResponseHeaders() = ""

        [<CompileTo("getResponseHeader")>]
        member x.GetResponseHeader() = ""

        [<CompileTo("open")>]
        member x.Open(methodType: string, url: string, async: bool) = ()

        [<CompileTo("send")>]
        member x.Send(data:string) = ()

        [<CompileTo("setRequestHeader")>]
        member x.SetRequestHeader(key:string, value:string) = ()

        [<CompileTo("onreadystatechange")>]
        member x.OnReadyStateChange
            with set(v:unit->unit) = ()

        [<CompileTo("readyState")>]
        member x.ReadyState = 0

        [<CompileTo("responseText")>]
        member x.ResponseText = ""

        [<CompileTo("responseXML")>]
        member x.ResponseXML  = ""

        [<CompileTo("status")>]
        member x.Status = ""

        [<CompileTo("statusText")>]
        member x.StatusText = ""

    // IE Specific strongly typed classes
    [<Alias("ActiveXObject");JsIgnore(IgnoreNamespace=true)>]
    type XMLHttpRequestIE(name:string) =
        inherit XMLHttpRequest()