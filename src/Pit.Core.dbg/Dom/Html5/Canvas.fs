namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom
open Pit.Javascript
open System.Windows.Browser

[<AllowNullLiteral>]
type Canvas =
    inherit DomElement
    val mutable canvas: ScriptObject

    internal new (_canvas) =
        { inherit DomElement(_canvas); canvas=_canvas }

    static member Of(el:DomElement) =
        new Canvas(el.InternalScriptObject)

    member x.Width
        with get() = x.canvas.GetProperty<float>("width")
        and set(v: float) = x.canvas.SetProperty("width", v)

    member x.Height
        with get() = x.canvas.GetProperty<float>("height")
        and set(v: float) = x.canvas.SetProperty("height", v)

    member x.ToDataUrl() =
        x.canvas.Invoke("toDataUrl", [||]) |> ignore

    member x.ToDataUrl(dataType:string) =
        x.canvas.Invoke("toDataUrl", [|box(dataType)|]) |> ignore

    member x.GetContext(d:string) =
        let context = x.canvas |> invoke2 "getContext" [| d |]
        new Canvas2DRenderingContext(context :?> ScriptObject)

and Canvas2DRenderingContext =
    val mutable context: ScriptObject

    internal new(context : ScriptObject) = { context = context }

    member x.Canvas
        with get() =
           new Canvas(x.context.GetProperty<ScriptObject>("canvas"))

    member x.GlobalAlpha
        with get() = x.context.GetProperty<float>("globalAlpha")
        and set(v: float) = x.context.SetProperty("globalAlpha", v)

    (*member x.GlobalCompositeOperation
        with get() =
            match x.context.GetProperty<string>("globalCompositeOperation") with
            | "source-over" -> GlobalCompositeOperation.SourceOver
            | "source-in" -> GlobalCompositeOperation.SourceIn
            | "source-out" -> GlobalCompositeOperation.SourceOut
            | "source-atop" -> GlobalCompositeOperation.SourceATop
            | "destination-over" -> GlobalCompositeOperation.DestinationOver
            | "destination-in" -> GlobalCompositeOperation.DestinationIn
            | "destination-out" -> GlobalCompositeOperation.DestinationOut
            | "destination-atop" -> GlobalCompositeOperation.DestionationATop
            | "lighter" -> GlobalCompositeOperation.Lighter
            | "copy" -> GlobalCompositeOperation.Copy
            | "xor" -> GlobalCompositeOperation.Xor
            | _ -> GlobalCompositeOperation.SourceOver
        and set(v: GlobalCompositeOperation) = x.context.SetProperty("globalCompositeOperation", v.ToString())*)

    member x.GlobalCompositeOperation
        with get() = x.context.GetProperty<string>("globalCompositeOperation")
        and set(v: string) = x.context.SetProperty("globalCompositeOperation", v)

    member x.LineWidth
        with get() = x.context.GetProperty<float>("lineWidth")
        and set(v: float) = x.context.SetProperty("lineWidth", v)

    member x.LineCap
        with get() = x.context.GetProperty<string>("lineCap")
        and set(v: string) = x.context.SetProperty("lineCap", v)

    member x.LineJoin
        with get() = x.context.GetProperty<string>("lineJoin")
        and set(v: string) = x.context.SetProperty("lineJoin", v)

    member x.MiterLimit
        with get() = x.context.GetProperty<float>("miterLimit")
        and set(v: float) = x.context.SetProperty("miterLimit", v)

    member x.StrokeStyle
        with get() = x.context.GetProperty<string>("strokeStyle")
        and set(v: string) = x.context.SetProperty("strokeStyle", v)

    member x.FillStyle
        with get() = x.context.GetProperty<string>("fillStyle")
        and set(v: string) = x.context.SetProperty("fillStyle", v)

    member x.FillPatternStyle
        with get() =
            let pattern = x.context.GetProperty<ScriptObject>("fillStyle")
            new CanvasPattern(pattern)
        and set(v: CanvasPattern) = x.context.SetProperty("fillStyle", v.InternalScriptObject)

    member x.FillGradientStyle
        with get() =
            let gradient = x.context.GetProperty<ScriptObject>("fillStyle")
            new CanvasGradient(gradient)
        and set(v: CanvasGradient) = x.context.SetProperty("fillStyle", v.InternalScriptObject)

    member x.ShadowOffsetX
        with get() = x.context.GetProperty<float>("shadowOffsetX")
        and set(v: float) = x.context.SetProperty("shadowOffsetX", v)

    member x.ShadowOffsetY
        with get() = x.context.GetProperty<float>("shadowOffsetY")
        and set(v: float) = x.context.SetProperty("shadowOffsetY", v)

    member x.ShadowBlur
        with get() = x.context.GetProperty<float>("shadowBlur")
        and set(v: float) = x.context.SetProperty("shadowBlur", v)

    member x.ShadowColor
        with get() = x.context.GetProperty<string>("shadowColor")
        and set(v: string) = x.context.SetProperty("shadowColor", v)

    member x.Font
        with get() = x.context.GetProperty<string>("font")
        and set(v: string) = x.context.SetProperty("font", v)

    member x.TextAlign
        with get() = x.context.GetProperty<string>("textAlign")
        and set(v: string) = x.context.SetProperty("textAlign", v)

    member x.TextBaseline
        with get() = x.context.GetProperty<string>("textBaseline")
        and set(v: string) = x.context.SetProperty("textBaseline", v)

    member x.Save() =
        x.context.Invoke("save", [||]) |> ignore

    member x.Restore() =
        x.context |> invoke ("restore") [||]

    member x.Scale(x1: float, y1: float) =
        x.context |> invoke ("scale") [| x1; y1;|]

    member x.Rotate(angle: float) =
        x.context |> invoke("rotate") [| angle |]

    member x.Translate(x1: float, y1: float) =
        x.context |> invoke "translate" [| x1; y1 |]

    member x.Transform(m11: float, m12: float, m21: float, m22: float, dx: float, dy: float) =
        x.context |> invoke "transform" [|m11; m12; m21; m22; dx; dy |]

    member x.SetTransform(m11: float, m12: float, m21: float, m22: float, dx: float, dy: float) =
        x.context |> invoke "setTransform" [| m11; m12; m21; m22; dx; dy |]

    member x.DrawImageElement(image: DomImage, dx: float, dy: float) =
        x.context |> invoke "drawImage" [| box(image.image); box(dx); box(dy); |]

    member x.DrawImageElement(image: DomImage, dx: float, dy: float, dw: float, dh: float) =
        x.context |> invoke "drawImage" [| box(image.image); box(dx); box(dy); box(dw); box(dh) |]

    member x.DrawCanvasElement(canvas: Canvas,  dx: float, dy: float) =
        x.context |> invoke "drawImage" [| box(canvas.canvas); box(dx); box(dy); |]

    member x.DrawCanvasElement(canvas: Canvas,  dx: float, dy: float, dw: float, dh: float) =
        x.context |> invoke "drawImage" [| box(canvas.canvas); box(dx); box(dy); box(dw); box(dh) |]

    member x.CreateLinearGradient(x0: float, y0: float, x1: float, y1: float) =
        let gradient = (x.context |> invoke2 "createLinearGradient" [|box(x0);box(y0);box(x1);box(y1)|]) :?> ScriptObject
        new CanvasGradient(gradient)

    member x.CreateRadialGradient(x0: float, y0: float, r0: float, x1: float, y1: float, r1: float) =
        let gradient = (x.context |> invoke2 "createRadialGradient" [|box(x0);box(y0);box(r0);box(x1);box(y1);box(r1)|]) :?> ScriptObject
        new CanvasGradient(gradient)

    member x.CreatePattern(image: DomImage, repetition: string) =
        let pattern = x.context |> invoke2 "createPattern" [|box(image.image);box(repetition);|] :?> ScriptObject
        new CanvasPattern(pattern)

    member x.BeginPath() =
        x.context |> invoke "beginPath" [||]

    member x.ClosePath() =
        x.context |> invoke "closePath" [||]

    member x.Fill() =
        x.context |> invoke "fill" [||]

    member x.Stroke() =
        x.context |> invoke "stroke" [||]

    member x.Clip() =
        x.context |> invoke "clip" [||]

    member x.MoveTo(x1: float, y1: float) =
        x.context |> invoke "moveTo" [| x1; y1 |]

    member x.LineTo(x1: float, y1: float) =
        x.context |> invoke "lineTo" [|x1; y1|]

    member x.QuadraticCurveTo(cpx: float, cpy: float, x1: float, y1: float) =
        x.context |> invoke "quadraticCurveTo" [| cpx; cpy; x1; y1 |]

    member x.BezierCurveTo(cp1x: float, cp1y: float, cp2x: float, cp2y: float, x1: float, y1: float) =
        x.context |> invoke "bezierCurveTo" [| cp1x; cp1y; cp2x; cp2y; x1; y1 |]

    member x.ArcTo(x1: float, y1: float, x2: float, y2: float, radius: float) =
        x.context |> invoke "arcTo" [| x1; y1; x2; y2; radius |]

    member x.Arc(x1: float, y1: float, radius: float, startAngle: float, endAngle: float, antiClockwise: bool) =
        x.context |> invoke "arc" [| x1; y1; radius; startAngle; endAngle; antiClockwise; |]

    member x.Rect(x1: float, y1: float, w: float, h: float) =
        x.context |> invoke "rect" [|x1; y1; w; h;|]

    member x.IsPointInPath(x1: float, y1: float) =
        x.context |> invoke "isPointInPath" [| x1; y1 |]

    member x.FillText(text: string, x1: float, y1: float) =
        x.context |> invoke "fillText" [| box(text); box(x1); box(y1); |]

    member x.FillText(text: string, x1: float, y1: float, maxWidth: float) =
        x.context |> invoke "fillText" [| box(text); box(x1); box(y1); box(maxWidth) |]

    member x.StrokeText(text: string, x1: float, y1: float) =
        x.context |> invoke "strokeText" [| box(text); box(x1); box(y1); |]

    member x.StrokeText(text: string, x1: float, y1: float, maxWidth: float) =
        x.context |> invoke "strokeText" [| box(text); box(x1); box(y1); box(maxWidth) |]

    member x.MeasureText(text : string) =
        let textMetric = (x.context |> invoke2 "measureText" [| text |]) :?> ScriptObject
        new TextMetrics(textMetric)

    member x.ClearRect(x1: float, y1: float, w: float, h: float) =
        x.context |> invoke "clearRect" [| x1; y1; w; h; |]

    member x.FillRect(x1: float, y1: float, w: float, h: float) =
        x.context |> invoke "fillRect" [| x1; y1; w; h; |]

    member x.StrokeRect(x1: float, y1: float, w: float, h: float) =
        x.context |> invoke "strokeRect" [| x1; y1; w; h; |]

    member x.CreateImageData(sw: float, sh: float) =
        let imgd = (x.context |> invoke2 "createImageData" [| box(sw); box(sh) |]) :?> ScriptObject
        new ImageData(imgd)

    member x.GetImageData(sx: float, sy: float, sw: float, sh: float) =
        let imgd = (x.context |> invoke2 "getImageData" [| box(sx); box(sy); box(sw); box(sh) |]) :?> ScriptObject
        new ImageData(imgd)

    member x.PutImageData(imageData: ImageData, dx: float, dy: float) =
        x.context |> invoke "putImageData" [| box(imageData.imageData); box(dx); box(dy); |]

    member x.PutImageData(imageData: ImageData, dx: float, dy: float, dirtyX: float, dirtyY: float, dirtyWidth: float, dirtyHeight: float) =
        x.context |> invoke "putImageData" [| box(imageData.imageData); box(dx); box(dy); box(dirtyX); box(dirtyY); box(dirtyWidth); box(dirtyHeight) |]

