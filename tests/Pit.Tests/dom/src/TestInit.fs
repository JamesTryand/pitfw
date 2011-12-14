namespace Pit.Dom.Tests

    [<AutoOpen>]
    module Init =
        open Pit.Dom

        let hook (id:string) (fn: unit->unit) =
            let btn = document.GetElementById(id)
            btn
            |> Event.click
            |> Event.add(fun _ -> fn())

        #if DBG
        module JsLiterals =

            let JsArray() =
                JsArrayTest.Create()
                JsArrayTest.ItemAccess()
                JsArrayTest.Join()
                JsArrayTest.Pop()
                JsArrayTest.Push1()
                JsArrayTest.Push2()
                JsArrayTest.Reverse()
                JsArrayTest.Shift()
                JsArrayTest.Slice1()
                JsArrayTest.Slice2()
                JsArrayTest.Sort()
                JsArrayTest.Splice()
                JsArrayTest.Unshift()

            let JsString() =
                JsStringTest.Length()
                JsStringTest.CharAt()
                JsStringTest.CharCodeAt()
                JsStringTest.Concat()
                JsStringTest.IndexOf()
                JsStringTest.LastIndexOf()
                JsStringTest.Match()
                JsStringTest.Replace()
                JsStringTest.Search()
                JsStringTest.Slice()
                JsStringTest.Split()
                JsStringTest.Substring1()
                JsStringTest.Substring2()
                JsStringTest.ToLower()
                JsStringTest.ToUpper()
        #endif

        module SVG =
            let CircleTests() =
                let circleinput = document.GetElementById("circleinput")
                circleinput
                |> Event.click
                |> Event.add( fun _ ->
                    SVGCircleTests.svgCircleSetup()
                    SVGCircleTests.CheckCircleCX()
                    SVGCircleTests.CheckCircleCY()
                    SVGCircleTests.CheckCircleR()
                )

            let ATests() =
                let ainput = document.GetElementById("ainput")
                ainput
                |> Event.click
                |> Event.add( fun _ ->
                    SVGATests.svgASetup()
                    SVGATests.CheckTarget()
                )

            let EllipseTests() =
                let input = document.GetElementById("ellipseinput")
                input
                |> Event.click
                |> Event.add( fun _ ->
                    SVGEllipseTests.svgEllipseSetup()
                    SVGEllipseTests.CheckEllipseCX()
                    SVGEllipseTests.CheckEllipseCY()
                    SVGEllipseTests.CheckEllipseRX()
                    SVGEllipseTests.CheckEllipseRY()
                )

            let RectTests() =
                let input = document.GetElementById("rectinput")
                input
                |> Event.click
                |> Event.add( fun _ ->
                    SVGRectTests.svgRectSetup()
                    SVGRectTests.CheckRectHeigth()
                    SVGRectTests.CheckRectWidth()
                )

            let AnimationTest() =
                let input = document.GetElementById("anim")
                input
                |> Event.click
                |> Event.add( fun _ ->
                    SVGCommonTests.AnimationTest()
                    )
            let PolygonTest() =
                let poly = document.GetElementById("poly")
                poly
                |> Event.click
                |> Event.add(fun _ ->
                    SVGCommonTests.SVGPolygonTest())

        module HTML5 =
            let HTML5CommonTests() =
                let common = document.GetElementById("commoninput")
                common
                |> Event.click
                |> Event.add( fun _ ->
                    CommonTests.DomArticleSetup()
                    CommonTests.DomArticleCheck()
                    CommonTests.DomAsideSetup()
                    CommonTests.DomAsideCheck()
                    CommonTests.DomFigureCaptionCheck()
                    CommonTests.DomFigureCheck()
                    CommonTests.DomFooterCheck()
                    CommonTests.DomHGroupCheck()
                    CommonTests.DomHeaderCheck()
                    CommonTests.DomMarkCheck()
                    CommonTests.DomNavCheck()
                    //CommonTests.DomProgressCheck()
                    CommonTests.DomRPCheck()
                    CommonTests.DomRTCheck()
                    CommonTests.DomRubyCheck()
                    CommonTests.DomSectionCheck()
                    CommonTests.DomEmbedCheck()

                )

        module DOM =
            let InputTests() =
                let dominput = document.GetElementById("dominput")
                dominput
                |> Event.click
                |> Event.add( fun _ ->
                    DomInputTests.TestButton()
                    DomInputTests.TestCheckbox()
                    DomInputTests.TestRadio()
                    DomInputTests.TestPassword()
                    DomInputTests.TestText()
                )

            let WindowTests() =
                let domwindow = document.GetElementById("domwindow")
                domwindow
                |> Event.click
                |> Event.add( fun _ ->
                    DomWindow.DefaultStatus()
                    DomWindow.Document()
                    DomWindow.History()
                    DomWindow.Location()
                    DomWindow.Navigator()
                    DomWindow.Name()
                    DomWindow.OuterHeightTest()
                    DomWindow.OuterWidthTest()
                    DomWindow.Parent()
                    DomWindow.Screen()
                    DomWindow.ScreenLeft()
                    DomWindow.ScreenTop()
                    DomWindow.ScreenX()
                    DomWindow.ScreenY()
                    DomWindow.Self()
                    DomWindow.Status()
                    DomWindow.Top()
                )

            let DocumentTests() =
                let domdoc = document.GetElementById("domdoc")
                domdoc
                |> Event.click
                |> Event.add( fun _ ->
                    DomDocument.Body()
                    DomDocument.Cookie()
                    DomDocument.DocumentMode()
                    DomDocument.DocumentUri()
                    DomDocument.Domain()
                    DomDocument.LastModified()
                    DomDocument.ReadyState()
                    DomDocument.Title()
                    DomDocument.Referrer()
                    DomDocument.Url()
                    DomDocument.GetElementById()
                    DomDocument.GetElementsByName()
                    DomDocument.GetElementsByTagName()
                )

            let OptionTests() =
                let option = document.GetElementById("option")
                option
                |> Event.click
                |> Event.add( fun _ ->
                    DomOptionTests.DomOptionSetup()
                    DomOptionTests.DefaultSelected()
                    DomOptionTests.Form()
                    DomOptionTests.Index()
                    DomOptionTests.Selected()
                    DomOptionTests.Text()
                    DomOptionTests.Value()
                )

            let SelectTests() =
                let select = document.GetElementById("select")
                select
                |> Event.click
                |> Event.add( fun _ ->
                    DomSelectTests.DomSelectSetup()
                    DomSelectTests.Form()
                    DomSelectTests.Length()
                    DomSelectTests.Multiple()
                    DomSelectTests.Name()
                    DomSelectTests.SelectedIndex()
                    DomSelectTests.Size()
                    DomSelectTests.Type()
                    DomSelectTests.Options()
                )

            let AnchorTests() =
                let anc = document.GetElementById("anc")
                anc
                |> Event.click
                |> Event.add(fun _ ->
                    DomAnchorTests.DomAnchorSetup()
                    DomAnchorTests.Charset()
                    DomAnchorTests.Href()
                    DomAnchorTests.HrefLang()
                    DomAnchorTests.Name()
                    DomAnchorTests.Rel()
                    DomAnchorTests.Rev()
                    DomAnchorTests.Target()
                    DomAnchorTests.Type()
                )

            let TextAreaTests() =
                let area = document.GetElementById("area")
                area
                |> Event.click
                |> Event.add(fun _ ->
                    DomTextAreaTests.Setup()
                    DomTextAreaTests.ColsRows()
                    DomTextAreaTests.DefaultValue()
                    DomTextAreaTests.IsReadOnly()
                    DomTextAreaTests.Type()
                    DomTextAreaTests.Value()
                )

        module Canvas =
            open Pit.Dom.Html5.Tests

            let TestCanvas() =
                (fun() ->
                    CanvasTest.WidthHeight()
                    CanvasTest.CtxCanvas())
                |> hook("testCanvas")

            let DrawLine() =
                (fun _ -> CanvasTest.DrawLines())
                |> hook("line")

            let DrawLineCaps() =
                (fun _ -> CanvasTest.DrawLineCaps())
                |> hook("linecaps")

            let DrawArc() =
                (fun _ -> CanvasTest.DrawArc())
                |> hook("arc")

            let DrawQuadArc() =
                (fun _ -> CanvasTest.DrawQuadArc())
                |> hook("quadarc")

            let DrawBezierCurve() =
                (fun _ -> CanvasTest.DrawBezierCurve())
                |> hook("bezier")

            let DrawLineJoins()   =
                (fun _ -> CanvasTest.LineJoins())
                |> hook("linejoins")

            let DrawRectangle()   =
                (fun _ -> CanvasTest.Rectangle())
                |> hook("rectangle")

            let LinearGradient()  =
                (fun _ -> CanvasTest.LinearGradient())
                |> hook("lg")

            let RadialGraident()  =
                (fun _ -> CanvasTest.RadialGradient())
                |> hook("rg")

            let Clip()            =
                (fun _ -> CanvasTest.Clip())
                |> hook("clip")

            let GlobalAlpha()     =
                (fun _ -> CanvasTest.GlobalAlpha())
                |> hook("globalA")

            let GlobalCompositeSourceATop() =
                (fun _ -> CanvasTest.GlobalCompositeSourceATop())
                |> hook("globalSourceATop")

            let GlobalCompositeSourceIn()   =
                (fun _ -> CanvasTest.GlobalCompositeSourceIn())
                |> hook("globalSourceIn")

            let GlobalCompositeSourceOver() =
                (fun _ -> CanvasTest.GlobalCompositeSourceOver())
                |> hook("globalSourceOver")

            let GlobalCompositeSourceOut()  =
                (fun _ -> CanvasTest.GlobalCompositeSourceOut())
                |> hook("globalSourceOut")

            let GlobalCompositeDestinationATop() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationATop())
                |> hook("globalDestATop")

            let GlobalCompositeDestinationIn() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationIn())
                |> hook("globalDestIn")

            let GlobalCompositeDestinationOut() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationOut())
                |> hook("globalDestOut")

            let GlobalCompositeDestinationOver() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationOver())
                |> hook("globalDestOver")

            let GlobalCompositeDestinationLighter() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationLighter())
                |> hook("globalDestLighter")

            let GlobalCompositeDestinationXor() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationXor())
                |> hook("globalDestXor")

            let GlobalCompositeDestinationCopy() =
                (fun _ -> CanvasTest.GlobalCompositeDestinationCopy())
                |> hook("globalDestCopy")

            let Translate() =
                (fun _ -> CanvasTest.Translate())
                |> hook("translate")

            let Scale() =
                (fun _ -> CanvasTest.Scale())
                |> hook("scale")

            let Rotate() =
                (fun _ -> CanvasTest.Rotate())
                |> hook("rotate")

            let Transform() =
                (fun _ -> CanvasTest.Transform())
                |> hook("transform")

            let FillText() =
                (fun _ -> CanvasTest.FillText())
                |> hook("filltext")

            let StrokeText() =
                (fun _ -> CanvasTest.StrokText())
                |> hook("stroketext")

            let LoadImage() =
                (fun _ -> CanvasTest.LoadImage())
                |> hook("loadimage")

            let LoadPattern() =
                (fun _ -> CanvasTest.LoadPattern())
                |> hook("loadpattern")

            let LoadImageData() =
                (fun _ -> CanvasTest.LoadImageData())
                |> hook("imagedata")

        #if DBG
        let LoadLiterals() =
            JsLiterals.JsArray()
            JsLiterals.JsString()
            MathTest.TestAll()
            DateTest.TestAll()
            RegexTest.Create()
        #endif

        let LoadCanvas() =
            Canvas.TestCanvas()
            Canvas.DrawLine()
            Canvas.DrawLineCaps()
            Canvas.DrawArc()
            Canvas.DrawQuadArc()
            Canvas.DrawBezierCurve()
            Canvas.DrawLineJoins()
            Canvas.DrawRectangle()
            Canvas.LinearGradient()
            Canvas.RadialGraident()
            Canvas.Clip()
            Canvas.GlobalAlpha()
            Canvas.GlobalCompositeSourceATop()
            Canvas.GlobalCompositeSourceIn()
            Canvas.GlobalCompositeSourceOut()
            Canvas.GlobalCompositeDestinationATop()
            Canvas.GlobalCompositeDestinationIn()
            Canvas.GlobalCompositeDestinationOver()
            Canvas.GlobalCompositeDestinationOut()
            Canvas.GlobalCompositeDestinationLighter()
            Canvas.GlobalCompositeDestinationXor()
            Canvas.GlobalCompositeDestinationCopy()
            Canvas.Translate()
            Canvas.Scale()
            Canvas.Rotate()
            Canvas.Transform()
            Canvas.FillText()
            Canvas.StrokeText()
            Canvas.LoadImage()
            Canvas.LoadPattern()
            Canvas.LoadImageData()

        let LoadDOMTests() =
            DOM.InputTests()
            DOM.AnchorTests()
            DOM.DocumentTests()
            DOM.OptionTests()
            DOM.SelectTests()
            DOM.TextAreaTests()
            DOM.WindowTests()

        let LoadSVGTests() =
            SVG.CircleTests()
            SVG.ATests()
            SVG.EllipseTests()
            SVG.RectTests()
            SVG.AnimationTest()
            SVG.PolygonTest()

        let LoadHTML5Test() =
            HTML5.HTML5CommonTests()

        let LoadSampleTest() =
            ()