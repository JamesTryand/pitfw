namespace Pit.Dom
open System
open System.Windows.Browser
open Utils
open Pit

[<AllowNullLiteral>]
type DomNavigator(navigator : ScriptObject) =

    static member Of(navigator:ScriptObject) =
        new DomNavigator(navigator)

    member x.AppCodeName
        with get() = navigator.GetProperty("appCodeName") |> string

    member x.AppName
        with get() = navigator.GetProperty("appName") |> string

    member x.AppVersion
        with get() = navigator.GetProperty("appVersion") |> string

    member x.CookieEnabled
        with get() = navigator.GetProperty("cookieEnabled") |> unbox<bool>

    member x.Platform
        with get() = navigator.GetProperty("platform") |> string

    member x.UserAgent
        with get() = navigator.GetProperty("userAgent") |> string

    member x.JavaEnabled() =
        navigator.Invoke("javaEnabled", null) |> unbox<bool>

    member x.TaintEnabled() =  //IE, Firefox check to be added
        navigator.Invoke("taintEnabled", null) |> unbox<bool>

[<AllowNullLiteral>]
type DomScreen(screen : ScriptObject) =

    static member Of(screen:ScriptObject) =
        new DomScreen(screen)

    member x.AvailableHeight
        with get() = screen.GetProperty("availHeight") |> unbox<float>

    member x.AvailableWidth
        with get() = screen.GetProperty("availWidth") |> unbox<float>

    member x.ColorDepth
        with get() = screen.GetProperty("colorDepth") |> unbox<float>

    member x.Height
        with get() = screen.GetProperty("height") |> unbox<float>

    member x.Width
        with get() = screen.GetProperty("width") |> unbox<float>

    member x.PixelDepth
        with get() = screen.GetProperty("pixelDepth") |> unbox<float>

[<AllowNullLiteral>]
type DomHistory(history : ScriptObject) =

    static member Of(history:ScriptObject) =
        new DomHistory(history)

    member x.Length
        with get() = history.GetProperty("length") |> unbox<int>

    member x.Back() =
        history.Invoke("back", null)

    member x.Forward() =
        history.Invoke("forward", null)

    member x.Go(url : string) =
        history.Invoke("go", url)

[<AllowNullLiteral>]
type DomLocation(location : ScriptObject) =

    static member Of(location:ScriptObject) =
        new DomLocation(location)

    member x.Hash
        with get() = location.GetProperty("hash") |> string

    member x.Host
        with get() = location.GetProperty("host") |> string

    member x.HostName
        with get() = location.GetProperty("hostname") |> string

    member x.Href
        with get() = location.GetProperty("href") |> string

    member x.PathName
        with get() = location.GetProperty("pathname") |> string

    member x.Port
        with get() = location.GetProperty<string>("port") |> string

    member x.Protocol
        with get() = location.GetProperty("protocol") |> string

    member x.Search
        with get() = location.GetProperty("search") |> string

    member x.Assign(url : string) =
        location.Invoke("assign", url)

    member x.Reload() =
        location.Invoke("reload", null)

    member x.Replace(newUrl : string) =
        location.Invoke("replace", newUrl)

module internal UIHelpers =
    open System.Windows.Threading

    let randomId =
        let rand = new Random()
        fun() -> rand.Next()

    let setTimeout (milliseconds: int) (f: unit->unit) =
        // simulating a timer with async, works nicely
        let timer = async {
            do! Async.Sleep(milliseconds)
            do f()
        }
        timer |> Async.StartImmediate

    let setInterval (milliseconds: int) (f:unit->unit) =
        let rec timer() = async {
            do! Async.Sleep(milliseconds)
            do f()
            return! timer()
        }
        let cts = new System.Threading.CancellationTokenSource()
        Async.StartImmediate(timer(), cts.Token)
        { new System.IDisposable with
            member x.Dispose() =
                cts.Cancel()
                cts.Dispose() }

