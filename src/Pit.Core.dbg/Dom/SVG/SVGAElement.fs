namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type SVGAElement =
    inherit SVGTransformableElement

    val mutable ele : ScriptObject

    internal new (ele) =
        { inherit SVGTransformableElement(ele); ele=ele }

    static member Of(el:DomElement) =
        new SVGAElement(el.InternalScriptObject)            

    member x.Target 
        with get() = 
            let svg = x.element.GetProperty("target") :?> ScriptObject
            new SVGAnimatedString(svg) 