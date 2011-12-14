namespace Pit.Test
open Pit
open Pit.Javascript

module SeqTest =

    [<Js>]
    let Declare() =
        let s = seq{ 1..10 }
        let i = s |> Seq.nth 5
        Assert.AreEqual "Seq Declare" 6 i

    [<Js>]
    let Initialize() =
        let s = Seq.init 5 (fun i -> i)
        let i = s |> Seq.nth 2
        Assert.AreEqual "Seq Init" 2 i

    [<Js>]
    let InitializeInfinite() =
        let s =
            Seq.initInfinite (fun i->i + 1)
            |> Seq.take 10
        let len = s |> Seq.length
        Assert.AreEqual "Seq Infinite" 10 len

    [<Js>]
    let OfArray() =
        let array = [|1;2;3;|]
        let s = Seq.ofArray array
        let i = s |> Seq.nth 0
        Assert.AreEqual "Seq OfArray" 1 i

    [<Js>]
    let Iterate() =
        let s = seq{1..2}
        let idx = ref 1
        s
        |> Seq.iter(fun i ->
            Assert.AreEqual "Seq Iterate" i !idx
            idx := !idx + 1
        )

    [<Js>]
    let IterateIndexed() =
        let s = seq{1..2}
        let r = ref 1
        s
        |> Seq.iteri(fun idx i ->
            Assert.AreEqual "Seq IterateIndexed" i !r
            r := !r + 1
        )

    [<Js>]
    let Exists() =
        let s = seq{1..3}
        let r = s |> Seq.exists(fun i -> i = 2)
        Assert.AreEqual "Seq Exists" true r

    [<Js>]
    let ForAll() =
        let allPositive = Seq.forall (fun elem -> elem >= 0)
        let r = (allPositive [|0;1;2;3|])
        Assert.AreEqual "Seq ForAll" true r

    [<Js>]
    let Iterate2() =
        let s1 = seq{1..2}
        let s2 = seq{1..2}
        let r = ref 1
        Seq.iter2(fun i1 i2 ->
            Assert.AreEqual "Seq Iterate2" i1 !r
            Assert.AreEqual "Seq Iterate2" i2 !r
            r := !r + 1
        ) s1 s2

    [<Js>]
    let Filter() =
        let s = seq{1..10}
        let r = s |> Seq.filter(fun i -> i < 5)
        Assert.AreEqual "Seq Filter" 4 (r|>Seq.length)

    [<Js>]
    let Map() =
        let s = seq{1..5}
        let r = s |> Seq.map(fun i -> i + i)
        Assert.AreEqual "Seq Map" 10 (r|>Seq.nth 4)

    [<Js>]
    let MapIndexed() =
        let s = seq{1..5}
        let r = s |> Seq.mapi(fun idx i -> idx + i)
        Assert.AreEqual "Seq MapIndexed" 9 (r|>Seq.nth 4)

    [<Js>]
    let Map2() =
        let s1 = seq{1..5}
        let s2 = seq{6..10}
        let r = Seq.map2 (fun i1 i2 -> i1+i2) s1 s2
        Assert.AreEqual "Seq Map2" 11 (r|>Seq.nth 2)

    [<Js>]
    let Choose() =
        let numbers = seq {1..10}
        let evens = Seq.choose(fun x ->
                                match x with
                                | x when x%2=0 -> Some(x)
                                | _ -> None ) numbers
        Assert.AreEqual "Seq Choose" 4 (evens|>Seq.nth 1)

    [<Js>]
    let Zip() =
        let s1 = seq { 1..2 }
        let s2 = seq { 3..4 }
        let r = Seq.zip s1 s2
        let i1,i2 = r |> Seq.nth 0
        Assert.AreEqual "Seq Zip" 1 i1
        Assert.AreEqual "Seq Zip" 3 i2

    [<Js>]
    let Zip3() =
        let s1 = seq { 1..2 }
        let s2 = seq { 3..4 }
        let s3 = seq { 5..6 }
        let r = Seq.zip3 s1 s2 s3
        let i1,i2,i3 = r |> Seq.nth 0
        Assert.AreEqual "Seq Zip" 1 i1
        Assert.AreEqual "Seq Zip" 3 i2
        Assert.AreEqual "Seq Zip" 5 i3

    [<Js>]
    let TryPick() =
        let values = [| ("a", 1); ("b", 2); ("c", 3) |]
        let resultPick = Seq.tryPick (fun elem ->
                            match elem with
                            | (value, 2) -> Some value
                            | _ -> None) values
        match resultPick with
        | Some(r) -> Assert.AreEqual "Seq TryPick" "b" r
        | None    -> failwith "Seq TryPick Failure"

    [<Js>]
    let Pick() =
        let values = [| ("a", 1); ("b", 2); ("c", 3) |]
        let resultPick = Seq.pick (fun elem ->
                            match elem with
                            | (value, 2) -> Some value
                            | _ -> None) values
        Assert.AreEqual "Seq Pick" "b" resultPick

    [<Js>]
    let TryFind() =
        let s = seq{1..5}
        match (s|>Seq.tryFind(fun i -> i = 2)) with
        | Some(t) -> Assert.AreEqual "Seq TryFind" 2 t
        | None    -> failwith "Seq TryFind Failure"

    [<Js>]
    let Find() =
        let s = seq{1..5}
        let r = s |> Seq.find(fun i -> i = 3)
        Assert.AreEqual "Seq Find" 3 r

    [<Js>]
    let Concat() =
         let s = Seq.concat [| [| 1; 2; 3 |]; [| 4; 5; 6 |]; [|7; 8; 9|] |]
         Assert.AreEqual "Seq Concat" 9 (s|>Seq.length)

    [<Js>]
    let Length() =
        let s = seq{1..5}
        Assert.AreEqual "Seq Length" 5 (s|>Seq.length)

    [<Js>]
    let Fold() =
        let sumSeq sequence1 = Seq.fold (fun acc elem -> acc + elem) 0 sequence1
        let sum =
            Seq.init 10 (fun index -> index * index)
            |> sumSeq
        Assert.AreEqual "Seq Sum" 285 sum

    [<Js>]
    let Reduce() =
        let names = [| "A"; "man"; "landed"; "on"; "the"; "moon" |]
        let sentence = names |> Seq.reduce (fun acc item -> acc + " " + item)
        Assert.AreEqual "Seq Reduce" sentence "A man landed on the moon"

    [<Js>]
    let Append() =
        let s1 = seq {1..5}
        let s2 = seq {6..10}
        let r = Seq.append s1 s2
        Assert.AreEqual "Seq Append" 10 (r|>Seq.length)

    [<Js>]
    let Collect() =
        let k = Seq.collect (fun elem -> [| 0 .. elem |]) [| 1; 5; 10|]
        Assert.AreEqual "Seq Collect" 19 (k|>Seq.length)

    [<Js>]
    let CompareWith() =
        let s1 = seq { 1..10 }
        let s2 = seq { 10..-1..1 }
        let compareSequences =
            Seq.compareWith
                (fun elem1 elem2 ->
                    if elem1 > elem2 then 1
                    elif elem1 < elem2 then -1
                    else 0)
        let compareResult1 = compareSequences s1 s2
        let res =
            match compareResult1 with
            | 1  -> "Sequence1 is greater than sequence2."
            | -1 -> "Sequence1 is less than sequence2."
            | 0  -> "Sequence1 is equal to sequence2."
            | _  -> failwith("Invalid comparison result.")
        Assert.AreEqual "Seq CompareWith" "Sequence1 is less than sequence2." res

    [<Js>]
    let Singleton() =
        let res1 = Seq.singleton 10
        let i1 = res1 |> Seq.nth 0
        let i2 = res1 |> Seq.nth 0
        Assert.AreEqual "Seq Singleton" i1 i2

    [<Js>]
    let Truncate() =
        let mySeq = seq { for i in 1 .. 10 -> i*i }
        let truncatedSeq = Seq.truncate 5 mySeq
        Assert.AreEqual "Seq Truncate" 1 (truncatedSeq|>Seq.nth 0)

    [<Js>]
    let Take() =
        let s = seq {1..10}
        let r = s |> Seq.take 5
        Assert.AreEqual "Seq Take" 5 (r|>Seq.nth 4)

    [<Js>]
    let TakeWhile() =
        let mySeq = seq { for i in 1 .. 10 -> i*i }
        let res = Seq.takeWhile (fun elem -> elem < 10) mySeq
        Assert.AreEqual "Seq TakeWhile" 9 (res|>Seq.nth 2)

    [<Js>]
    let Skip() =
        let s = seq {1..10}
        let r = s |> Seq.skip 4
        Assert.AreEqual "Seq Skip" 7 (r|>Seq.nth 2)

    [<Js>]
    let SkipWhile() =
        let mySeq = seq { for i in 1 .. 10 -> i*i }
        let res = mySeq |> Seq.skipWhile (fun el-> el<10)
        Assert.AreEqual "Seq SkipWhile" 36 (res|>Seq.nth 2)

    [<Js>]
    let PairWise() =
        let s = Seq.pairwise (seq { for i in 1 .. 10 -> i*i })
        let i1,i2 = s|>Seq.nth 2
        Assert.AreEqual "Seq PairWise" i1 9
        Assert.AreEqual "Seq PairWise" i2 16

    [<Js>]
    let Scan() =
        let sumSeq sequence1 = Seq.scan (fun acc elem -> acc + elem) 0 sequence1
        let sum =
            Seq.init 10 (fun index -> index * index)
            |> sumSeq
        Assert.AreEqual "Seq Scan" 14 (sum|>Seq.nth 4)

    [<Js>]
    let FindIndex() =
        let s = seq { 1..10 }
        let f = s |> Seq.findIndex(fun i -> i = 5)
        Assert.AreEqual "Seq FindIndex" 4 f

    [<Js>]
    let TryFindIndex() =
        let s = seq { 1..10 }
        match s |> Seq.tryFindIndex(fun i -> i = 5) with
        | Some(t) -> Assert.AreEqual "Seq TryFindIndex" 4 t
        | None    -> failwith "Seq TryFindIndex Failure"

    [<Js>]
    let ToList() =
        let s = seq { 1..10 }
        let r = s |> Seq.toList
        Assert.AreEqual "Seq ToList" 5 (List.nth r 4)

    [<Js>]
    let OfList() =
        let l = [1..10]
        let s = l |> Seq.ofList
        Assert.AreEqual "Seq OfList" 2 (s|>Seq.nth 1)

    [<Js>]
    let ToArray() =
        let s = seq { 1..10 }
        let a = s |> Seq.toArray
        Assert.AreEqual "Seq ToArray" 2 a.[1]

    [<Js>]
    let Sum() =
        let s = seq { 1..10 }
        let r = s |> Seq.sum
        Assert.AreEqual "Seq Sum" 55 r

    [<Js>]
    let SumBy() =
        let s = seq { 1..10 }
        let r = s |> Seq.sumBy(fun x -> x * x)
        Assert.AreEqual "Seq SumBy" 385 r

    [<Js>]
    let Average() =
        let s = seq { 1.0..10.0 }
        let r = s |> Seq.average
        Assert.AreEqual "Seq Average" 5.5 r

    [<Js>]
    let AverageBy() =
        let avg2 = Seq.averageBy(fun el -> float(el)) (seq { 1..10 })
        Assert.AreEqual "Seq Average" 5.5 avg2

    [<Js>]
    let Min() =
        let s = seq { 1..10 }
        let r = s |> Seq.min
        Assert.AreEqual "Seq Min" 1 r

    [<Js>]
    let MinBy() =
        let s = seq { -10..10 }
        let r = s |> Seq.minBy(fun x -> x * x - 1)
        Assert.AreEqual "Seq MinBy" 0 r

    [<Js>]
    let Max() =
        let s = seq{ 1..10 }
        let r = s |> Seq.max
        Assert.AreEqual "Seq Max" 10 r

    [<Js>]
    let MaxBy() =
        let s = seq{ -10..10 }
        //let s1 = s |> Seq.map(fun x -> x * x - 1) |> Seq.toArray
        let r = s |> Seq.maxBy (fun x-> (x * x) - 1)
        Assert.AreEqual "Seq MaxBy" -10 r

    [<Js>]
    let ForAll2() =
        let allEqual = Seq.forall2 (fun elem1 elem2 -> elem1 = elem2)
        let r1 = allEqual [|1;2|] [|1;2|]
        let r2 = allEqual [|2;1|] [|1;2|]
        Assert.AreEqual "Seq ForAll2" true r1
        Assert.AreEqual "Seq ForAll2" false r2

    [<Js>]
    let Exists2() =
        let s1 = seq{1..5}
        let s2 = seq{5..-1..1}
        let r = Seq.exists2(fun i1 i2 -> i1 = i2) s1 s2
        Assert.AreEqual "Seq Exists" true r

    [<Js>]
    let Head() =
        let s1 = seq{1..5}
        let r = s1 |> Seq.head
        Assert.AreEqual "Seq Head" 1 r

    [<Js>]
    let GroupBy() =
        let s = seq{1..10}
        let g = s |> Seq.groupBy(fun i->i%2=0)
        let r1,i1 = g |> Seq.nth 0
        let r2,i2 = g |> Seq.nth 1
        Assert.AreEqual "Seq GroupBy" (i1|>Seq.length) (i2|>Seq.length)

    [<Js>]
    let CountBy() =
        let s = seq{1..10}
        let g = s |> Seq.countBy(fun i->i%2=0)
        let r1,i1 = g |> Seq.nth 0
        let r2,i2 = g |> Seq.nth 1
        Assert.AreEqual "Seq CountBy" i1 i2

    [<Js>]
    let Distinct() =
        let s = [|1;1;2;2;|]
        let r = s |> Seq.distinct
        Assert.AreEqual "Seq Distinct" 2 (r|>Seq.length)

    [<Js>]
    let DistinctBy() =
        let s = { -5 .. 10 }
        let r = Seq.distinctBy (fun elem -> abs elem) s
        Assert.AreEqual "Seq DistinctBy" 0 (r|>Seq.nth 5)

    [<Js>]
    let Sort() =
        let s = [|10; -2; 4; 9|]
        let r = s |> Seq.sort
        Assert.AreEqual "Seq Sort" -2 (r|>Seq.nth 0)

    [<Js>]
    let SortBy() =
        let s = [|10;-2;4;9|]
        let r = s |> Seq.sortBy(fun i -> i % 2 = 0)
        Assert.AreEqual "Seq SortBy" -2 (r|>Seq.nth 2)

    [<Js>]
    let Windowed() =
        let s = [| 1..9 |]
        let r = s |> Seq.windowed 3
        Assert.AreEqual "Seq Windowed" 7 (r|>Seq.length)

    [<Js>]
    let ReadOnly() =
        let s = seq { 1..10 }
        let r = s |> Seq.readonly
        use e = r.GetEnumerator()
        let m = e.MoveNext()
        Assert.AreEqual "Seq ReadOnly" true m