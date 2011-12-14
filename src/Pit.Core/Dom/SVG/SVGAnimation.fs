namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimationElement() =
    inherit SVGElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAnimationElement()           

    [<CompileTo("targetElement")>]
    member this.TargetElement 
        with get() = 
            new SVGElement()

    [<CompileTo("getStartTime")>]
    member this.GetStartTime() = 
        0.

    [<CompileTo("getCurrentTime")>]
    member this.GetCurrentTime() = 
        0.

    [<CompileTo("getSimpleDuration")>]
    member this.GetSimpleDuration() = 
        0.       

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimateElement() =
    inherit SVGAnimationElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAnimateElement()        
                
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimateColorElement() =
    inherit SVGAnimationElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAnimateColorElement()        


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimateMotionElement() =
    inherit SVGAnimationElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAnimateMotionElement()        


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimateTransformElement()  =
    inherit SVGAnimationElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAnimateTransformElement ()       

        
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGSetElement()   =
    inherit SVGAnimationElement()
    
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAnimateTransformElement()       