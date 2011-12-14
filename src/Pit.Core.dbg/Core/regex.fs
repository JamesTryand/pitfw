namespace Pit.Javascript

open System
open System.Windows.Browser

    type RegExp(patterns:string, modifier:string) =
        let mutable regexSO : ScriptObject = null

        member internal this.RegExSO
            with get() =
                if regexSO = null then
                    regexSO <- HtmlPage.Window.CreateInstance("RegExp",  patterns, modifier)
                regexSO

        member this.Exec(exp:string) =
            match this.RegExSO.Invoke("exec", exp) with
            | x when x <> null ->
                let s = x :?> ScriptObject
                new JsArray<string>(s)
            | _ -> new JsArray<string>([||])

        member this.Test(exp:string) = this.RegExSO.Invoke("test" , exp) :?> bool

        static member Compile(regexp: RegExp, modifier: string) =
            regexp.RegExSO.Invoke("compile" , modifier) |> ignore

        (*member this.Global
            with get() = this.RegEx.GetProperty("global") :?> bool

        member this.IgnoreCase
            with get() = this.RegEx.GetProperty("ignoreCase") :?> bool

        member this.LastIndex
            with get() = this.RegEx.GetProperty("lastIndex") :?> int

        member this.MultiLine
            with get() = this.RegEx.GetProperty("multiline") :?> bool

        member this.Source
            with get() = this.RegEx.GetProperty("source")*)