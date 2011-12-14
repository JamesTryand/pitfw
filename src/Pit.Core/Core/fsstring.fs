namespace Pit.Text
open Pit
open Pit.Javascript

    type StringBuilder[<Js>]() =
        let array = new JsArray<obj>([||])

        [<Js>]
        member this.Append(s:obj) =
            array.Push(s)

        [<Js>]
        override this.ToString() =
            array.JoinAll("")

namespace Pit.FSharp.Core
open Pit
open Pit.Javascript
    module StringModule =

        [<Js>]
        let inline emptyIfNull str =
            if str = null then "" else str

        [<Js;CompiledName("Concat")>]
        let concat sep (strings : seq<string>) =
            let strArray = Pit.FSharp.Collections.SeqModule.ToArray strings
            let js = new JsArray<string>(strArray)
            js.JoinAll()

        [<Js;CompiledName("Iterate")>]
        let iter (f : (char -> unit)) (str:string) =
            let str = emptyIfNull str
            for i = 0 to str.Length - 1 do
                f str.[i]

        [<Js;CompiledName("IterateIndexed")>]
        let iteri f (str:string) =
            let str = emptyIfNull str
            for i = 0 to str.Length - 1 do
                f i str.[i]

        [<Js;CompiledName("Map")>]
        let map (f: char -> char) (str:string) =
            let str = emptyIfNull str
            let res = new Pit.Text.StringBuilder()
            str |> iter (fun c -> res.Append(f c) |> ignore);
            res.ToString()

        [<Js;CompiledName("MapIndexed")>]
        let mapi (f: int -> char -> char) (str:string) =
            let str = emptyIfNull str
            let res = new Pit.Text.StringBuilder()
            str |> iteri (fun i c -> res.Append(f i c) |> ignore);
            res.ToString()

        [<Js;CompiledName("Collect")>]
        let collect (f: char -> string) (str:string) =
            let str = emptyIfNull str
            let res = new Pit.Text.StringBuilder()
            str |> iter (fun c -> res.Append(f c) |> ignore);
            res.ToString()

        [<Js;CompiledName("Initialize")>]
        let init (count:int) (initializer: int-> string) =
            if count < 0 then  raise(new System.InvalidOperationException("Input must be non-negative"))
            let res = new Pit.Text.StringBuilder()
            for i = 0 to count - 1 do
               res.Append(initializer i) |> ignore;
            res.ToString()

        [<Js;CompiledName("Replicate")>]
        let replicate (count:int) (str:string) =
            if count < 0 then  raise(new System.InvalidOperationException("Input must be non-negative"))
            let str = emptyIfNull str
            let res = new Pit.Text.StringBuilder()
            for i = 0 to count - 1 do
               res.Append(str) |> ignore;
            res.ToString()

        [<Js;CompiledName("ForAll")>]
        let forall f (str:string) =
            let str = emptyIfNull str
            let rec check i = (i >= str.Length) || (f str.[i] && check (i+1))
            check 0

        [<Js;CompiledName("Exists")>]
        let exists f (str:string) =
            let str = emptyIfNull str
            let rec check i = (i < str.Length) && (f str.[i] || check (i+1))
            check 0

        [<Js;CompiledName("Length")>]
        let length (str:string) =
            let str = emptyIfNull str
            str.Length