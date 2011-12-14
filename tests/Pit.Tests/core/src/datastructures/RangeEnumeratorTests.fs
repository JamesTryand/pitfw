namespace Pit.Test
open Pit
open Pit.Javascript


    module RangeEnumeratorTests =

        [<Js>]
        let Increment() =
            let s = seq { 1..5 } |> Array.ofSeq
            Assert.AreEqual "Range Increment" 5 s.[4]

        [<Js>]
        let Decrement() =
            let s = seq { 5..-1..1 } |> Array.ofSeq
            Assert.AreEqual "Range Decrement" 1 s.[4]

        [<Js>]
        let IncrementTwoWay() =
            let s = seq { 2..2..5 } |> Array.ofSeq
            Assert.AreEqual "Range Increment 2 Way" 4 s.[1]

        [<Js>]
        let NestedRanges() =
            let s = seq { for i in 2..5 do yield Array.ofList [i..i..5] } |> Seq.toArray
            let len = s.Length
            Assert.AreEqual "Range Nested Length" 4 len
            Assert.AreEqual "Range Nested Value" 5 s.[3].[0]