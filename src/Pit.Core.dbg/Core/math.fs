namespace Pit.Javascript

open System
open System.Windows.Browser
open Pit

    module Math =

        let private math() =  HtmlPage.Window.GetProperty("Math") :?> ScriptObject

        let abs(x:float)    = math().Invoke("abs" , x) :?> float

        let acos(x:float)   = math().Invoke("acos", x) :?> float

        let asin(x:float)   = math().Invoke("asin", x) :?> float

        let atan(x:float)   = math().Invoke("atan", x) :?> float

        let atan2(x:float)  = math().Invoke("atan2", x) :?> float

        let ceil(x:float)   = math().Invoke("ceil", x) :?> float

        let cos(x:float)    = math().Invoke("cos", x) :?> float

        let exp(x:float)    = math().Invoke("exp", x) :?> float

        let floor(x:float)  = math().Invoke("floor", x) :?> float

        let log(x:float)    = math().Invoke("log", x) :?> float

        let pow (x:float)(y:float) = math().Invoke("pow", [| box(x); box(y) |]) :?> float

        let round(x:float)  = math().Invoke("round", x) :?> float

        let random()        = math().Invoke("random", null) :?> float

        let sin(x:float)    = math().Invoke("sin", x) :?> float

        let sqrt(x:float)   = math().Invoke("sqrt", x) :?> float

        let tan(x:float)    = math().Invoke("tan", x) :?> float

        let PI              = 3.1415926535897931

        let E               = 2.7182818284590451

        let LN2             = 0.69314718055994529

        let LN10            = 2.3025850929940459

        let LOG2E           = 1.4426950408889634

        let LOG10E          = 0.43429448190325182

        let SQR1_2          = 0.7071067811865476

        let SQRT2           = 1.4142135623730951