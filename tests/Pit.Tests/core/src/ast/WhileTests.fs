namespace Pit.Test
open Pit

module WhileTests =

    [<Js>]
    let WhileDeclare() =
        let lookForValue value maxValue =
            let mutable continueLooping = true
            let mutable acc = 0
            while continueLooping do
                acc <- acc + 1
                if acc < maxValue then
                    if acc = value then
                        continueLooping <- false
                        Assert.AreEqual "While Loop" (acc = value) true
                else continueLooping <- false

        lookForValue 10 20
        lookForValue 22 20