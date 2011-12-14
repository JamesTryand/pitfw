namespace Pit.Dom.Html5

open System
open Pit
open Pit.Dom

[<JsIgnoreAttribute(IgnoreGetSet=true)>]
type Canvas() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement)=
        new Canvas()

    [<CompileTo("width")>]
    member x.Width
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("height")>]
    member x.Height
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("toDataURL")>]
    member x.ToDataUrl() = ""

    [<CompileTo("toDataURL")>]
    member x.ToDataUrl(dataType:string) = ""

    [<CompileTo("getContext")>]
    member x.GetContext(d:string) =
        new Canvas2DRenderingContext()

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    Canvas2DRenderingContext() =

    [<CompileTo("canvas")>]
    member x.Canvas
        with get() = Canvas()

    [<CompileTo("globalAlpha")>]
    member x.GlobalAlpha
        with get() = 1.
        and set(v: float) = ()

    [<CompileTo("globalCompositeOperation")>]
    member x.GlobalCompositeOperation
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("lineWidth")>]
    member x.LineWidth
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("lineCap")>]
    member x.LineCap
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("lineJoin")>]
    member x.LineJoin
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("miterLimit")>]
    member x.MiterLimit
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("strokeStyle")>]
    member x.StrokeStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fillStyle")>]
    member x.FillStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fillStyle")>]
    member x.FillPatternStyle
        with get() = Unchecked.defaultof<CanvasPattern>
        and set(v: CanvasPattern) = ()

    [<CompileTo("fillStyle")>]
    member x.FillGradientStyle
        with get() = Unchecked.defaultof<CanvasGradient>
        and set(v: CanvasGradient) = ()

    [<CompileTo("shadowOffsetX")>]
    member x.ShadowOffsetX
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("shadowOffsetY")>]
    member x.ShadowOffsetY
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("shadowBlur")>]
    member x.ShadowBlur
        with get() = 0.
        and set(v: float) = ()

    [<CompileTo("shadowColor")>]
    member x.ShadowColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("font")>]
    member x.Font
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textAlign")>]
    member x.TextAlign
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textBaseline")>]
    member x.TextBaseline
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("save")>]
    member x.Save() = ()

    [<CompileTo("restore")>]
    member x.Restore() = ()

    [<CompileTo("scale")>]
    member x.Scale(x1: float, y1: float) =
        ()

    [<CompileTo("rotate")>]
    member x.Rotate(angle: float) =
        ()

    [<CompileTo("translate")>]
    member x.Translate(x1: float, y1: float) =
        ()

    [<CompileTo("transform")>]
    member x.Transform(m11: float, m12: float, m21: float, m22: float, dx: float, dy: float) =
        ()

    [<CompileTo("setTransform")>]
    member x.SetTransform(m11: float, m12: float, m21: float, m22: float, dx: float, dy: float) =
        ()

    [<CompileTo("drawImage")>]
    member x.DrawImageElement(image : DomImage, dx: float, dy: float) =
        ()

    [<CompileTo("drawImage")>]
    member x.DrawImageElement(image : DomImage, dx: float, dy: float, dw:float, dh:float) =
        ()

    [<CompileTo("drawImage")>]
    member x.DrawCanvasElement(canvas: Canvas, dx: float, dy: float) =
        ()

    [<CompileTo("drawImage")>]
    member x.DrawCanvasElement(canvas: Canvas, dx: float, dy: float, dw :float, dh: float) =
        ()

    [<CompileTo("createLinearGradient")>]
    member x.CreateLinearGradient(x0: float, y0: float, x1: float, y1: float) =
        CanvasGradient()

    [<CompileTo("createRadialGradient")>]
    member x.CreateRadialGradient(x0: float, y0: float, r0: float, x1: float, y1: float, r1: float) =
        CanvasGradient()

    [<CompileTo("createPattern")>]
    member x.CreatePattern(image: obj, repitition: string) =
        CanvasPattern()

    [<CompileTo("beginPath")>]
    member x.BeginPath() = ()

    [<CompileTo("closePath")>]
    member x.ClosePath() = ()

    [<CompileTo("fill")>]
    member x.Fill() = ()

    [<CompileTo("stroke")>]
    member x.Stroke() = ()

    [<CompileTo("clip")>]
    member x.Clip() = ()

    [<CompileTo("moveTo")>]
    member x.MoveTo(x1: float, y1: float) = ()

    [<CompileTo("lineTo")>]
    member x.LineTo(x1: float, y1: float) = ()

    [<CompileTo("quadraticCurveTo")>]
    member x.QuadraticCurveTo(cpx: float, cpy: float, x1: float, y1: float) =   ()

    [<CompileTo("bezierCurveTo")>]
    member x.BezierCurveTo(cp1x: float, cp1y: float, cp2x: float, cp2y: float, x1: float, y1: float) = ()

    [<CompileTo("arcTo")>]
    member x.ArcTo(x1: float, y1: float, x2: float, y2: float, radius: float) = ()

    [<CompileTo("arc")>]
    member x.Arc(x1: float, y1: float, radius: float, startAngle: float, endAngle: float, antiClockwise: bool) = ()

    [<CompileTo("rect")>]
    member x.Rect(x1: float, y1: float, w: float, h: float) = ()

    [<CompileTo("isPointInPath")>]
    member x.IsPointInPath(x1: float, y1: float) = ()

    [<CompileTo("fillText")>]
    member x.FillText(text: string, x1: float, y1: float) = ()

    [<CompileTo("fillText")>]
    member x.FillText(text: string, x1: float, y1: float, maxWidth: float) = ()

    [<CompileTo("strokeText")>]
    member x.StrokeText(text: string, x1: float, y1: float) = ()

    [<CompileTo("strokeText")>]
    member x.StrokeText(text: string, x1: float, y1: float, maxWidth: float) = ()

    [<CompileTo("measureText")>]
    member x.MeasureText(text : string) = TextMetrics()

    [<CompileTo("clearRect")>]
    member x.ClearRect(x1: float, y1: float, w: float, h: float) = ()

    [<CompileTo("fillRect")>]
    member x.FillRect(x1: float, y1: float, w: float, h: float) = ()

    [<CompileTo("strokeRect")>]
    member x.StrokeRect(x1: float, y1: float, w: float, h: float) = ()

    [<CompileTo("createImageData")>]
    member x.CreateImageData(sw: float, sh: float) = ImageData()

    [<CompileTo("createImageData")>]
    member x.CreateImageData(data: ImageData) = ImageData()

    [<CompileTo("getImageData")>]
    member x.GetImageData(sx: float, sy: float, sw: float, sh: float) = ImageData()

    [<CompileTo("putImageData")>]
    member x.PutImageData(imageData: ImageData, dx: float, dy: float, ?dirtyX: float, ?dirtyY: float, ?dirtyWidth: float, ?dirtyHeight: float) = ()

and CanvasGradient() =

    [<CompileTo("addColorStop")>]
    member x.AddColorStop(offset: float, color: string) =
        ()

and CanvasPattern() =
    class
    end

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    TextMetrics() =

    [<CompileTo("width")>]
    member x.Width
        with get() = 0.

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    ImageData() =

    [<CompileTo("width")>]
    member x.Width
        with get() = 0.

    [<CompileTo("height")>]
    member x.Height
        with get() = 0.

    [<CompileTo("data")>]
    member x.Data
        with get() = CanvasPixelArray()

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    CanvasPixelArray() =

    // indexers will be automatically compiled to []
    member x.Item
        with get(i: int) = 0
        and set index (v: int) = ()

    [<CompileTo("length")>]
    member x.Length
        with get() = 0