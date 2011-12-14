namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser


[<AllowNullLiteral>]
type SVGEllipseElement =
    inherit SVGTransformableElement

    val mutable svgellipse : ScriptObject

    internal new (svgellipse) =
        { inherit SVGTransformableElement(svgellipse); svgellipse=svgellipse }

    static member Of(el:DomElement) =
        new SVGEllipseElement(el.InternalScriptObject)            

    member x.CX 
        with get() = 
            let v = x.svgellipse.GetProperty("cx") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.CY 
        with get() = 
            let v = x.svgellipse.GetProperty("cy") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.RX 
        with get() = 
            let v = x.svgellipse.GetProperty("rx") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.RY
        with get() = 
            let v = x.svgellipse.GetProperty("ry") :?> ScriptObject
            new SVGAnimatedLength(v)
