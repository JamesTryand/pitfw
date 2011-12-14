namespace Pit.Javascript
open Pit
open System.Collections.Generic
open System.Windows.Browser

    [<AllowNullLiteral>]
    type JsArray<'a> =
        val arraySO: ScriptObject

        new(array: 'a[]) as x =
            { arraySO = HtmlPage.Window.CreateInstance("Array", null)}
                then
                    let mutable i = 0
                    for a in array do
                        x.arraySO.SetProperty(i, a)
                        i <- i + 1

        internal new(array: ScriptObject) = { arraySO = array }

        member x.Length = x.arraySO.GetProperty("length") |> toInt

        member x.Item
            with get(i:int) = x.arraySO.GetProperty(i) :?> 'a
            and set (i:int) (v:'a) = x.arraySO.SetProperty(i, v)
                (*let aty = typeof<'a>
                if aty.Equals(typeof<int>) then v :?> float
                elif aty.Equals(typeof<double>) then v :?> float
                else v :?> 'a*)

        member x.JoinAll() =
            x.arraySO.Invoke("join", null) :?> string

        member x.JoinAll(separator: string) =
            x.arraySO.Invoke("join", [| box(separator) |]) :?> string

        member x.Pop() =
            x.arraySO.Invoke("pop", null)

        member x.Push(item: 'a) =
            x.arraySO.Invoke("push", [| box(item) |]) |> toInt

        member x.Push(items: 'a[]) =
            x.arraySO.Invoke("push", [| box(items) |]) |> toInt

        member x.Reverse() =
            let rev = x.arraySO.Invoke("reverse", null) :?> ScriptObject
            new JsArray<'a>(rev)

        member x.Shift() = x.arraySO.Invoke("shift", null)

        member x.Slice(start:int) =
            let sliced = x.arraySO.Invoke("slice", [| box(start) |]) :?> ScriptObject
            new JsArray<'a>(sliced)

        member x.Slice(start:int, length: int) =
            let sliced = x.arraySO.Invoke("slice", [| box(start); box(length) |]) :?> ScriptObject
            new JsArray<'a>(sliced)

        member x.Sort() =
            let sort = x.arraySO.Invoke("sort", null) :?> ScriptObject
            new JsArray<'a>(sort)

        member x.Splice(start:int, length:int, item: 'a) =
            x.arraySO.Invoke("splice", [| box(start); box(length); box(item) |]) |> ignore

        member x.Unshift(item: 'a) =
            x.arraySO.Invoke("unshift", [| box(item) |]) |> toInt

        override x.ToString() =
            x.arraySO.Invoke("toString", null) :?> string