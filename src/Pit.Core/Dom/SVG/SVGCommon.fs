namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGLength() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGLength()

    [<CompileTo("unitType")>]
    member x.UnitType
        with get() = 0

    [<CompileTo("value")>]
    member x.Value
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("valueInSpecifiedUnits")>]
    member x.ValueInSpecifiedUnits
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("valueAsString")>]
    member x.ValueAsString
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("newValueSpecifiedUnits")>]
    member x.NewValueSpecifiedUnits(unitType:int , valueInSpecifiedUnits:float)
        = ()

    [<CompileTo("convertToSpecifiedUnits")>]
    member x.ConvertToSpecifiedUnits(unitType:int)
        = ()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimatedLength() =
   
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGAnimatedLength()

    [<CompileTo("baseVal")>]
    member x.BaseVal 
        with get() = new SVGLength()

    [<CompileTo("animVal")>]
    member x.AnimVal 
        with get() =
            new SVGLength()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGLengthList() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGLengthList()

    [<CompileTo("numberOfItems")>]
    member x.NumberOfItems
        with get() = 0

    [<CompileTo("clear")>]
    member x.Clear() = ()

    [<CompileTo("initialize")>]
    member x.Initialize(newItem:SVGLength) =
        new SVGLength()

    [<CompileTo("getItem")>]
    member x.GetItem(index:int) =
        new SVGLength()

    [<CompileTo("insertItemBefore")>]
    member x.InsertItemBefore(newItem:SVGLength, index:int) =
        new SVGLength()

    [<CompileTo("replaceItem")>]
    member x.ReplaceItem(newItem:SVGLength, index:int) =
        new SVGLength()

    [<CompileTo("removeItem")>]
    member x.RemoveItem(index:int) = 
        new SVGLength()

    [<CompileTo("appendItem")>]
    member x.AppendItem(newItem:SVGLength) =
        new SVGLength()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGNumber() = 

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGNumber()

    [<CompileTo("value")>]
    member x.Value
        with get() = 0.
        and set(v:float) = ()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGNumberList() = 
    
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGNumberList()
  
    [<CompileTo("numberOfItems")>]
    member x.NumberOfItems
        with get() = 0

    [<CompileTo("clear")>]
    member x.Clear()
        = ()

    [<CompileTo("initialize")>]
    member x.Initialize(newItem:SVGNumber) =
        new SVGNumber()

    [<CompileTo("getItem")>]
    member x.GetItem(index:int) =
        new SVGNumber()

    [<CompileTo("insertItemBefore")>]
    member x.InsertItemBefore(newItem:SVGNumber, index:int) =
        new SVGNumber()
        
    [<CompileTo("replaceItem")>]
    member x.ReplaceItem(newItem:SVGNumber, index:int) =
        new SVGNumber()

    [<CompileTo("removeItem")>]
    member x.RemoveItem(index:int) = 
        new SVGNumber()

    [<CompileTo("appendItem")>]
    member x.AppendItem(newItem:SVGNumber) =
        new SVGNumber()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGMatrix() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGMatrix()
          
    [<CompileTo("a")>] 
    member x.A 
        with get() = 0.
        and set(v:float) = ()
    
    [<CompileTo("b")>]
    member x.B 
        with get() = 0.
        and set(v:float) = ()

    [<CompileTo("c")>]
    member x.C 
        with get() = 0.
        and set(v:float) = ()
    
    [<CompileTo("d")>]
    member x.D 
        with get() = 0.
        and set(v:float) = ()
    
    [<CompileTo("e")>]
    member x.E 
        with get() = 0.
        and set(v:float) = ()
    
    [<CompileTo("f")>]
    member x.F 
        with get() = 0.
        and set(v:float) = ()

    [<CompileTo("multiply")>]
    member x.Multiply(secondMatrix:SVGMatrix) =
        new SVGMatrix()
        
    [<CompileTo("inverse")>]
    member x.Inverse() =
        new SVGMatrix()

    [<CompileTo("translate")>]
    member this.Translate(x:float, y:float) =
        new SVGMatrix()

    [<CompileTo("scale")>]
    member this.Scale(scaleFactor:float) =
        new SVGMatrix()

    [<CompileTo("scaleNonUniform")>]
    member this.ScaleNonUniform(scaleFactorX:float,scaleFactorY:float) =
        new SVGMatrix()
        
    [<CompileTo("rotate")>]
    member this.Rotate(angle:float) =
        new SVGMatrix()

    [<CompileTo("rotateFromVector")>]
    member this.RotateFromVector(x:float,y:float) =
        new SVGMatrix()

    [<CompileTo("flipX")>]
    member this.FlipX() =
        new SVGMatrix()

    [<CompileTo("flipY")>]
    member this.FlipY(angle:float) =
        new SVGMatrix()

    [<CompileTo("skewX")>]
    member this.SkewX(angle:float) =
        new SVGMatrix()

    [<CompileTo("skewY")>]
    member this.SkewY(angle:float) =
        new SVGMatrix()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGTransform() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGTransform()

    [<CompileTo("type")>]
    member x.Type 
        with get() = 0

    [<CompileTo("matrix")>]
    member x.Matrix 
        with get() = 
            new SVGMatrix()

    [<CompileTo("angle")>]
    member x.Angle 
        with get() = 0.

    [<CompileTo("setMatrix")>]
    member x.SetMatrix(matrix:SVGMatrix) = ()

    [<CompileTo("setTranslate")>]
    member x.SetTranslate(tx:float, ty:float) = ()

    [<CompileTo("setScale")>]
    member x.SetScale(sx:float, sy:float) = ()

    [<CompileTo("setRotate")>]
    member x.SetRotate(angle:float, cx:float, cy:float) = ()
    
    [<CompileTo("setSkewX")>]
    member x.SetSkewX(angle:float) = ()

    [<CompileTo("setSkewY")>]
    member x.SetSkewY(angle:float) = ()


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGTransformList() = 
    
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGTransformList()
  
    [<CompileTo("numberOfItems")>]
    member x.NumberOfItems
        with get() = 0

    [<CompileTo("clear")>]
    member x.Clear() = ()

    [<CompileTo("initialize")>]
    member x.Initialize(newItem:SVGTransform) =
        new SVGTransform()

    [<CompileTo("getItem")>]
    member x.GetItem(index:int) =
        new SVGTransform()

    [<CompileTo("insertItemBefore")>]
    member x.InsertItemBefore(newItem:SVGTransform, index:int) =
        new SVGTransform()

    [<CompileTo("replaceItem")>]
    member x.ReplaceItem(newItem:SVGTransform, index:int) =
        new SVGTransform()

    [<CompileTo("removeItem")>]
    member x.RemoveItem(index:int) = 
        new SVGTransform()

    [<CompileTo("appendItem")>]
    member x.AppendItem(newItem:SVGTransform) =
        new SVGTransform()

    [<CompileTo("createSVGTransformFromMatrix")>]
    member x.CreateSVGTransformFromMatrix(matrix:SVGMatrix) =
        new SVGTransform()

    [<CompileTo("consolidate")>]
    member x.Consolidate() =
        new SVGTransform()

