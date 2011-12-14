#nowarn "42"
namespace Pit.FSharp.Core
open Pit
open Pit.Javascript
open System.Collections
open System.Collections.Generic

module Operators =

    [<Js>]
    let (|>) x f = f x

    [<Js>]
    let (||>) (x1,x2) f = f x1 x2

    [<Js>]
    let (|||>) (x1,x2,x3) f = f x1 x2 x3

    [<Js>]
    let (<|) f x = f x

    [<Js>]
    let (<||) f (x1,x2) = f x1 x2

    [<Js>]
    let (<|||) f (x1,x2,x3) = f x1 x2 x3

    [<Js>]
    let (>>) f g x = g(f x)

    [<Js>]
    let (<<) f g x = f(g x)

    [<Js>]
    let (^) (x:string) (y:string) = x + y

    [<Js>]
    let (@) list1 list2 = List.append list1 list2

    [<CompiledName("Fst");Js>]
    let fst (a,_) = a

    [<CompiledName("Snd");Js>]
    let snd (_,b) = b

    [<Js>]
    let Ignore _ = ()

    [<Js>]
    let defaultArg x y = match x with None -> y | Some v -> v

    [<Js>]
    let Raise (e: System.Exception) = e //failwith e.Message

    [<Js>]
    let NullArg(argName) = failwith argName

    [<Js>]
    let InvalidOp (opName) = failwith opName

    [<Js>]
    let CreateSequence (x:seq<_>) = x

    [<Js>]
    let ToDouble(x:int) = Global.parseFloat(x)

    [<Js>]
    let ToInt(x:obj) = Global.parseInt x
        (*match x with
        | :? string as s ->
            let js = new JsString(s)
            js.CharCodeAt(0)
        | _             -> Global.parseInt(x)*)

    [<Js>]
    let ToChar(x:obj) =
        match x with
        | :? int as i ->
            JsString.FromCharCode(i)
        | _ -> x.ToString()

    [<Js>]
    let ToString(x:obj) = x

    [<Js>]
    let Max (x:float) (y:float) = if x < y then y else x

    [<Js>]
    let Min (x:float) (y:float) = if x < y then x else y

    [<Js>]
    let Abs(f:float) = Math.abs f

    [<Js>]
    let Acos f = Math.acos f

    [<Js>]
    let Asin f = Math.asin f

    [<Js>]
    let Atan f = Math.atan f

    [<Js>]
    let Atan2 f = Math.atan2 f

    [<Js>]
    let Ceiling f = Math.ceil f

    [<Js>]
    let Exp f = Math.exp f

    [<Js>]
    let Floor f = Math.floor f

    [<Js>]
    let Round f = Math.round f

    [<Js>]
    let Sin f = Math.sin

    [<Js>]
    let Log f = Math.log f

    [<Js>]
    let Sqrt f = Math.sqrt f

    [<Js>]
    let Cos f = Math.cos f

    [<Js>]
    let Tan f = Math.tan f

    [<Js>]
    let Pow x y = Math.pow x y

    (*[<Js>]
    let invalidArg x y = ArgumentException(Message="Argument " + x + y)*)

    type mkSeq<'U> [<Js>](f) =
        interface IEnumerable<'U> with
            [<Js>]
            member x.GetEnumerator() = f()
        interface IEnumerable with
            [<Js>]
            member x.GetEnumerator() = (f() :> IEnumerator)

    type RangeEnumerator [<Js>](start:int, step:int, endto:int) =
        let mutable curr = start - step

        [<Js>]
        member x.Get() = curr
            (*if curr <> (endto+step) then
                curr
            else
                -1*)

        interface IEnumerator<int> with
            [<Js>]
            member this.Current = this.Get()

        interface System.Collections.IEnumerator with
            [<Js>]
            member this.MoveNext() =
                curr <- curr + step
                if (start <= endto) then
                    if curr <= endto then true
                    else false
                else
                    if curr >= endto then true
                    else false

            [<Js>]
            member this.Current = box(this.Get())

            [<Js>]
            member this.Reset() = curr <- start - step

        interface System.IDisposable with
            [<Js>]
            member this.Dispose() = ()

    [<Js>]
    let (..) s e = mkSeq(fun () -> (new RangeEnumerator(s, 1, e)) :> IEnumerator<_>)

    [<Js>]
    let (.. ..) s sp e = mkSeq(fun () -> (new RangeEnumerator(s, sp, e)) :> IEnumerator<_>)

    [<Js>]
    let ( ** ) x y = Math.pow x y

(*[<DefaultAugmentationAttribute(false)>]
[<CompilationRepresentation(CompilationRepresentationFlags.UseNullAsTrueValue)>]
type Option<'T> =
    | None :    'T option
    | Some : Value:'T -> 'T option

    [<Js>]
    member x.IsNone = match x with None -> true | _ -> false

    [<Js>]
    member x.IsSome = match x with Some _ -> true | _ -> false

    //[<Js>]
    //member x.Value = match x with Some x -> x | None -> raise (new System.InvalidOperationException("Option.Value"))

and 'T option = Option<'T>*)

