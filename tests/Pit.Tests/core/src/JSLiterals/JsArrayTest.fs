#if DOM
namespace Pit.Dom.Tests
#endif
#if AST
namespace Pit.Test
#endif
open Pit
open Pit.Javascript

    module JsArrayTest =

        let GetArray() =
            let a = [|1;2;3|]
            new JsArray<_>(a)

        let Create() =
            let js = GetArray()
            Assert.AreEqual "JsArray Length" 3 js.Length

        let ItemAccess() =
            let js = GetArray()
            let i = js.[1]
            Assert.AreEqual "JsArray Item Access" 2. js.[1]

        let Join() =
            let js = GetArray()
            let j = js.JoinAll()
            Assert.AreEqual "JsArray Join" "1,2,3" j

        let Pop() =
            let js = GetArray()
            let p = js.Pop()
            Assert.AreEqual "JsArray Pop" 3. p

        let Push1() =
            let js = GetArray()
            let i = js.Push(4)
            Assert.AreEqual "JsArray Push one item" i 4

        let Push2() =
            let js = GetArray()
            let i = js.Push([|4;5;|])
            Assert.AreEqual "JsArray Push 2 items" i 4.

        let Reverse() =
            let js = GetArray()
            let rev = js.Reverse()
            let revlen = rev.Length
            let r1 = rev.[0]
            Assert.AreEqual "JsArray Reverse len" revlen 3
            Assert.AreEqual "JsArray Reverse Index Access" 3 r1

        let Shift() =
            let js = GetArray()
            let s = js.Shift()
            Assert.AreEqual "JsArray Shift" 1. s

        let Slice1() =
            let js = GetArray()
            let s = js.Slice(1)
            Assert.AreEqual "JsArray Slice1" 2 s.Length

        let Slice2() =
            let js = GetArray()
            let s = js.Slice(0, 2)
            Assert.AreEqual "JsArray Slice2" 2 s.Length

        let Sort() =
            let js = GetArray()
            let r = js.Reverse()
            let s = js.Sort()
            Assert.AreEqual "JsArray sort" 1 s.[0]

        let Splice() =
            let js = GetArray()
            js.Splice(1, 0, 4)
            Assert.AreEqual "JsArray Splice at 1" 4. (js.[1] |> float)

        let Unshift() =
            let js = GetArray()
            let s = js.Unshift(4)
            Assert.AreEqual "JsArray unshift" 4 s