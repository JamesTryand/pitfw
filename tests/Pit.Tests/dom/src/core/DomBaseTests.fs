namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomBaseTests =
        [<Js>]
        let DomBaseSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<base id='bas'></base>"

        [<Js>]
        let baseType =
            document.GetElementById("bas") |> DomBase.Of

        [<Js>]
        let Href() =
            baseType.Href <- "http://www.google.com"
            Assert.AreEqual "Href" baseType.Href "http://www.google.com"


        [<Js>]
        let Target() =
            baseType.Target <- "_blank"
            Assert.AreEqual "Target" baseType.Target "_blank"


