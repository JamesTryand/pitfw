namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open Pit.Dom.Html5
    open HtmlModule

    module SVGRectTests = 
        
        [<Js>]
        let svgRectSetup() = 
            let div = document.GetElementById("check")              
            div.InnerHTML <- "<svg xmlns='http://www.w3.org/2000/svg' version='1.1'><rect id='rec' width='300' height='100' style='fill:rgb(0,0,255);stroke-width:1;stroke:rgb(0,0,0)'/></svg>"
       
        [<Js>]
        let CheckRectWidth() = 
            let rect = document.GetElementById("rec") |> SVGRectElement.Of
            Assert.AreEqual "Rect width test" rect.Width.BaseVal.ValueAsString "300"

       
        [<Js>]
        let CheckRectHeigth() = 
            let rect = document.GetElementById("rec") |> SVGRectElement.Of
            Assert.AreEqual "Rect height test" rect.Height.BaseVal.ValueAsString "100"
