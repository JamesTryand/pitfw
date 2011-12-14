namespace Pit.Javascript

open Pit

[<JsIgnore(IgnoreNamespace=true,IgnoreTypeName=true)>]
module Global =

    [<CompileTo("undefined")>]
    let undefined = "undefined"

    [<CompileTo("isNan")>]
    let isNan(v : float) = true

    [<CompileTo("isFinite")>]
    let isFinite(v : float) = true

    [<CompileTo("escape")>]
    let escape(uri : string) = ""

    [<CompileTo("unescape")>]
    let unescape(uri : string) = ""

    [<CompileTo("eval")>]
    let eval(script : string) = ()

    [<CompileTo("parseFloat")>]
    let parseFloat(v : obj) = 0.

    [<CompileTo("parseInt")>]
    let parseInt(v : obj) = 0

    [<CompileTo("Number.Nan")>]
    let Nan = 0.

    [<CompileTo("encodeURI")>]
    let encodeUri(uri : string) = ""

    [<CompileTo("decodeURI")>]
    let decodeUri(uri : string) = ""