namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open Pit.Dom.Html5
    open HtmlModule

    module SVGCommonTests = 

        [<Js>]
        let AnimationTest() =
            let div = document.GetElementById("check")    
            div.InnerHTML <- "<svg width='300px' height='100px'><rect x='0' y='0' width='300' height='100' stroke='black' stroke-width='1' /><circle cx='0' cy='50' r='15' fill='blue' stroke='black' stroke-width='1'><animate attributeName='cx' from='0' to='100' dur='5s' repeatCount='indefinite' /></circle></svg>"

        [<Js>]
        let SVGPolygonTest() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<svg xmlns='http://www.w3.org/2000/svg' version='1.1'><polygon id='polygone' style='fill:lime;stroke:purple;stroke-width:1' /></svg>"
            let poly = document.GetElementById("polygone") |> SVGPolygonElement.Of
            poly.SetAttribute("points" , "200,10 250,190 160,210")
