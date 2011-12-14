namespace Pit.Test
open Pit
open Pit.Javascript

module ActivePatternsTest =

    [<Js>]
    let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

    [<Js>]
    let isEven input =
        match input with
        | Even -> true
        | Odd  -> false

    [<Js>]
    let ActivePatterns() =
        let res = isEven 2
        Assert.AreEqual "Active Pattern" true res

    [<Js>]
    let err = 1.e-10

    [<Js>]
    let floatequal x y =
       abs (x - y) < err

    [<Js>]
    let (|Square|_|) (x : int) =
      if floatequal (sqrt (float x)) (round (sqrt (float x))) then Some(x)
      else None

    [<Js>]
    let (|Cube|_|) (x : int) =
      if floatequal ((float x) ** ( 1.0 / 3.0)) (round ((float x) ** (1.0 / 3.0))) then Some(x)
      else None

    [<Js>]
    let findSquareCubes x =
       if (match x with
           | Cube x -> true
           | _ -> false
           &&
           match x with
           | Square x -> true
           | _ -> false
          )
       then Assert.AreEqual "Partial Active Patterns" 64 x

    [<Js>]
    let PartialActivePatterns() =
        findSquareCubes 64