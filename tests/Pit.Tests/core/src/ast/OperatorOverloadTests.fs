namespace Pit.Test
open Pit

    module OperatorOverloadTests =

        type t = {
            x: int
            y: int
        } with
            [<Js>]
            static member (+) (t1:t, t2:t) =
                { x = t1.x + t2.x; y = t1.y + t2.y }

            [<Js>]
            static member (-) (t1:t, t2:t) =
                { x = t1.x - t2.x; y = t1.y - t2.y }

            [<Js>]
            static member (*) (t1:t, v) =
                { x = t1.x * v; y = t1.y * v }

        [<Js>]
        let OpOverload1() =
            let t1 = {x=10; y=10;}
            let t2 = {x=20; y=20;}
            let t3 = t1 + t2
            let t4 = t1 - t2
            let t5 = t1 * 10
            Assert.AreEqual "Op Overload 1" t3.x 30
            Assert.AreEqual "Op Overload 2" t4.x -10
            Assert.AreEqual "Op Overload 3" t5.x 100

        type Expression =
        | Add of Expression * Expression
        | Constant of int
        with
            [<Js>]
            static member (+) (x, y) = Add(x, y)

        [<Js>]
        let OpOverload2() =
            let a = Constant(10)
            let b = Constant(20)
            let c = a + b
            let res =
                match c with
                | Add(_,_) -> true
                | _        -> false
            Assert.AreEqual "Union case overload" true res
