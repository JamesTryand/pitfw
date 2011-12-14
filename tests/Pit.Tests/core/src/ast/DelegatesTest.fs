namespace Pit.Test
open Pit

module DelegateTests =

    type Delegate1 = delegate of (int * int) -> int
    type Delegate2 = delegate of int * int -> int

    [<Js>]
    let Declare1() =
        let f1 = new Delegate1(fun (a,b) -> a + b)
        let r = f1.Invoke((1,2))
        Assert.AreEqual "Delegate Declare1 Test" r 3

    [<Js>]
    let Declare2() =
        let f1 = new Delegate2(fun a b -> a + b)
        let r = f1.Invoke(1,2)
        Assert.AreEqual "Delegate Declare2 Test" r 3