namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type SVGAnimationElement =
    inherit SVGElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGAnimationElement(el.InternalScriptObject)           

    member this.TargetElement 
        with get() = 
            let e = this.element.GetProperty("targetElement") :?> ScriptObject
            new SVGElement(e)

    member this.GetStartTime() = 
        this.element.GetProperty<float>("getStartTime")

    member this.GetCurrentTime() = 
        this.element.GetProperty<float>("getCurrentTime")

    member this.GetSimpleDuration() = 
        this.element.GetProperty<float>("getSimpleDuration")        

[<AllowNullLiteral>]
type SVGAnimateElement =
    inherit SVGAnimationElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGAnimationElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGAnimateElement(el.InternalScriptObject)        
                
[<AllowNullLiteral>]
type SVGAnimateColorElement =
    inherit SVGAnimationElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGAnimationElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGAnimateColorElement(el.InternalScriptObject)        


[<AllowNullLiteral>]
type SVGAnimateMotionElement =
    inherit SVGAnimationElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGAnimationElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGAnimateMotionElement(el.InternalScriptObject)        


[<AllowNullLiteral>]
type SVGAnimateTransformElement  =
    inherit SVGAnimationElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGAnimationElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGAnimateTransformElement (el.InternalScriptObject)       

        
[<AllowNullLiteral>]
type SVGSetElement   =
    inherit SVGAnimationElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGAnimationElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGAnimateTransformElement (el.InternalScriptObject)       