namespace Pit.Test
open Pit

module TupleTests =

    [<Js>]
    let TupleDecalre() =
        let a,b,c = (1,2,3)
        Assert.AreEqual "Tuple Decalre" a 1


    [<Js>]
    let TupleFst() =
        let r = fst (1,2)
        Assert.AreEqual "Tuple First(fst)" r 1

    [<Js>]
    let TupleSnd() =
        let r = snd (1,2)
        Assert.AreEqual "Tuple Second(fst)" r 2

    [<Js>]
    let MixedTuple() =
        let mixedTuple = ( 1, "two", 3.3 )
        let _,r,_ = mixedTuple
        Assert.AreEqual "Mixed Tuple" r "two"

    [<Js>]
    let TupleFunctions() =
        let avg (a,b) = (a + b)/2.0
        let r = avg(5.0, 5.0)
        Assert.AreEqual "Functions with Tuple arguements" r 5.0

    [<Js>]
    let TupleFunctions2() =
        let scalarMultiply (s : float) (a, b) = (a * s, b * s)
        let r = fst (scalarMultiply(5.0) (1.0,2.0))
        Assert.AreEqual "Functions with Tuple arguements 2" r 5.0

    [<Js>]
    let t1 x = x, (fun x -> x + 1)

    [<Js>]
    let TupleFunctions3() =
        let r = ((snd (t1 3)) 3)
        Assert.AreEqual "Tuple with Functions arguements 2" r 4


    [<Js>]
    let TupleArrays() =
        let a =  ([|1;2;3|], [|4;5;6|])
        Assert.AreEqual "Tuple of Arrays" ((fst a).[0]) 1


    type tRec = {
                p1 : int
                p2 : int
             }

    [<Js>]
    let TupleRecords() =
        let j = { p1 = 5 ; p2 = 5 }
        let k = { p1 = 5 ; p2 = 5 }
        let tupRec (a:tRec, b:tRec) = a.p1 + a.p2 + b.p1 + b.p2
        let r = tupRec (j,k)
        Assert.AreEqual "Tuple with records" r 20

    type TupleIgnore() =

        [<Js>]
        [<JsIgnore(IgnoreTuple=true)>]
        member this.CallTuple2(s1:int, s2: int) =
            s1+s2

    [<Js>]
    let TupleCallAsNormalFunction() =
        let a = new TupleIgnore()
        let s = a.CallTuple2(1,1)
        Assert.AreEqual "Tuple Call as Normal Function IgnoreTuple=true" 2 s