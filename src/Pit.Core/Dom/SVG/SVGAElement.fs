namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAElement() =
    inherit SVGTransformableElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new SVGAElement()            

    [<CompileTo("target")>]
    member x.Target 
        with get() = 
            new SVGAnimatedString() 