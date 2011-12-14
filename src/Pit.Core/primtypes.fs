namespace Pit.Core

open Pit

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
    let fst (a,_) = a

    [<Js>]
    let snd (_,b) = b

    [<Js>]
    let ignore _ = ()

    [<Js>]
    let defaultArg x y = match x with None -> y | Some v -> v

[<CompiledName("FSharpChoice`2")>]
type Choice<'T1,'T2> =
    | Choice1Of2 of 'T1
    | Choice2Of2 of 'T2

[<CompiledName("FSharpChoice`3")>]
type Choice<'T1,'T2,'T3> =
    | Choice1Of3 of 'T1
    | Choice2Of3 of 'T2
    | Choice3Of3 of 'T3

[<CompiledName("FSharpChoice`4")>]
type Choice<'T1,'T2,'T3,'T4> =
    | Choice1Of4 of 'T1
    | Choice2Of4 of 'T2
    | Choice3Of4 of 'T3
    | Choice4Of4 of 'T4

[<CompiledName("FSharpChoice`5")>]
type Choice<'T1,'T2,'T3,'T4,'T5> =
    | Choice1Of5 of 'T1
    | Choice2Of5 of 'T2
    | Choice3Of5 of 'T3
    | Choice4Of5 of 'T4
    | Choice5Of5 of 'T5

[<CompiledName("FSharpChoice`6")>]
type Choice<'T1,'T2,'T3,'T4,'T5,'T6> =
    | Choice1Of6 of 'T1
    | Choice2Of6 of 'T2
    | Choice3Of6 of 'T3
    | Choice4Of6 of 'T4
    | Choice5Of6 of 'T5
    | Choice6Of6 of 'T6

[<CompiledName("FSharpChoice`7")>]
type Choice<'T1,'T2,'T3,'T4,'T5,'T6,'T7> =
    | Choice1Of7 of 'T1
    | Choice2Of7 of 'T2
    | Choice3Of7 of 'T3
    | Choice4Of7 of 'T4
    | Choice5Of7 of 'T5
    | Choice6Of7 of 'T6
    | Choice7Of7 of 'T7

(*[<CompiledName("FSharpOption`1")>]
type Option<'T> =
    | None :       'T option
    | Some : Value:'T -> 'T option

    member x.Value = match x with Some x -> x | None -> raise (new System.InvalidOperationException("Option.Value"))
    member x.IsNone = match x with None -> true | _ -> false
    member x.IsSome = match x with Some _ -> true | _ -> false
    static member None : 'T option = None
    static member Some(x) : 'T option = Some(x)

and 'T option = Option<'T>*)