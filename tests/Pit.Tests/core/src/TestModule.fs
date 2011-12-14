namespace Pit.Test
open Pit
open Pit.Javascript

module TestModule =
    #if DOM
        open Pit.Dom

        [<AutoOpen;JsIgnore(IgnoreTypeName=true,IgnoreNamespace=true)>]
        module Extensions =

            type DomDocument with
                [<CompileTo("querySelector");JsExtensionType>]
                member this.QuerySelector(query:string) =
                    DomElement()

        [<Js>]
        let test() =
            let el = document.QuerySelector("H")
            ()
    #endif
    #if AST
        ()
    #endif