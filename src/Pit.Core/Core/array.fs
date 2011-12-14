namespace Pit.FSharp.Collections
open Pit
open Pit.Javascript
open System

module ArrayModule =

    [<Js>]
    let checkNonNull argName arg =
        match arg with
        | null -> raise(new Exception("null"))
        | _ -> ()

    [<Js>]
    let Length (array: _[]) = array.Length

    [<Js>]
    let Initialize<'a> count f =
        let result : 'a[] = [||]
        for i = 0 to count - 1 do
            result.[i] <- f i
        result

    [<Js>]
    let ZeroCreate count =
        if count < 0 then raise (new Exception("Input must be non-negative"))
        let array : int[] = [||] // no matter what the type, the js will work, just initialize it like this
        for i=0 to count-1 do
            array.[i] <- 0
        array

    [<Js>]
    let Create (count:int) (x:'T) =
        if count < 0 then raise(new Exception("Invalid count"))
        let array : 'T[] = [||]
        for i = 0 to count - 1 do
            array.[i] <- x
        array

    [<Js>]
    let IsEmpty (array:_[]) =
        array.Length = 0

    [<Js>]
    let Empty<'T> = ([||]:'T[])

    [<Js>]
    let CopyTo (sourceArray: 'T[]) (sourceIndex : int) (destinationArray: 'T[]) (destinationIndex : int) (length: int) =
        checkNonNull "array" sourceArray
        checkNonNull "array" destinationArray
        let mutable d = destinationIndex
        for i = sourceIndex to length - 1 do
            destinationArray.[d] <- sourceArray.[i]
            d <- d + 1

    [<Js>]
    let rec concatAddLengths (arrs:'T[][]) i acc =
            if i >= arrs.Length then acc
            else concatAddLengths arrs (i+1) (acc + arrs.[i].Length)

    [<Js>]
    let rec concatBlit (arrs:'T[][]) i j (tgt:'T[]) =
        if i < arrs.Length then
            let h = arrs.[i]
            let len = h.Length
            CopyTo h 0 tgt j len
            concatBlit arrs (i+1) (j+len) tgt

    [<Js>]
    let concatArrays (arrs : 'T[][]) =
        let res = Array.zeroCreate (concatAddLengths arrs 0 0)
        concatBlit arrs 0 0 res ;
        res

    [<Js>]
    let Concat (arrays: seq<'T[]>) =
        checkNonNull "arrays" arrays
        arrays |> Seq.toArray |> concatArrays
        //match arrays with
        //| :? ('T[][]) as ts -> ts |> concatArrays // avoid a clone, since we only read the array
        //| _ -> arrays |> Seq.toArray |> concatArrays

    [<Js>]
    let Collect (f : 'T -> 'U[])  (array : 'T[]) : 'U[]=
        checkNonNull "array" array
        let len = array.Length
        let result : 'U[][] = [||]
        for i = 0 to len - 1 do
            result.[i] <- f array.[i]
        concatArrays result

    [<Js>]
    let Append (array1:'T[]) (array2:'T[]) =
        checkNonNull "array" array1
        checkNonNull "array" array2
        let n1 = array1.Length
        let n2 = array2.Length
        let res : 'T[] = [||]
        CopyTo array1 0 res 0 n1
        CopyTo array2 0 res n1 n2
        res

    [<Js>]
    let Copy(array:'T[]) =
        let len = array.Length
        let res = Array.zeroCreate len
        for i=0 to len-1 do
            res.[i] <- array.[i]
        res

    [<Js>]
    let Iterate f (array : 'T[]) =
        checkNonNull "array" array
        for i = 0 to array.Length - 1 do
            f array.[i]

    [<Js>]
    let Map (f: 'T -> 'U) (array: 'T[]) =
        checkNonNull "array" array
        let len = array.Length
        let res : 'U[] =  [||]
        for i = 0 to len - 1 do
            res.[i] <- f array.[i]
        res

    [<Js>]
    let Iterate2 f (array1:'T[]) (array2:'U[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("Arrays have different length"))
        for i=0 to len1-1 do
            f array1.[i] array2.[i]

    [<Js>]
    let Map2 f (array1:'t[]) (array2:'u[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("Arrays have different length"))
        let res : 'u[] = [||]
        for i=0 to len1-1 do
            res.[i] <- f array1.[i] array2.[i]
        res

    [<Js>]
    let MapIndexed2 f (array1:'t[]) (array2:'u[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("Arrays have different length"))
        let res : 'u[] = [||]
        for i=0 to len1-1 do
            res.[i] <- f i array1.[i] array2.[i]
        res

    [<Js>]
    let IterateIndexed f (array:'T[]) =
        checkNonNull "array" array
        let len = array.Length
        for i = 0 to len - 1 do
            f i array.[i]

    [<Js>]
    let IterateIndexed2 f (array1:'t[]) (array2:'u[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("Arrays have different length"))
        let res : 'u[] = [||]
        for i=0 to len1-1 do
            res.[i] <- f i array1.[i] array2.[i]
        res

    [<Js>]
    let MapIndexed (f : int -> 'T -> 'U) (array: 'T[]) =
        checkNonNull "array" array
        let len = array.Length
        let res : 'U[] = [||]
        for i = 0 to len - 1 do
            res.[i] <- f(i)(array.[i])
        res

    [<Js>]
    let Exists (f: 'T -> bool) (array:'T[]) =
        checkNonNull "array" array
        let len = array.Length
        let rec loop i = i < len && (f array.[i] || loop (i+1))
        loop 0

    [<Js>]
    let Exists2 f (array1: 'T[]) (array2: 'T[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("Invalid argument array2"))
        let rec loop i = i < len1 && (f (array1.[i])(array2.[i]) || loop (i+1))
        loop 0

    [<Js>]
    let ForAll (f: 'T -> bool) (array:'T[]) =
        checkNonNull "array" array
        let len = array.Length
        let rec loop i = i >= len || (f array.[i] && loop (i+1))
        loop 0

    [<Js>]
    let ForAll2 f (array1: 'T[]) (array2: 'T[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("invalid argument array2"))
        let rec loop i = i >= len1 || (f (array1.[i]) (array2.[i]) && loop (i+1))
        loop 0

    [<Js>]
    let Pick f (array: _[]) =
        checkNonNull "array" array
        let rec loop i =
            if i >= array.Length then
                raise (new Exception("Key not found"))
            else
                match f array.[i] with
                | None -> loop(i+1)
                | Some res -> res
        loop 0

    [<Js>]
    let TryPick f (array: _[]) =
        checkNonNull "array" array
        let rec loop i =
            if i >= array.Length then None else
            match f array.[i] with
            | None -> loop(i+1)
            | res -> res
        loop 0

    [<Js>]
    let Choose f (array:'t[]) =
        checkNonNull "array" array
        let res : 't[] = [||]
        let mutable j = 0
        for i=0 to array.Length - 1 do
            match f array.[i] with
            | None      -> ()
            | Some(t)   ->
                res.[j] <- t
                j <- j + 1
        res

    [<Js>]
    let Filter f (array:'t[]) =
        checkNonNull "array" array
        let res:'t[] = [||]
        let mutable j=0
        for i=0 to array.Length - 1 do
            let x = array.[i]
            if f x then
                res.[j] <- x
                j <- j+1
        res

    [<Js>]
    let Partition f (array:'t[]) =
        checkNonNull "array" array
        let res1 :'t[] = [||]
        let res2 :'t[] = [||]
        let mutable j1=0
        let mutable j2=0
        for i=0 to array.Length-1 do
            let x = array.[i]
            if f x then
                res1.[j1]<-x
                j1<-j1+1
            else
                res2.[j2]<-x
                j2<-j2+1
        res1,res2

    [<Js>]
    let Find f (array: _[]) =
        checkNonNull "array" array
        let rec loop i =
            if i >= array.Length then raise (new Exception("Key not found")) else
            if f array.[i] then array.[i]  else loop (i+1)
        loop 0

    [<Js>]
    let TryFind f (array: _[]) =
        checkNonNull "array" array
        let rec loop i =
            if i >= array.Length then None else
            if f array.[i] then Some array.[i]  else loop (i+1)
        loop 0

    [<Js>]
    let Zip (array1: 'T[]) (array2: 'T[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("invalid argument length array2"))
        let res : ('T * 'T)[] = [||]
        for i = 0 to len1 - 1 do
            res.[i] <- (array1.[i],array2.[i])
        res

    [<Js>]
    let Zip3 (array1: 'T[]) (array2: 'T[]) (array3: 'T[]) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        checkNonNull "array3" array3
        let len1 = array1.Length
        if len1 <> array2.Length then raise(new Exception("invalid argument length array2"))
        if len1 <> array3.Length then raise(new Exception("invalid argument length array3"))
        let res : ('T * 'T * 'T)[] = [||]
        for i = 0 to len1 - 1 do
            res.[i] <- (array1.[i],array2.[i],array3.[i])
        res

    [<Js>]
    let Unzip (array: ('a * 'b)[]) =
        checkNonNull "array" array
        let len = array.Length
        let res1 : 'a[] = [||]
        let res2 : 'b[] = [||]
        for i = 0 to len - 1 do
            let x, y = array.[i]
            res1.[i] <- x;
            res2.[i] <- y;
        res1, res2

    [<Js>]
    let Unzip3 (array: ('a * 'b * 'c)[]) =
        checkNonNull "array" array
        let len = array.Length
        let res1 : 'a[] = [||]
        let res2 : 'b[] = [||]
        let res3 : 'c[] = [||]
        for i = 0 to len - 1 do
            let x,y,z = array.[i]
            res1.[i] <- x;
            res2.[i] <- y;
            res3.[i] <- z;
        res1,res2,res3

    [<Js>]
    let Reverse (array: 'T[]) =
        checkNonNull "array" array
        let len = array.Length
        let res : 'T[] = [||]
        for i = 0 to len - 1 do
            res.[(len - i) - 1] <- array.[i]
        res

    [<Js>]
    let Fold<'T,'State> (f : 'State -> 'T -> 'State) (acc: 'State) (array:'T[]) =
        checkNonNull "array" array
        let mutable state = acc
        let len = array.Length
        for i = 0 to len - 1 do
            state <- f(state)(array.[i])
        state

    [<Js>]
    let FoldBack<'T,'State> (f : 'T -> 'State -> 'State) (array:'T[]) (acc: 'State) =
        checkNonNull "array" array
        let mutable res = acc
        let len = array.Length - 1
        for i = 0 to len do
            res <- f (array.[len - i]) (res)
        res

    [<Js>]
    let FoldBack2<'T1,'T2,'State> f (array1:'T1[]) (array2:'T2 []) (acc: 'State) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let mutable res = acc
        let len = array1.Length - 1
        if len <> array2.Length - 1 then raise(new Exception("invalid argument length array2"))
        for i = 0 to len do
            res <- f (array1.[len - i]) (array2.[len - i]) (res)
        res

    [<Js>]
    let Fold2<'T1,'T2,'State>  f (acc: 'State) (array1:'T1[]) (array2:'T2 []) =
        checkNonNull "array1" array1
        checkNonNull "array2" array2
        let mutable state = acc
        let len = array1.Length
        if len <> array2.Length then raise(new Exception("invalid argument length array2"))
        for i = 0 to len - 1 do
            state <- f(state)(array1.[i])(array2.[i])
        state

    [<Js>]
    let FoldSubRight f (array : 'T[]) start fin acc =
        checkNonNull "array" array
        let mutable res = acc
        let mutable i = fin
        while i >= start do
            res <- f array.[i] res
            i <- i - 1
        res
        (*for i = 0 to fin - 1 do
            res <- f(array.[fin - i])(res)
        res*)

    [<Js>]
    let ScanSubRight f (array : 'T[]) start fin initState =
        checkNonNull "array" array
        let mutable state = initState
        let res = Create (2+fin-start) initState
        let mutable i = fin
        while i >= start do
            state <- f(array.[i])(state)
            res.[i-start] <- state
            i <- i - 1
        res
        (*for i = 0 to fin do
            state <- f(array.[fin - i])(state)
            res.[i-start] <- state
        res*)

    [<Js>]
    let ScanSubLeft f  initState (array : _[]) start fin =
        checkNonNull "array" array
        let mutable state = initState
        let res = Create (2+fin-start) initState
        for i = start to fin do
            state <- f(state)(array.[i])
            res.[i - start+1] <- state
        res

    [<Js>]
    let Scan<'T,'State> f (acc:'State) (array : 'T[]) =
        checkNonNull "array" array
        let len = array.Length
        ScanSubLeft f acc array 0 (len - 1)

    [<Js>]
    let ScanBack<'T,'State> f (array : 'T[]) (acc:'State) =
        checkNonNull "array" array
        let len = array.Length
        ScanSubRight f array 0 (len - 1) acc

    [<Js>]
    let Reduce f (array : _[]) =
        checkNonNull "array" array
        let len = array.Length
        if len = 0 then
            raise(new Exception("input array empty"))
        else
            let mutable res = array.[0]
            for i = 1 to len - 1 do
                res <- f(res)(array.[i])
            res

    [<Js>]
    let ReduceBack f (array : _[]) =
        checkNonNull "array" array
        let len = array.Length
        if len = 0 then raise(new Exception("input array empty"))
        else FoldSubRight f array 0 (len - 2) array.[len - 1]

    [<Js>]
    let SortInPlace (source:'T[]) =
        checkNonNull "source" source
        let source = new JsArray<_>(source)
        source.Sort(new JsArraySortDelegate
            (fun a b ->
                if a < b then -1
                else
                    if a=b then 0
                    else 1))
        //source.Sort(new JsArraySortDelegate(fun a b -> a-b))
        //source.SortInPlace()

    [<Js>]
    let SortInPlaceBy f (source:'T[]) =
        checkNonNull "source" source
        let source = new JsArray<_>(source)
        source.Sort(
            new JsArraySortDelegate(
                fun a b ->
                    let item1 = f(a)
                    let item2 = f(b)
                    item1-item2))
        //source.SortInPlaceBy(f)

    [<Js>]
    let SortInPlaceWith f (source:'T[]) =
        checkNonNull "source" source
        let source = new JsArray<_>(source)
        source.Sort( new JsArraySortDelegate( fun a b -> f a b ) )
        //source.SortInPlaceWith(f)

    [<Js>]
    let SortWith f (source:'T[]) =
        checkNonNull "source" source
        let result = Array.copy source
        Array.sortInPlaceWith f result;
        result

    [<Js>]
    let SortBy f array =
        checkNonNull "source" array
        let result = Array.copy array
        Array.sortInPlaceBy f result
        result

    [<Js>]
    let Sort array =
        checkNonNull "source" array
        let result = Array.copy array
        Array.sortInPlace result
        result

    [<Js>]
    let FindIndex f (array : _[]) =
        checkNonNull "array" array
        let len = array.Length
        let rec go n =
            if n >= len then
                raise (new Exception("Key not found"))
            elif f array.[n] then
                n
            else go (n+1)
        go 0

    [<Js>]
    let TryFindIndex f (array : _[]) =
        checkNonNull "array" array
        let len = array.Length
        let rec go n = if n >= len then None elif f array.[n] then Some n else go (n+1)
        go 0

    [<Js>]
    let Permute f (array:'t[]) =
        checkNonNull "array" array
        let arr :'t[] = [||]
        for i=0 to array.Length-1 do
            let idx = f i
            arr.[idx] <- array.[i]
        arr

    [<Js>]
    let Sum (array: ('T)[] ) =
        checkNonNull "array" array
        let mutable acc = 0
        for i = 0 to array.Length - 1 do
            acc <- acc + array.[i]
        acc

    [<Js>]
    let SumBy (f: 'T -> ^U) (array:'T[]) =
        checkNonNull "array" array
        let mutable acc = 0
        for i = 0 to array.Length - 1 do
            acc <- acc + (f array.[i])
        acc

    [<Js>]
    let Min (array:_[]) =
        checkNonNull "array" array
        if array.Length = 0 then raise(new Exception("Invalid array"))
        let mutable acc = array.[0]
        for i = 1 to array.Length - 1 do
            let curr = array.[i]
            if curr < acc then
                acc <- curr
        acc

    [<Js>]
    let MinBy f (array:_[]) =
        checkNonNull "array" array
        if array.Length = 0 then raise(new Exception("Invalid array"))
        let mutable accv = array.[0]
        let mutable acc = f accv
        for i = 1 to array.Length - 1 do
            let currv = array.[i]
            let curr = f currv
            if curr < acc then
                acc <- curr
                accv <- currv
        accv

    [<Js>]
    let Max (array:_[]) =
        checkNonNull "array" array
        if array.Length = 0 then raise(new Exception("Invalid array"))
        let mutable acc = array.[0]
        for i = 1 to array.Length - 1 do
            let curr = array.[i]
            if curr > acc then
                    acc <- curr
        acc

    [<Js>]
    let MaxBy f (array:_[]) =
        checkNonNull "array" array
        if array.Length = 0 then raise(new Exception("Invalid array"))
        let mutable accv = array.[0]
        let mutable acc = f accv
        for i = 1 to array.Length - 1 do
            let currv = array.[i]
            let curr = f currv
            if curr > acc then
                acc <- curr
                accv <- currv
        accv

    [<Js>]
    let Average (array:_[]) =
        checkNonNull "array" array
        if array.Length = 0 then raise(new Exception("Invalid array"))
        let mutable acc = 0
        let mutable count = 0
        for i = 0 to array.Length - 1 do
            acc <- acc + array.[i]
            count <- count + 1
        (acc/count)

    [<Js>]
    let AverageBy f (array:_[]) =
        checkNonNull "array" array
        if array.Length = 0 then raise(new Exception("Invalid array"))
        let mutable acc = 0
        let mutable count = 0
        for i = 0 to array.Length - 1 do
            acc <- acc + (f array.[i])
            count <- count + 1
        (acc/count)

    [<Js>]
    let GetSubArray (array:'T[]) (startIndex:int) (count:int) =
        checkNonNull "array" array
        if startIndex < 0 then raise(new Exception("Input must be non-negative"))
        if count < 0 then raise(new Exception("Input must be non-negative"))
        if startIndex + count > array.Length then raise(new Exception("Length out of range"))
        let res : 'T[] = [||]
        for i = 0 to count - 1 do
            res.[i] <- array.[startIndex + i]
        res

    [<Js>]
    let Get(array:_[]) n =
        array.[n]

    [<Js>]
    let Set(array:_[]) n v =
        array.[n] <- v

    [<Js>]
    let Fill (target:'T[]) (targetIndex:int) (count:int) (x:'T) =
        checkNonNull "target" target
        if targetIndex < 0 then raise(new Exception("Input must be non-negative"))
        if count < 0 then raise(new Exception("Input must be non-negative"))
        for i = targetIndex to targetIndex + count - 1 do
            target.[i] <- x

    [<Js>]
    let ToList (array:_[]) = List.ofArray array

    [<Js>]
    let OfList (list:'a list) = List.toArray list

    [<Js>]
    let ToSeq (array:_[]) = Seq.ofArray array

    [<Js>]
    let OfSeq (e:seq<_>) = Seq.toArray e