namespace Pit.Dom

open System
open Pit
open Pit.Javascript

[<Alias("navigator");JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomNavigator() =

    static member Of() =
        new DomNavigator()

    [<CompileTo("appCodeName")>]
    member x.AppCodeName
        with get() = ""

    [<CompileTo("appName")>]
    member x.AppName
        with get() = ""

    [<CompileTo("appVersion")>]
    member x.AppVersion
        with get() = ""

    [<CompileTo("cookieEnabled")>]
    member x.CookieEnabled
        with get() = false

    [<CompileTo("platform")>]
    member x.Platform
        with get() = ""

    [<CompileTo("userAgent")>]
    member x.UserAgent
        with get() = ""

    [<CompileTo("javaEnabled")>]
    member x.JavaEnabled() =
        false

    [<CompileTo("taintEnabled")>]
    member x.TaintEnabled() =
        false

[<Alias("screen");JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomScreen() =

    static member Of() =
        new DomScreen()

    [<CompileTo("availHeight")>]
    member x.AvailableHeight
        with get() = 0.

    [<CompileTo("availWidth")>]
    member x.AvailableWidth
        with get() = 0.

    [<CompileTo("colorDepth")>]
    member x.ColorDepth
        with get() = 0

    [<CompileTo("height")>]
    member x.Height
        with get() = 0.

    [<CompileTo("pixelDepth")>]
    member x.PixelDepth
        with get() = 0

    [<CompileTo("width")>]
    member x.Width
        with get() = 0.

[<Alias("history");JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomHistory() =

    static member Of() =
        new DomHistory()


    [<CompileTo("length")>]
    member x.Length
        with get() = 0

    [<CompileTo("back")>]
    member x.Back() =
        ()

    [<CompileTo("forward")>]
    member x.Forward() =
        ()

    [<CompileTo("go")>]
    member x.Go(url : string) =
        ()

[<Alias("location");JsIgnoreAttribute(IgnoreGetSet=true)>]
[<AllowNullLiteral>]
type DomLocation() =

    static member Of() =
        new DomLocation()

    [<CompileTo("hash")>]
    member x.Hash
        with get() = ""

    [<CompileTo("host")>]
    member x.Host
        with get() = ""

    [<CompileTo("hostName")>]
    member x.HostName
        with get() = ""

    [<CompileTo("href")>]
    member x.Href
        with get() = ""

    [<CompileTo("pathName")>]
    member x.PathName
        with get() = ""

    [<CompileTo("port")>]
    member x.Port
        with get() = ""

    [<CompileTo("protocol")>]
    member x.Protocol
        with get() = ""

    [<CompileTo("search")>]
    member x.Search
        with get() = ""

    [<CompileTo("assign")>]
    member x.Assign(url : string) =
        ()

    [<CompileTo("reload")>]
    member x.Reload() =
        ()

    [<CompileTo("replace")>]
    member x.Replace(newUrl : string) =
        ()

[<AllowNullLiteral;JsIgnoreAttribute(IgnoreGetSet=true);Alias("frame")>]
type DomFrame() =
    inherit DomElement()

    static member Of() =
        new DomFrame()

    [<CompileTo("align")>]
    member x.Align
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("contentDocument")>]
    member x.ContentDocument
        with get() = Unchecked.defaultof<DomDocument>

    [<CompileTo("contentWindow")>]
    member x.ContentWindow
        with get() = Unchecked.defaultof<DomWindow>

    [<CompileTo("frameBorder")>]
    member x.FrameBorder
        with get() = 0
        and set(v : int) = ()

    [<CompileTo("height")>]
    member x.Height
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("longDesc")>]
    member x.LongDesc
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("marginHeight")>]
    member x.MarginHeight
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("marginWidth")>]
    member x.MarginWidth
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("noResize")>]
    member x.NoResize
        with get() = true
        and set(v: bool) = ()

    [<CompileTo("scrolling")>]
    member x.Scrolling
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("src")>]
    member x.Src
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("width")>]
    member x.Width
        with get() = 0.
        and set(v : float) = ()

and
    [<Alias("window");JsIgnoreAttribute(IgnoreGetSet=true)>]
    [<AllowNullLiteral>]
    DomWindow() =
    inherit DomObject()

    static member Of() =
        new DomWindow()

    [<CompileTo("closed")>]
    member x.Closed
        with get() = false

    [<CompileTo("defaultStatus")>]
    member x.DefaultStatus
        with get() = String.Empty
        and set(v : String) = ()

    [<CompileTo("document")>]
    member x.Document
        with get() = new DomDocument()

    [<CompileTo("history")>]
    member x.History
        with get() = new DomHistory()

    [<CompileTo("location")>]
    member x.Location
        with get() = new DomLocation()

    [<CompileTo("navigator")>]
    member x.Navigator
        with get() = new DomNavigator()

