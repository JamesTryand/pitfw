namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

type SVGLength = 
    
    val mutable svglength : ScriptObject

    internal new (_svglength) =
        { svglength=_svglength }

    static member Of(obj:ScriptObject) =
        new SVGLength(obj)

    member x.UnitType 
        with get() = x.svglength.GetProperty<float>("unitType") |> int

    member x.Value
        with get() = x.svglength.GetProperty<float>("value")
        and set(v:float) = x.svglength.SetProperty("value" , box(v))

    member x.ValueInSpecifiedUnits
        with get() = x.svglength.GetProperty<float>("valueInSpecifiedUnits")
        and set(v:float) = x.svglength.SetProperty("valueInSpecifiedUnits" , box(v))    

    member x.ValueAsString
        with get() = x.svglength.GetProperty<string>("valueAsString")
        and set(v:string) = x.svglength.SetProperty("valueAsString" , box(v))

    member x.NewValueSpecifiedUnits(unitType:int , valueInSpecifiedUnits:float)
        = x.svglength.Invoke("newValueSpecifiedUnits", box(unitType) , box(valueInSpecifiedUnits)) |> ignore

    member x.ConvertToSpecifiedUnits(unitType:int)
        = x.svglength.Invoke("convertToSpecifiedUnits", box(unitType)) |> ignore

type SVGAnimatedLength =
    
    val mutable svganimatedlength : ScriptObject

    internal new (svganimatedlength) =
        { svganimatedlength = svganimatedlength }

    static member Of(obj:ScriptObject) =
        new SVGAnimatedLength(obj)

    member x.BaseVal 
        with get() = 
            let v = x.svganimatedlength.GetProperty("baseVal") :?> ScriptObject
            new SVGLength(v)

    member x.AnimVal 
        with get() = 
            let v = x.svganimatedlength.GetProperty("animVal") :?> ScriptObject
            new SVGLength(v)

type SVGAnimatedNumber =
    
    val mutable element : ScriptObject

    internal new (element) =
        { element = element }

    static member Of(obj:ScriptObject) =
        new SVGAnimatedNumber(obj)

    member x.BaseVal 
        with get() = 
            x.element.GetProperty<float>("baseVal")

    member x.AnimVal 
        with get() = 
            x.element.GetProperty<float>("animVal") 


type SVGLengthList = 
    
    val mutable svglengthlist : ScriptObject

    internal new (svglengthlist) =
        { svglengthlist=svglengthlist }

    static member Of(obj:ScriptObject) =
        new SVGLengthList(obj)
  
    member x.NumberOfItems 
        with get() = x.svglengthlist.GetProperty<float>("numberOfItems") |> int

    member x.Clear()
        = x.svglengthlist.Invoke("clear") |> ignore

    member x.Initialize(newItem:SVGLength) =
        let svg = x.svglengthlist.Invoke("initialize", box(newItem)) :?> ScriptObject 
        new SVGLength(svg)

    member x.GetItem(index:int) =
        let svg = x.svglengthlist.Invoke("getItem", box(index)) :?> ScriptObject 
        new SVGLength(svg)

    member x.InsertItemBefore(newItem:SVGLength, index:int) =
        let svg = x.svglengthlist.Invoke("insertItemBefore", box(newItem), box(index)) :?> ScriptObject 
        new SVGLength(svg)

    member x.ReplaceItem(newItem:SVGLength, index:int) =
        let svg = x.svglengthlist.Invoke("replaceItem",box(newItem),  box(index)) :?> ScriptObject 
        new SVGLength(svg)

    member x.RemoveItem(index:int) = 
        let svg = x.svglengthlist.Invoke("removeItem", box(index)) :?> ScriptObject 
        new SVGLength(svg)

    member x.AppendItem(newItem:SVGLength) =
        let svg = x.svglengthlist.Invoke("appendItem", box(newItem)) :?> ScriptObject 
        new SVGLength(svg)


type SVGNumber = 
    
    val mutable svgnumber : ScriptObject

    internal new (svgnumber) =
        { svgnumber=svgnumber }

    static member Of(obj:ScriptObject) =
        new SVGNumber(obj)

    member x.Value
        with get() = x.svgnumber.GetProperty<float>("value")
        and set(v:float) = x.svgnumber.SetProperty("value" , box(v))


