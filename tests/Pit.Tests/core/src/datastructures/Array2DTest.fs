namespace Pit.Test
open Pit
open Pit.Javascript

module Array2DTest =

    [<Js>]
    let Init() =
        let arr = Array2D.init 2 2 (fun i j -> i + j)
        let r = Array2D.get arr 1 1
        Assert.AreEqual "Array2D Init" r 2

    [<Js>]
    let Length1() =
        let arr = Array2D.init 2 2 (fun i j -> i + j)
        let len1 = arr |> Array2D.length1
        Assert.AreEqual "Array2D Length1" len1 2

    [<Js>]
    let Length2() =
        let arr = Array2D.init 2 2 (fun i j -> i + j)
        let len1 = arr |> Array2D.length2
        Assert.AreEqual "Array2D Length2" len1 2

    [<Js>]
    let GetSet() =
        let arr = Array2D.init 2 2 (fun i j -> i + j)
        Array2D.set arr 1 0 3
        let r = Array2D.get arr 1 0
        Assert.AreEqual "Array2D GetSet" r 3

    [<Js>]
    let ZeroCreate() =
        let arr = Array2D.zeroCreate 2 2
        let r   = Array2D.get arr 1 1
        Assert.AreEqual "Array2D ZeroCreate" r 0

    [<Js>]
    let Iter() =
        let arr : int[,] = Array2D.zeroCreate 2 2
        let r = Array2D.length1 arr
        Assert.AreEqual "Array2D Iter" r 2
        arr
        |> Array2D.iter(fun i ->
            Assert.AreEqual "Array2D Iter" i 0
        )

    [<Js>]
    let IterateIndex() =
        let arr : int[,] = Array2D.init 2 2 (fun i j -> i + j)
        arr
        |> Array2D.iteri(fun i j x ->
            Assert.AreEqual "Array2D IterateIndex" (i+j) x
        )

    [<Js>]
    let Map() =
        let arr : int[,] = Array2D.zeroCreate 2 2
        arr
        |> Array2D.map(fun i -> 1)
        |> Array2D.iter(fun i ->
            Assert.AreEqual "Array2D Iter" i 1
        )

    [<Js>]
    let MapIndexed() =
        let arr : int[,] = Array2D.zeroCreate 2 2
        arr
        |> Array2D.mapi(fun i j x -> i + j)
        |> Array2D.iteri(fun i j x ->
            Assert.AreEqual "Array2D IterateIndex" (i+j) x
        )