//    [<CompileTo("frames")>]
//    member x.Frames
//        with get() =
//            let items : DomFrame[] = Array.zeroCreate(0)
//            items

    [<CompileTo("innerHeight")>]
    member x.InnerHeight
        with get() = 0.

    [<CompileTo("innerWidth")>]
    member x.InnerWidth
        with get() = 0.

    [<CompileTo("length")>]
    member x.Length
        with get() = 0

    [<CompileTo("name")>]
    member x.Name
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("opener")>]
    member x.Opener
        with get() = Some(new DomWindow())

    [<CompileTo("outerWidth")>]
    member x.OuterWidth
        with get() = 0.
        and set(v : float) = ()

    [<CompileTo("outerHeight")>]
    member x.OuterHeight
        with get() = 0.
        and set(v : float) = ()

    [<CompileTo("pageXOffset")>]
    member x.PageXOffset
        with get() = 0.

    [<CompileTo("pageYOffset")>]
    member x.PageYOffset
        with get() = 0.

    [<CompileTo("parent")>]
    member x.Parent
        with get() = Some(new DomWindow())

    [<CompileTo("screen")>]
    member x.Screen
        with get() = new DomScreen()

    [<CompileTo("screenLeft")>]
    member x.ScreenLeft
        with get() = 0.

    [<CompileTo("screenTop")>]
    member x.ScreenTop
        with get() = 0.

    [<CompileTo("screenX")>]
    member x.ScreenX
        with get() = 0.

    [<CompileTo("screenY")>]
    member x.ScreenY
        with get() = 0.

    [<CompileTo("self")>]
    member x.Self
        with get() = new DomWindow()

    [<CompileTo("status")>]
    member x.Status
        with get() = ""
        and set(v : string) = ()

    [<CompileTo("top")>]
    member x.Top
        with get() = new DomWindow()

    member x.ActiveXObject
        with get() = new ActiveXObject("")

    [<CompileTo("alert")>]
    member x.Alert(str : string) =
        ()

    [<CompileTo("blur")>]
    member x.Blur() =
        ()

    [<CompileTo("close")>]
    member x.Close() =
        ()

    [<CompileTo("clearInterval")>]
    member x.ClearInterval(setIntervalId : int) =
        ()

    [<CompileTo("clearTimeout")>]
    member x.ClearTimeout(setTimeoutId : int) =
        ()

    [<CompileTo("confirm")>]
    member x.Confirm(message : string) =
        true

    [<CompileTo("createPopup")>]
    member x.CreatePopup() =
        new DomWindow()

    [<CompileTo("focus")>]
    member x.Focus() =
        ()

    [<CompileTo("moveBy")>]
    member x.MoveBy(x1 : float, y1 : float) =
        ()

    [<CompileTo("moveTo")>]
    member x.MoveTo(x1 : float, y1 : float) =
        ()

    [<CompileTo("open")>]
    member x.Open(url : string) =
        new DomWindow()

    [<CompileTo("open")>]
    member x.Open(url: string, name : string, specs : string, replace : bool)=
        new DomWindow()

    [<CompileTo("print")>]
    member x.Print() =
        ()

    [<CompileTo("prompt")>]
    member x.Prompt(msg : string, ?defaultText : string) =
        ()

    [<CompileTo("resizeBy")>]
    member x.ResizeBy(width : float, height : float) =
        ()

    [<CompileTo("resizeTo")>]
    member x.ResizeTo(width : float, height : float) =
        ()

    [<CompileTo("scrollBy")>]
    member x.ScrollBy(xnum : float, ynum : float) =
        ()

    [<CompileTo("scrollTo")>]
    member x.ScrollTo(xpos : float, ypos : float) =
        ()

    [<CompileTo("setInterval")>]
    member x.SetInterval(codeName : string, milliseconds : int) =
        0

    [<CompileTo("setInterval")>]
    member x.SetInterval(fn: unit->unit, milliseconds:int) =
        0

    [<CompileTo("setTimeout")>]
    member x.SetTimeout(code : string, milliseconds : int) =
        0

    [<CompileTo("setTimeout")>]
    member x.SetTimeout(fn : unit->unit, milliseconds: int) =
        0