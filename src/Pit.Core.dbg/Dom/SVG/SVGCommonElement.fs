namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type SVGClipPathElement  =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGClipPathElement (el.InternalScriptObject)       
        
    member this.ClipPathUnits 
        with get() = 
            let e = this.element.GetProperty("clipPathUnits") :?> ScriptObject
            new SVGAnimatedEnumeration(e)
    

[<AllowNullLiteral>]
type SVGDefsElement   =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGDefsElement(el.InternalScriptObject)       
        

[<AllowNullLiteral>]
type SVGDescElement   =
    inherit SVGElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGDescElement(el.InternalScriptObject)       


[<AllowNullLiteral>]
type SVGFontElement   =
    inherit SVGElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGFontElement(el.InternalScriptObject)   
        

[<AllowNullLiteral>]
type SVGFontFaceElement   =
    inherit SVGElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGFontFaceElement(el.InternalScriptObject)    
        
        

[<AllowNullLiteral>]
type SVGPolygonElement =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGPolygonElement(el.InternalScriptObject)          


[<AllowNullLiteral>]
type SVGPolylineElement =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGPolylineElement(el.InternalScriptObject)     
        
        
[<AllowNullLiteral>]
type SVGPathElement =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGPathElement(el.InternalScriptObject)                  
        
    member x.PathLength
        with get()  = 
            let svg = x.element.GetProperty("pathLength") :?> ScriptObject
            new SVGAnimatedNumber(svg) 
                
    member x.GetTotalLength() =
        x.element.Invoke("getTotalLength").ToString() |> float        

    member x.GetPathSegAtLength(distance:float) =
        x.element.Invoke("getPathSegAtLength", box(distance)).ToString() |> float        


        
[<AllowNullLiteral>]
type SVGLineElement =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGLineElement(el.InternalScriptObject)                  
        
    member this.X1
        with get()  = 
            let svg = this.element.GetProperty("x1") :?> ScriptObject
            new SVGAnimatedLength(svg) 

    member this.X2
        with get()  = 
            let svg = this.element.GetProperty("x2") :?> ScriptObject
            new SVGAnimatedLength(svg) 

    member this.Y1
        with get()  = 
            let svg = this.element.GetProperty("y1") :?> ScriptObject
            new SVGAnimatedLength(svg) 

    member this.Y2
        with get()  = 
            let svg = this.element.GetProperty("y2") :?> ScriptObject
            new SVGAnimatedLength(svg) 
                
                
[<AllowNullLiteral>]
type SVGImageElement =
    inherit SVGTransformableElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGTransformableElement(element); element=element }

    static member Of(el:DomElement) = 
        new SVGImageElement(el.InternalScriptObject)                  
        
    member this.X
        with get()  = 
            let svg = this.element.GetProperty("x") :?> ScriptObject
            new SVGAnimatedLength(svg) 

    member this.Y
        with get()  = 
            let svg = this.element.GetProperty("y") :?> ScriptObject
            new SVGAnimatedLength(svg) 

    member this.Width
        with get()  = 
            let svg = this.element.GetProperty("width") :?> ScriptObject
            new SVGAnimatedLength(svg) 

    member this.Height
        with get()  = 
            let svg = this.element.GetProperty("height") :?> ScriptObject
            new SVGAnimatedLength(svg) 
                
             
        
            
        
        