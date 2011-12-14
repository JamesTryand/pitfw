namespace Pit.Dom.TestHelpers
    open System.Windows
    open Pit.Dom.Tests

    type App()  =
        inherit Application()
        do
            DomEventsTest.Select()