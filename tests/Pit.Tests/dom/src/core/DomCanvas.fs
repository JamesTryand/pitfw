namespace Pit.Dom.Html5.Tests
open Pit
open Pit.Dom
open Pit.Dom.Tests
open Pit.Dom.Html5

module CanvasTest =

    [<Js>]
    let canvas() = document.GetElementById("canvas") |> Canvas.Of

    [<Js>]
    let reset() =
        let canvas = canvas()
        canvas.Width  <- 400.
        canvas.Height <- 400.
        let ctx = canvas.GetContext("2d")
        ctx.ClearRect(0., 0., canvas.Width, canvas.Height)

    [<Js>]
    let GetContext() =
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        Assert.IsNull "Canvas.getContext" ctx

    [<Js>]
    let WidthHeight() =
        let canvas = canvas()
        canvas.Width  <- 500.
        canvas.Height <- 500.
        Assert.AreEqual "Canvas.Width" 500. canvas.Width
        Assert.AreEqual "Canvas.Width" 500. canvas.Height

    [<Js>]
    let CtxCanvas() =
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        Assert.IsNotNull "Ctx.Canvas" ctx.Canvas

    [<Js>]
    let DrawLines() =
        let canvas = canvas()
        reset()
        let ctx = canvas.GetContext("2d")
        ctx.MoveTo(100., 50.)
        ctx.LineTo(450., 50.)
        ctx.LineWidth <- 15.
        ctx.StrokeStyle <- "ff0000"
        ctx.Stroke()

    [<Js>]
    let DrawLineCaps() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.BeginPath()
        ctx.MoveTo(200., canvas.Height / 2. - 50.)
        ctx.LineTo(canvas.Width - 200., canvas.Height / 2. - 50.)
        ctx.LineWidth <- 20.
        ctx.StrokeStyle <- "#0000ff"
        ctx.LineCap <- "butt"
        ctx.Stroke()
        // round line cap
        ctx.BeginPath()
        ctx.MoveTo(200., canvas.Height / 2.)
        ctx.LineTo(canvas.Width - 200., canvas.Height/2.)
        ctx.LineWidth <- 20.
        ctx.StrokeStyle <- "#0000ff"
        ctx.LineCap <- "round"
        ctx.Stroke()
        // square line cap
        ctx.BeginPath()
        ctx.MoveTo(200., canvas.Height / 2. + 50.)
        ctx.LineTo(canvas.Width - 200., canvas.Height / 2. + 50.)
        ctx.LineWidth <- 20.
        ctx.StrokeStyle <- "#0000ff"
        ctx.LineCap <- "square"
        ctx.Stroke()

    [<Js>]
    let DrawArc() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        let centerX = 288.
        let centerY = 160.
        let radius = 75.
        let startingAngle = 1.1 * Pit.Javascript.Math.PI
        let endingAngle = 1.9 * Pit.Javascript.Math.PI
        let counterClockwise = false
        ctx.Arc(centerX, centerY, radius, startingAngle, endingAngle, counterClockwise)
        ctx.LineWidth <- 15.
        ctx.StrokeStyle <- "black"
        ctx.Stroke()

    [<Js>]
    let DrawQuadArc() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.MoveTo(188., 150.)
        ctx.QuadraticCurveTo(288., 0., 388., 150.)
        ctx.LineWidth <- 10.
        ctx.StrokeStyle <- "black"
        ctx.Stroke()

    [<Js>]
    let DrawBezierCurve() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.MoveTo(188., 130.)
        ctx.BezierCurveTo(140., 10., 388., 10., 388., 170.)
        ctx.LineWidth <- 10.
        ctx.StrokeStyle <- "black"
        ctx.Stroke()

    [<Js>]
    let LineJoins() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        //miter line join
        ctx.BeginPath()
        ctx.MoveTo(canvas.Width / 2. - 50. - 140., canvas.Height - 50.)
        ctx.LineTo(canvas.Width / 2. - 140., 50.)
        ctx.LineTo(canvas.Width / 2. + 50. - 140., canvas.Height - 50.)
        ctx.LineWidth <- 25.
        ctx.LineJoin <- "miter"
        ctx.Stroke()

        //round line join
        ctx.BeginPath()
        ctx.MoveTo(canvas.Width / 2. - 50., canvas.Height - 50.)
        ctx.LineTo(canvas.Width / 2., 50.)
        ctx.LineTo(canvas.Width / 2. + 50., canvas.Height - 50.)
        ctx.LineWidth <- 25.
        ctx.LineJoin <- "round"
        ctx.Stroke()

        //bevel line join
        ctx.BeginPath()
        ctx.MoveTo(canvas.Width/2. - 50. + 140., canvas.Height - 50.)
        ctx.LineTo(canvas.Width/2. + 140., 50.)
        ctx.LineTo(canvas.Width/2. + 50. + 140., canvas.Height - 50.)
        ctx.LineWidth <- 25.
        ctx.LineJoin <- "bevel"
        ctx.Stroke()

    [<Js>]
    let Rectangle() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.BeginPath()
        ctx.Rect(188., 50., 200., 100.)
        ctx.FillStyle <- "#8ED6FF"
        ctx.Fill()
        ctx.LineWidth <- 5.
        ctx.StrokeStyle <- "black"
        ctx.Stroke()

    [<Js>]
    let LinearGradient() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.BeginPath()
        ctx.Rect(188., 50., 200., 100.)
        ctx.ClosePath()
        let grd = ctx.CreateLinearGradient(230., 0., 370., 200.)
        grd.AddColorStop(0., "#8ED6FF")
        grd.AddColorStop(1., "#004CB3")
        ctx.FillGradientStyle <- grd
        ctx.Fill()

    [<Js>]
    let RadialGradient() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.BeginPath()
        ctx.MoveTo(170., 80.)
        ctx.BezierCurveTo(130., 100., 130., 150., 230., 150.)
        ctx.BezierCurveTo(250., 180., 320., 180., 340., 150.)
        ctx.BezierCurveTo(420., 150., 420., 120., 390., 100.)
        ctx.BezierCurveTo(430., 40., 370., 30., 340., 50.)
        ctx.BezierCurveTo(320., 5., 250., 20., 250., 50.)
        ctx.BezierCurveTo(200., 5., 150., 20., 170., 80.)
        ctx.ClosePath()
        let grd = ctx.CreateRadialGradient(238., 50., 10., 338., 50., 200.)
        grd.AddColorStop(0., "red")
        grd.AddColorStop(0.17, "orange")
        grd.AddColorStop(0.33, "yellow")
        grd.AddColorStop(0.5, "green")
        grd.AddColorStop(0.666, "blue")
        grd.AddColorStop(1., "violet")
        ctx.FillGradientStyle <- grd
        ctx.Fill()
        ctx.LineWidth <- 5.
        ctx.StrokeStyle <- "#0000FF"
        ctx.Fill()

    [<Js>]
    let Clip() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        let centerX = canvas.Width / 2.
        let centerY = canvas.Height / 2.
        let radius = 75.
        let offset = 50.
        ctx.Save()
        ctx.BeginPath()
        ctx.Arc(centerX, centerY, radius, 0., 2. * Pit.Javascript.Math.PI, false)
        ctx.Clip()

        // draw blue
        ctx.BeginPath()
        ctx.Arc(centerX - offset, centerY - offset, radius, 0., 2. * Pit.Javascript.Math.PI, false)
        ctx.FillStyle <- "#00D2FF"
        ctx.Fill()

        // draw yellow
        ctx.BeginPath()
        ctx.Arc(centerX + offset, centerY, radius, 0., 2. * Pit.Javascript.Math.PI, false)
        ctx.FillStyle <- "yellow"
        ctx.Fill()

        // draw red
        ctx.BeginPath()
        ctx.Arc(centerX, centerY + offset, radius, 0., 2. * Pit.Javascript.Math.PI, false)
        ctx.FillStyle <- "red"
        ctx.Fill()

        ctx.Restore()
        ctx.BeginPath()
        ctx.Arc(centerX, centerY, radius, 0., 2. * Pit.Javascript.Math.PI, false)
        ctx.LineWidth <- 3.
        ctx.StrokeStyle <- "black"
        ctx.Stroke()

    [<Js>]
    let GlobalAlpha() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.GlobalAlpha <- 0.5
        ctx.BeginPath()
        ctx.Rect(200., 20., 100., 100.)
        ctx.FillStyle <- "blue"
        ctx.Fill()

    [<Js>]
    let drawGlobalCompisitionSource(globalComposistionSource) =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.BeginPath()
        ctx.Rect(10., 0., 55., 55.)
        ctx.FillStyle <- "blue"
        ctx.Fill()
        ctx.GlobalCompositeOperation <- globalComposistionSource
        ctx.BeginPath()
        ctx.Arc(50., 50., 35., 0., 2. * Pit.Javascript.Math.PI, false)
        ctx.FillStyle <- "red"
        ctx.Fill()

    [<Js>]
    let GlobalCompositeSourceATop() =
        drawGlobalCompisitionSource "source-atop"

    [<Js>]
    let GlobalCompositeSourceIn() =
        drawGlobalCompisitionSource "source-in"

    [<Js>]
    let GlobalCompositeSourceOut() =
        drawGlobalCompisitionSource "source-out"

    [<Js>]
    let GlobalCompositeSourceOver() =
        drawGlobalCompisitionSource "source-over"

    [<Js>]
    let GlobalCompositeDestinationATop() =
        drawGlobalCompisitionSource "destination-atop"

    [<Js>]
    let GlobalCompositeDestinationIn() =
        drawGlobalCompisitionSource "destination-in"

    [<Js>]
    let GlobalCompositeDestinationOut() =
        drawGlobalCompisitionSource "destination-out"

    [<Js>]
    let GlobalCompositeDestinationOver() =
        drawGlobalCompisitionSource "destination-over"

    [<Js>]
    let GlobalCompositeDestinationLighter() =
        drawGlobalCompisitionSource "lighter"

    [<Js>]
    let GlobalCompositeDestinationXor() =
        drawGlobalCompisitionSource "xor"

    [<Js>]
    let GlobalCompositeDestinationCopy() =
        drawGlobalCompisitionSource "copy"

    [<Js>]
    let Translate() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.Translate(canvas.Width/2., canvas.Height/2.)
        ctx.FillStyle <- "blue"
        ctx.FillRect(-150./2., 75./2., 150., 75.)

    [<Js>]
    let Scale() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.Translate(canvas.Width/2., canvas.Height/2.)
        ctx.Scale(1., 0.5)
        ctx.FillStyle <- "blue"
        ctx.FillRect(-150./2., 75./2., 150., 75.)

    [<Js>]
    let Rotate() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.Translate(canvas.Width/2., canvas.Height/2.)
        ctx.Rotate(Pit.Javascript.Math.PI/ 4.)
        ctx.FillStyle <- "blue"
        ctx.FillRect(-150./2., 75./2., 150., 75.)

    [<Js>]
    let Transform() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        let tx = canvas.Width / 2.
        let ty = canvas.Height / 2.
        ctx.Transform(1., 0., 0., 1., tx, ty)
        ctx.FillStyle <- "blue"
        ctx.FillRect(-150./2., 75./2., 150., 75.)

    [<Js>]
    let FillText() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.FillStyle <- "blue"
        ctx.Font <- "14px sans-serif"
        ctx.FillText("Hello World", 20., 20.)
        ctx.Fill()

    [<Js>]
    let StrokText() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        ctx.StrokeStyle <- "blue"
        ctx.StrokeText("Hello World", 20., 20.)
        ctx.Font <- "14px sans-serif"
        ctx.Stroke()

    [<Js>]
    let LoadImage() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        let destX, destY = 60., 50.
        let image = document.CreateElement("image") |> DomImage.Of
        image
        |> Event.load
        |> Event.add(fun _ ->
            ctx.DrawImageElement(image, destX, destY)
        )
        image.Src <- "../assets/image.png"

    [<Js>]
    let LoadPattern() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        let image = document.CreateElement("image") |> DomImage.Of
        image
        |> Event.load
        |> Event.add(fun _ ->
            let pattern = ctx.CreatePattern(image, "repeat")
            ctx.Rect(10., 10., canvas.Width - 20., canvas.Height - 20.)
            ctx.FillPatternStyle <- pattern
            ctx.Fill()
        )
        image.Src <- "../assets/pattern.png"

    [<Js>]
    let LoadImageData() =
        reset()
        let canvas = canvas()
        let ctx = canvas.GetContext("2d")
        let imgd = ctx.CreateImageData(50.,50.)
        let pix = imgd.Data
        let mutable i = 0
        while i < pix.Length do
            i <- i + 4
            pix.[i]     <- 255
            pix.[i+3]   <- 127
        ctx.PutImageData(imgd, 0., 0.)