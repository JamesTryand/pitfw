namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGCircleElement() =
    inherit SVGTransformableElement()
    
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGCircleElement()            

    [<CompileTo("cx")>]
    member x.CX 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("cy")>]
    member x.CY 
        with get() = 
            new SVGAnimatedLength()

    [<CompileTo("r")>]
    member x.R 
        with get() = 
            new SVGAnimatedLength()
