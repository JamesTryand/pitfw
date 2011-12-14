namespace Pit.Test
open Pit
open Pit.Javascript

module UOMTest =
    [<Measure>] type C
    [<Measure>] type F

    [<Js>]
    let to_farenheit (x : float<C>) = x * (9.0<F>/5.0<C>) + 32.0<F>
    [<Js>]
    let to_celsius (x : float<F>) = (x - 32.0<F>) * (5.0<C>/9.0<F>)

    [<Js>]
    let UOMeasure1() =
        let f = to_farenheit 20.<C>
        Assert.AreEqual "Units Of Measure To Farenheit" 68 f

    [<Measure>] type m
    [<Measure>] type kg

    [<Js>]
    let vanillaFloats = [10.0; 15.5; 17.0]
    [<Js>]
    let lengths = [ for a in [2.0; 7.0; 14.0; 5.0] -> a * 1.0<m> ]
    [<Js>]
    let masses = [ for a in [155.54; 179.01; 135.90] -> a * 1.0<kg> ]
    [<Js>]
    let densities = [ for a in [0.54; 1.0; 1.1; 0.25; 0.7] -> a * 1.0<kg/m^3> ]

    [<Js>]
    let average (l : float<'u> list) =
        let sum, count = l |> List.fold (fun (sum, count) x -> sum + x, count + 1.0<_>) (0.0<_>, 0.0<_>)
        sum / count

    [<Js>]
    let UOMeasure2() =
        let f, l, m, d = average vanillaFloats, average lengths, average masses, average densities
        Assert.AreEqual "UOM Floats" (f |> Global.parseInt) 14
        Assert.AreEqual "UOM Lengths" l 7
        Assert.AreEqual "UOM Masses" (m |> Global.parseInt) 156
        Assert.AreEqual "UOM Densities" d 0.718