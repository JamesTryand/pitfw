namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open HtmlModule

    module DomMediaTests =

        [<Js>]
        let DomSourceSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<a id='anc'><a/>"