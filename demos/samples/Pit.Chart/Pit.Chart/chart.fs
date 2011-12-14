namespace Pit.Chart
open Pit
open Pit.Javascript
open Pit.Dom
open Pit.Dom.Html5

module Basic =

    [<JsObject>]
    /// <summary>
    /// Thickness record type, marked as JsObject so this will be treated as a JSON type in JavaScript.
    /// </summary>
    type thickness = {
        left    : float
        right   : float
        top     : float
        bottom  : float
    }

    module Pie =

        /// <summary>
        /// Settings type for Pie Chart.
        /// </summary>
        [<JsObject>]
        type settings = {
            canvas          : Canvas
            container       : DomElement
            context         : Canvas2DRenderingContext
            height          : float
            width           : float
            labels          : string[]
            fontSize        : float
            labelColor      : string
            labelWidth      : float
            values          : float[]
            padding         : thickness
        }

        /// <summary>
        /// Initialize the settings for the Pie chart.
        /// </summary>
        [<Js>]
        let init(canvas : Canvas) = {
            canvas      = canvas
            container   = canvas.ParentNode
            context     = canvas.GetContext("2d")
            height      = canvas.Height
            width       = canvas.Width
            labels      = [||]
            fontSize    = 10.
            labelColor  = "black"
            labelWidth  = 50.
            values      = [||]
            padding     = { top = 20.; left = 30.; bottom = 30.; right = 20.; }
            }

        /// <summary>
        /// Creates a DataSet and returns it.
        /// </summary>
        [<Js>]
        let setDataset (v: float[]) (t: settings) =
            {t with
                values = v }

        /// <summary>
        /// Calculates padding for the settings and returns a tuple.
        /// </summary>
        [<Js>]
        let private calcPadding (t:settings) =
            t.padding.left, (t.width - t.padding.right), t.padding.top, (t.height - t.padding.bottom)

        /// <summary>
        /// Creates a label text to be placed in the pie
        /// </summary>
        [<Js>]
        let private createLabelText labelText (sliceMiddle:float) (labelx:float) (labely:float) (t:settings) =
            let label               = document.CreateElement("div")
            label.InnerHTML         <- labelText
            label.Style.Position    <- "absolute"
            label.Style.ZIndex      <- 11
            label.Style.Width       <- t.labelWidth.ToString() + "px"
            label.Style.FontSize    <- t.fontSize.ToString() + "px"
            label.Style.Color       <- t.labelColor

            match sliceMiddle with
            | x when x <= 0.25 ->
                label.Style.TextAlign       <- "left"
                label.Style.VerticalAlign   <- "top"
                label.Style.Left            <- labelx.ToString() + "px"
                label.Style.Top             <- (labely - t.fontSize).ToString() + "px"

            | x when x > 0.25 && x <= 0.5 ->
                label.Style.TextAlign       <- "left"
                label.Style.VerticalAlign   <- "bottom"
                label.Style.Left            <- labelx.ToString() + "px"
                label.Style.Top             <- labely.ToString() + "px"

            | x when x > 0.5 && x <= 0.75 ->
                label.Style.TextAlign       <- "right"
                label.Style.VerticalAlign   <- "bottom"
                label.Style.Left            <- (labelx - t.labelWidth).ToString() + "px"
                label.Style.Top             <- labely.ToString() + "px"

            | _ ->
                label.Style.TextAlign       <- "right"
                label.Style.VerticalAlign   <- "bottom"
                label.Style.Left            <- (labelx - t.labelWidth).ToString() + "px"
                label.Style.Top             <- (labely - t.fontSize).ToString() + "px"

            label

        /// <summary>
        /// Draws the pie chart based on the settings. Settings should also contain the DataSet for rendering the values.
        /// </summary>
        [<Js>]
        let draw (t:settings) =
            let ctx     = t.context
            let values  = t.values
            let total   = values |> Array.sum
            let xmin, xmax, ymin, ymax = calcPadding t
            let radius  = min (ymax - ymin) (xmax - xmin) / 2.
            let centerx = (xmax - xmin) / 2. + xmin
            let centery = (ymax - ymin) / 2. + ymin
            let colors  = Colors.get values.Length
            let mutable counter = 0.
            ctx.Save()
            for i = 0 to values.Length - 1 do
                let frac = values.[i] / total
                ctx.BeginPath()
                ctx.MoveTo(centerx, centery)
                // calculate the arch using JavaScript Math methods
                ctx.Arc(centerx, centery, radius, counter * Math.PI * 2. - Math.PI * 0.5, (counter + frac) * Math.PI * 2. - Math.PI * 0.5, false)
                ctx.LineTo(centerx, centery)
                ctx.ClosePath()
                ctx.FillStyle <- colors.[i]
                ctx.Fill()

                let sliceMiddle = (counter + frac/2.)
                let labelx      = centerx + Math.sin(sliceMiddle * Math.PI * 2.) * (radius + 10.)
                let labely      = centery - Math.cos(sliceMiddle * Math.PI * 2.) * (radius + 10.)
                let labelText   = values.[i].ToString()
                let label       = createLabelText labelText sliceMiddle labelx labely t
                t.container.AppendChild(label)
                // increment counter
                counter <- counter + frac
            ctx.Restore()

    module Line =

        /// <summary>
        /// Settings type for Line Chart. Also implements extensions using "with" in F#.
        /// </summary>
        type settings = {
            canvas      : Canvas
            context     : Canvas2DRenderingContext
            height      : float
            width       : float
            xvalues     : string[]
            yvalues     : float[]
            xpadding    : float
            ypadding    : float
            lineWidth   : float
            axisColor   : string
            lineColor   : string
            font        : string
            xTextAlign  : string
            yTextAlign  : string
            yTextBaseline : string
            dotColor    : string
        } with
            [<Js>]
            member this.GetXPixel(v: float) =
                ((this.width - this.xpadding) / float(this.xvalues.Length)) * v + (this.xpadding * 1.5)
            [<Js>]
            member this.GetYPixel(v: float, ymax: float) =
                this.height - ((( this.height - this.ypadding) / ymax) * v) - this.ypadding

        /// <summary>
        /// Initialize the settings for Line Chart.
        /// </summary>
        [<Js>]
        let init(canvas: Canvas) = {
            canvas  = canvas
            context = canvas.GetContext("2d")
            height  = canvas.Height
            width   = canvas.Width
            xvalues = [||]
            yvalues = [||]
            xpadding    = 20.
            ypadding    = 20.
            lineWidth   = 2.
            axisColor   = "#333"
            lineColor   = "#F00"
            font        = "italic 8pt sans-serif"
            xTextAlign  = "center"
            yTextAlign  = "right"
            yTextBaseline   = "middle"
            dotColor        = "#333"
        }

        /// <summary>
        /// Internal draw function based on the settings. Make sure to provide the DataSet with the settings.
        /// </summary>
        [<Js>]
        let private _draw (t:settings) =
            let ctx = t.context
            // draw axis
            ctx.LineWidth   <- t.lineWidth
            ctx.StrokeStyle <- t.axisColor
            ctx.Font        <- t.font
            ctx.TextAlign   <- t.xTextAlign

            ctx.BeginPath()
            ctx.MoveTo(t.xpadding, 0.)
            ctx.LineTo(t.xpadding, t.height - t.ypadding)
            ctx.LineTo(t.width, t.height - t.ypadding)
            ctx.Stroke()

            for i = 0 to t.xvalues.Length - 1 do
                ctx.FillText(t.xvalues.[i], t.GetXPixel(float(i)), t.height - t.ypadding + 20.)

            // draw y value text
            ctx.TextAlign       <- t.yTextAlign
            ctx.TextBaseline    <- t.yTextBaseline
            let ymax = let m = t.yvalues |> Array.max in m + 10. - m % 10.
            for i in 0. .. 10. .. ymax do
                ctx.FillText(i.ToString(), t.xpadding - 10., t.GetYPixel(i, ymax))

            // draw line
            ctx.StrokeStyle <- t.lineColor
            ctx.BeginPath()
            ctx.MoveTo(t.GetXPixel(0.), t.GetYPixel(t.yvalues.[0], ymax))
            for i = 1 to t.yvalues.Length - 1 do
                ctx.LineTo(t.GetXPixel(float(i)), t.GetYPixel(t.yvalues.[i], ymax))
            ctx.Stroke()

            // draw dots
            ctx.FillStyle <- t.dotColor
            for i = 0 to t.yvalues.Length - 1 do
                ctx.BeginPath()
                ctx.Arc(t.GetXPixel(float(i)), t.GetYPixel(t.yvalues.[i], ymax), 4., 0., Math.PI * 2., true)
                ctx.Fill()

        /// <summary>
        /// Initializes the x and y DataSet values to the settings.
        /// </summary>
        [<Js>]
        let setDataset (xvalues: string[]) (yvalues: float[]) (t:settings) =
            { t with
                xvalues = xvalues
                yvalues = yvalues
            }

        /// <summary>
        /// Draws the Line Chart based on the DataSet values
        /// </summary>
        [<Js>]
        let draw (xvalues: string[]) (yvalues: float[]) (t:settings) =
            setDataset xvalues yvalues t
            |> _draw

        /// <summary>
        /// Draws the Line Chart based on the settings. Assumes that a DataSet has already been supplied to the settings.
        /// </summary>
        [<Js>]
        let draw2 (t:settings) =
            _draw

    module Bar =

        /// <summary>
        /// Settings for the Bar Chart.
        /// </summary>
        type settings = {
            canvas          : Canvas
            context         : Canvas2DRenderingContext
            height          : float
            width           : float
            axisLine        : float
            axisColor       : string
            xpadding        : float
            ypadding        : float
            xvalues         : string[]
            yvalues         : float[]
            colors          : string[]
            textColor       : string
            headLineFont    : string
            labelFont       : string
            barLabelFont    : string
        } with
            [<Js>]
            member this.barMaxHeight = this.height - this.ypadding * 2.
            [<Js>]
            member this.barWidth     = (this.width - (float this.yvalues.Length + 2.) * this.xpadding) / (this.yvalues.Length |> float)
            [<Js>]
            member this.ybarStartPoint = this.height - this.ypadding
            [<Js>]
            member this.ylabelStartPoint = this.ybarStartPoint + 5.

        /// <summary>
        /// Initialize settings for the Bar Chart.
        /// </summary>
        [<Js>]
        let init(canvas: Canvas) = {
            canvas          = canvas
            context         = canvas.GetContext("2d")
            height          = canvas.Height
            width           = canvas.Width
            axisLine        = 1.
            axisColor       = "black"
            xpadding        = 10.
            ypadding        = 25.
            xvalues         = [||]
            yvalues         = [||]
            colors          = [||]
            textColor       = "rgb(0,0,0)"
            headLineFont    = "14px sans-serif"
            labelFont       = "11px sans-serif"
            barLabelFont    = "10px sans-serif"
        }

        /// <summary>
        /// Set DataSet and colors in the settings
        /// </summary>
        [<Js>]
        let setDataset (xvalues: string[]) (yvalues: float[]) (colors: string[]) (t: settings) =
            {t with
                xvalues = xvalues
                yvalues = yvalues
                colors  = colors
            }

        /// <summary>
        /// Draw the Bar Chart based on the settings.
        /// </summary>
        [<Js>]
        let draw (xvalues: string[]) (yvalues: float[]) (colors: string[]) (t: settings) =
            let t = setDataset xvalues yvalues colors t
            let ctx = t.context
            ctx.FillStyle <- t.textColor
            ctx.ShadowOffsetX   <- 3.
            ctx.ShadowOffsetY   <- 3.
            ctx.ShadowBlur      <- 10.
            ctx.ShadowColor     <- "rgba(64, 64, 64, 0.5)"
            // draw axis
            ctx.LineWidth   <- t.axisLine
            ctx.StrokeStyle <- t.axisColor
            ctx.BeginPath()
            ctx.MoveTo(t.xpadding, 0.)
            ctx.LineTo(t.xpadding, t.height - t.ypadding)
            ctx.LineTo(t.width, t.height - t.ypadding)
            ctx.Stroke()

            ctx.Save()
            let ymax = t.yvalues |> Array.max
            for i = 0 to t.yvalues.Length - 1 do
                let label = t.xvalues.[i]
                let value = t.yvalues.[i]
                let color = t.colors.[i % t.colors.Length]

                let barHeight    = (value / ymax) * t.barMaxHeight
                let xmiddleOfBar = t.barWidth / 2. + t.xpadding + t.xpadding * float(i) + t.barWidth * float(i)
                let ymiddleOfBar = t.ybarStartPoint - barHeight / 2.

                // draw labels
                ctx.FillStyle       <- t.textColor
                ctx.Font            <- t.labelFont
                ctx.TextBaseline    <- "top"
                ctx.TextAlign       <- "center"
                ctx.FillText(label, xmiddleOfBar, t.ylabelStartPoint)

                // draw bars
                ctx.FillStyle <- color
                ctx.FillRect(t.xpadding + t.xpadding * float(i) + t.barWidth * float(i), t.ybarStartPoint, t.barWidth, barHeight * -1.)

                // draw value with units
                ctx.FillStyle <- t.textColor
                ctx.Font <- t.barLabelFont
                ctx.TextBaseline <- "top"
                ctx.TextAlign <- "center"
                ctx.FillText(value.ToString(), xmiddleOfBar, ymiddleOfBar)

            ctx.Restore()