[<AllowNullLiteral>]
type DomWindow =
    inherit DomObject
    val htmlWin: HtmlObject
    val private clearIds : System.Collections.Generic.Dictionary<int,IDisposable>

    internal new (htmlWin) =
        {   inherit DomObject(htmlWin)
            htmlWin  = htmlWin
            clearIds = new System.Collections.Generic.Dictionary<int,IDisposable>() }

    static member Of(htmlWin:HtmlObject) =
        new DomWindow(htmlWin)

    member x.Closed
        with get() = x.htmlWin.GetProperty<bool>("closed")

    member x.DefaultStatus
        with get() = x.htmlWin.GetProperty<string>("defaultStatus")
        and set(v : string) = x.htmlWin.SetProperty("defaultStatus", box(v))

    member x.Document
        with get() =
            let el = x.htmlWin.GetProperty<HtmlObject>("document")
            new DomDocument(el)

    member x.History
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("history")
            new DomHistory(el)

    member x.Location
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("location")
            new DomLocation(el)

    member x.Navigator
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("navigator")
            new DomNavigator(el)

    member x.InnerHeight
        with get() = x.htmlWin.GetProperty<float>("innerHeight")

    member x.InnerWidth
        with get() = x.htmlWin.GetProperty<float>("innerWidth")

    member x.Length
        with get() = x.htmlWin.GetProperty<float>("length") |> int

    member x.Name
        with get() = x.htmlWin.GetProperty<string>("name")
        and set(v : string) = x.htmlWin.SetProperty("name", box(v))

    member x.Opener
        with get() =
            let el = x.htmlWin.GetProperty<HtmlObject>("opener")
            if el <> null then
                Some(new DomWindow(el))
            else
                None

    member x.OuterWidth
        with get() = x.htmlWin.GetProperty<float>("outerWidth")
        and set(v : float) = x.htmlWin.SetProperty("outerWidth", box(v))

    member x.OuterHeight
        with get() = x.htmlWin.GetProperty<float>("outerHeight")
        and set(v : float) = x.htmlWin.SetProperty("outerHeight", box(v))

    member x.PageXOffset
        with get() = x.htmlWin.GetProperty<float>("pageXOffset")

    member x.PageYOffset
        with get() = x.htmlWin.GetProperty<float>("pageYOffset")

    member x.Parent
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("parent") :?> HtmlObject
            if el <> null then
                Some(new DomWindow(el))
            else
                None

    member x.Screen
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("screen")
            new DomScreen(el)

    member x.ScreenLeft
        with get() = x.htmlWin.GetProperty("screenLeft") |> unbox<float>

    member x.ScreenTop
        with get() = x.htmlWin.GetProperty("screenTop") |> unbox<float>

    member x.ScreenX
        with get() = x.htmlWin.GetProperty("screenX") |> unbox<float>

    member x.ScreenY
        with get() = x.htmlWin.GetProperty("screenY") |> unbox<float>

    member x.Self
        with get() = x

    member x.Status
        with get() = x.htmlWin.GetProperty("status") |> string
        and set(v : string) =
            x.htmlWin.SetProperty("status", v)

    member x.Top
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("top") :?> HtmlObject
            new DomWindow(el)

    member x.ActiveXObject
        with get() =
            let el = x.htmlWin.GetProperty<ScriptObject>("ActiveXObject")
            el

    member x.Blur() = // IE & firefox
        x.htmlWin.Invoke("blur", null) |> ignore

    member x.Confirm(message : string) =
        x.htmlWin.Invoke("confirm", [|box(message)|]) |> ignore

    member x.CreatePopup() =
        let popupEl = x.htmlWin.Invoke("createPopup", null) :?> HtmlObject
        new DomWindow(popupEl)

    member x.Close() =
        x.htmlWin.Invoke("close", null) |> ignore

    member x.Focus() =
        x.htmlWin.Invoke("focus", null) |> ignore

    member x.MoveBy(x1 : float, y1 : float) =
        x.htmlWin.Invoke("moveBy", [| box(x1); box(y1) |]) |> ignore

    member x.MoveTo(x1 : float, y1 : float) =
        x.htmlWin.Invoke("moveTo", [| box(x1); box(y1) |]) |> ignore

    (*member x.Open(?url : string, ?name : string, ?specs : string, ?replace : bool) =
        let mutable info  : obj[] = Array.zeroCreate 0
        if url.IsSome then
            info <- Array.append info [| box(url.Value) |]
        if name.IsSome then
            info <- Array.append info [|box(name.Value)|]
        if specs.IsSome then
            info <- Array.append info [| box(specs.Value) |]
        if replace.IsSome then
            info <- Array.append info [| box(replace.Value) |]
        let so = x.htmlWin.Invoke("open", info) :?> HtmlObject
        new DomWindow(so)*)

    member x.Open(url: string) =
        let so = x.htmlWin.Invoke("open", [|box(url)|]) :?> HtmlObject
        new DomWindow(so)

    member x.Open(url: string, name : string, specs : string, replace : bool) =
        let so = x.htmlWin.Invoke("open", [|box(url);box(name);box(specs);box(replace)|]) :?> HtmlObject
        new DomWindow(so)

    member x.Print() =
        x.htmlWin.Invoke("print", null) |> ignore

    member x.Prompt(msg : string, ?defaultText : string) =
        if defaultText.IsSome then
            x.htmlWin.Invoke("prompt", [|box(msg);box(defaultText.Value)|]) |> ignore
        else
            x.htmlWin.Invoke("prompt", [|box(msg);|]) |> ignore

    member x.ResizeBy(width : float, height : float) =
        x.htmlWin.Invoke("resizeBy", [| box(width); box(height) |]) |> ignore

    member x.ResizeTo(width : float, height : float) =
        x.htmlWin.Invoke("resizeTo", [| box(width); box(height) |]) |> ignore

    member x.ScrollBy(xNum : float, yNum : float) =
        x.htmlWin.Invoke("scrollBy", [| box(xNum); box(yNum)|]) |> ignore

    member x.ScrollTo(xPos : float, yPos : float) =
        x.htmlWin.Invoke("scrollTo", [| box(xPos); box(yPos) |]) |> ignore

    member x.ClearInterval(setIntervalId : int) =
        if x.clearIds.ContainsKey(setIntervalId) then
            x.clearIds.[setIntervalId].Dispose() // disposing the F# async thread
        //x.htmlWin.Invoke("clearInterval", [|box(setIntervalId)|]) |> ignore

    member x.ClearTimeout(setTimeoutId : float) =
        () // timeout is handled using F# async, so no need to invoke here
        //x.htmlWin.Invoke("clearTimeout", [|box(setTimeoutId)|]) |> ignore

    member x.SetInterval(code : string, millisec : int) =
        x.htmlWin.Invoke("setInterval", [| box(code); box(millisec) |]) |> toInt

    member x.SetInterval(fn: unit->unit, millisec:int) =
        let disp = UIHelpers.setInterval millisec fn
        let id   = UIHelpers.randomId()
        x.clearIds.Add(id, disp)
        id

    member x.SetTimeout(code : string, millisec : int) =
        x.htmlWin.Invoke("setTimeout", [| box(code); box(millisec) |]) |> toInt

    member x.SetTimeout(fn : _ -> unit, millisec : int) =
        UIHelpers.setTimeout (millisec) fn
        UIHelpers.randomId()

