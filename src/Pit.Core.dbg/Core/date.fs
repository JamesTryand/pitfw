namespace Pit.Javascript

open System
open Pit
open System.Windows.Browser

    type Date =
        val dateSO: ScriptObject

        new (year:int, month:int, day: int, hours:int, minutes:int, seconds: int, milliseconds:int) = { dateSO = HtmlPage.Window.CreateInstance("Date", year, month, day, hours, minutes, seconds, milliseconds) }

        new() = { dateSO = HtmlPage.Window.CreateInstance("Date", null) }

        //internal new(dateSO: ScriptObject) = { dateSO = dateSO }

        member x.GetDate() = x.dateSO.Invoke("getDate", null) |> toInt

        member x.GetDay() = x.dateSO.Invoke("getDay", null) |> toInt

        member x.GetFullYear() = x.dateSO.Invoke("getFullYear", null) |> toInt

        member x.GetHours() = x.dateSO.Invoke("getHours", null) |> toInt

        member x.GetMilliseconds() = x.dateSO.Invoke("getMilliseconds", null) |> toInt

        member x.GetMinutes() = x.dateSO.Invoke("getMinutes", null) |> toInt

        member x.GetMonth() = x.dateSO.Invoke("getMonth", null) |> toInt

        member x.GetSeconds() = x.dateSO.Invoke("getSeconds", null) |> toInt

        member x.GetTime() = x.dateSO.Invoke("getTime", null) :?> float

        member x.GetTimezoneOffset() = x.dateSO.Invoke("getTimezoneOffset", null) :?> float

        member x.GetUTCDate() = x.dateSO.Invoke("getUTCDate", null) |> toInt

        member x.GetUTCDay() = x.dateSO.Invoke("getUTCDay", null) |> toInt

        member x.GetUTCFullYear() = x.dateSO.Invoke("getUTCFullYear", null) |> toInt

        member x.GetUTCHours() = x.dateSO.Invoke("getUTCHours", null) |> toInt

        member x.GetUTCMilliseconds() = x.dateSO.Invoke("getUTCMilliseconds", null) |> toInt

        member x.GetUTCMinutes() = x.dateSO.Invoke("getUTCMinutes", null) |> toInt

        member x.GetUTCMonth() = x.dateSO.Invoke("getUTCMonth", null) |> toInt

        member x.GetUTCSeconds() = x.dateSO.Invoke("getUTCSeconds", null) |> toInt

        member x.SetDate(date:int) = x.dateSO.Invoke("setDate", date)

        member x.SetFullYear(year:int) = x.dateSO.Invoke("setFullYear", year)

        member x.SetHours(hour:int) = x.dateSO.Invoke("setHours", hour)

        member x.SetMilliseconds(milliseconds:int) = x.dateSO.Invoke("setMilliseconds", milliseconds)

        member x.SetMinutes(minutes:int) = x.dateSO.Invoke("setMinutes", minutes)

        member x.SetMonth(month:int) = x.dateSO.Invoke("setMonth", month)

        member x.SetSeconds(seconds:int) = x.dateSO.Invoke("setSeconds", seconds)

        member x.SetTime(milliseconds:int) = x.dateSO.Invoke("setTime", milliseconds)

        member x.SetUTCDate(date:int) = x.dateSO.Invoke("setUTCDate", date)

        member x.SetUTCFullYear(year:int) = x.dateSO.Invoke("setUTCFullYear", year)

        member x.SetUTCHours(hour:int) = x.dateSO.Invoke("setUTCHours", hour)

        member x.SetUTCMilliseconds(milliseconds:int) = x.dateSO.Invoke("setUTCMilliseconds", milliseconds)

        member x.SetUTCMinutes(minutes:int) = x.dateSO.Invoke("setUTCMinutes", minutes)

        member x.SetUTCMonth(month:int) = x.dateSO.Invoke("setUTCMonth", month)

        member x.SetUTCSeconds(seconds:int) = x.dateSO.Invoke("setUTCSeconds", seconds)

        member x.ToDateString() = x.dateSO.Invoke("toDateString", null)

        member x.ToLocaleDateString() = x.dateSO.Invoke("toLocaleDateString", null)

        member x.ToLocaleTimeString() = x.dateSO.Invoke("toLocaleTimeString", null)

        member x.ToLocaleString() = x.dateSO.Invoke("toLocaleString", null)

        member x.ToTimeString() = x.dateSO.Invoke("toTimeString", null)

        member x.ToUTCString() = x.dateSO.Invoke("toUTCString", null)

        override x.ToString() = x.dateSO.Invoke("toString", null).ToString()

        static member UTC() =
            let d = HtmlPage.Window.GetProperty("Date") :?> ScriptObject
            d.Invoke("UTC", null) :?> float

        static member Parse(dateString:string) =
            let d = HtmlPage.Window.GetProperty("Date") :?> ScriptObject
            d.Invoke("parse", dateString)