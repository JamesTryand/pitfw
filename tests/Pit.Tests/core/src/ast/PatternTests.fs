namespace Pit.Test
open Pit

module PatternTests =

    [<Js>]
    let ConstantMatchTest() =

        let filter123 x =
            match x with
            | 1 | 2 | 3 -> Assert.AreEqual "Constant Match Test" true (x < 4 && x > 0)
            | _ -> ()

        for x in 1..10 do filter123 x

    type Color =
    | Red = 0
    | Green = 1
    | Blue = 2

    [<Js>]
    let ConstantMatchTest2() =
        let printColorName (color:Color) =
            match color with
            | Color.Red -> Assert.AreEqual "Constant Match Test 2" color Color.Red
            | Color.Green -> Assert.AreEqual "Constant Match Test 2" color Color.Green
            | Color.Blue -> Assert.AreEqual "Constant Match Test 2" color Color.Blue
            | _ -> ()

        printColorName Color.Red
        printColorName Color.Green
        printColorName Color.Blue


    type PersonName =
        | FirstOnly of string
        | LastOnly of string
        | FirstLast of string * string

    [<Js>]
    let IdentifierPattern() =
        let constructQuery personName =
            match personName with
            | FirstOnly(firstName) -> "first"
            | LastOnly(lastName) -> "last"
            | FirstLast(firstName, lastName) -> "firstlast"


        let r = constructQuery(FirstOnly("Steve"))
        Assert.AreEqual "Identifier Pattern Test" r "first"
        let r1 = constructQuery(LastOnly("Jobs"))
        Assert.AreEqual "Identifier Pattern Test" r1 "last"
        let r2 = constructQuery(FirstLast("John","Jobs"))
        Assert.AreEqual "Identifier Pattern Test" r2 "firstlast"

    [<Js>]
    let function1 x =
            match x with
            | (var1, var2) when var1 > var2 -> var2
            | (var1, var2) when var1 < var2 -> var2
            | (var1, var2) -> var1

    [<Js>]
    let VariablePatternTest() =
        let r1 = function1 (1, 2)
        Assert.AreEqual "Variable Pattern Test" r1 2
        let r2 = function1 (2, 1)
        Assert.AreEqual "Identifier Pattern Test" r2 1
        let r3 = function1 (0, 0)
        Assert.AreEqual "Identifier Pattern Test" r3 0

    [<Js>]
    let VariablePatternTest2() =
        let sliceMiddle = 0.4
        match sliceMiddle with
        | x when x <= 0.25 -> ()
        | x when x > 0.25 && x <= 0.5 ->Assert.AreEqual "SliceMiddle" 0.4 sliceMiddle
        | _ -> ()

    [<Js>]
    let AsPatternTest() =
        let (var1, var2) as tuple1 = (1, 2)
        Assert.AreEqual "As Pattern Test" var1 1
        Assert.AreEqual "As Pattern Test" var2 2

    [<Js>]
    let OrPatternTest() =
        let detectZeroOR point =
            match point with
            | (0, 0) | (0, _) | (_, 0) -> "Zero found."
            | _ -> "Both nonzero."
        let r1 = detectZeroOR (0, 0)
        Assert.AreEqual "Or Pattern Test" r1 "Zero found."
        let r2 = detectZeroOR (1, 0)
        Assert.AreEqual "Or Pattern Test" r2 "Zero found."
        let r3 = detectZeroOR (0, 10)
        Assert.AreEqual "Or Pattern Test" r3 "Zero found."
        let r4 = detectZeroOR (10, 15)
        Assert.AreEqual "Or Pattern Test" r4 "Both nonzero."


    [<Js>]
    let AndPatternTest() =

        let detectZeroAND point =
            match point with
            | (0, 0) -> "Both values zero."
            | (var1, var2) & (0, _) -> "nonzero " + var2.ToString()
            | (var1, var2)  & (_, 0) -> "nonzero " + var1.ToString()
            | _ -> "Both nonzero."

        let r1 = detectZeroAND (0, 0)
        Assert.AreEqual "And Pattern Test" r1 "Both values zero."
        let r2 = detectZeroAND (1, 0)
        Assert.AreEqual "And Pattern Test" r2  "nonzero 1"
        let r3 = detectZeroAND (0, 10)
        Assert.AreEqual "And Pattern Test" r3 "nonzero 10"
        let r4 = detectZeroAND (10, 15)
        Assert.AreEqual "And Pattern Test" r4 "Both nonzero."

    [<Js>]
    let ConsPatternTest() =
        let list1 = [ 1; 2; 3; 4 ]
        let rec printList l =
            match l with
            | head :: tail ->
                Assert.AreEqual "Cons Pattern test" true (head <= 4)
                printList tail
            | [] -> ()

        printList list1

    [<Js>]
    let listLength list =
            match list with
            | [] -> 0
            | [ _ ] -> 1
            | [ _; _ ] -> 2
            | [ _; _; _ ] -> 3
            | _ -> List.length list

    [<Js>]
    let ListPatternTest() =
        Assert.AreEqual "List Pattern test 1" (listLength [ 1 ]) 1
        Assert.AreEqual "List Pattern test 2" (listLength [ 1; 1 ]) 2
        Assert.AreEqual "List Pattern test 3" (listLength [ ] ) 0
