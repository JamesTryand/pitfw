namespace Pit.Test
open Pit

module TryWithTests =

    exception Error1 of string
    exception Error2 of string * int

    [<Js>]
    let TryWith1() =
        let function1 (x:int) (y:int) =
           try
              if x = y then raise (Error1("x"))
              else raise (Error2("x", 10))
           with
              | Error1(str)     -> Assert.AreEqual "TryWith Error1" "x" str
              | Error2(str, i)  -> Assert.AreEqual "TryWith Error2" 10 i
        function1 10 10
        function1 10 20