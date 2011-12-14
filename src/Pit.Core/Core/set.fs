namespace Pit.FSharp.Collections

open Pit
open Pit.Javascript
open System
open System.Collections
open System.Collections.Generic

    [<CompilationRepresentation(CompilationRepresentationFlags.UseNullAsTrueValue)>]
    [<NoEquality; NoComparison>]
    type SetTree<'T> when 'T : comparison =
        | SetEmpty                                          // height = 0
        | SetNode of 'T * SetTree<'T> *  SetTree<'T> * int    // height = int
        | SetOne  of 'T                                     // height = 1

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module SetTree =

        let [<Js>] height t =
            match t with
            | SetEmpty          -> 0
            | SetOne _          -> 1
            | SetNode (_,_,_,h) -> h

    #if CHECKED
        let rec checkInvariant t =
            // A good sanity check, loss of balance can hit perf
            match t with
            | SetEmpty -> true
            | SetOne _ -> true
            | SetNode (k,t1,t2,h) ->
                let h1 = height t1
                let h2 = height t2
                (-2 <= (h1 - h2) && (h1 - h2) <= 2) && checkInvariant t1 && checkInvariant t2
    #endif

        let [<Js>] tolerance = 2

        let [<Js>] mk l k r =
            match l,r with
            | SetEmpty,SetEmpty -> SetOne (k)
            | _ ->
              let hl = height l
              let hr = height r
              let m = if hl < hr then hr else hl
              SetNode(k,l,r,m+1)

        let [<Js>] rebalance t1 k t2 =
            let t1h = height t1
            let t2h = height t2
            if  t2h > t1h + tolerance then // right is heavier than left
                match t2 with
                | SetNode(t2k,t2l,t2r,_) ->
                    // one of the nodes must have height > height t1 + 1
                    if height t2l > t1h + 1 then  // balance left: combination
                        match t2l with
                        | SetNode(t2lk,t2ll,t2lr,_) ->
                            mk (mk t1 k t2ll) t2lk (mk t2lr t2k t2r)
                        | _ -> failwith "rebalance"
                    else // rotate left
                        mk (mk t1 k t2l) t2k t2r
                | _ -> failwith "rebalance"
            else
                if  t1h > t2h + tolerance then // left is heavier than right
                    match t1 with
                    | SetNode(t1k,t1l,t1r,_) ->
                        // one of the nodes must have height > height t2 + 1
                        if height t1r > t2h + 1 then
                            // balance right: combination
                            match t1r with
                            | SetNode(t1rk,t1rl,t1rr,_) ->
                                mk (mk t1l t1k t1rl) t1rk (mk t1rr k t2)
                            | _ -> failwith "rebalance"
                        else
                            mk t1l t1k (mk t1r k t2)
                    | _ -> failwith "rebalance"
                else mk t1 k t2

        let rec [<Js>] add (comparer: IComparer<'T>) k t =
            match t with
            | SetNode (k2,l,r,_) ->
                let c = comparer.Compare(k,k2)
                if   c < 0 then rebalance (add comparer k l) k2 r
                elif c = 0 then t
                else            rebalance l k2 (add comparer k r)
            | SetOne(k2) ->
                // nb. no check for rebalance needed for small trees, also be sure to reuse node already allocated
                let c = comparer.Compare(k,k2)
                if c < 0   then SetNode (k,SetEmpty,t,2)
                elif c = 0 then t
                else            SetNode (k,t,SetEmpty,2)
            | SetEmpty -> SetOne(k)

        let rec [<Js>] balance comparer t1 k t2 =
            // Given t1 < k < t2 where t1 and t2 are "balanced",
            // return a balanced tree for <t1,k,t2>.
            // Recall: balance means subtrees heights differ by at most "tolerance"
            match t1,t2 with
            | SetEmpty,t2  -> add comparer k t2 // drop t1 = empty
            | t1,SetEmpty  -> add comparer k t1 // drop t2 = empty
            | SetOne k1,t2 -> add comparer k (add comparer k1 t2)
            | t1,SetOne k2 -> add comparer k (add comparer k2 t1)
            | SetNode(k1,t11,t12,h1),SetNode(k2,t21,t22,h2) ->
                // Have:  (t11 < k1 < t12) < k < (t21 < k2 < t22)
                // Either (a) h1,h2 differ by at most 2 - no rebalance needed.
                //        (b) h1 too small, i.e. h1+2 < h2
                //        (c) h2 too small, i.e. h2+2 < h1
                if   h1+tolerance < h2 then
                    // case: b, h1 too small
                    // push t1 into low side of t2, may increase height by 1 so rebalance
                    rebalance (balance comparer t1 k t21) k2 t22
                elif h2+tolerance < h1 then
                    // case: c, h2 too small
                    // push t2 into high side of t1, may increase height by 1 so rebalance
                    rebalance t11 k1 (balance comparer t12 k t2)
                else
                    // case: a, h1 and h2 meet balance requirement
                    mk t1 k t2

        let rec [<Js>] split (comparer : IComparer<'T>) pivot t =
            // Given a pivot and a set t
            // Return { x in t s.t. x < pivot }, pivot in t? , { x in t s.t. x > pivot }
            match t with
            | SetNode(k1,t11,t12,_) ->
                let c = comparer.Compare(pivot,k1)
                if   c < 0 then // pivot t1
                    let t11Lo,havePivot,t11Hi = split comparer pivot t11
                    t11Lo,havePivot,balance comparer t11Hi k1 t12
                elif c = 0 then // pivot is k1
                    t11,true,t12
                else            // pivot t2
                    let t12Lo,havePivot,t12Hi = split comparer pivot t12
                    balance comparer t11 k1 t12Lo,havePivot,t12Hi
            | SetOne k1 ->
                let c = comparer.Compare(k1,pivot)
                if   c < 0 then t       ,false,SetEmpty // singleton under pivot
                elif c = 0 then SetEmpty,true ,SetEmpty // singleton is    pivot
                else            SetEmpty,false,t        // singleton over  pivot
            | SetEmpty  ->
                SetEmpty,false,SetEmpty

        let rec [<Js>] spliceOutSuccessor t =
            match t with
            | SetEmpty -> failwith "internal error: Map.spliceOutSuccessor"
            | SetOne (k2) -> k2,SetEmpty
            | SetNode (k2,l,r,_) ->
                match l with
                | SetEmpty -> k2,r
                | _ -> let k3,l' = spliceOutSuccessor l in k3,mk l' k2 r

        let rec [<Js>] remove (comparer: IComparer<'T>) k t =
            match t with
            | SetEmpty -> t
            | SetOne (k2) ->
                let c = comparer.Compare(k,k2)
                if   c = 0 then SetEmpty
                else            t
            | SetNode (k2,l,r,_) ->
                let c = comparer.Compare(k,k2)
                if   c < 0 then rebalance (remove comparer k l) k2 r
                elif c = 0 then
                  match l,r with
                  | SetEmpty,_ -> r
                  | _,SetEmpty -> l
                  | _ ->
                      let sk,r' = spliceOutSuccessor r
                      mk l sk r'
                else rebalance l k2 (remove comparer k r)

        let rec [<Js>] mem (comparer: IComparer<'T>) k t =
            match t with
            | SetNode(k2,l,r,_) ->
                let c = comparer.Compare(k,k2)
                if   c < 0 then mem comparer k l
                elif c = 0 then true
                else mem comparer k r
            | SetOne(k2) -> (comparer.Compare(k,k2) = 0)
            | SetEmpty -> false

        let rec [<Js>] iter f t =
            match t with
            | SetNode(k2,l,r,_) -> iter f l; f k2; iter f r
            | SetOne(k2) -> f k2
            | SetEmpty -> ()

        let rec [<Js>] foldBack f m x =
            match m with
            | SetNode(k,l,r,_) -> foldBack f l (f k (foldBack f r x))
            | SetOne(k) -> f k x
            | SetEmpty -> x

        let rec [<Js>] fold f x m =
            match m with
            | SetNode(k,l,r,_) ->
                let x = fold f x l in
                let x = f x k
                fold f x r
            | SetOne(k) -> f x k
            | SetEmpty -> x

        let rec [<Js>] forall f m =
            match m with
            | SetNode(k2,l,r,_) -> f k2 && forall f l && forall f r
            | SetOne(k2) -> f k2
            | SetEmpty -> true

        let rec [<Js>] exists f m =
            match m with
            | SetNode(k2,l,r,_) -> f k2 || exists f l || exists f r
            | SetOne(k2) -> f k2
            | SetEmpty -> false

        let [<Js>] isEmpty m = match m with  | SetEmpty -> true | _ -> false

        let [<Js>] subset comparer a b  = forall (fun x -> mem comparer x b) a

        let [<Js>] psubset comparer a b  = forall (fun x -> mem comparer x b) a && exists (fun x -> not (mem comparer x a)) b

        let rec [<Js>] filterAux comparer f s acc =
            match s with
            | SetNode(k,l,r,_) ->
                let acc = if f k then add comparer k acc else acc
                filterAux comparer f l (filterAux comparer f r acc)
            | SetOne(k) -> if f k then add comparer k acc else acc
            | SetEmpty -> acc

        let [<Js>] filter comparer f s = filterAux comparer f s SetEmpty

        let rec [<Js>] diffAux comparer m acc =
            match m with
            | SetNode(k,l,r,_) -> diffAux comparer l (diffAux comparer r (remove comparer k acc))
            | SetOne(k) -> remove comparer k acc
            | SetEmpty -> acc

        let [<Js>] diff comparer a b = diffAux comparer b a

        let rec [<Js>] countAux s acc =
            match s with
            | SetNode(_,l,r,_) -> countAux l (countAux r (acc+1))
            | SetOne(_) -> acc+1
            | SetEmpty -> acc

        let [<Js>] count s = countAux s 0

        let rec [<Js>] union comparer t1 t2 =
            // Perf: tried bruteForce for low heights, but nothing significant
            match t1,t2 with
            | SetNode(k1,t11,t12,h1),SetNode(k2,t21,t22,h2) -> // (t11 < k < t12) AND (t21 < k2 < t22)
                // Divide and Quonquer:
                //   Suppose t1 is largest.
                //   Split t2 using pivot k1 into lo and hi.
                //   Union disjoint subproblems and then combine.
                if h1 > h2 then
                  let lo,_,hi = split comparer k1 t2 in
                  balance comparer (union comparer t11 lo) k1 (union comparer t12 hi)
                else
                  let lo,_,hi = split comparer k2 t1 in
                  balance comparer (union comparer t21 lo) k2 (union comparer t22 hi)
            | SetEmpty,t -> t
            | t,SetEmpty -> t
            | SetOne k1,t2 -> add comparer k1 t2
            | t1,SetOne k2 -> add comparer k2 t1

        let rec [<Js>] intersectionAux comparer b m acc =
            match m with
            | SetNode(k,l,r,_) ->
                let acc = intersectionAux comparer b r acc
                let acc = if mem comparer k b then add comparer k acc else acc
                intersectionAux comparer b l acc
            | SetOne(k) ->
                if mem comparer k b then add comparer k acc else acc
            | SetEmpty -> acc

        let [<Js>] intersection comparer a b = intersectionAux comparer b a SetEmpty

        let [<Js>] partition1 comparer f k (acc1,acc2) = if f k then (add comparer k acc1,acc2) else (acc1,add comparer k acc2)

        let rec [<Js>] partitionAux comparer f s acc =
            match s with
            | SetNode(k,l,r,_) ->
                let pacc = partitionAux comparer f r acc
                let lacc = partition1 comparer f k pacc
                partitionAux comparer f l lacc
            | SetOne(k) -> partition1 comparer f k acc
            | SetEmpty -> acc

        let [<Js>] partition comparer f s = partitionAux comparer f s (SetEmpty,SetEmpty)

        // It's easier to get many less-important algorithms right using this active pattern
        let [<Js>] (|MatchSetNode|MatchSetEmpty|) s =
            match s with
            | SetNode(k2,l,r,_) -> MatchSetNode(k2,l,r)
            | SetOne(k2) -> MatchSetNode(k2,SetEmpty,SetEmpty)
            | SetEmpty -> MatchSetEmpty

        let rec [<Js>] nextElemCont (comparer: IComparer<'T>) k s cont =
            match s with
            | MatchSetNode(k2,l,r) ->
                let c = comparer.Compare(k,k2)
                if   c < 0 then nextElemCont comparer k l (function None -> cont(Some(k2)) | res -> res)
                elif c = 0 then cont(minimumElementOpt r)
                else nextElemCont comparer k r cont
            | MatchSetEmpty -> cont(None)

        and [<Js>] nextElem comparer k s = nextElemCont comparer k s (fun res -> res)

        and [<Js>] prevElemCont (comparer: IComparer<'T>) k s cont =
                match s with
                | MatchSetNode(k2,l,r) ->
                    let c = comparer.Compare(k,k2)
                    if   c > 0 then prevElemCont comparer k r (function None -> cont(Some(k2)) | res -> res)
                    elif c = 0 then cont(maximumElementOpt r)
                    else prevElemCont comparer k l cont
                | MatchSetEmpty -> cont(None)

        and [<Js>] prevElem comparer k s = prevElemCont comparer k s (fun res -> res)

        and [<Js>] minimumElementAux s n =
            match s with
            | SetNode(k,l,_,_) -> minimumElementAux l k
            | SetOne(k) -> k
            | SetEmpty -> n

        and [<Js>] minimumElementOpt s =
            match s with
            | SetNode(k,l,_,_) -> Some(minimumElementAux l k)
            | SetOne(k) -> Some k
            | SetEmpty -> None

        and [<Js>] maximumElementAux s n =
            match s with
            | SetNode(k,_,r,_) -> maximumElementAux r k
            | SetOne(k) -> k
            | SetEmpty -> n

        and [<Js>] maximumElementOpt s =
            match s with
            | SetNode(k,_,r,_) -> Some(maximumElementAux r k)
            | SetOne(k) -> Some(k)
            | SetEmpty -> None

        let [<Js>] minimumElement s =
            match minimumElementOpt s with
            | Some(k) -> k
            | None -> failwith "Set contains no element" //invalidArg "s" (SR.GetString(SR.setContainsNoElements))

        let [<Js>] maximumElement s =
            match maximumElementOpt s with
            | Some(k) -> k
            | None -> failwith "Set contains no element" //invalidArg "s" (SR.GetString(SR.setContainsNoElements))


        //--------------------------------------------------------------------------
        // Imperative left-to-right iterators.
        //--------------------------------------------------------------------------

        [<NoEquality; NoComparison;>]
        type SetIterator<'T> when 'T : comparison  =
            { mutable stack: SetTree<'T> list;  // invariant: always collapseLHS result
              mutable started : bool           // true when MoveNext has been called
            }

        // collapseLHS:
        // a) Always returns either [] or a list starting with SetOne.
        // b) The "fringe" of the set stack is unchanged.
        let rec [<Js>] collapseLHS stack =
            match stack with
            | []                        -> []
            | SetEmpty         :: rest  -> collapseLHS rest
            | SetOne _         :: _     -> stack
            | SetNode(k,l,r,_) :: rest  -> collapseLHS (l :: SetOne k :: r :: rest)

        let [<Js>] mkIterator s = { stack = collapseLHS [s]; started = false }

        let [<Js>] notStarted() = raise (new System.InvalidOperationException("Enumeration Not Started"))
        let [<Js>] alreadyFinished() = raise (new System.InvalidOperationException("Enumeration Already Finished"))

        let [<Js>] current i =
            if i.started then
                match i.stack with
                  | SetOne k :: _ -> k
                  | []            -> alreadyFinished()
                  | _             -> failwith "Please report error: Set iterator, unexpected stack for current"
            else
                notStarted()

        let rec [<Js>] moveNext i =
            if i.started then
                match i.stack with
                  | SetOne _ :: rest ->
                      i.stack <- collapseLHS rest;
                      not i.stack.IsEmpty
                  | [] -> false
                  | _ -> failwith "Please report error: Set iterator, unexpected stack for moveNext"
            else
                i.started <- true;  // The first call to MoveNext "starts" the enumeration.
                not i.stack.IsEmpty

        type SetTreeEnumerator<'T when 'T: comparison> [<Js>](s:SetTree<'T>) =
            let i = ref (mkIterator s)
            interface IEnumerator<'T> with
                [<Js>]
                member this.Current = current !i
            interface IEnumerator with
                [<Js>]
                member this.Current = box (current !i)
                [<Js>]
                member this.MoveNext() = moveNext !i
                [<Js>]
                member this.Reset()    = i := mkIterator s
            interface IDisposable with
                [<Js>]
                member this.Dispose() = ()

        let [<Js>] mkIEnumerator s =
            new SetTreeEnumerator<_>(s) :> IEnumerator<_>
            (*let i = ref (mkIterator s)
            { new IEnumerator<_> with
                  member x.Current = current !i
              interface IEnumerator with
                  member x.Current = box (current !i)
                  member x.MoveNext() = moveNext !i
                  member x.Reset() = i :=  mkIterator s
              interface System.IDisposable with
                  member x.Dispose() = () }*)

        //--------------------------------------------------------------------------
        // Set comparison.  This can be expensive.
        //--------------------------------------------------------------------------
        let rec [<Js>] compareStacks (comparer: IComparer<'T>) l1 l2 =
            match l1,l2 with
            | [],[] ->  0
            | [],_  -> -1
            | _ ,[] ->  1
            | (SetEmpty  _ :: t1),(SetEmpty    :: t2) -> compareStacks comparer t1 t2
            | (SetOne(n1k) :: t1),(SetOne(n2k) :: t2) ->
                 let c = comparer.Compare(n1k,n2k)
                 if c <> 0 then c else compareStacks comparer t1 t2
            | (SetOne(n1k) :: t1),(SetNode(n2k,SetEmpty,n2r,_) :: t2) ->
                 let c = comparer.Compare(n1k,n2k)
                 if c <> 0 then c else compareStacks comparer (SetEmpty :: t1) (n2r :: t2)
            | (SetNode(n1k,(SetEmpty as emp),n1r,_) :: t1),(SetOne(n2k) :: t2) ->
                 let c = comparer.Compare(n1k,n2k)
                 if c <> 0 then c else compareStacks comparer (n1r :: t1) (emp :: t2)
            | (SetNode(n1k,SetEmpty,n1r,_) :: t1),(SetNode(n2k,SetEmpty,n2r,_) :: t2) ->
                 let c = comparer.Compare(n1k,n2k)
                 if c <> 0 then c else compareStacks comparer (n1r :: t1) (n2r :: t2)
            | (SetOne(n1k) :: t1),_ ->
                compareStacks comparer (SetEmpty :: SetOne(n1k) :: t1) l2
            | (SetNode(n1k,n1l,n1r,_) :: t1),_ ->
                compareStacks comparer (n1l :: SetNode(n1k,SetEmpty,n1r,0) :: t1) l2
            | _,(SetOne(n2k) :: t2) ->
                compareStacks comparer l1 (SetEmpty :: SetOne(n2k) :: t2)
            | _,(SetNode(n2k,n2l,n2r,_) :: t2) ->
                compareStacks comparer l1 (n2l :: SetNode(n2k,SetEmpty,n2r,0) :: t2)

        let [<Js>] compare comparer s1 s2 =
            match s1,s2 with
            | SetEmpty,SetEmpty -> 0
            | SetEmpty,_ -> -1
            | _,SetEmpty -> 1
            | _ -> compareStacks comparer [s1] [s2]

        let [<Js>] choose s = minimumElement s

        let rec [<Js>]toListLoop m acc =
            match m with
            | SetNode(k,l,r,_) -> toListLoop l (k :: toListLoop r acc)
            | SetOne(k) ->  k ::acc
            | SetEmpty -> acc

        let [<Js>] toList s =
            toListLoop s []

        let [<Js>] copyToArray s (arr: _[]) i =
            let j = ref i
            iter (fun x -> arr.[!j] <- x; j := !j + 1) s

        let [<Js>] toArray s =
            let n = (count s)
            let res = Array.zeroCreate n
            copyToArray s res 0;
            res

        let rec [<Js>] mkFromEnumerator comparer acc (e : IEnumerator<_>) =
          if e.MoveNext() then
            mkFromEnumerator comparer (add comparer e.Current acc) e
          else acc

        let [<Js>] ofSeq comparer (c : IEnumerable<_>) =
          use ie = c.GetEnumerator()
          mkFromEnumerator comparer SetEmpty ie

        let [<Js>] ofArray comparer l = Array.fold (fun acc k -> add comparer k acc) SetEmpty l

    [<CompiledName("FSharpSet`1")>]
    type Set<[<EqualityConditionalOn>]'T when 'T : comparison >[<Js>](comparer:IComparer<'T>, tree: SetTree<'T>) =

        [<Js>]
        member this.Comparer = comparer
        //[<DebuggerBrowsable(DebuggerBrowsableState.Never)>]
        [<Js>]
        member this.Tree : SetTree<'T> = tree

        [<Js>]
        static member Empty : Set<'T> =
            let comparer = LanguagePrimitives.FastGenericComparer<'T>
            new Set<'T>(comparer, SetEmpty)

        [<Js>]
        member this.Add(x) : Set<'T> = new Set<'T>(this.Comparer,SetTree.add this.Comparer x this.Tree )
        [<Js>]
        member this.Remove(x) : Set<'T> = new Set<'T>(this.Comparer,SetTree.remove this.Comparer x this.Tree)
        [<Js>]
        member this.Count = SetTree.count this.Tree
        [<Js>]
        member this.Contains(x) = SetTree.mem this.Comparer x this.Tree
        [<Js>]
        member this.Iterate(x) = SetTree.iter x this.Tree
        [<Js>]
        member this.Fold f z  = SetTree.fold (fun x z -> f z x) z this.Tree
        [<Js>]
        member this.IsEmpty  = SetTree.isEmpty this.Tree
        [<Js>]
        member this.Partition f  : Set<'T> *  Set<'T> =
            match this.Tree with
            | SetEmpty -> this,this
            | _ -> let t1,t2 = SetTree.partition this.Comparer f this.Tree in new Set<_>(this.Comparer,t1), new Set<_>(this.Comparer,t2)

        [<Js>]
        member this.Filter f  : Set<'T> =
            match this.Tree with
            | SetEmpty -> this
            | _ -> new Set<_>(this.Comparer,SetTree.filter this.Comparer f this.Tree)

        [<Js>]
        member this.Map f  : Set<'U> =
            let comparer = LanguagePrimitives.FastGenericComparer<'U>
            new Set<_>(comparer,SetTree.fold (fun acc k -> SetTree.add comparer (f k) acc) (SetTree<_>.SetEmpty) this.Tree)

        [<Js>]
        member this.Exists f = SetTree.exists f this.Tree

        [<Js>]
        member this.ForAll f = SetTree.forall f this.Tree

        [<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")>]
        [<Js;JsIgnore(IgnoreTuple=true)>]
        static member (-) (a: Set<'T>, b: Set<'T>) =
            match a.Tree with
            | SetEmpty -> a (* 0 - B = 0 *)
            | _ ->
            match b.Tree with
            | SetEmpty -> a (* A - 0 = A *)
            | _ -> new Set<_>(a.Comparer,SetTree.diff a.Comparer  a.Tree b.Tree)

        [<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")>]
        [<Js;JsIgnore(IgnoreTuple=true)>]
        static member (+) (a: Set<'T>, b: Set<'T>) =
            match b.Tree with
            | SetEmpty -> a  (* A U 0 = A *)
            | _ ->
            match a.Tree with
            | SetEmpty -> b  (* 0 U B = B *)
            | _ -> new Set<_>(a.Comparer,SetTree.union a.Comparer  a.Tree b.Tree)

        [<Js;JsOverloadMember("Intersection1")>]
        static member Intersection(a: Set<'T>, b: Set<'T>) : Set<'T>  =
            match b.Tree with
            | SetEmpty -> b  (* A INTER 0 = 0 *)
            | _ ->
            match a.Tree with
            | SetEmpty -> a (* 0 INTER B = 0 *)
            | _ -> new Set<_>(a.Comparer,SetTree.intersection a.Comparer a.Tree b.Tree)

        [<Js>]
        static member Union(sets:seq<Set<'T>>) : Set<'T>  =
            Seq.fold (fun s1 s2 -> s1 + s2) Set<'T>.Empty sets

        [<Js;JsOverloadMember("Intersection2")>]
        static member Intersection(sets:seq<Set<'T>>) : Set<'T>  =
            Seq.reduce (fun s1 s2 -> Set<_>.Intersection(s1,s2)) sets

        [<Js>]
        static member Equality(a: Set<'T>, b: Set<'T>) = (SetTree.compare a.Comparer  a.Tree b.Tree = 0)

        [<Js>]
        static member Compare(a: Set<'T>, b: Set<'T>) = SetTree.compare a.Comparer  a.Tree b.Tree
        [<Js>]
        member this.Choose = SetTree.choose this.Tree
        [<Js>]
        member this.MinimumElement = SetTree.minimumElement this.Tree
        [<Js>]
        member this.MaximumElement = SetTree.maximumElement this.Tree
        [<Js>]
        member this.GetNextElement(e) = SetTree.nextElem this.Comparer e this.Tree
        [<Js>]
        member this.GetPreviousElement(e) = SetTree.prevElem this.Comparer  e this.Tree
        [<Js>]
        member this.IsSubsetOf(y: Set<'T>) = SetTree.subset this.Comparer this.Tree y.Tree
        [<Js>]
        member this.IsSupersetOf(y: Set<'T>) = SetTree.subset this.Comparer y.Tree this.Tree
        [<Js>]
        member this.IsProperSubsetOf(y: Set<'T>) = SetTree.psubset this.Comparer this.Tree y.Tree
        [<Js>]
        member this.IsProperSupersetOf(y: Set<'T>) = SetTree.psubset this.Comparer y.Tree this.Tree
        [<Js>]
        member this.ToList () = SetTree.toList this.Tree
        [<Js>]
        member this.ToArray () = SetTree.toArray this.Tree

        [<Js>]
        member this.ComputeHashCode() =
            let combineHash x y = (x <<< 1) + y + 631
            let mutable res = 0
            for x in this do
                res <- combineHash res (hash x)
            abs res

        [<Js>]
        override this.GetHashCode() = this.ComputeHashCode()

        [<Js>]
        override this.Equals(that) =
            match that with
            | :? Set<'T> as that ->
                use e1 = (this :> seq<_>).GetEnumerator()
                use e2 = (that :> seq<_>).GetEnumerator()
                let rec loop () =
                    let m1 = e1.MoveNext()
                    let m2 = e2.MoveNext()
                    (m1 = m2) && (not m1 || ((e1.Current = e2.Current) && loop()))
                loop()
            | _ -> false

        interface System.IComparable with
            [<Js>]
            member this.CompareTo(that: obj) = SetTree.compare this.Comparer this.Tree ((that :?> Set<'T>).Tree)

        interface ICollection<'T> with
            [<Js>]
            member this.Add(x)         = raise (new System.NotSupportedException("ReadOnlyCollection"))
            [<Js>]
            member this.Clear()        = raise (new System.NotSupportedException("ReadOnlyCollection"))
            [<Js>]
            member this.Remove(x)      = raise (new System.NotSupportedException("ReadOnlyCollection"))
            [<Js>]
            member this.Contains(x)    = SetTree.mem this.Comparer x this.Tree
            [<Js>]
            member this.CopyTo(arr,i)  = SetTree.copyToArray this.Tree arr i
            [<Js>]
            member this.IsReadOnly     = true
            [<Js>]
            member this.Count          = SetTree.count this.Tree

        interface IEnumerable<'T> with
            [<Js>]
            member this.GetEnumerator() = SetTree.mkIEnumerator this.Tree

        interface IEnumerable with
            [<Js>]
            override this.GetEnumerator() = (SetTree.mkIEnumerator this.Tree :> IEnumerator)

        [<Js>]
        static member Singleton(x:'T) : Set<'T> = Set<'T>.Empty.Add(x)

        [<Js>]
        new (elements : seq<'T>) =
            let comparer = LanguagePrimitives.FastGenericComparer<'T>
            new Set<_>(comparer,SetTree.ofSeq comparer elements)

        [<Js>]
        static member Create(elements : seq<'T>) =  new Set<'T>(elements)

        [<Js>]
        static member FromArray(arr : 'T array) : Set<'T> =
            let comparer = LanguagePrimitives.FastGenericComparer<'T>
            new Set<_>(comparer,SetTree.ofArray comparer arr)

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Set =

        [<Js;CompiledName("IsEmpty");>]
        let isEmpty (s : Set<'T>) = s.IsEmpty

        [<Js;CompiledName("Contains");>]
        let contains x (s : Set<'T>) = s.Contains(x)

        [<Js;CompiledName("Add");>]
        let add x (s : Set<'T>) = s.Add(x)

        [<Js;CompiledName("Singleton");>]
        let singleton x = Set<'T>.Singleton(x)

        [<Js;CompiledName("Remove");>]
        let remove x (s : Set<'T>) = s.Remove(x)

        [<Js;CompiledName("Union");>]
        let union (s1 : Set<'T>)  (s2 : Set<'T>)  = s1 + s2

        [<Js;CompiledName("UnionMany");>]
        let unionMany sets  = Set<_>.Union(sets)

        [<Js;CompiledName("Intersect");>]
        let intersect (s1 : Set<'T>)  (s2 : Set<'T>)  = Set<'T>.Intersection(s1,s2)

        [<Js;CompiledName("IntersectMany");>]
        let intersectMany sets  = Set<_>.Intersection(sets)

        [<Js;CompiledName("Iterate");>]
        let iter f (s : Set<'T>)  = s.Iterate(f)

        [<Js;CompiledName("Empty");>]
        let empty<'T when 'T : comparison>() : Set<'T> = Set<'T>.Empty

        [<Js;CompiledName("ForAll");>]
        let forall f (s : Set<'T>) = s.ForAll f

        [<Js;CompiledName("Exists");>]
        let exists f (s : Set<'T>) = s.Exists f

        [<Js;CompiledName("Filter");>]
        let filter f (s : Set<'T>) = s.Filter f

        [<Js;CompiledName("Partition");>]
        let partition f (s : Set<'T>) = s.Partition f

        [<Js;CompiledName("Fold");>]
        let fold<'T,'State  when 'T : comparison> f (z:'State) (s : Set<'T>) = SetTree.fold f z s.Tree

        [<Js;CompiledName("FoldBack");>]
        let foldBack<'T,'State when 'T : comparison> f (s : Set<'T>) (z:'State) = SetTree.foldBack f s.Tree z

        [<Js;CompiledName("Map");>]
        let map f (s : Set<'T>) = s.Map f

        [<Js;CompiledName("Count");>]
        let count (s : Set<'T>) = s.Count

        [<Js;CompiledName("MinumumElement");>]
        let minimumElement (s : Set<'T>) = s.MinimumElement

        [<Js;CompiledName("MaximumElement");>]
        let maximumElement (s : Set<'T>) = s.MaximumElement

        [<Js;CompiledName("OfList");>]
        let ofList l = new Set<_>(List.toSeq l)

        [<Js;CompiledName("OfArray");>]
        let ofArray (l : 'T array) = Set<'T>.FromArray(l)

        [<Js;CompiledName("ToList");>]
        let toList (s : Set<'T>) = s.ToList()

        [<Js;CompiledName("ToArray");>]
        let toArray (s : Set<'T>) = s.ToArray()

        [<Js;CompiledName("ToSeq");>]
        let toSeq (s : Set<'T>) = (s :> seq<'T>)

        [<Js;CompiledName("OfSeq");>]
        let ofSeq (c : seq<_>) = new Set<_>(c)

        [<Js;CompiledName("Difference");>]
        let difference (s1: Set<'T>) (s2: Set<'T>) = s1 - s2

        [<Js;CompiledName("IsSubset");>]
        let isSubset (x:Set<'T>) (y: Set<'T>) = SetTree.subset x.Comparer x.Tree y.Tree

        [<Js;CompiledName("IsSuperset");>]
        let isSuperset (x:Set<'T>) (y: Set<'T>) = SetTree.subset x.Comparer y.Tree x.Tree

        [<Js;CompiledName("IsProperSubset");>]
        let isProperSubset (x:Set<'T>) (y: Set<'T>) = SetTree.psubset x.Comparer x.Tree y.Tree

        [<Js;CompiledName("IsProperSuperset");>]
        let isProperSuperset (x:Set<'T>) (y: Set<'T>) = SetTree.psubset x.Comparer y.Tree x.Tree

        [<Js;CompiledName("MinElement");>]
        let minElement (s : Set<'T>) = s.MinimumElement

        [<Js;CompiledName("MaxElement");>]
        let maxElement (s : Set<'T>) = s.MaximumElement