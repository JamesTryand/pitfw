namespace Pit.Dom.Tests
open Pit
open System.Windows.Browser

[<JsIgnore(IgnoreNamespace=true)>]
module Assert =

    let assertType = HtmlPage.Window.GetProperty("Assert") :?> ScriptObject
    let AreEqual (fnName: string) (a1: obj) (a2:obj) =
        assertType.Invoke("AreEqual", [| box(fnName); a1; a2 |]) |> ignore

    let AreNotEqual (fnName: string) (a1: obj) (a2:obj) =
        assertType.Invoke("AreNotEqual", [| box(fnName); a1; a2 |]) |> ignore

    let IsNull (fnName: string) (a1:obj) =
        assertType.Invoke("IsNull", [| box(fnName); a1 |]) |> ignore

    let IsNotNull (fnName: string) (a1:obj) =
        assertType.Invoke("IsNotNull", [| box(fnName); a1 |]) |> ignore