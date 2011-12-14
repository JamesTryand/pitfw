namespace Pit.FSharp.Collections
open Pit
open Pit.Javascript
open System

module Array2DModule =
    [<Js>]
    let checkNonNull argName arg =
        match box arg with
        | null -> nullArg argName
        | _    -> ()

    [<Js>]
    [<CompiledName("Length1")>]
    let length1 (arr: _[,]) =
        let js = new JsArray2D<JsArray<_>>(arr)
        js.Length

    [<Js>]
    [<CompiledName("Length2")>]
    let length2 (arr:_[,]) =
        let js = new JsArray2D<JsArray<_>>(arr)
        js.[0].Length

    // http://stackoverflow.com/questions/1301276/what-is-the-purpose-of-array-getlowerboundint
    // Only for COM interop Array.GetLowerBound is really useful to identify 0 or 1 based arrays
    // JS arrays are always 0 based so just returning 0 here
    [<Js>]
    [<CompiledName("Base1")>]
    let base1 (arr:_[,]) = 0

    [<Js>]
    [<CompiledName("Base2")>]
    let base2 (arr:_[,]) = 0

    [<Js>]
    [<CompiledName("Get")>]
    let get (arr:_[,]) (n:int) (m:int) =
        let js = new JsArray2D<JsArray<_>>(arr)
        js.[n].[m]

    [<Js>]
    [<CompiledName("Set")>]
    let set (arr:_[,]) (n:int) (m:int) (x:_) =
        let js = new JsArray2D<JsArray<_>>(arr)
        js.[n].[m] <- x

    [<Js>]
    [<CompiledName("ZeroCreate")>]
    let zeroCreate (n:int) (m:int) =
        if n < 0 then raise (new Exception("Input must be non-negative"))
        if m < 0 then raise (new Exception("Input must be non-negative"))
        let arr = new JsArray<JsArray<_>>([||])
        for i=0 to n-1 do
            arr.[i] <- new JsArray<_>([||])
            for j=0 to m-1 do
                arr.[i].[j] <- 0
        arr

    [<Js>]
    [<CompiledName("ZeroCreateBased")>]
    let zeroCreateBased b1 b2 n m =
        let l1 = (b1 + n) - 1
        let l2 = (b2 + m) - 1
        let arr = new JsArray<JsArray<_>>([||])
        for i=b1 to l1 do
            arr.[i] <- new JsArray<_>([||])
            for j=b2 to l2 do
                arr.[i].[j] <- 0
        arr

    [<Js>]
    [<CompiledName("CreateBased")>]
    let createBased b1 b2 n m (x:_) =
        let array = (zeroCreateBased b1 b2 n m : JsArray<JsArray<_>>)
        for i=b1 to b1+n - 1 do
            for j=b2 to b2+m - 1 do
                array.[i].[j] <- x
        array

    [<Js>]
    [<CompiledName("InitializedBased")>]
    let initBased b1 b2 n m f =
        let array = zeroCreateBased b1 b2 n m
        let l1 = b1+n - 1
        let l2 = b2+m - 1
        for i=b1 to l1 do
            for j=b2 to l2 do
                array.[i].[j] <- f i j
        array

    [<Js>]
    [<CompiledName("Create")>]
    let create n m (x:_) =
        createBased 0 0 n m x

    [<Js>]
    [<CompiledName("Initialize")>]
    let init n m f =
        initBased 0 0 n m f

    [<Js>]
    [<CompiledName("Iterate")>]
    let iter f array =
        checkNonNull "array" array
        let count1 = length1 array
        let count2 = length2 array
        let b1 = base1 array
        let b2 = base2 array
        let js = new JsArray2D<JsArray<_>>(array)
        for i = b1 to b1+count1 - 1 do
            for j = b2 to b2+count2 - 1 do
            f js.[i].[j]

    [<Js>]
    [<CompiledName("IterateIndexed")>]
    let iteri (f : int -> int -> _ -> unit) (array:_[,]) =
        checkNonNull "array" array
        let count1 = length1 array
        let count2 = length2 array
        let b1 = base1 array
        let b2 = base2 array
        let js = new JsArray2D<JsArray<_>>(array)
        for i = b1 to b1+count1 - 1 do
            for j = b2 to b2+count2 - 1 do
            f i j js.[i].[j]

    [<Js>]
    [<CompiledName("Map")>]
    let map f array =
        checkNonNull "array" array
        let js = new JsArray2D<JsArray<_>>(array)
        initBased (base1 array) (base2 array) (length1 array) (length2 array) (fun i j -> f js.[i].[j])

    [<Js>]
    [<CompiledName("MapIndexed")>]
    let mapi f array =
        checkNonNull "array" array
        let js = new JsArray2D<JsArray<_>>(array)
        initBased (base1 array) (base2 array) (length1 array) (length2 array) (fun i j -> f i j js.[i].[j])

    [<Js>]
    [<CompiledName("Copy")>]
    let copy array =
        checkNonNull "array" array
        let js = new JsArray2D<JsArray<_>>(array)
        initBased (base1 array) (base2 array) (length1 array) (length2 array) (fun i j -> js.[i].[j])

    [<Js>]
    [<CompiledName("Rebase")>]
    let rebase array =
        checkNonNull "array" array
        let b1 = base1 array
        let b2 = base2 array
        let js = new JsArray2D<JsArray<_>>(array)
        init (length1 array) (length2 array) (fun i j -> js.[b1+i].[b2+j])

    [<Js>]
    [<CompiledName("CopyTo")>]
    let blit (source : _[,])  sourceIndex1 sourceIndex2 (target : _[,]) targetIndex1 targetIndex2 count1 count2 =
        checkNonNull "source" source
        checkNonNull "target" target
        if sourceIndex1 < 0 then raise (new Exception("Input must be non-negative"))
        if sourceIndex2 < 0 then raise (new Exception("Input must be non-negative"))
        if targetIndex1 < 0 then raise (new Exception("Input must be non-negative"))
        if targetIndex2 < 0 then raise (new Exception("Input must be non-negative"))
        if sourceIndex1 + count1 > (length1 source) then raise (new Exception("Out of range"))
        if sourceIndex2 + count2 > (length2 source) then raise (new Exception("Out of range"))
        if targetIndex1 + count1 > (length1 target) then raise (new Exception("Out of range"))
        if targetIndex2 + count2 > (length2 target) then raise (new Exception("Out of range"))
        let sourceJs = new JsArray2D<JsArray<_>>(source)
        let targetJs = new JsArray2D<JsArray<_>>(target)
        for i = 0 to count1 - 1 do
            for j = 0 to count2 - 1 do
                targetJs.[targetIndex1+i].[targetIndex2+j] <- sourceJs.[sourceIndex1+i].[sourceIndex2+j]