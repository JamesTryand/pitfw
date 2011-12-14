namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type SVGRectElement =
    inherit SVGTransformableElement

    val mutable svgrect : ScriptObject

    internal new (svgrect) =
        { inherit SVGTransformableElement(svgrect); svgrect=svgrect }

    static member Of(el:DomElement) =
        new SVGRectElement(el.InternalScriptObject)            

    member x.X 
        with get() = 
            let v = x.svgrect.GetProperty("x") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.Y 
        with get() = 
            let v = x.svgrect.GetProperty("y") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.Width 
        with get() = 
            let v = x.svgrect.GetProperty("width") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.Height 
        with get() = 
            let v = x.svgrect.GetProperty("height") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.RX 
        with get() = 
            let v = x.svgrect.GetProperty("rx") :?> ScriptObject
            new SVGAnimatedLength(v)

    member x.RY
        with get() = 
            let v = x.svgrect.GetProperty("ry") :?> ScriptObject
            new SVGAnimatedLength(v)

