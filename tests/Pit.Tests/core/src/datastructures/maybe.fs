namespace Pit
open Pit

module Monads =

    type Maybe<'a> = option<'a>

    [<Js>]
    let succeed x = Some(x)

    [<Js>]
    let bind p rest =
        match p with
            | None      -> None
            | Some r    -> rest r

    [<Js>]
    let delay f = f()

    type MaybeBuilder() =
        [<Js>]
        member b.Return(x)  = succeed x
        [<Js>]
        member b.Bind(p, rest) = bind p rest
        [<Js>]
        member b.Delay(f)   = delay f
        [<Js>]
        member b.Let(p,rest) : Maybe<'a> = rest p
        [<Js>]
        member b.ReturnFrom(x) = x

    [<Js>]
    let maybe = MaybeBuilder()

module MonadsTest =
    open Monads

    [<Js>]
    let failIfBig n = maybe {if n > 1000 then return! None else return n}

    [<Js>]
    let safesum (inp1,inp2) =
        maybe { let! n1 = failIfBig inp1
                let! n2 = failIfBig inp2
                let sum = n1 + n2
                return sum }

    [<Js>]
    let result1() = safesum (999,1000)

    [<Js>]
    let result2() = safesum (1000,1001)