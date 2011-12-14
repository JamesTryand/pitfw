namespace Pit.Rx

    open Pit
    open Pit.Dom

    module App =
        /// Entry Point to the application
        [<DomEntryPoint>]
        [<Js>]
        let main() =
            let divEl = document.GetElementById("rx")
            RxSample.trackMouse "Time Flies" divEl