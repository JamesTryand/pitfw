namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGRectElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGRectElement()            

    [<CompileTo("x")>]
    member x.X 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("y")>]
    member x.Y 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("width")>]
    member x.Width 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("height")>]
    member x.Height 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("rx")>]
    member x.RX 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("ry")>]
    member x.RY
        with get() = 
            new SVGAnimatedLength()