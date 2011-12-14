namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open Pit.Dom.Html5
    open HtmlModule

    module SVGEllipseTests =
    
        [<Js>]
        let svgEllipseSetup() = 
            let div = document.GetElementById("check")              
            div.InnerHTML <- "<svg xmlns='http://www.w3.org/2000/svg' version='1.1'><ellipse id='ell' cx='300' cy='80' rx='100' ry='50' style='fill:yellow;stroke:purple;stroke-width:2'/></svg>"
       
        [<Js>]
        let CheckEllipseCX() = 
            let ell = document.GetElementById("ell") |> SVGEllipseElement.Of
            Assert.AreEqual "Ellipse cx test" ell.CX.BaseVal.ValueAsString "300"

        [<Js>]
        let CheckEllipseCY() = 
            let ell = document.GetElementById("ell") |> SVGEllipseElement.Of
            Assert.AreEqual "Ellipse cy test" ell.CY.BaseVal.ValueAsString "80"

        [<Js>]
        let CheckEllipseRX() = 
            let ell = document.GetElementById("ell") |> SVGEllipseElement.Of
            Assert.AreEqual "Ellipse rx test" ell.RX.BaseVal.ValueAsString "100"

        [<Js>]
        let CheckEllipseRY() = 
            let ell = document.GetElementById("ell") |> SVGEllipseElement.Of
            Assert.AreEqual "Ellipse rx test" ell.RX.BaseVal.ValueAsString "50"