namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type SVGCircleElement =
    inherit SVGTransformableElement

    val mutable svgcircle : ScriptObject

    internal new (svgcircle) =
        { inherit SVGTransformableElement(svgcircle); svgcircle=svgcircle }

    static member Of(el:DomElement) =
        new SVGCircleElement(el.InternalScriptObject)            

    member x.CX 
        with get() = 
            let v = x.svgcircle.GetProperty("cx") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.CY 
        with get() = 
            let v = x.svgcircle.GetProperty("cy") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.R 
        with get() = 
            let v = x.svgcircle.GetProperty("r") :?> ScriptObject
            new SVGAnimatedLength(v)