type SVGNumberList = 
    
    val mutable svgnumberlist : ScriptObject

    internal new (svgnumberlist) =
        { svgnumberlist=svgnumberlist }

    static member Of(obj:ScriptObject) =
        new SVGNumberList(obj)
  
    member x.NumberOfItems
        with get() = x.svgnumberlist.GetProperty<float>("numberOfItems") |> int

    member x.Clear()
        = x.svgnumberlist.Invoke("clear") |> ignore

    member x.Initialize(newItem:SVGNumber) =
        let svg = x.svgnumberlist.Invoke("initialize", box(newItem)) :?> ScriptObject 
        new SVGNumber(svg)

    member x.GetItem(index:int) =
        let svg = x.svgnumberlist.Invoke("getItem", box(index)) :?> ScriptObject 
        new SVGNumber(svg)

    member x.InsertItemBefore(newItem:SVGNumber, index:int) =
        let svg = x.svgnumberlist.Invoke("insertItemBefore", box(newItem), box(index)) :?> ScriptObject 
        new SVGNumber(svg)

    member x.ReplaceItem(newItem:SVGNumber, index:int) =
        let svg = x.svgnumberlist.Invoke("replaceItem",box(newItem),  box(index)) :?> ScriptObject 
        new SVGNumber(svg)

    member x.RemoveItem(index:int) = 
        let svg = x.svgnumberlist.Invoke("removeItem", box(index)) :?> ScriptObject 
        new SVGNumber(svg)

    member x.AppendItem(newItem:SVGNumber) =
        let svg = x.svgnumberlist.Invoke("appendItem", box(newItem)) :?> ScriptObject 
        new SVGNumber(svg)

type SVGMatrix =

    val mutable svgmatrix : ScriptObject

    internal new (svgmatrix) =
        { svgmatrix=svgmatrix }

    static member Of(obj:ScriptObject) =
        new SVGMatrix(obj)
           
    member x.A 
        with get() = x.svgmatrix.GetProperty<float>("a") 
        and set(v:float) = x.svgmatrix.SetProperty("a" , box(v) )
    
    member x.B 
        with get() = x.svgmatrix.GetProperty<float>("b") 
        and set(v:float) = x.svgmatrix.SetProperty("b" , box(v) )
    
    member x.C 
        with get() = x.svgmatrix.GetProperty<float>("c") 
        and set(v:float) = x.svgmatrix.SetProperty("c" , box(v) )
    
    member x.D 
        with get() = x.svgmatrix.GetProperty<float>("d") 
        and set(v:float) = x.svgmatrix.SetProperty("d" , box(v) )
    
    member x.E 
        with get() = x.svgmatrix.GetProperty<float>("e") 
        and set(v:float) = x.svgmatrix.SetProperty("e" , box(v) )
    
    member x.F 
        with get() = x.svgmatrix.GetProperty<float>("f") 
        and set(v:float) = x.svgmatrix.SetProperty("f" , box(v) )

    member x.Multiply(secondMatrix:SVGMatrix) =
        let mat = x.svgmatrix.Invoke("multiply", box(secondMatrix)) :?> ScriptObject
        new SVGMatrix(mat)
        
    member x.Inverse() =
        let mat = x.svgmatrix.Invoke("inverse") :?> ScriptObject
        new SVGMatrix(mat)

    member this.Translate(x:float, y:float) =
        let mat = this.svgmatrix.Invoke("translate",box(x),box(y)) :?> ScriptObject
        new SVGMatrix(mat)

    member this.Scale(scaleFactor:float) =
        let mat = this.svgmatrix.Invoke("scale",box(scaleFactor)) :?> ScriptObject
        new SVGMatrix(mat)

    member this.ScaleNonUniform(scaleFactorX:float,scaleFactorY:float) =
        let mat = this.svgmatrix.Invoke("scaleNonUniform",box(scaleFactorX),box(scaleFactorY)) :?> ScriptObject
        new SVGMatrix(mat)
        
    member this.Rotate(angle:float) =
        let mat = this.svgmatrix.Invoke("rotate",box(angle)) :?> ScriptObject
        new SVGMatrix(mat)

    member this.RotateFromVector(x:float,y:float) =
        let mat = this.svgmatrix.Invoke("rotateFromVector",box(x), box(y)) :?> ScriptObject
        new SVGMatrix(mat)

    member this.FlipX() =
        let mat = this.svgmatrix.Invoke("flipX") :?> ScriptObject
        new SVGMatrix(mat)

    member this.FlipY(angle:float) =
        let mat = this.svgmatrix.Invoke("flipY") :?> ScriptObject
        new SVGMatrix(mat)

    member this.SkewX(angle:float) =
        let mat = this.svgmatrix.Invoke("skewX",box(angle)) :?> ScriptObject
        new SVGMatrix(mat)

    member this.SkewY(angle:float) =
        let mat = this.svgmatrix.Invoke("skewY",box(angle)) :?> ScriptObject
        new SVGMatrix(mat)

