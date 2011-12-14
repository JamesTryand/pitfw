namespace Pit.Test
open Pit
open Pit.Javascript

    module SetTests =

        [<Js>]
        let Create() =
            let s = Set.empty.Add(1).Add(2)
            let c = Set.count s
            Assert.AreEqual "Set Create" 2 c

        [<Js>]
        let Add() =
            let s =
                Set.empty
                |> Set.add 1
                |> Set.add 2
            let c = Set.count s
            Assert.AreEqual "Set Add" 2 c

        [<Js>]
        let AddOp() =
            let s1 = Set.ofArray([|1;2;3|])
            let s2 = Set.ofArray([|4;5;|])
            let add = s1 + s2
            let c   = Set.count add
            Assert.AreEqual "Set AddOp" 5 c

        [<Js>]
        let Contains() =
            let s = Set.empty.Add(1).Add(2)
            let f =
                s
                |> Set.contains 2
            Assert.AreEqual "Set Contains" true f

        [<Js>]
        let Exists() =
            let s = Set.ofArray [|1;2;3;4;5;|]
            let f = s |> Set.contains 3
            Assert.AreEqual "Set Exists" true f

        [<Js>]
        let Filter() =
            let s = Set.ofList [1..10] |> Set.filter(fun e -> e%2=0)
            let c = Set.count s
            Assert.AreEqual "Set Filter" 5 c

        [<Js>]
        let Difference() =
            let s1 = Set.ofArray [|1;2;3|]
            let s2 = Set.ofArray [|3;4;5;|]
            let diff = Set.difference s1 s2
            let diffc = Set.count diff
            Assert.AreEqual "Set Difference" 2 diffc

        [<Js>]
        let DifferenceOp() =
            let s1 = Set.ofArray [|1;2;3|]
            let s2 = Set.ofArray [|3;4;5;|]
            let diff = s1 - s2
            let diffc = Set.count diff
            Assert.AreEqual "Set Difference" 2 diffc

        [<Js>]
        let Fold() =
            let sumSet set = Set.fold(fun acc elem -> acc + elem) 0 set
            let s = Set.ofArray [|1;2;3;|]
            let res = sumSet s
            Assert.AreEqual "Set Fold" 6 res

        [<Js>]
        let FoldBack() =
            let subSetBack set = Set.foldBack (fun elem acc -> elem - acc) set 0
            let s = Set.ofArray [|1;2;3|]
            let res = subSetBack s
            Assert.AreEqual "Set Foldback" 2 res

        [<Js>]
        let ForAll() =
            let allPositive = Set.forall(fun el -> el >= 0)
            let s = Set.ofArray [|1;2;3|]
            let f = allPositive s
            Assert.AreEqual "Set ForAll" true f

        [<Js>]
        let Intersect() =
            let set1 = Set.ofList [ 1..3 ]
            let set2 = Set.ofList [ 2..6 ]
            let s    = Set.intersect set1 set2
            let c    = Set.count s
            Assert.AreEqual "Set Intersect" 2 c

        [<Js>]
        let IntersectMany() =
            let sets = [| Set.ofArray[|1..9|]; Set.ofArray[|5..8|] |]
            let setres = Set.intersectMany sets
            let c = Set.count setres
            Assert.AreEqual "Set Intersect Many" 4 c

        [<Js>]
        let IsEmpty() =
            let f = Set.empty |> Set.isEmpty
            Assert.AreEqual "Set IsEmpty" true f

        [<Js>]
        let IsProperSubset() =
            let s1 = Set.ofList [1..6]
            let s2 = Set.ofList [1..4]
            let f  = Set.isProperSubset s2 s1
            let f2 = Set.isSubset s2 s1
            Assert.AreEqual "Set IsProperSubset" true f
            Assert.AreEqual "Set IsSubset" true f2

        [<Js>]
        let IsProperSuperset() =
            let s1 = Set.ofList [1..4]
            let s2 = Set.ofList [1..6]
            let f  = Set.isProperSuperset s2 s1
            let f2 = Set.isSuperset s2 s1
            Assert.AreEqual "Set IsProperSuperset" true f
            Assert.AreEqual "Set IsSuperset" true f2

        [<Js>]
        let Iterate() =
            let s = Set.empty.Add(1).Add(2)
            let i = ref 1
            s
            |> Set.iter(fun e ->
                Assert.AreEqual "Set Iterate" e i
                i := 2
            )

        [<Js>]
        let Map() =
            let s =
                Set.empty.Add(1).Add(2)
                |> Set.map(fun e -> e + 2)
            let i = ref 3
            s
            |> Set.iter(fun e ->
                Assert.AreEqual "Set Iterate" e i
                i := 4
            )

        [<Js>]
        let MaxElement() =
            let s = Set.ofList [ 1..10 ]
            let m = Set.maxElement s
            Assert.AreEqual "Set MaxElement" 10 m

        [<Js>]
        let MinElement() =
            let s = Set.ofList [ 5..10 ]
            let m = Set.minElement s
            Assert.AreEqual "Set MinElement" 5 m

        [<Js>]
        let OfArray() =
            let s = Set.ofArray [|1;2;3|]
            let c = Set.count s
            Assert.AreEqual "Set OfArray" 3 c

        [<Js>]
        let OfList() =
            let s = Set.ofList [1..3]
            let c = Set.count s
            Assert.AreEqual "Set OfList" 3 c

        [<Js>]
        let OfSeq() =
            let s = Set.ofSeq (seq { 1..3 })
            let c = Set.count s
            Assert.AreEqual "Set OfSeq" 3 c

        [<Js>]
        let Partition() =
            let s = Set.ofArray [|-2;-1;1;2|]
            let n,p = s |> Set.partition(fun t -> t<0)
            let nc = Set.count n
            Assert.AreEqual "Set Partition" 2 nc

        [<Js>]
        let Remove() =
            let s = Set.empty.Add(1).Add(2)
            let r = s |> Set.remove 2
            let c = r |> Set.count
            Assert.AreEqual "Set Remove" 1 c

        [<Js>]
        let Singleton() =
            let s = Set.singleton(1)
            let c = Set.count s
            Assert.AreEqual "Set Singleton" 1 c

        [<Js>]
        let Union() =
            let s1 = Set.ofList [2..2..8]
            let s2 = Set.ofList [1..2..9]
            let s3 = Set.union s1 s2
            let c = Set.count s3
            Assert.AreEqual "Set Union" 9 c

        [<Js>]
        let UnionMany() =
            let sets = [| Set.ofArray[|1..9|]; Set.ofArray[|5..8|] |]
            let setres = Set.unionMany sets
            let c = Set.count setres
            Assert.AreEqual "Set Union Many" 9 c