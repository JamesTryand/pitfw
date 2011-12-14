namespace Pit.Javascript
open Pit

    [<JsIgnore(IgnoreNamespace=true)>]
    type Date(year:int, month:int, day: int, hours:int, minutes:int, seconds: int, milliseconds:int) =

        [<CompileTo("getDate")>]
        member x.GetDate() = 0

        [<CompileTo("getDay")>]
        member x.GetDay() = 0

        [<CompileTo("getFullYear")>]
        member x.GetFullYear() = 0

        [<CompileTo("getHours")>]
        member x.GetHours() = 0

        [<CompileTo("getMilliseconds")>]
        member x.GetMilliseconds() = 0

        [<CompileTo("getMinutes")>]
        member x.GetMinutes() = 0

        [<CompileTo("getMonth")>]
        member x.GetMonth() = 0

        [<CompileTo("getSeconds")>]
        member x.GetSeconds() = 0

        [<CompileTo("getTime")>]
        member x.GetTime() = 0

        [<CompileTo("getTimezoneOffset")>]
        member x.GetTimezoneOffset() = 0.

        [<CompileTo("getUTCDate")>]
        member x.GetUTCDate() = 0

        [<CompileTo("getUTCDay")>]
        member x.GetUTCDay() = 0

        [<CompileTo("getUTCFullYear")>]
        member x.GetUTCFullYear() = 0

        [<CompileTo("getUTCHours")>]
        member x.GetUTCHours() = 0

        [<CompileTo("getUTCMilliseconds")>]
        member x.GetUTCMilliseconds() = 0

        [<CompileTo("getUTCMinutes")>]
        member x.GetUTCMinutes() = 0

        [<CompileTo("getUTCMonth")>]
        member x.GetUTCMonth() = 0

        [<CompileTo("getUTCSeconds")>]
        member x.GetUTCSeconds() = 0

        [<CompileTo("setDate")>]
        member x.SetDate(date:int) = ()

        [<CompileTo("setFullYear")>]
        member x.SetFullYear(year:int) = ()

        [<CompileTo("setHours")>]
        member x.SetHours(hour:int) = ()

        [<CompileTo("setMilliseconds")>]
        member x.SetMilliseconds(milliseconds:int) = ()

        [<CompileTo("setMinutes")>]
        member x.SetMinutes(minutes:int) = ()

        [<CompileTo("setMonth")>]
        member x.SetMonth(month:int) = ()

        [<CompileTo("setSeconds")>]
        member x.SetSeconds(seconds:int) = ()

        [<CompileTo("setTime")>]
        member x.SetTime(milliseconds:int) = ()

        [<CompileTo("setUTCDate")>]
        member x.SetUTCDate(date:int) = ()

        [<CompileTo("setUTCFullYear")>]
        member x.SetUTCFullYear(year:int) = ()

        [<CompileTo("setUTCHours")>]
        member x.SetUTCHours(hour:int) = ()

        [<CompileTo("setUTCMilliseconds")>]
        member x.SetUTCMilliseconds(milliseconds:int) = ()

        [<CompileTo("setUTCMinutes")>]
        member x.SetUTCMinutes(minutes:int) = ()

        [<CompileTo("setUTCMonth")>]
        member x.SetUTCMonth(month:int) = ()

        [<CompileTo("setUTCSeconds")>]
        member x.SetUTCSeconds(seconds:int) = ()

        [<CompileTo("toDateString")>]
        member x.ToDateString() = ""

        [<CompileTo("toLocaleDateString")>]
        member x.ToLocaleDateString() = ""

        [<CompileTo("toLocaleTimeString")>]
        member x.ToLocaleTimeString() = ""

        [<CompileTo("toLocaleString")>]
        member x.ToLocaleString() = ""

        [<CompileTo("toTimeString")>]
        member x.ToTimeString() = ""

        [<CompileTo("toUTCString")>]
        member x.ToUTCString() = ""

        [<CompileTo("toString")>]
        override x.ToString() = ""

        static member UTC() = Date()

        [<CompileTo("parse")>]
        static member Parse(date:string) = Date()

        new() = new Date(0, 0, 0, 0, 0, 0, 0)