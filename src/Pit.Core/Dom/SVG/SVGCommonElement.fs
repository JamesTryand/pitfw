namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGClipPathElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGClipPathElement()         

    [<CompileTo("clipPathUnits")>]
    member this.ClipPathUnits 
        with get() = 
            new SVGAnimatedEnumeration()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGDefsElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGDefsElement ()         
        

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGDescElement() =
    inherit SVGElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGDefsElement()      
        

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGFontElement() =
    inherit SVGElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGFontElement()       
                

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGFontFaceElement() =
    inherit SVGElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGFontFaceElement()           
        

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGPolygonElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGPolygonElement()         



[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGPolylineElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGPolylineElement()         
        


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGPathElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGPathElement()           

    [<CompileTo("pathLength")>]
    member x.PathLength
        with get()  = 
            new SVGAnimatedNumber() 

    [<CompileTo("getTotalLength")>]
    member x.GetTotalLength() =
        0.

    [<CompileTo("getPathSegAtLength")>]
    member x.GetPathSegAtLength(distance:float) =
        0.      
             

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGLineElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGLineElement()           

    [<CompileTo("x1")>]
    member this.X1
        with get()  = 
            new SVGAnimatedLength() 

    [<CompileTo("x2")>]
    member this.X2
        with get()  = 
            new SVGAnimatedLength() 

    [<CompileTo("y1")>]
    member this.Y1
        with get()  = 
            new SVGAnimatedLength() 

    [<CompileTo("y2")>]
    member this.Y2
        with get()  = 
            new SVGAnimatedLength() 

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGImageElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGImageElement()           

    [<CompileTo("x")>]
    member this.X
        with get()  = 
            new SVGAnimatedLength() 

    [<CompileTo("y")>]
    member this.Y
        with get()  = 
            new SVGAnimatedLength() 

    [<CompileTo("width")>]
    member this.Width
        with get()  = 
            new SVGAnimatedLength() 

    [<CompileTo("height")>]
    member this.Height
        with get()  = 
            new SVGAnimatedLength() 



             