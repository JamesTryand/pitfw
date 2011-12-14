namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type SVGElement =
    inherit DomElement

    val mutable svgelement : ScriptObject

    internal new (_svgelement) =
        { inherit DomElement(_svgelement); svgelement=_svgelement }

    static member Of(el:DomElement) =
        new SVGElement(el.InternalScriptObject)            

    member x.Xmlbase
        with get()  = x.svgelement.GetProperty("xmlbase") |> string

    member x.OwnerSVGElement
        with get()  = 
            let svg = x.svgelement.GetProperty("ownerSVGElement") :?> ScriptObject
            new SVGSVGElement(svg)

    member x.ViewportElement
        with get()  = 
            let svg = x.svgelement.GetProperty("viewportElement") :?> ScriptObject
            new SVGElement(svg)        
        
and      
    [<AllowNullLiteral>]
    SVGSVGElement =
        inherit DomElement

        val mutable svgsvgelement : ScriptObject

        internal new (_svgsvgelement) =
            { inherit DomElement(_svgsvgelement); svgsvgelement=_svgsvgelement }
    
        static member Of(el:DomElement) =
            new SVGSVGElement(el.InternalScriptObject)

        member this.X
            with get()  = 
                let svg = this.svgsvgelement.GetProperty("x") :?> ScriptObject
                new SVGAnimatedLength(svg)

        member x.Y
            with get()  = 
                let svg = x.svgsvgelement.GetProperty("y") :?> ScriptObject
                new SVGAnimatedLength(svg)

        member x.Height
            with get()  = 
                let svg = x.svgsvgelement.GetProperty("height") :?> ScriptObject
                new SVGAnimatedLength(svg)

        member x.Width
            with get()  = 
                let svg = x.svgsvgelement.GetProperty("width") :?> ScriptObject
                new SVGAnimatedLength(svg) 

        member x.ContentScriptType
            with get()  =  
                x.svgsvgelement.GetProperty<string>("contentScriptType") 

        member x.ContentStyleType
            with get()  =  
                x.svgsvgelement.GetProperty<string>("contentStyleType")
                
        member x.PixelUnitToMillimeterX
            with get()  =  
                x.svgsvgelement.GetProperty<float>("pixelUnitToMillimeterX")                
                
        member x.PixelUnitToMillimeterY
            with get()  =  
                x.svgsvgelement.GetProperty<float>("pixelUnitToMillimeterY")
                       
        member x.ScreenPixelToMillimeterX
            with get()  =  
                x.svgsvgelement.GetProperty<float>("screenPixelToMillimeterX")                
                
        member x.ScreenPixelToMillimeterY
            with get()  =  
                x.svgsvgelement.GetProperty<float>("screenPixelToMillimeterY")

        member x.UseCurrentView
            with get()  =  
                x.svgsvgelement.GetProperty<bool>("useCurrentView")                
                
        member x.currentScale
            with get()  =  
                x.svgsvgelement.GetProperty<float>("currentScale")                              
                

        member x.SuspendRedraw(maxWaitMilliseconds:int) =
            x.svgsvgelement.Invoke("suspendRedraw", box(maxWaitMilliseconds)).ToString() |> int 
           
        member x.UnsuspendRedraw(suspendHandleID:int) =
            x.svgsvgelement.Invoke("unsuspendRedraw", box(suspendHandleID)) |> ignore
            
        member x.UnsuspendRedrawAll() =
            x.svgsvgelement.Invoke("unsuspendRedrawAll") |> ignore

        member x.ForceRedraw() =
            x.svgsvgelement.Invoke("forceRedraw") |> ignore 

        member x.PauseAnimations() =
            x.svgsvgelement.Invoke("pauseAnimations") |> ignore

        member x.UnpauseAnimations() =
            x.svgsvgelement.Invoke("unpauseAnimations") |> ignore

        member x.AnimationsPaused() =
            x.svgsvgelement.Invoke("animationsPaused") |> ignore

        member x.GetCurrentTime() =
            x.svgsvgelement.Invoke("getCurrentTime").ToString() |> float 

        member x.SetCurrentTime(seconds:float) =
            x.svgsvgelement.Invoke("setCurrentTime", box(seconds)) |> ignore  

        member x.DeselectAll() = 
            x.svgsvgelement.Invoke("deselectAll") |> ignore  

        member x.CreateSVGNumber() = 
            let svg = x.svgsvgelement.Invoke("createSVGNumber") :?> ScriptObject
            new SVGNumber(svg)

        member x.CreateSVGLength() = 
            let svg = x.svgsvgelement.Invoke("createSVGLength") :?> ScriptObject
            new SVGLength(svg)

[<AllowNullLiteral>]
type SVGTransformableElement =
    inherit SVGElement

    val mutable element : ScriptObject

    internal new (element) =
        { inherit SVGElement(element); element=element }

    static member Of(el:DomElement) =
        new SVGTransformableElement(el.InternalScriptObject)           
      
    member x.Transform 
        with get() = 
            let svg = x.element.GetProperty("transform") :?> ScriptObject
            new SVGAnimatedTransformList(svg) 
