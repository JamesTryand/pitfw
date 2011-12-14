namespace Pit.Chart

open Pit
open Pit.Dom
open Pit.Dom.Html5

module App =
    [<DomEntryPoint>]
    [<Js>]
    let main() =
        let xvalues = [| "Jan"; "Feb"; "Mar"; "Apr"; "May"; "Jun" |]
        let yvalues = [| 12.; 28.; 18.; 34.; 40.; 17. |]

        let canvas = document.GetElementById("pie") |> Canvas.Of
        let values = [| 0.2; 0.3; 0.1; 0.5 |]
        Basic.Pie.init canvas
        |> Basic.Pie.setDataset values
        |> Basic.Pie.draw

        let canvas1 = document.GetElementById("bar") |> Canvas.Of
        let values = [| 10.; 20.; 30.; 50.; |]
        Basic.Bar.init canvas1
        |> Basic.Bar.draw xvalues yvalues (Colors.get xvalues.Length)

        let canvas2 = document.GetElementById("line") |> Canvas.Of
        Basic.Line.init canvas2
        |> Basic.Line.draw xvalues yvalues