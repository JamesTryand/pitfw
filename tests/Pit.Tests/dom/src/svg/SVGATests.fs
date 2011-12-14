namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open Pit.Dom.Html5
    open HtmlModule

    module SVGATests =

        [<Js>]
        let svgASetup() = 
            let div = document.GetElementById("check")              
            div.InnerHTML <- "<svg xmlns='http://www.w3.org/2000/svg' version='1.1' xmlns:xlink='http://www.w3.org/1999/xlink'><a id='aEl' xlink:href='http://www.w3schools.com/svg/' target='_blank'><text x='0' y='15' fill='red'>I love SVG</text></a></svg>" 
                    
        [<Js>]
        let CheckTarget() = 
            let a = document.GetElementById("aEl") |> SVGAElement.Of
            Assert.AreEqual "Target test" a.Target.BaseVal "_blank"

