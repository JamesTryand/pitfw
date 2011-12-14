namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGEllipseElement() =
    inherit SVGTransformableElement()
        
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGEllipseElement()            

    [<CompileTo("cx")>]
    member x.CX 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("cy")>]
    member x.CY 
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