[<AllowNullLiteral>]
type DomFrame(scriptObj : ScriptObject) =

    static member Of(scriptObj:ScriptObject) =
        new DomFrame(scriptObj)

    member x.Align
        with get() =  scriptObj.GetProperty("align")
        and set(v:string) = scriptObj.SetProperty("frameBorder", box(v))

    member x.ContentDocument
        with get() =
            let doc = scriptObj.GetProperty<HtmlDocument>("contentDocument")
            new DomDocument(doc)

    member x.ContentWindow
        with get() =
            let win = scriptObj.GetProperty<HtmlWindow>("contentWindow")
            new DomWindow(win)

    member x.FrameBorder
        with get() = scriptObj.GetProperty<float>("frameBorder") |> int
        and set(v : int) =
            if v <> 0 || v <> 1 then
                raise(new ArgumentException("Value should be either 0 or 1"))
            scriptObj.SetProperty("frameBorder", v)

    member x.Height
        with get() = scriptObj.GetProperty<float>("height")
        and set(v : float) =
            scriptObj.SetProperty("height", v)

    member x.LongDesc
        with get() = scriptObj.GetProperty<string>("longDesc")
        and set(v : string) =
            scriptObj.SetProperty("longDesc", v)

    member x.MarginHeight
        with get() = scriptObj.GetProperty<float>("marginHeight")
        and set(v : float) =
            scriptObj.SetProperty("marginHeight", v)

    member x.MarginWidth
        with get() = scriptObj.GetProperty<float>("marginWidth")
        and set(v : float) =
            scriptObj.SetProperty("marginWidth", v)

    member x.Name
        with get() = scriptObj.GetProperty<string>("name")
        and set(v : string) = scriptObj.SetProperty("name", v)

    member x.NoResize
        with get() = scriptObj.GetProperty<bool>("noResize")
        and set(v : bool) = scriptObj.SetProperty("noResize", v)

    member x.Scrolling
        with get() = scriptObj.GetProperty("scrolling")
        and set(v) = scriptObj.SetProperty("scrolling", v.ToString())

    member x.Src
        with get() = scriptObj.GetProperty<string>("src")
        and set(v : string) = scriptObj.SetProperty("src", v)

    member x.Width
        with get() = scriptObj.GetProperty<float>("width")
        and set(v : float) = scriptObj.SetProperty("width", v)