namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGElement() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj:DomElement) =
        new SVGElement()

    [<CompileTo("xmlbase")>]
    member x.Xmlbase
        with get()  = ""

    [<CompileTo("ownerSVGElement")>]
    member x.OwnerSVGElement
        with get()  = 
            new SVGSVGElement()

    [<CompileTo("viewportElement")>]
    member x.ViewportElement
        with get()  = 
            new SVGElement()        
        
and      
    //Reference - https://developer.mozilla.org/en/DOM/SVGSVGElement
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    SVGSVGElement() =
        inherit DomElement()
        
        [<JsIgnore(IgnoreCall=true)>]
        static member Of(el:DomElement) =
            new SVGSVGElement()
        
        [<CompileTo("x")>]
        member this.X
            with get()  = 
                new SVGAnimatedLength()

        [<CompileTo("y")>]
        member this.Y
            with get()  = 
                new SVGAnimatedLength()

        [<CompileTo("height")>]
        member x.Height
            with get()  = new SVGAnimatedLength()

        [<CompileTo("width")>]
        member x.Width
            with get()  = 
                new SVGAnimatedLength() 

        [<CompileTo("contentScriptType")>]
        member x.ContentScriptType
            with get()  =  
                ""

        [<CompileTo("contentStyleType")>]
        member x.ContentStyleType
            with get()  =  
                ""
                
        [<CompileTo("pixelUnitToMillimeterX")>]
        member x.PixelUnitToMillimeterX
            with get()  =  
                0.
                
        [<CompileTo("pixelUnitToMillimeterY")>]
        member x.PixelUnitToMillimeterY
            with get()  =  
                0.
                       
        [<CompileTo("screenPixelToMillimeterX")>]
        member x.ScreenPixelToMillimeterX
            with get()  =  
                0.
                
        [<CompileTo("screenPixelToMillimeterY")>]
        member x.ScreenPixelToMillimeterY
            with get()  =  
                0.

        [<CompileTo("useCurrentView")>]
        member x.UseCurrentView
            with get()  =  
                true
                
        [<CompileTo("currentScale")>]
        member x.CurrentScale
            with get()  =  
                0.
                
        [<CompileTo("suspendRedraw")>]
        member x.SuspendRedraw(maxWaitMilliseconds:int) =
            0
           
        [<CompileTo("unsuspendRedraw")>]
        member x.UnsuspendRedraw(suspendHandleID:int) =
            ()
            
        [<CompileTo("unsuspendRedrawAll")>]
        member x.UnsuspendRedrawAll() =
            ()

        [<CompileTo("forceRedraw")>]
        member x.ForceRedraw() =
            ()

        [<CompileTo("pauseAnimations")>]
        member x.PauseAnimations() =
            ()

        [<CompileTo("unpauseAnimations")>]
        member x.UnpauseAnimations() =
            ()

        [<CompileTo("animationsPaused")>]
        member x.AnimationsPaused() =
            ()

        [<CompileTo("getCurrentTime")>]
        member x.GetCurrentTime() =
            0.

        [<CompileTo("setCurrentTime")>]
        member x.SetCurrentTime(seconds:float) =
            ()

        [<CompileTo("deselectAll")>]
        member x.DeselectAll() = 
            ()

        [<CompileTo("createSVGNumber")>]
        member x.CreateSVGNumber() = 
            new SVGNumber()

        [<CompileTo("createSVGLength")>]
        member x.CreateSVGLength() = 
            new SVGLength()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGTransformableElement() =
    inherit SVGElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj:DomElement) =
        new SVGTransformableElement()

    [<CompileTo("transform")>]
    member x.Transform 
        with get() = 
            new SVGAnimatedTransformList() 