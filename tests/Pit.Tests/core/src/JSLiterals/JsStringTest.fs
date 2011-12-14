#if DOM
namespace Pit.Dom.Tests
#endif
#if AST
namespace Pit.Test
#endif
open Pit
open Pit.Javascript

    module JsStringTest =
        [<Js>]
        let Create() = new JsString("Hello")

        [<Js>]
        let Length() =
            let s = Create()
            Assert.AreEqual "String Length" s.Length 5

        [<Js>]
        let CharAt() =
            let s = Create()
            let c = s.CharAt(1)
            Assert.AreEqual "String CharAt" "e" c

        [<Js>]
        let CharCodeAt() =
            let s = Create()
            let cc = s.CharCodeAt(1)
            Assert.AreEqual "String CharCodeAt" 101 cc

        [<Js>]
        let Concat() =
            let s = Create()
            let js = s.Concat(new JsString(" World"))
            Assert.AreEqual "String Concat" "Hello World" (js.ToString())

        [<Js>]
        let IndexOf() =
            let s = Create()
            let i = s.IndexOf("o")
            Assert.AreEqual "String IndexOf" 4 i
            let i1 = s.IndexOf("HELLO")
            Assert.AreEqual "String IndexOf" -1 i1

        [<Js>]
        let LastIndexOf() =
            let s = Create()
            let i = s.LastIndexOf("l")
            Assert.AreEqual "String LastIndexOf" 3 i

        [<Js>]
        let Match() =
            let s = new JsString("The rain in SPAIN stays mainly in the plain")
            let m = s.Match(new RegExp("ain", "gi"))
            Assert.AreEqual "String Match" 4 m.Length

        [<Js>]
        let Replace() =
            let s = Create()
            let s1 = s.Replace("e", "w")
            Assert.AreEqual "String Replace" "Hwllo" s1

        [<Js>]
        let Search() =
            let s = Create()
            let s1 = s.Search("l")
            Assert.AreEqual "String Search" s1 2

        [<Js>]
        let Slice() =
            let s = Create()
            let s1 = s.Slice(1)
            Assert.AreEqual "String slice" 4 s1.Length

        [<Js>]
        let Split() =
            let s = new JsString("Hello World")
            let s1 = s.Split(" ")
            Assert.AreEqual "String Split" 2 s1.Length

        [<Js>]
        let Substring1() =
            let s = Create()
            let s1 = s.Substring(1)
            Assert.AreEqual "String substring" s1.Length 4

        [<Js>]
        let Substring2() =
            let s = Create()
            let s1 = s.Substring(1,1)
            Assert.AreEqual "String substring" s1.Length 1

        [<Js>]
        let ToLower() =
            let s = Create()
            let s1 = s.ToLower()
            Assert.AreEqual "String ToLower" "hello" s1

        [<Js>]
        let ToUpper() =
            let s = Create()
            let s1 = s.ToUpper()
            Assert.AreEqual "String ToUpper" "HELLO" s1

    #if AST
    module StringMapTest =

        [<Js>]
        let Length() =
            let s = "Hello World"
            let len = s.Length
            Assert.AreEqual "String Map length" len 11

        [<Js>]
        let GetCharAt() =
            let s = "Hello World"
            let c = s.[3]
            Assert.AreEqual "String Map GetCharAt" 'l' c

        [<Js>]
        let Substring1() =
            let s = "Hello World"
            let s1 = s.Substring(1,4)
            Assert.AreEqual "String Map Substring1" "ello" s1

        [<Js>]
        let Substring2() =
            let s = "Hello World"
            let s1 = s.Substring(4)
            Assert.AreEqual "String Map Substring2" "o World" s1

        [<Js>]
        let ContainsPass() =
            let s = "Hello"
            let r = s.Contains("e")
            Assert.AreEqual "String Map Contains Pass" true r

        [<Js>]
        let ContainsFail() =
            let s = "Hello"
            let r = s.Contains("r")
            Assert.AreEqual "String Map Contains Fail" false r

        [<Js>]
        let EndsWith() =
            let s = "Hello"
            let r = s.EndsWith("o")
            Assert.AreEqual "String Map EndsWith" true r

        [<Js>]
        let Equals() =
            let s = "Hello"
            let s1 = "Hello"
            let r = s.Equals(s1)
            Assert.AreEqual "String Map Equals" true r

        [<Js>]
        let IndexOf1() =
            let s = "Hello"
            let i = s.IndexOf("l")
            Assert.AreEqual "String Map IndexOf" 2 i

        [<Js>]
        let IndexOf2() =
            let s = "Hello"
            let i = s.IndexOf("o",3)
            Assert.AreEqual "String Map IndexOf" 4 i

        [<Js>]
        let LastIndexOf() =
            let s = "Hello"
            let i = s.LastIndexOf("l")
            Assert.AreEqual "String Map LastIndexOf" 3 i

        [<Js>]
        let Replace() =
            let s = "Hello"
            let s1 = s.Replace("l","w")
            Assert.AreEqual "String Map Replace" "Hewwo" s1

        [<Js>]
        let Split() =
            let s = "Hello World"
            let s1 = s.Split([|' '|])
            Assert.AreEqual "String Map Split" 2 s1.Length

        [<Js>]
        let ToLower() =
            let s = "Hello World"
            let r = s.ToLower()
            Assert.AreEqual "String Map ToLower" "hello world" r

        [<Js>]
        let ToUpper() =
            let s = "Hello World"
            let r = s.ToUpper()
            Assert.AreEqual "String Map ToUpper" "HELLO WORLD" r
    #endif