namespace Pit.Javascript

open System
open System.Windows.Browser
open Pit

    type JsString =
        val stringSO: ScriptObject
        val private str: string

        new(str: string) = { stringSO = HtmlPage.Window.CreateInstance("String", str);str=str }

        member x.Length = x.stringSO.GetProperty("length") |> toInt

        member x.CharAt(index:int) = x.stringSO.Invoke("charAt", index) :?> string

        member x.CharCodeAt(index:int) = x.stringSO.Invoke("charCodeAt") |> toInt

        member x.Concat(other: JsString) =
            let str = x.stringSO.Invoke("concat", [| box(other.str) |]) :?> string
            JsString(str)

        member x.IndexOf(searchString:string) = x.stringSO.Invoke("indexOf", [| box(searchString); |]) |> toInt

        member x.IndexOf(searchString:string, start:int) = x.stringSO.Invoke("indexOf", [| box(searchString); box(start) |]) |> toInt

        member x.LastIndexOf(searchString:string) = x.stringSO.Invoke("lastIndexOf", [|box(searchString)|]) |> toInt

        member x.LastIndexOf(searchString:string, start:int) = x.stringSO.Invoke("lastIndexOf", [|box(searchString); box(start)|]) |> toInt

        member x.Match(regex: RegExp) =
            let arr = x.stringSO.Invoke("match", [| box(regex.RegExSO) |]) :?> ScriptObject
            new JsArray<string>(arr)

        member x.Replace(from: string, tostr:string) = x.stringSO.Invoke("replace", [| box(from); box(tostr) |]) :?> string

        member x.Replace(regex: RegExp, tostr:string) = x.stringSO.Invoke("replace", [| box(regex.RegExSO); box(tostr) |]) :?> string

        member x.Search(searchString: string) = x.stringSO.Invoke("search", [| box(searchString)|]) |> toInt

        member x.Search(regex: RegExp) = x.stringSO.Invoke("search", [|box(regex.RegExSO)|]) |> toInt

        member x.Slice(startIndex:int) = x.stringSO.Invoke("slice", [| box(startIndex) |]) :?> string

        member x.Slice(startIndex: int, endIndex: int) = x.stringSO.Invoke("slice", [| box(startIndex); box(endIndex) |]) :?> string

        member x.Split() =
            let arr = x.stringSO.Invoke("split", null) :?> ScriptObject
            new JsArray<string>(arr)

        member x.Split(separator: string) =
            let arr = x.stringSO.Invoke("split", [| box(separator) |]) :?> ScriptObject
            new JsArray<string>(arr)

        member x.Split(separator: string, limit:int) =
            let arr = x.stringSO.Invoke("split", [| box(separator); box(limit) |]) :?> ScriptObject
            new JsArray<string>(arr)

        member x.Substring(start:int) = x.stringSO.Invoke("substr", [| box(start) |]) :?> string

        member x.Substring(start:int, length:int) = x.stringSO.Invoke("substr", [| box(start); box(length) |]) :?> string

        member x.ToLower() = x.stringSO.Invoke("toLowerCase", null) :?> string

        member x.ToUpper() = x.stringSO.Invoke("toUpperCase", null) :?> string

        static member FromCharCode(code:int) =
            let s = HtmlPage.Window.GetProperty("String") :?> ScriptObject
            s.Invoke("fromCharCode", [| box(code |> float) |]) :?> string