and CanvasGradient(gradient : ScriptObject) =

    member internal x.InternalScriptObject = gradient

    member x.AddColorStop(offset: float, color: string) =
        gradient |> invoke "addColorStop" [| box(offset); box(color) |]

and CanvasPattern(pattern: ScriptObject) =

    member internal x.InternalScriptObject = pattern

and TextMetrics(textMetric: ScriptObject) =

    member internal x.InternalScriptObject = textMetric

    member x.Width
        with get() = textMetric.GetProperty<float>("width")

and ImageData =
    val imageData: ScriptObject
    [<DefaultValue>]
    val mutable pixelArray: CanvasPixelArray
    internal new(imageData: ScriptObject) = { imageData = imageData }

    member x.Width
        with get() = x.imageData.GetProperty<float>("width")

    member x.Height
        with get() = x.imageData.GetProperty<float>("height")

    member x.Data
        with get() =
            if x.pixelArray = null then
                x.pixelArray <- new CanvasPixelArray(x.Width |> int, x.Height |> int)
            x.pixelArray

and
    [<AllowNullLiteral>]
    CanvasPixelArray =
    val arr : ScriptObject
    val length : int
    internal new(w:int, h:int) as x =
        { arr = HtmlPage.Window.CreateInstance("Array", null)
          length = w*h*4
        } then
            // http://www.w3.org/TR/2dcontext/#canvaspixelarray, array of w*h*4
            for i = 0 to x.length - 1 do
                x.arr.SetProperty(i, 0)

    member x.Length
        with get() = x.length

    member x.Item
        with get(i:int) = x.arr.GetProperty(i) :?> float |> int
        and set(i:int) (v:int) = x.arr.SetProperty(i, v)