type SVGTransform =

    val mutable svgtransform : ScriptObject

    internal new (svgtransform) =
        { svgtransform=svgtransform }

    static member Of(obj:ScriptObject) =
        new SVGTransform(obj)

    member x.Type 
        with get() = x.svgtransform.GetProperty<float>("type") |> int

    member x.Matrix 
        with get() = 
            let mat = x.svgtransform.GetProperty("matrix") :?> ScriptObject
            new SVGMatrix(mat)

    member x.Angle 
        with get() = x.svgtransform.GetProperty<float>("angle")

    member x.SetMatrix(matrix:SVGMatrix) =
        x.svgtransform.Invoke("setMatrix" , box(matrix)) |> ignore

    member x.SetTranslate(tx:float, ty:float) =
        x.svgtransform.Invoke("setTranslate" , box(tx),box(ty)) |> ignore

    member x.SetScale(sx:float, sy:float) =
        x.svgtransform.Invoke("setScale" , box(sx),box(sy)) |> ignore

    member x.SetRotate(angle:float, cx:float, cy:float) =
        x.svgtransform.Invoke("setRotate" , box(angle), box(cx),box(cy)) |> ignore
  
    member x.SetSkewX(angle:float) =
        x.svgtransform.Invoke("setSkewX" , box(angle)) |> ignore

    member x.SetSkewY(angle:float) =
        x.svgtransform.Invoke("setSkewY" , box(angle)) |> ignore


type SVGTransformList = 
    
    val mutable svgtransformlist : ScriptObject

    internal new (svgtransformlist) =
        { svgtransformlist=svgtransformlist }

    static member Of(obj:ScriptObject) =
        new SVGTransformList(obj)
  
    member x.NumberOfItems
        with get() = x.svgtransformlist.GetProperty<float>("numberOfItems") |> int

    member x.Clear()
        = x.svgtransformlist.Invoke("clear") |> ignore

    member x.Initialize(newItem:SVGTransform) =
        let svg = x.svgtransformlist.Invoke("initialize", box(newItem)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.GetItem(index:int) =
        let svg = x.svgtransformlist.Invoke("getItem", box(index)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.InsertItemBefore(newItem:SVGTransform, index:int) =
        let svg = x.svgtransformlist.Invoke("insertItemBefore", box(newItem), box(index)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.ReplaceItem(newItem:SVGTransform, index:int) =
        let svg = x.svgtransformlist.Invoke("replaceItem",box(newItem),  box(index)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.RemoveItem(index:int) = 
        let svg = x.svgtransformlist.Invoke("removeItem", box(index)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.AppendItem(newItem:SVGTransform) =
        let svg = x.svgtransformlist.Invoke("appendItem", box(newItem)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.CreateSVGTransformFromMatrix(matrix:SVGMatrix) =
        let svg = x.svgtransformlist.Invoke("createSVGTransformFromMatrix", box(matrix)) :?> ScriptObject 
        new SVGTransform(svg)

    member x.Consolidate() =
        let svg = x.svgtransformlist.Invoke("consolidate") :?> ScriptObject 
        new SVGTransform(svg)

//http://www.w3.org/TR/SVG/coords.html#InterfaceSVGAnimatedTransformList
type SVGAnimatedTransformList =
    
    val mutable list : ScriptObject

    internal new (list) =
        { list = list }

    static member Of(obj:ScriptObject) =
        new SVGAnimatedTransformList(obj)

    member x.BaseVal 
        with get() = 
            let v = x.list.GetProperty("baseVal") :?> ScriptObject
            new SVGTransformList(v)

    member x.AnimVal 
        with get() = 
            let v = x.list.GetProperty("animVal") :?> ScriptObject
            new SVGTransformList(v)

type SVGAnimatedString =
    
    val mutable str : ScriptObject

    internal new (str) =
        { str = str }

    static member Of(obj:ScriptObject) =
        new SVGAnimatedString(obj)

    member x.BaseVal 
        with get() = 
            x.str.GetProperty<String>("baseVal")

    member x.AnimVal 
        with get() = 
            x.str.GetProperty<string>("animVal") 

type SVGAnimatedEnumeration  =
    
    val mutable str : ScriptObject

    internal new (str) =
        { str = str }

    static member Of(obj:ScriptObject) =
        new SVGAnimatedEnumeration (obj)

    member x.BaseVal 
        with get() = 
            x.str.GetProperty<String>("baseVal")

    member x.AnimVal 
        with get() = 
            x.str.GetProperty<string>("animVal") 

type SVGTransformable =
    
    val mutable str : ScriptObject

    internal new (str) =
        { str = str }

    static member Of(obj:ScriptObject) =
        new SVGTransformable(obj)

    member x.Transform 
        with get() = 
            let svg = x.str.GetProperty("transform") :?> ScriptObject
            new SVGAnimatedTransformList(svg)