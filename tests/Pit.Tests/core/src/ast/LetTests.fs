namespace Pit.Test
open Pit

module LetTests =
    [<Js>]
    let LetDeclare() =
        let x = 1
        Assert.AreEqual "Let Declare 1" x 1

    [<Js>]
    let Let() =
        let f x = x + 1
        Assert.AreEqual "Let Declare 2" (f 1) 2


    [<Js>]
    let Let3() =
        let cylinderVolume radius length =
            // Define a local value pi.
            let pi = 3.14159
            length * pi * radius * radius

        let vol = cylinderVolume 2. 3.
        Assert.AreEqual "Let Declare 3" (vol|>int) 37

    [<Js>]
    let LetRecursive()=
        let rec fib n = if n < 2 then 1 else fib (n - 1) + fib (n - 2)
        Assert.AreEqual "Let Recursive 1" (fib 10) 89

    [<Js>]
    let LetRecursive2() =
        let rec Even x =
            if x = 0 then true
            else Odd (x - 1)
        and Odd x =
            if x = 1 then true
            else Even (x - 1)
        let e = Even 2
        Assert.AreEqual "Let Recursive 2" e true
        let o = Odd 3
        Assert.AreEqual "Let Recursive 2" o true

    [<Js>]
    let LetFunctionValues() =
        let apply1 (transform : int -> int ) y = transform y
        let increment x = x + 1
        let result1 = apply1 increment 100
        Assert.AreEqual "Let Function Values" result1 101

    [<Js>]
    let LetLambdaFunctions()=
        let apply1 (transform : int -> int ) y = transform y
        let result3 = apply1 (fun x -> x + 1) 100
        Assert.AreEqual "Let Lambda Fucntions" result3 101

    [<Js>]
    let LetFunctionComposition()=
        let function1 x = x + 1
        let function2 x = x * 2
        let h = function1 >> function2
        let result5 = h 100
        Assert.AreEqual "Let Function Composition" result5 202

    [<Js>]
    let LetTuple() =
        let i,j,k = (1,2,3)
        Assert.AreEqual "Let Tuple 1" i 1
        Assert.AreEqual "Let Tuple 1" j 2
        Assert.AreEqual "Let Tuple 1" k 3

    [<Js>]
    let LetTuple2() =
        let function2 (a, b) = a + b
        let f = function2(10, 10)
        Assert.AreEqual "Let Tuple 2" f 20

    [<Js>]
    let LetMutable() =
        let mutable x = 0
        Assert.AreEqual "Let Mutable" x 0
        x <- x + 1
        Assert.AreEqual "Let Mutable" x 1

    type t = {
        mutable left : string
    }

    [<Js>]
    let LetMutableSetter() =
        let t = { left = "10"; }
        let x = 20
        t.left <- float(x).ToString()
        Assert.AreEqual "Let Mutable Setter" t.left "20"

    module Test =
        let mutable v = 0

    [<Js>]
    let LetMutableSetterInModule() =
        Test.v <- 10
        Assert.AreEqual "Let Mutable Setter in Module" Test.v 10