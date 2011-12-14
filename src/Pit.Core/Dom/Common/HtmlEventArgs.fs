namespace Pit.Dom

open System
open Pit

[<JsIgnore>]
type MouseButtons =
    | None = 0
    | Left = 1
    | Right = 2
    | Middle = 4

[<JsIgnore(IgnoreGetSet=true)>]
type HtmlEventArgs() =

    [<CompileTo("altKey")>]
    member x.AltKey with get() = false

    [<CompileTo("characterCode")>]
    member x.CharacterCode with get() = 0

    [<CompileTo("clientX")>]
    member x.ClientX with get() = 0

    [<CompileTo("clientY")>]
    member x.ClientY with get() = 0

    [<CompileTo("ctrlKey")>]
    member x.CtrlKey with get() = false

    [<CompileTo("eventType")>]
    member x.EventType with get() = String.Empty

    [<CompileTo("keyCode")>]
    member x.KeyCode with get() = 0

    [<CompileTo("mouseButton")>]
    member x.MouseButton with get() = MouseButtons.None

    [<CompileTo("offsetX")>]
    member x.OffsetX with get() = 0

    [<CompileTo("offsetY")>]
    member x.OffsetY with get() = 0

    [<CompileTo("screenX")>]
    member x.ScreenX with get() = 0

    [<CompileTo("screenY")>]
    member x.ScreenY with get() = 0

    [<CompileTo("shiftKey")>]
    member x.ShiftKey with get() = false

    [<CompileTo("preventDefault")>]
    member x.PreventDefault() =
        ()

    [<CompileTo("stopPropagation")>]
    member x.StopPropagation() =
        ()