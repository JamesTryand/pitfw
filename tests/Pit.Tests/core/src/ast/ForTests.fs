namespace Pit.Test
open Pit

module ForTests =
    [<Js>]
    let ForSimple() =
        let mutable acc = 0
        for i = 1 to 3 do
            acc <- acc + 1
            Assert.AreEqual "For Simple" acc i

//    [<Js>]
//    let ForReverse() =
//        let mutable acc = 3
//        for i = 3 downto 1 do
//            Assert.AreEqual "For Simple" acc i
//            acc <- acc - 1


    [<Js>]
    let ForFunctions() =
        let beginning x y = x - 2*y
        let ending x y = x + 2*y
        let function3 x y =
            let mutable acc = 1
            for i = (beginning x y) to (ending x y) do
                acc <- acc + 1
                Assert.AreEqual "For Functions" acc i

        function3 10 4


    [<Js>]
    let ForInDeclare() =
        let mutable count = 0
        let list1 = [ 1; 5; 100; 450; 788 ]
        for _ in list1 do
           count <- count + 1
        Assert.AreEqual "For In Declare 1" list1.Length count

    [<Js>]
    let ForInDeclare2() =
        let seq1 = seq { for i in 1 .. 10 -> (i, i*i) }
        for (a, asqr) in seq1 do
             Assert.AreEqual "For In Declare 2" ( a * a ) asqr