//
//    [<Js>]
//    let vectorLength vec =
//            match vec with
//            | [| var1 |] -> var1
//            | [| var1; var2 |] -> sqrt (var1*var1 + var2*var2)
//            | [| var1; var2; var3 |] -> sqrt (var1*var1 + var2*var2 + var3*var3)
//            | _ -> failwith "vectorLength called with an unsupported array size of %d." (vec.Length)
//    [<Js>]
//    let ArrayPatternTest() =
//
//
//        Assert.AreEqual "Array Pattern test" (vectorLength [| 1. |]) 1.000000
//        Assert.AreEqual "Array Pattern test" (vectorLength [| 1.; 1. |]) 1.414214
//        Assert.AreEqual "Array Pattern test" (vectorLength [| 1.; 1.; 1.; |]) 1.732051
//        (vectorLength [| 1. |]) |> ignore


    [<Js>]
    let countValues list value =
            let rec checkList list acc =
               match list with
               | (elem1 & head) :: tail when elem1 = value -> checkList tail (acc + 1)
               | head :: tail -> checkList tail acc
               | [] -> acc
            checkList list 0
    [<Js>]
    let ParanthesizedPatternTest() =


        let result = countValues [ for x in -10..10 -> x*x - 4 ] 0
        Assert.AreEqual "Array Pattern test" result 2


    [<Js>]
    let TuplePatternTest() =
        let detectZeroTuple point =
            match point with
            | (0, 0) -> "Both values zero."
            | (0, var2) -> "First value zero"
            | (var1, 0) -> "Second value zero"
            | _ -> "Both nonzero."
        let r1 = detectZeroTuple (0, 0)
        Assert.AreEqual "Tuple Pattern test" r1 "Both values zero."
        let r2 = detectZeroTuple (1, 0)
        Assert.AreEqual "Tuple Pattern test" r2 "Second value zero"
        let r3 = detectZeroTuple (0, 10)
        Assert.AreEqual "Tuple Pattern test" r3 "First value zero"
        let r4 = detectZeroTuple (10, 15)
        Assert.AreEqual "Tuple Pattern test" r4 "Both nonzero."

//
//    type MyRecord = { Name: string; ID: int }
//
//    [<Js>]
//    let RecordPatternTest() =
//
//        let IsMatchByName record1 (name: string) =
//            match record1 with
//            | { MyRecord.Name = nameFound; MyRecord.ID = _; } when nameFound = name -> true
//            | _ -> false
//
//        let recordX = { Name = "Parker"; ID = 10 }
//        let isMatched1 = IsMatchByName recordX "Parker"
//        Assert.AreEqual "Record Pattern test" isMatched1 true
//        let isMatched2 = IsMatchByName recordX "Hartono"
//        Assert.AreEqual "Record Pattern test" isMatched2 false

    [<Js>]
    let WildCardPatternTest() =
        let detect1 x =
            match x with
            | 1 -> "Found"
            | (var1 : int) -> "NotFound"
        let r1 = detect1 0
        Assert.AreEqual "WildCard Pattern test" r1 "NotFound"
        let r2 = detect1 1
        Assert.AreEqual "WildCard Pattern test" r2 "Found"

    [<Js>]
    let MultiplePatternTest() =
        let function1 (x:int*int) (y:int*int) =
            match x with
            | (var1, var2) when var1 > var2 ->
                match y with
                | (var1, var2) when var1 < var2 -> true
                | _                             -> false
            | _ -> false

        let r = function1 (3,2) (3,5)
        Assert.AreEqual "Multiple Patterns Test" true r