//http://www.w3.org/TR/SVG/coords.html#InterfaceSVGAnimatedTransformList
[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimatedTransformList() =
        
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGAnimatedTransformList()

    [<CompileTo("baseVal")>]
    member x.BaseVal 
        with get() = 
            new SVGTransformList()

    [<CompileTo("animVal")>]
    member x.AnimVal 
        with get() = 
            new SVGTransformList()

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimatedString() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGAnimatedString()
        
    [<CompileTo("baseVal")>]
    member x.BaseVal 
        with get() = ""

    [<CompileTo("animVal")>]
    member x.AnimVal 
        with get() = ""


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimatedEnumeration() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGAnimatedEnumeration()
        
    [<CompileTo("baseVal")>]
    member x.BaseVal 
        with get() = ""

    [<CompileTo("animVal")>]
    member x.AnimVal 
        with get() = ""


[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGAnimatedNumber() =

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGAnimatedNumber()
        
    [<CompileTo("baseVal")>]
    member x.BaseVal 
        with get() = 0.

    [<CompileTo("animVal")>]
    member x.AnimVal 
        with get() = 0.



[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type SVGTransformable() =
    
    [<JsIgnore(IgnoreCall=true)>]
    static member Of(obj) =
        new SVGTransformable()

    [<CompileTo("transform")>]
    member x.Transform 
        with get() = 
            new SVGAnimatedTransformList()