module OptionModule =

    [<Js>]
    let IsSome option = match option with None -> false | Some _ -> true

    [<Js>]
    let IsNone option = match option with  None -> true | Some _ -> false

    [<Js>]
    let Count option = match option with  None -> 0 | Some _ -> 1

    [<Js>]
    let Fold<'T,'State> f (s:'State) (inp: option<'T>) = match inp with None -> s | Some x -> f s x

    [<Js>]
    let FoldBack<'T,'State> f (inp: option<'T>) (s:'State) =  match inp with None -> s | Some x -> f x s

    [<Js>]
    let Exists p inp = match inp with None -> false | Some x -> p x

    [<Js>]
    let ForAll p inp = match inp with None -> true | Some x -> p x

    [<Js>]
    let Iterate f inp = match inp with None -> () | Some x -> f x

    [<Js>]
    let Map f inp = match inp with None -> None | Some x -> Some (f x)

    [<Js>]
    let Bind f inp = match inp with None -> None | Some x -> f x

    [<Js>]
    let ToArray option = match option with  None -> [| |] | Some x -> [| x |]

    [<Js>]
    let ToList option = match option with  None -> [ ] | Some x -> [ x ]

[<Alias("FSharpOption1")>]
[<DefaultAugmentationAttribute(false)>]
[<CompilationRepresentation(CompilationRepresentationFlags.UseNullAsTrueValue)>]
[<CompiledName("FSharpOption")>]
type Option<'T> =
    | None :    'T option
    | Some : Value:'T -> 'T option

    [<Js>]
    member x.IsNone = match x with None -> true | _ -> false

    [<Js>]
    member x.IsSome = match x with Some _ -> true | _ -> false

    [<Js>]
    static member None : 'T option = None

    [<Js>]
    static member Some
        with get(value) : 'T option = Some(value)

//    [<CompilationRepresentation(CompilationRepresentationFlags.Instance)>]
//    [<Js>]
//    member x.Value = match x with Some x -> x | None -> raise (new System.InvalidOperationException("Option.Value"))

and 'T option = Option<'T>

module LanguagePrimitives =

    module IntrinsicFunctions =

        [<Js>]
        let GetArray (arr: JsArray<'T>) (n:int) = arr.[n]

        [<Js>]
        let SetArray (arr: JsArray<'T>) (n:int) (x:'T) = arr.[n] <- x

        [<Js>]
        let GetString (s:string) (i:int) =
            let js = new JsString(s)
            js.CharAt(i)

    type GenericComparer<'T when 'T : comparison>() =
        interface System.Collections.Generic.IComparer<'T> with
            [<Js;JsIgnore(IgnoreTuple=true)>]
            member this.Compare(x,y) =
                if x < y then -1
                else
                    if x=y then 0
                    else 1

    [<Js>]
    let FastGenericComparer() =
        new GenericComparer<_>()

        (*type GenericZeroDynamic<'T>() =
            static let result : 'T =
                let aty = typeof<'T>
                if aty.Equals(typeof<int32>) then unbox<'T>(box 0)
                elif aty.Equals(typeof<float>) then unbox<'T>(box 0.)
                else unbox<'T>(box 0)
            static member Result : 'T = result

        [<Js>]
        let GenericZero<'T> = GenericZeroDynamic<'T>.Result*)

namespace Pit.FSharp.Control
    open Pit

    module LazyExtensions =

        type Lazy<'T> [<Js>](f) =
            let mutable isValueCreated  = false
            let mutable value: 'T       = Unchecked.defaultof<'T>

            [<Js>]
            member x.IsValueCreated
                with get() = isValueCreated
                and set(v) = isValueCreated <- v

            [<Js>]
            member x.Value
                with get() =
                    if not(isValueCreated) then raise (new System.InvalidOperationException("Value not created"))
                    value

            [<Js>]
            member this.Force() =
                if not(isValueCreated) then
                    isValueCreated  <- true
                    value           <- f()
                value

            [<Js>]
            member x.IsDelayed = not(x.IsValueCreated)

            [<Js>]
            member x.IsForced = x.IsValueCreated

            [<CompiledName("Create")>] // give the extension member a 'nice', unmangled compiled name, unique within this module
            [<Js>]
            static member Create(f : unit -> 'T) : Lazy<'T> =
                Lazy<'T>(f)

            [<CompiledName("CreateFromValue")>] // give the extension member a 'nice', unmangled compiled name, unique within this module
            [<Js>]
            static member CreateFromValue(value : 'T) : Lazy<'T> =
                Lazy<'T>.Create(fun () -> value)

        [<Js>]
        let Create f =
            Lazy<_>.Create(f)

        [<Js>]
        let CreateFromValue v =
            Lazy<_>.CreateFromValue(v)

        [<Js>]
        let Force (v:Lazy<_>) =
            v.Force()