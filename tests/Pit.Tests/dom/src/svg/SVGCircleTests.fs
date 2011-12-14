namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open Pit.Dom.Html5
    open HtmlModule

    module SVGCircleTests =

        [<Js>]
        let svgCircleSetup() = 
            let div = document.GetElementById("check")              
            div.InnerHTML <- "<svg xmlns='http://www.w3.org/2000/svg' version='1.1'><circle id='cir' cx='100' cy='50' r='40' stroke='black' stroke-width='2' fill='red'/></svg>"

        
        [<Js>]
        let CheckCircleCX() = 
            let cir = document.GetElementById("cir") |> SVGCircleElement.Of
            Assert.AreEqual "Circle cx test" cir.CX.BaseVal.ValueAsString "100"

        [<Js>]
        let CheckCircleCY() = 
            let cir = document.GetElementById("cir") |> SVGCircleElement.Of
            Assert.AreEqual "Circle cy test" cir.CY.BaseVal.ValueAsString "50"

        [<Js>]
        let CheckCircleR() = 
            let cir = document.GetElementById("cir") |> SVGCircleElement.Of
            Assert.AreEqual "Circle r test" cir.R.BaseVal.ValueAsString "40"