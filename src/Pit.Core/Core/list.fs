#nowarn "42"
namespace Pit.FSharp.Collections
open Pit
open System.Collections
open System.Collections.Generic

[<AliasAttribute("FSharpList1")>]
[<CompiledName("FSharpList")>]
type List<'a> =
    | Empty
    | Cons of 'a * 'a list
    with

        [<Js>]
        member this.Head
            with get() =
                match this with
                | Cons(a,_) -> a
                | Empty -> raise (System.InvalidOperationException("Input list was empty"))

        [<Js>]
        member this.Tail =
            match this with
            | Cons(_,a) -> a
            | Empty     -> raise (System.InvalidOperationException("Input list was empty"))

        [<Js>]
        member this.Length =
            let rec lengthAcc acc (xs:'a list) =
                match xs with
                | Empty      -> acc
                | Cons(_, t) -> lengthAcc (acc+1) t
            lengthAcc 0 this

        [<Js>]
        member this.Item
            with get(index) =
                let rec nth (xs:'a list) n =
                    match xs with
                    | Empty -> raise(new System.ArgumentException("Index out of bounds"))
                    | Cons(h, t) ->
                        if n < 0 then raise(new System.ArgumentException("Index must be non-negative"))
                        elif n = 0 then h
                        else nth t (n-1)
                nth this index

        interface IEnumerable<'a> with
            [<Js>]
            member this.GetEnumerator() = (new ListEnumerator<'a>(this) :> IEnumerator<'a>)
        interface IEnumerable with
            [<Js>]
            member this.GetEnumerator() = (new ListEnumerator<'a>(this) :> IEnumerator)

and 'a list = List<'a>

and ListEnumerator<'T> [<Js>](s:'T list) =
    let mutable curr = s
    let mutable started = false

    [<Js>]
    member this.nonempty x = match x with | Empty -> false | _ -> true

    [<Js>]
    member this.GetCurrent() =
        if started then
            match curr with
            | Empty -> raise (new System.InvalidOperationException("List already finished"))
            | Cons(h, t) -> h
        else
            raise(new System.InvalidOperationException("List not started"))

    interface System.Collections.Generic.IEnumerator<'T> with
        [<Js>]
        member this.Current = this.GetCurrent()

    interface System.Collections.IEnumerator with
        [<Js>]
        member this.MoveNext() =
            if started then
                match curr with
                | Cons(h,t) ->
                    curr <- t;
                    this.nonempty curr
                | _ -> false
            else
                started <- true;
                this.nonempty curr
        [<Js>]
        member this.Current = box (this.GetCurrent())
        [<Js>]
        member this.Reset() =
            started <- false;
            curr <- s

    interface System.IDisposable with
        [<Js>]
        member x.Dispose() = ()


module ListModule =
    open System.Collections
    open System.Collections.Generic

    [<Js>]
    let Length (l: 'a list) = l.Length

    [<Js>]
    let rec revAcc (xs:'a list) acc =
        match xs with
        | Empty -> acc
        | Cons(h, t) -> revAcc t (Cons(h, acc))

    [<Js>]
    let Reverse (xs:'a list) =
        match xs with
        | Empty -> xs
        | Cons(_, Empty) -> xs
        | Cons(h1, Cons(h2, t)) -> revAcc t (Cons(h2,Cons(h1, Empty)))

    [<Js>]
    let ToArray (l:'T list) =
        let len = l.Length
        let res = Array.zeroCreate len
        let rec loop i l =
            match l with
            | Empty -> ()
            | Cons(h,t) ->
                res.[i] <- h
                loop (i+1) t
        loop 0 l
        res

    [<Js>]
    let OfArray (arr:'T[]) =
        let len = arr.Length
        let mutable res = Empty //([]: 'T list)
        let mutable i = len - 1
        while i >= 0 do
            res <- Cons(arr.[i], res)
            i <- i - 1
        res

    [<Js>]
    let rec ForAll f (xs1: 'a list) =
        match xs1 with
        | Empty -> true
        | Cons(h1, t1) -> f h1 && ForAll f t1

    [<Js>]
    let rec forall2aux f list1 list2 =
        match list1,list2 with
        | Empty,Empty -> true
        | Cons(h1,t1),Cons(h2,t2) -> f h1 h2 && forall2aux f t1 t2
        | _ -> raise(System.ArgumentException("List had different lengths"))

    [<Js>]
    let ForAll2 f list1 list2 =
        match list1,list2 with
        | Empty,Empty   -> true
        | _             -> forall2aux f list1 list2

    [<Js>]
    let rec Exists f (xs1: 'a list) =
        match xs1 with
        | Empty -> false
        | Cons(h1, t1) -> f h1 || Exists f t1

    [<Js>]
    let rec exists2aux f list1 list2 =
        match list1,list2 with
        | Empty,Empty -> false
        | Cons(h1,t1),Cons(h2,t2) -> f h1 h2 || exists2aux f t1 t2
        | _ -> raise(System.ArgumentException("List had different lengths"))

    [<Js>]
    let rec Exists2 f list1 list2 =
        match list1,list2 with
        | Empty,Empty   -> false
        | _             -> exists2aux f list1 list2

    [<Js>]
    let rec Find f list =
        match list with
        | Empty     -> raise (System.ArgumentException("Key not found"))
        | Cons(h,t) -> if f h then h else Find f t

    [<Js>]
    let rec TryFind f list =
        match list with
        | Empty     -> None
        | Cons(h,t) -> if f h then Some h else TryFind f t

    [<Js>]
    let rec TryPick f list =
        match list with
        | Empty -> None
        | Cons(h,t) ->
            match f h with
            | None -> TryPick f t
            | r -> r

    [<Js>]
    let rec Pick f list =
        match list with
        | Empty     -> raise (System.ArgumentException("Key not found"))
        | Cons(h,t) ->
            match f h with
            | None -> Pick f t
            | Some r -> r

    [<Js>]
    let TryFindIndex f list =
        let rec loop n = function
        | Empty     -> None
        | Cons(h,t) -> if f h then Some n else loop (n+1) t
        loop 0 list

    [<Js>]
    let FindIndex f list =
        let rec loop n = function
        | Empty     -> raise (System.ArgumentException("Key not found"))
        | Cons(h,t) -> if f h then n else loop (n+1) t
        loop 0 list

    [<Js>]
    let IterateIndexed f (x: 'a list) =
        let rec loop n x = match x with Empty -> () | Cons(h, t) -> f n h; loop (n+1) t
        loop 0 x

    [<Js>]
    let Iterate f (x: 'a list) =
        let rec iter x = match x with Empty -> () | Cons(h, t) -> f h; iter t
        iter x

    [<Js>]
    let Iterate2 f (x1:'a list) (x2:'a list) =
        let rec loop list1 list2 =
            match list1,list2 with
            | Empty,Empty -> ()
            | Cons(h1,t1),Cons(h2,t2) -> f h1 h2; loop t1 t2
            | _ -> raise(System.ArgumentException("Lists have different lengths"))
        loop x1 x2

    [<Js>]
    let IterateIndexed2 f list1 list2 =
        let rec loop n list1 list2 =
            match list1, list2 with
            | Empty,Empty -> ()
            | Cons(h1,t1),Cons(h2,t2) -> f n h1 h2; loop(n+1) t1 t2
            | _ -> raise(System.ArgumentException("Lists have different lengths"))
        loop list1 list2

    [<Js>]
    let Head (list:'a list) = match list with Cons(x, _) -> x | Empty -> raise(System.ArgumentException("List was empty"))

    [<Js>]
    let Tail (list:'a list) = match list with Cons(_, t) -> t | Empty -> raise(System.ArgumentException("List was empty"))

    [<Js>]
    let IsEmpty (list:'a list) = match list with Empty -> true | _ -> false

    [<Js>]
    let rec initConstAcc n x acc =
        if n <= 0 then acc else initConstAcc (n-1) x (Cons(x, acc))

    [<Js>]
    let Replicate count x =
        if count < 0 then raise(System.ArgumentException("Input must be non-negative"))
        initConstAcc count x Empty

    [<Js>]
    let rec initTo (cons: 'a list) i n f =
        if i < n then
            let cons2 = Cons(f i, Empty)
            let cons1 = Cons(cons.Head, cons2)
            initTo cons1 (i+1) n f
        else
            cons

    [<Js>]
    let Initialize count f =
        if count < 0 then raise(System.ArgumentException("Count must be non negative string"))
        elif count = 0 then Empty
        else
            let res = Cons(f 0, Empty)
            initTo res 1 count f

    [<Js>]
    let rec mapTo (cons:'a list) f (x:'a list) =
        match x with
        | Empty -> cons
        | Cons(h, t) ->
            let cons2 = Cons(f h, Empty)
            let cons1 = Cons(cons.Head, cons2)
            mapTo cons1 f t

    [<Js>]
    let Map f (xs:'a list) =
        match xs with
        | Empty -> Empty
        | Cons(h, Empty) -> Cons(f h, Empty)
        | Cons(h, t) ->
            let cons = Cons(f h, Empty)
            mapTo cons f t

    [<Js>]
    let rec mapiTo (cons:'a list) f (x: 'a list) i =
        match x with
        | Empty -> cons
        | Cons(h, t) ->
            let cons2 = Cons(f i h, Empty)
            let cons1 = Cons(cons.Head, cons2)
            mapiTo cons1 f t (i+1)

    [<Js>]
    let MapIndexed f (x:'a list) =
        match x with
        | Empty -> Empty
        | Cons(h, Empty) -> Cons(f 0 h, Empty)
        | Cons(h, t) ->
            let cons = Cons(f 0 h, Empty)
            mapiTo cons f t 1

    [<Js>]
    let rec map2To (cons: 'a list) f (xs1: 'a list) (xs2: 'a list) =
        match xs1, xs2 with
        | Empty, Empty -> cons
        | Cons(h1, t1), Cons(h2, t2) ->
            let cons2 = Cons(f h1 h2, Empty)
            let cons1 = Cons(cons.Head, cons2)
            map2To cons1 f t1 t2
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let Map2 f (xs1: 'a list) (xs2: 'a list) =
        match xs1, xs2 with
        | Empty, Empty -> Empty
        | Cons(h1, t1), Cons(h2, t2) ->
            let cons = Cons(f h1 h2, Empty)
            map2To cons f t1 t2
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let rec map3To (cons: 'a list) f (xs1: 'a list) (xs2: 'a list) (xs3: 'a list) =
        match xs1, xs2, xs3 with
        | Empty, Empty, Empty -> cons
        | Cons(h1,t1), Cons(h2,t2), Cons(h3,t3) ->
            let cons2 = Cons(f h1 h2 h3, Empty)
            let cons1 = Cons(cons.Head, cons2)
            map3To cons1 f t1 t2 t3
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let Map3 f (xs1: 'a list) (xs2: 'a list) (xs3: 'a list) =
        match xs1, xs2, xs3 with
        | Empty, Empty, Empty -> Empty
        | Cons(h1, t1), Cons(h2, t2), Cons(h3,t3) ->
            let cons = Cons(f h1 h2 h3, Empty)
            map3To cons f t1 t2 t3
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let rec mapi2To (cons: 'a list) f (xs1: 'a list) (xs2: 'a list) i =
        match xs1,xs2 with
        | Empty, Empty -> cons
        | Cons(h1, t1), Cons(h2,t2) ->
            let cons2 = Cons(f i h1 h2, Empty)
            let cons1 = Cons(cons.Head, cons2)
            mapi2To cons1 f t1 t2 (i+1)
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let MapIndexed2 f list1 list2 =
        match list1,list2 with
        | Empty,Empty -> Empty
        | Cons(h1,t1),Cons(h2,t2) ->
            let cons = Cons(f 0 h1 h2, Empty)
            mapi2To cons f t1 t2 1
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let Fold<'T,'State> f (s:'State) (list: 'T list) =
        match list with
        | Empty -> s
        | _ ->
            let rec loop s xs =
                match xs with
                | Empty -> s
                | Cons(h,t) -> loop (f s h) t
            loop s list

    [<Js>]
    let Reduce f list =
        match list with
        | Empty -> raise(System.ArgumentException("Input List is empty"))
        | Cons(h,t) -> Fold f h t

    [<Js>]
    let Scan<'T,'State> f (s:'State) (list:'T list) =
        let rec loop s xs acc =
            match xs with
            | Empty -> Reverse acc
            | Cons(h,t) -> let s = f(s,h) in loop s t (Cons(s, acc))
        loop s list (Cons(s,Empty))

    [<Js>]
    let Fold2<'T1,'T2,'State> f (acc:'State) (list1:list<'T1>) (list2:list<'T2>) =
        let rec loop acc list1 list2 =
            match list1,list2 with
            | Empty,Empty -> acc
            | Cons(h1,t1),Cons(h2,t2) -> loop (f acc h1 h2) t1 t2
            | _ -> raise(System.ArgumentException("List had different lengths"))
        loop acc list1 list2

    [<Js>]
    let foldArraySubRight f (arr: 'T[]) start fin acc =
        let mutable state = acc
        let mutable i = fin
        while i >= start do
            state <- f arr.[i] state
            i <- i - 1
        state
        (*for i = fin downto start do
            state <- f(arr.[i], state)
        state*)

    [<Js>]
    let FoldBack<'T,'State> f (list:'T list) (acc:'State) =
        match list with
        | Empty -> acc
        | Cons(h, Empty) -> f h acc
        | Cons(h1, Cons(h2, Empty)) -> f h1 (f h2 acc)
        | Cons(h1, Cons(h2, Cons(h3, Empty))) -> f h1 (f h2 (f h3 acc))
        | Cons(h1, Cons(h2, Cons(h3, Cons(h4, Empty)))) -> f h1 (f h2 (f h3 (f h4 acc)))
        | _ ->
            // It is faster to allocate and iterate an array than to create all those
            // highly nested stacks.  It also means we won't get stack overflows here.
            let arr = ToArray list
            let arrn = arr.Length
            foldArraySubRight f arr 0 (arrn - 1) acc

    [<Js>]
    let ReduceBack f list =
        match list with
        | Empty -> raise(System.ArgumentException("List had different lengths"))
        | _ ->
            let arr = ToArray list
            let arrn = arr.Length
            foldArraySubRight f arr 0 (arrn - 2) arr.[arrn - 1]

    [<Js>]
    let scanArraySubRight<'T,'State> (f:'T -> 'State -> 'State) (arr:_[]) start fin initState =
        let mutable state = initState
        let mutable res = Cons(state, Empty)
        let mutable i = fin
        while i >= start do
            state <- f arr.[i] state
            res <- Cons(state, res)
            i <- i - 1
        res
        (*for i = fin downto start do
            state <- f.Invoke(arr.[i], state);
            res <- state :: res
        res*)

    [<Js>]
    let ScanBack<'T,'State> f (list:'T list) (s:'State) =
        match list with
        | Empty -> Cons(s, Empty)
        | Cons(h, Empty) -> Cons(f h s, Cons(s, Empty))
        | _ ->
            // It is faster to allocate and iterate an array than to create all those
            // highly nested stacks.  It also means we won't get stack overflows here.
            let arr = ToArray list
            let arrn = arr.Length
            scanArraySubRight f arr 0 (arrn - 1) s
    [<Js>]
    let foldBack2UsingArrays (f: 'T1 -> 'T2 -> 'State -> 'State) list1 list2 acc =
        let arr1 = ToArray list1
        let arr2 = ToArray list2
        let n1 = arr1.Length
        let n2 = arr2.Length
        if n1 <> n2 then raise(System.ArgumentException("List had different lengths"))
        let mutable res = acc
        let mutable i = n1
        while i >= 0 do
            res <- f arr1.[i] arr2.[i] res
            i <- i - 1
        res
        (*for i = n1 - 1 downto 0 do
            res <- f arr1.[i] arr2.[i] res
        res*)

    [<Js>]
    let rec FoldBack2<'T1,'T2,'State> (f: 'T1 -> 'T2 -> 'State -> 'State) (list1:'T1 list) (list2:'T2 list) (acc:'State) =
        match list1,list2 with
        | Empty,Empty -> acc
        | Cons(h1,rest1), Cons(k1,rest2) ->
            match rest1, rest2 with
            | Empty,Empty -> f h1 k1 acc
            | Cons(h2, Empty), Cons(k2, Empty) -> f h1 k1 (f h2 k2 acc)
            | Cons(h2, Cons(h3, Empty)), Cons(k2, Cons(k3, Empty)) -> f h1 k1 (f h2 k2 (f h3 k3 acc))
            | Cons(h2, Cons(h3, Cons(h4, Empty))), Cons(k2, Cons(k3, Cons(k4, Empty))) -> f h1 k1 (f h2 k2 (f h3 k3 (f h4 k4 acc)))
            | _ -> foldBack2UsingArrays f list1 list2 acc
        | _ -> raise(System.ArgumentException("List had different lengths"))

    [<Js>]
    let rec appendTo (cons:'a list) (xs: 'a list) =
        match xs with
        | Empty -> cons
        | Cons(h, t) ->
            let cons2 = Cons(h, Empty)
            let cons1 = Cons(cons.Head, cons2)
            appendTo cons1 t

    [<Js>]
    let Append (list1:'a list) (list2:'a list) = //appendTo list1 list2
        Seq.append list1 list2 |> Seq.toList

    [<Js>]
    let rec Get (list:'a list) index =
        match list with
        | Cons(h,t) when index >= 0 ->
            if index = 0 then h else Get t (index - 1)
        | _ ->
            raise(System.InvalidOperationException("Index Out of bounds"))

    [<Js>]
    let rec chooseAllAcc f xs acc =
        match xs with
        | Empty -> Reverse acc
        | Cons(h,t) ->
                match f h with
                | None -> chooseAllAcc f t acc
                | Some x -> chooseAllAcc f t (Cons(x, acc))

    [<Js>]
    let Choose f xs = chooseAllAcc f xs Empty

    [<Js>]
    let rec collectTo (f:'t -> 'u list) (list:'t list) cons =
        match list with
        | Empty -> cons
        | Cons(h, t) ->
            collectTo f t (appendTo cons (f h))

    (*[<Js>]
    let rec Collect (f:'t -> 'u list) (list:'t list) =
        match list with
        | Empty -> Empty
        | Cons(h, Empty) -> f h
        | Cons(h, t) ->
            let cons = Cons(Unchecked.defaultof<'u>, Empty)
            let c = collectTo f list cons
            c.Tail*)

    [<Js>]
    let Collect f list =
        list
        |> List.toSeq
        |> Seq.collect f
        |> Seq.toList

    [<Js>]
    let Filter f l =
        l
        |> List.toSeq
        |> Seq.filter f
        |> Seq.toList

    (*[<Js>]
    let rec filterTo (cons: 'a list) f l =
        match l with
        | Empty -> cons
        | Cons(h, t) ->
            if f h then
                let cons2 = Cons(h, Empty)
                let cons1 = Cons(cons.Head, cons2)
                filterTo cons1 f t
            else
                filterTo cons f t

    [<Js>]
    let rec Filter f l =
        match l with
        | Empty -> l
        | Cons(h, Empty) -> if f h then l else Empty
        | Cons(h, t) ->
            if f h then
                let cons = Cons(h, Empty)
                filterTo cons f t
            else
                Filter f t*)

    [<Js>]
    let rec concatTo (cons: 'a list) h1 l =
        match l with
        | Empty -> cons
        | Cons(h2, t) -> concatTo (appendTo cons h1) h2 t

    [<Js>]
    let rec concatToEmpty l =
        match l with
        | Empty -> Empty
        | Cons(Empty, t) -> concatToEmpty t
        | Cons(Cons(h, t1), tt2) ->
            let res = Cons(h, Empty)
            concatTo res t1 tt2

    [<Js>]
    let seqToList (e:seq<_>)=
        use ie = e.GetEnumerator()
        let mutable res = Empty
        while ie.MoveNext() do
            res <- Cons(ie.Current, res)
        res

    [<Js>]
    let OfSeq (l:seq<_>) =
        seqToList(l)

    [<Js>]
    let ToSeq (l:'t list) =
        (l :> IEnumerable<'t>)

    [<Js>]
    let Concat (l:seq<_>)=
        l
        |> Seq.concat
        |> Seq.toList
        (*match seqToList l with
        | Empty                     -> Empty
        | Cons(h, Empty)            -> h
        | Cons(h1, Cons(h2, Empty)) -> appendTo h1 h2
        | x                         -> concatToEmpty x*)

    (*let seqToList (e: IEnumerable<'t>) =
//        match e with
//        | :? list<'t> as l -> l
//        | _ ->
        use ie = e.GetEnumerator()
        let mutable res = []
        while ie.MoveNext() do
            res <- Cons(ie.Current, res)
        Reverse res*)

    [<Js>]
    let rec partitionTo (consL: 'a list) (consR: 'a list) p l =
        match l with
        | Empty ->
            consL, consR
        | Cons(h, t) ->
            let cons11 = Cons(h, Empty)
            if p h then
                let cons1 = Cons(consL.Head, cons11)
                partitionTo cons1 consR p t
            else
                let cons1 = Cons(consR.Head, cons11)
                partitionTo consL cons1 p t

    [<Js>]
    let rec partitionToTailLeft (consL:'a list) p l =
        match l with
        | Empty ->
            consL, Empty
        | Cons(h, t) ->
            let cons11 = Cons(h, Empty)
            if p h then
                let cons1 = Cons(consL.Head, cons11)
                partitionToTailLeft cons1 p t
            else
                partitionTo consL cons11 p t

    [<Js>]
    let rec partitionToTailRight (consR:'a list) p l =
        match l with
        | Empty ->
            consR, Empty
        | Cons(h, t) ->
            let cons11 = Cons(h, Empty)
            if p h then
                let cons1 = Cons(consR.Head, cons11)
                partitionToTailRight cons1 p t
            else
                partitionTo consR cons11 p t

    [<Js>]
    let Partition p l =
        match l with
        | Empty -> l,l
        | Cons(h, Empty) -> if p h then l,Empty else Empty,l
        | Cons(h, t) ->
            let cons = Cons(h, Empty)
            if p h then
                let (consL,_) = partitionToTailLeft cons p t
                cons,consL
            else
                let (_,consR) = partitionToTailRight cons p t
                consR,cons

    [<Js>]
    let rec unzipTo (cons1a:'a list) (cons1b:'a list) x =
        match x with
        | Empty ->
            let cons1 = Cons(cons1a.Head, Empty)
            let cons2 = Cons(cons1b.Head, Empty)
            cons1, cons2
        | Cons((h1,h2),t) ->
            let cons1 = Cons(cons1a.Head, Cons(h1, Empty))
            let cons2 = Cons(cons1b.Head, Cons(h2, Empty))
            unzipTo cons1 cons2 t

    [<Js>]
    let Unzip x =
        match x with
        | Empty -> Empty, Empty
        | Cons((h1,h2), t) ->
            let res1a = Cons(h1, Empty)
            let res1b = Cons(h2, Empty)
            unzipTo res1a res1b t

    [<Js>]
    let rec unzipTo3 (cons1a:'a list) (cons1b:'a list) (cons1c:'a list) x =
        match x with
        | Empty ->
            let cons1 = Cons(cons1a.Head, Empty)
            let cons2 = Cons(cons1b.Head, Empty)
            let cons3 = Cons(cons1c.Head, Empty)
            cons1, cons2, cons3
        | Cons((h1,h2,h3),t) ->
            let cons1 = Cons(cons1a.Head, Cons(h1, Empty))
            let cons2 = Cons(cons1b.Head, Cons(h2, Empty))
            let cons3 = Cons(cons1c.Head, Cons(h3, Empty))
            unzipTo3 cons1 cons2 cons3 t

    [<Js>]
    let Unzip3 x =
        match x with
        | Empty -> Empty, Empty, Empty
        | Cons((h1,h2,h3), t) ->
            let res1a = Cons(h1, Empty)
            let res1b = Cons(h2, Empty)
            let res1c = Cons(h3, Empty)
            unzipTo3 res1a res1b res1c t

    [<Js>]
    let Permute f list =
        let array = ToArray(list)
        let res = Array.permute f array
        OfArray(res)

    [<Js>]
    let rec zipTo (cons:('a * 'b) list) xs1 xs2 =
        match xs1,xs2 with
        | Empty, Empty -> cons
        | Cons(h1, t1), Cons(h2, t2) ->
            let cons2 = Cons((h1, h2), Empty)
            let cons1 = Cons(cons.Head, cons2)
            zipTo cons1 t1 t2
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let Zip xs1 xs2 =
        match xs1, xs2 with
        | Empty, Empty -> Empty
        | Cons(h1,t1),Cons(h2,t2) ->
            let res = Cons((h1,h2), Empty)
            zipTo res t1 t2
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let rec zipTo3 (cons:('a * 'b * 'c) list) xs1 xs2 xs3 =
        match xs1, xs2, xs3 with
        | Empty, Empty, Empty -> cons
        | Cons(h1, t1), Cons(h2, t2), Cons(h3, t3) ->
            let cons2 = Cons((h1,h2,h3), Empty)
            let cons1 = Cons(cons.Head, cons2)
            zipTo3 cons1 t1 t2 t3
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let Zip3 xs1 xs2 xs3 =
        match xs1, xs2, xs3 with
        | Empty, Empty, Empty -> Empty
        | Cons(h1,t1), Cons(h2,t2), Cons(h3,t3) ->
            let res = Cons((h1,h2,h3), Empty)
            zipTo3 res t1 t2 t3
        | _ -> raise(System.InvalidOperationException("Lists have different lengths"))

    [<Js>]
    let Sum (list:list<int>) =
        list |> ToArray |> Array.sum

    [<Js>]
    let SumBy f (list:list<int>) =
        let s = ref 0
        list |> Iterate(fun i ->
            let t = f i
            s := t + !s
        )
        !s

    [<Js>]
    let Max (list:list<int>) =
        list |> ToArray |> Array.max

    [<Js>]
    let MaxBy f (list:list<int>) =
        list |> ToArray |> Array.maxBy f

    [<Js>]
    let Min (list:list<int>) =
        list |> ToArray |> Array.min

    [<Js>]
    let MinBy f (list:list<int>) =
        list |> ToArray |> Array.minBy f

    [<Js>]
    let Average (list:list<int>) =
        let acc = Sum(list)
        acc / list.Length

    [<Js>]
    let AverageBy f (list:list<int>) =
        let acc = SumBy f list
        acc / list.Length

    [<Js>]
    let SortWith cmp (xs:list<_>) =
        let e = xs |> ToArray
        Array.sortWith cmp e
        |> OfArray

    [<Js>]
    let SortBy f xs =
        xs
        |> List.toArray
        |> Array.sortBy f
        |> List.ofArray

    [<Js>]
    let Sort xs =
        xs
        |> List.toArray
        |> Array.sort
        |> List.ofArray

    [<Js>]
    let Empty<'T>() = (Empty:'T list)