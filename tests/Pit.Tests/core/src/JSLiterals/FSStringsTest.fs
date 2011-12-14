namespace Pit.Test
open Pit

module FSStringsTest =
    [<Js>]
    let Collect() =
        let spaceOut input =
            String.collect (fun c -> c.ToString() + " ") input
        let res = spaceOut "Hi"
        Assert.AreEqual "StringModule Collect" "H i " res

    //[<Js>]
    let Concat() =
        let strings = [|"tomatoes"; "bananas";|]
        let fullstr = String.concat ", " strings
        Assert.AreEqual "StringModule Concat" "tomatoes, bananas" fullstr

    [<Js>]
    let Exists() =
        let containsUpperCase str =
            if (String.exists (fun c -> c = 'e') str) then true else false
        let res = containsUpperCase "Hello"
        Assert.AreEqual "StringModule Exists" true res

    [<Js>]
    let ForAll() =
        let isAllS str =
            if (String.forall (fun c -> c = 's') str) then true else false
        let res1 = isAllS "ssss"
        let res2 = isAllS "hhis"
        Assert.AreEqual "StringModule Forall" true res1
        Assert.AreEqual "StringModule Forall" false res2

    [<Js>]
    let Init() =
        let res = String.init 5 (fun i -> i.ToString())
        Assert.AreEqual "StringModule Init" "01234" res

    [<Js>]
    let Iterate() =
        let str = "HE"
        let ch = ref 'H'
        str |> String.iter(fun c ->
            Assert.AreEqual "StringModule Iterate" !ch c
            ch := 'E'
        )

    [<Js>]
    let IterateIndex() =
        let str = "HE"
        str |> String.iteri(fun i c ->
            Assert.AreEqual "StringModule IterateIndex" (c.ToString()) str.[i]
        )

    [<Js>]
    let Length() =
        let str = "HELLO" |> String.length
        Assert.AreEqual "StringModule length" 5 str

    [<Js>]
    let Map() =
        let str = "hello"
        let res = str |> String.map(fun c -> (int c) + 5 |> char)
        Assert.AreEqual "StringModule Map" "mjqqt" res

    [<Js>]
    let Replicate() =
        let str = "XO"
        let res = str |> String.replicate 5
        Assert.AreEqual "StringModule Replicate" "XOXOXOXOXO" res
