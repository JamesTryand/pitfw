#nowarn "42"
namespace Pit.FSharp.Collections
open Pit
open Pit.Javascript
open System.Collections
open System.Collections.Generic

module IEnumerator =

    [<Js>]
    let noReset() = raise (new System.NotSupportedException("reset not supported"))

    [<Js>]
    let notStarted() = raise (new System.InvalidOperationException("enumeration not started"))

    [<Js>]
    let alreadyFinished() = raise (new System.InvalidOperationException("enumeration already finished"))

    [<Js>]
    let check started = if not started then notStarted()

    [<Js>]
    let dispose (r : System.IDisposable) = r.Dispose()

    [<Js>]
    let rec nth index (e : IEnumerator<'T>) =
        if not (e.MoveNext()) then raise (new System.InvalidOperationException("not enough elements"))
        if index < 0 then raise (new System.InvalidOperationException("input must be non-negative"))
        if index = 0 then e.Current
        else nth (index-1) e

    type EnumeratorState =
        | NotStarted
        | InProcess
        | Finished

    type SeqEnumerator<'T> [<Js>](m, d) =
        let mutable state = NotStarted
        [<DefaultValue(false)>]
        val mutable private curr : 'T

        [<Js>]
        member this.GetCurrent () =
            match state with
            |   NotStarted -> notStarted()
            |   Finished -> alreadyFinished()
            |   InProcess -> ()
            this.curr

        interface IEnumerator<'T> with
            [<Js>]
            member this.Current = this.GetCurrent()

        interface IEnumerator with
            [<Js>]
            member this.Current = box(this.GetCurrent())

            [<Js>]
            member this.MoveNext () =
                state <- InProcess
                let c, r = m(this.curr)
                this.curr <- c
                if r then
                    true
                else
                    state <- Finished
                    false

            [<Js>]
            member this.Reset() = noReset()

        interface System.IDisposable with
            [<Js>]
            member this.Dispose() = d()

    type ChooseEnumerator<'T> [<Js>](f, e: IEnumerator<'T>) =
        let started = ref false
        let curr    = ref None

        [<Js>]
        member this.get()   = check !started; (match !curr with None -> alreadyFinished() | Some x -> x)

        interface IEnumerator<'T> with
            [<Js>]
            member this.Current = this.get()

        interface IEnumerator with
            [<Js>]
            member this.Current = box(this.get())

            [<Js>]
            member this.MoveNext() =
                if not !started then started := true
                curr := None
                while ((!curr).IsNone && e.MoveNext()) do
                    curr := f e.Current
                Option.isSome !curr

            [<Js>]
            member this.Reset() = noReset()

        interface System.IDisposable with
            [<Js>]
            member this.Dispose() = e.Dispose()


    [<Js>]
    let map f (e: IEnumerator<'U>) =
        new SeqEnumerator<'T>(
            (fun _ ->
                if e.MoveNext() then
                    f(e.Current), true
                else
                    Unchecked.defaultof<_>, false
            ),
            (fun _ -> e.Dispose())) :> IEnumerator<_>

    [<Js>]
    let mapi f (e:IEnumerator<_>) =
        let i = ref(-1)
        new SeqEnumerator<'T>(
            (fun _ ->
                i := !i + 1
                if e.MoveNext() then
                    f !i e.Current, true
                else
                    Unchecked.defaultof<_>, false
            ),
            (fun _ -> e.Dispose())) :> IEnumerator<_>

    [<Js>]
    let map2 f (e1:IEnumerator<_>) (e2:IEnumerator<_>) =
        new SeqEnumerator<_>(
            (fun _ ->
                let n1 = e1.MoveNext()
                let n2 = e2.MoveNext()
                if n1 && n2 then
                    let curr = f e1.Current e2.Current
                    curr, true
                else
                    Unchecked.defaultof<_>, false
            ),
            (fun _ ->
                e1.Dispose()
                e2.Dispose())) :> IEnumerator<_>

    [<Js>]
    let choose f e =
        new ChooseEnumerator<'T>(f, e) :> IEnumerator<_>

    type FilterEnumerator<'T> [<Js>](f, e: IEnumerator<'T>) =
        let started = ref false

        [<Js>]
        let rec next() =
            if not !started then started := true;
            e.MoveNext() && (f  e.Current || next())

        [<Js>]
        member this.Next() =
            if not !started then started := true;
            e.MoveNext() && (f e.Current || this.Next())

        interface IEnumerator<'T> with
            [<Js>]
            member this.Current = check !started; e.Current

        interface IEnumerator with
            [<Js>]
            member this.Current = check !started; box e.Current

            [<Js>]
            member this.MoveNext() = this.Next()
            (*member this.MoveNext() =
                let rec next() =
                    if not !started then started := true;
                    e.MoveNext() && (f  e.Current || next())
                next()*)

            [<Js>]
            member this.Reset() = noReset()

        interface System.IDisposable with
            [<Js>]
            member x.Dispose() = e.Dispose()

    [<Js>]
    let filter (f:'a->bool) (e:IEnumerator<_>) =
        new FilterEnumerator<_>(f, e) :> IEnumerator<_>

    [<Js>]
    let unfold f x =
        let state = ref x
        new SeqEnumerator<_>(
            (fun _ ->
                match f !state with
                | None -> null, false
                | Some(r, s) ->
                    state := s
                    r, true
            ),
            (fun _ -> ())) :> IEnumerator<_>

    [<Sealed>]
    type ArrayEnumerator<'T> [<Js>](arr: 'T array) =
        let mutable curr = -1
        let mutable len = arr.Length
        [<Js>]
        member x.Get() =
            if curr >= 0 then
                if curr >= len then alreadyFinished()
                else arr.[curr]
            else
                notStarted()
        interface IEnumerator<'T> with
            [<Js>]
            member this.Current = this.Get()
        interface System.Collections.IEnumerator with
            [<Js>]
            member this.MoveNext() =
                    if curr >= len then false
                    else
                        curr <- curr + 1;
                        (curr < len)
            [<Js>]
            member this.Current = box(this.Get())
            [<Js>]
            member this.Reset() = noReset()
        interface System.IDisposable with
            [<Js>]
            member this.Dispose() = ()

    [<Js>]
    let ofArray arr = (new ArrayEnumerator<'T>(arr) :> IEnumerator<'T>)

    [<Sealed>]
    type EmptyEnumerator<'T>() =
        let mutable started = false
        interface IEnumerator<'T> with
            [<Js>]
            member x.Current =
                check started;
                (alreadyFinished() : 'T)

        interface System.Collections.IEnumerator with
            [<Js>]
            member x.Current =
                check started;
                (alreadyFinished() : obj)
            [<Js>]
            member x.MoveNext() =
                if not started then started <- true;
                false
            [<Js>]
            member x.Reset() = noReset()
        interface System.IDisposable with
            [<Js>]
            member x.Dispose() = ()

    [<Js>]
    let Empty<'T> () = (new EmptyEnumerator<'T>() :> IEnumerator<'T>)

    type Singleton<'T> [<Js>](v:'T) =
        let mutable started = false
        interface IEnumerator<'T> with
            [<Js>]
            member x.Current = v
        interface IEnumerator with
            [<Js>]
            member x.Current = box v
            [<Js>]
            member x.MoveNext() = if started then false else (started <- true; true)
            [<Js>]
            member x.Reset() = noReset()
        interface System.IDisposable with
            [<Js>]
            member x.Dispose() = ()

    [<Js>]
    let Singleton x = (new Singleton<'T>(x) :> IEnumerator<'T>)

    type EnumerateFinally<'T> [<Js>](f, e:IEnumerator<'T>)=
        interface IEnumerator<'T> with
            [<Js>]
            member x.Current = e.Current
        interface IEnumerator with
            [<Js>]
            member x.Current = (e :> IEnumerator).Current
            [<Js>]
            member x.MoveNext() = e.MoveNext()
            [<Js>]
            member x.Reset() = noReset()
        interface System.IDisposable with
            [<Js>]
            member x.Dispose() =
                try
                    e.Dispose()
                finally
                    f()

    [<Js>]
    let EnumerateThenFinally f e =
        (new EnumerateFinally<_>(f, e) :> IEnumerator<'T>)

    type UptoEnumerator<'T> [<Js>](lastOption, f) =
        let unstarted   = -1  // index value means unstarted (and no valid index)
        let completed   = -2  // index value means completed (and no valid index)
        let unreachable = -3  // index is unreachable from 0,1,2,3,...
        let finalIndex  = match lastOption with
                            | Some b -> b             // here b>=0, a valid end value.
                            | None   -> unreachable   // run "forever", well as far as Int32.MaxValue since indexing with a bounded type.
        let index   = ref unstarted
        // a Lazy node to cache the result/exception
        let current = ref (Unchecked.defaultof<_>)

        [<Js>]
        member this.setIndex i = index := i; current := (Unchecked.defaultof<_>) // cache node unprimed, initialised on demand.

        [<Js>]
        member this.getCurrent() =
            if !index = unstarted then notStarted()
            if !index = completed then alreadyFinished()
            match box !current with
            | null -> current := Lazy.Create(fun () -> f !index);
            | _ ->  ()
            // forced or re-forced immediately.
            (!current).Force()

        interface IEnumerator<'T> with
            [<Js>]
            member this.Current = this.getCurrent()
        interface IEnumerator with
            [<Js>]
            member this.Current = box (this.getCurrent())

            [<Js>]
            member this.MoveNext() =
                if !index = completed then
                    false
                elif !index = unstarted then
                    this.setIndex(0)
                    true
                else (
                    if !index = System.Int32.MaxValue then raise <| System.InvalidOperationException ("Enumeration pass Int maximum value")
                    if !index = finalIndex then
                        false
                    else
                        this.setIndex (!index + 1)
                        true
                )

            [<Js>]
            member this.Reset() = noReset()
        interface System.IDisposable with
            [<Js>]
            member this.Dispose() = ()

    [<Js>]
    let upto lastOption f =
        match lastOption with
        | Some b when b<0 -> Empty()    // a request for -ve length returns empty sequence
        | _ ->
            new UptoEnumerator<_>(lastOption, f) :> IEnumerator<_>
            (*let unstarted   = -1  // index value means unstarted (and no valid index)
            let completed   = -2  // index value means completed (and no valid index)
            let unreachable = -3  // index is unreachable from 0,1,2,3,...
            let finalIndex  = match lastOption with
                            | Some b -> b             // here b>=0, a valid end value.
                            | None   -> unreachable   // run "forever", well as far as Int32.MaxValue since indexing with a bounded type.
            // The Current value for a valid index is "f i".
            // Lazy<_> values are used as caches, to store either the result or an exception if thrown.
            // These "Lazy<_>" caches are created only on the first call to current and forced immediately.
            // The lazy creation of the cache nodes means enumerations that skip many Current values are not delayed by GC.
            // For example, the full enumeration of Seq.initInfinite in the tests.
            // state
            let index   = ref unstarted
            // a Lazy node to cache the result/exception
            let current = ref (Unchecked.defaultof<_>)
            let setIndex i = index := i; current := (Unchecked.defaultof<_>) // cache node unprimed, initialised on demand.
            let getCurrent() =
                if !index = unstarted then notStarted()
                if !index = completed then alreadyFinished()
                match box !current with
                | null -> current := Lazy.Create(fun () -> f !index);
                | _ ->  ()
                // forced or re-forced immediately.
                (!current).Force()
            { new IEnumerator<'U> with
                member x.Current = getCurrent()
            interface IEnumerator with
                member x.Current = box (getCurrent())
                member x.MoveNext() =
                    if !index = completed then
                        false
                    elif !index = unstarted then
                        setIndex 0
                        true
                    else (
                        if !index = System.Int32.MaxValue then raise <| System.InvalidOperationException (SR.GetString(SR.enumerationPastIntMaxValue))
                        if !index = finalIndex then
                            false
                        else
                            setIndex (!index + 1)
                            true
                    )
                member self.Reset() = noReset()
            interface System.IDisposable with
                member x.Dispose() = () }*)

    type WhileSomeEnumerator<'U> [<Js>](openf:unit -> 'U, compute, closef) =
        let started = ref false
        let curr = ref None
        let state = ref (Some(openf()))

        [<Js>]
        member this.getCurr() =
            check !started;
            match !curr with None -> alreadyFinished() | Some x -> x

        [<Js>]
        member this.start() = if not !started then (started := true)

        [<Js>]
        member this.dispose() =
            match !state with
            | None          -> None
            | Some _ as res ->
                state := None
                res
            |> Option.iter closef

        [<Js>]
        member this.finish() = (try this.dispose() finally curr := None)

        interface IEnumerator<'U> with
            [<Js>]
            member this.Current = this.getCurr()

        interface IEnumerator with
            [<Js>]
            member this.Current = box (this.getCurr())

            [<Js>]
            member this.MoveNext() =
                this.start();
                match !state with
                | None -> false (* we started, then reached the end, then got another MoveNext *)
                | Some s ->
                    match (try compute s with e -> this.finish(); reraise()) with
                    | None -> this.finish(); false
                    | Some _ as x -> curr := x; true

            [<Js>]
            member this.Reset() = noReset()

        interface System.IDisposable with
            [<Js>]
            member this.Dispose() = this.dispose()

    [<Js>]
    let generateWhileSome openf compute closef =
        (new WhileSomeEnumerator<_>(openf, compute, closef) :> IEnumerator<_>)

module Generator =

    open System.Collections
    open System.Collections.Generic

    [<NoEquality; NoComparison>]
    type Step<'T> =
        | Stop
        | Yield of 'T
        | Goto of Generator<'T>

    and Generator<'T> =
        abstract Apply: unit -> Step<'T>
        abstract Disposer: (unit -> unit) option

    type BaseGenerator<'T> [<Js>](apply, disposer) =
        interface Generator<'T> with
            [<Js>]
            member this.Apply() = apply
            [<Js>]
            member this.Disposer = disposer

    [<Js>]
    let disposeG (g:Generator<'T>) =
        match g.Disposer with
        | None -> ()
        | Some f -> f()

    [<Js>]
    let chainDisposeG d1 (g:Generator<'T>) =
        let app = g.Apply()
        let disp = match g.Disposer with Some f2 -> Some(fun () -> f2(); d1()) | None -> Some d1
        new BaseGenerator<_>(app, disp)

    [<Js>]
    let appG (g:Generator<_>) =
        let res = g.Apply()
        match res with
        | Goto(next) ->
            Goto(next)
        | Yield _ ->
            res
        | Stop ->
            disposeG g;
            res

    // Binding.
    //
    // We use a type defintion to apply a local dynamic optimization.
    // We automatically right-associate binding, i.e. push the continuations to the right.
    // That is, bindG (bindG G1 cont1) cont2 --> bindG G1 (cont1 o cont2)
    // This makes constructs such as the following linear rather than quadratic:
    //
    //  let rec rwalk n = { if n > 0 then
    //                         yield! rwalk (n-1)
    //                         yield n }

    type GenerateThen<'T> [<Js>](g:Generator<'T>, cont : unit -> Generator<'T>) =

        [<Js>]
        member this.Generator = g

        [<Js>]
        member this.Cont = cont

        interface Generator<'T> with
            [<Js>]
            member this.Apply() =
                match appG g with
                | Stop ->
                    // OK, move onto the generator given by the continuation
                    Goto(cont())

                | Yield _ as res ->
                    res

                | Goto next ->
                    Goto(GenerateThen<_>.Bind(next,cont))
            [<Js>]
            member this.Disposer =
                g.Disposer

        [<Js>]
        static member Bind (gen:Generator<'T>, cont) =
            match gen with
            | :? GenerateThen<'T> as g -> GenerateThen<_>.Bind(g.Generator,(fun () -> GenerateThen<_>.Bind (g.Cont(), cont)))
            | g -> (new GenerateThen<'T>(g, cont) :> Generator<'T>)

    [<Js>]
    let bindG g cont = GenerateThen<_>.Bind(g,cont)

    //let emptyG () =
    //    { new Generator<_> with
    //           member x.Apply = (fun () -> Stop)
    //           member x.Disposer = None }
    //
    //let delayG f  =
    //    { new Generator<_> with
    //           member x.Apply = fun () -> Goto(f())
    //           member x.Disposer = None }
    //
    //let useG (v: System.IDisposable) f =
    //    { new Generator<_> with
    //           member x.Apply = (fun () ->
    //               let g = f v in
    //               // We're leaving this generator but want to maintain the disposal on the target.
    //               // Hence chain it into the disposer of the target
    //               Goto(chainDisposeG v.Dispose g))
    //           member x.Disposer = Some (fun () -> v.Dispose()) }
    //
    //let yieldG (v:'T) =
    //    let yielded = ref false
    //    { new Generator<_> with
    //           member x.Apply = fun () -> if !yielded then Stop else (yielded := true; Yield(v))
    //           member x.Disposer = None }
    //
    //let rec whileG gd b = if gd() then bindG (b()) (fun () -> whileG gd b) else emptyG()
    //
    //let yieldThenG x b = bindG (yieldG x) b
    //
    //let forG (v: seq<'T>) f =
    //    let e = v.GetEnumerator() in
    //    whileG e.MoveNext (fun () -> f e.Current)

    // Internal type. Drive an underlying generator. Crucially when the generator returns
    // a new generator we simply update our current generator and continue. Thus the enumerator
    // effectively acts as a reference cell holding the current generator. This means that
    // infinite or large generation chains (e.g. caused by long sequences of append's, including
    // possible delay loops) can be referenced via a single enumerator.
    //
    // A classic case where this arises in this sort of sequence expression:
    //    let rec data s = { yield s;
    //                       yield! data (s + random()) }
    //
    // This translates to
    //    let rec data s = Seq.delay (fun () -> Seq.append (Seq.singleton s) (Seq.delay (fun () -> data (s+random()))))
    //
    // When you unwind through all the Seq, IEnumerator and Generator objects created,
    // you get (data s).GetEnumerator being an "GenerateFromEnumerator(EnumeratorWrappingLazyGenerator(...))" for the append.
    // After one element is yielded, we move on to the generator for the inner delay, which in turn
    // comes back to be a "GenerateFromEnumerator(EnumeratorWrappingLazyGenerator(...))".
    //
    // Defined as a type so we can optimize Enumerator/Generator chains in enumerateFromLazyGenerator
    // and GenerateFromEnumerator.

    [<Sealed>]
    type EnumeratorWrappingLazyGenerator<'T> [<Js>](gen:Generator<'T>) =
        let mutable g = gen
        let mutable curr = None
        let mutable finished = false

        [<Js>]
        member this.Generator = g

        interface IEnumerator<'T> with
            [<Js>]
            member this.Current = match curr with Some(v) -> v | None -> raise <| System.InvalidOperationException ("MoveNext called or finished")

        interface System.Collections.IEnumerator with
            [<Js>]
            member this.Current = box (this :> IEnumerator<_>).Current

            [<Js>]
            member this.MoveNext() =
                not finished &&
                (match appG g with
                    | Stop ->
                        curr <- None;
                        finished <- true;
                        false
                    | Yield(v) ->
                        curr <- Some(v);
                        true
                    | Goto(next) ->
                        (g <- next);
                        (this :> IEnumerator).MoveNext())
            [<Js>]
            member this.Reset() = IEnumerator.noReset()

        interface System.IDisposable with
            [<Js>]
            member this.Dispose() =
                if not finished then disposeG g

    // Internal type, used to optimize Enumerator/Generator chains
    type LazyGeneratorWrappingEnumerator<'T> [<Js>](e:System.Collections.Generic.IEnumerator<'T>) =
        [<Js>]
        member this.Enumerator = e

        interface Generator<'T> with
            [<Js>]
            member this.Apply() =
                if e.MoveNext() then
                    Yield(e.Current)
                else
                    Stop
            [<Js>]
            member this.Disposer =
                let d = e
                Some(fun () -> d.Dispose())

    [<Js>]
    let EnumerateFromGenerator(gen:Generator<'T>) =
        match gen with
        | :? LazyGeneratorWrappingEnumerator<'T> as g -> g.Enumerator
        | _ -> (new EnumeratorWrappingLazyGenerator<_>(gen) :> System.Collections.Generic.IEnumerator<_>)

    [<Js>]
    let GenerateFromEnumerator (t:System.Collections.Generic.IEnumerator<'T>) =
        match t with
        | :? EnumeratorWrappingLazyGenerator<'T> as e ->  e.Generator
        | _ -> (new LazyGeneratorWrappingEnumerator<'T>(t) :> Generator<'T>)


namespace Pit.FSharp.Core.CompilerServices
    open Pit
    open Pit.FSharp.Collections
    open System.Collections
    open System.Collections.Generic

    module RuntimeHelpers =

        [<Js>]
        let inline checkNonNull argName arg =
            match box arg with
            | null -> nullArg argName
            | _ -> ()

        // reusing mkSeq from operators
        [<Js>]
        let mkSeq f = new Pit.FSharp.Core.Operators.mkSeq<_>(f)

        (*type mkSeq<'U> [<Js>](f) =
            interface IEnumerable<'U> with
                [<Js>]
                member x.GetEnumerator() = f()
            interface IEnumerable with
                [<Js>]
                member x.GetEnumerator() = (f() :> IEnumerator)*)

        type EmptyEnumerable<'T> =
            | EmptyEnumerable
            interface IEnumerable<'T> with
                [<Js>]
                member x.GetEnumerator() = IEnumerator.Empty<'T>()
            interface IEnumerable with
                [<Js>]
                member x.GetEnumerator() = (IEnumerator.Empty<'T>() :> IEnumerator)
        [<Js>]
        let Generate openf compute closef =
            mkSeq (fun () -> IEnumerator.generateWhileSome openf compute closef)

        [<Js>]
        let GenerateUsing (openf : unit -> ('U :> System.IDisposable)) compute =
            Generate openf compute (fun (s:'U) -> s.Dispose())

        [<Js>]
        let EnumerateFromFunctions opener moveNext current =
            Generate
                opener
                (fun x -> if moveNext x then Some(current x) else None)
                (fun x -> match box(x) with :? System.IDisposable as id -> id.Dispose() | _ -> ())

        // A family of enumerators that can have additional 'finally' actions added to the enumerator through
        // the use of mutation. This is used to 'push' the disposal action for a 'use' into the next enumerator.
        // For example,
        //    seq { use x = ...
        //          while ... }
        // results in the 'while' loop giving an adjustable enumerator. This is then adjusted by adding the disposal action
        // from the 'use' into the enumerator. This means that we avoid constructing a two-deep enumerator chain in this
        // common case.
        type IFinallyEnumerator =
            abstract AppendFinallyAction : (unit -> unit) -> unit

        /// A concrete implementation of IEnumerable that adds the given compensation to the "Dispose" chain of any
        /// enumerators returned by the enumerable.
        [<Sealed>]
        type FinallyEnumerable<'T> [<Js>](compensation: unit -> unit, restf: unit -> seq<'T>) =
            interface IEnumerable<'T> with
                [<Js>]
                member x.GetEnumerator() =
                    try
                        let ie = restf().GetEnumerator()
                        match ie with
                        | :? IFinallyEnumerator as a ->
                            a.AppendFinallyAction(compensation);
                            ie
                        | _ ->
                            IEnumerator.EnumerateThenFinally compensation ie
                    with e ->
                        compensation();
                        reraise()
            interface IEnumerable with
                [<Js>]
                member x.GetEnumerator() = ((x :> IEnumerable<'T>).GetEnumerator() :> IEnumerator)

        /// An optimized object for concatenating a sequence of enumerables
        [<Sealed>]
        type ConcatEnumerator<'T,'U when 'U :> seq<'T>> [<Js>](sources: seq<'U>) =
            let mutable outerEnum = sources.GetEnumerator()
            let mutable currInnerEnum = IEnumerator.Empty()

            let mutable started = false
            let mutable finished = false
            let mutable compensations = []

            [<DefaultValue(false)>] // false = unchecked
            val mutable private currElement : 'T

            [<Js>]
            member this.Finish() =
                finished <- true
                try
                    match currInnerEnum with
                    | null -> ()
                    | _ ->
                        try
                            currInnerEnum.Dispose()
                        finally
                            currInnerEnum <- null
                finally
                    try
                        match outerEnum with
                        | null -> ()
                        | _ ->
                            try
                                outerEnum.Dispose()
                            finally
                                outerEnum <- null
                    finally
                        let rec iter comps =
                            match comps with
                            |   [] -> ()
                            |   h::t ->
                                    try h() finally iter t
                        try
                            compensations |> List.rev |> iter
                        finally
                            compensations <- []

            [<Js>]
            member this.GetCurrent() =
                IEnumerator.check started;
                if finished then IEnumerator.alreadyFinished() else this.currElement

            interface IFinallyEnumerator with
                [<Js>]
                member this.AppendFinallyAction(f) =
                    compensations <- f :: compensations

            interface IEnumerator<'T> with
                [<Js>]
                member this.Current = this.GetCurrent()

            interface IEnumerator with
                [<Js>]
                member this.Current = box (this.GetCurrent())

                [<Js>]
                member this.MoveNext() =
                   if not started then (started <- true)
                   if finished then false
                   else
                      let rec takeInner () =
                        // check the inner list
                        if currInnerEnum.MoveNext() then
                            this.currElement <- currInnerEnum.Current;
                            true
                        else
                            // check the outer list
                            let rec takeOuter() =
                                if outerEnum.MoveNext() then
                                    let ie = outerEnum.Current
                                    // Optimization to detect the statically-allocated empty IEnumerables
                                    match box ie with
                                    | :? EmptyEnumerable<'T> ->
                                         // This one is empty, just skip, don't call GetEnumerator, try again
                                         takeOuter()
                                    | _ ->
                                         // OK, this one may not be empty.
                                         // Don't forget to dispose of the enumerator for the inner list now we're done with it
                                         currInnerEnum.Dispose();
                                         currInnerEnum <- ie.GetEnumerator();
                                         takeInner ()
                                else
                                    // We're done
                                    this.Finish()
                                    false
                            takeOuter()
                      takeInner ()

                [<Js>]
                member this.Reset() = IEnumerator.noReset()

            interface System.IDisposable with
                [<Js>]
                member this.Dispose() =
                    if not finished then
                        this.Finish()

        [<Js>]
        let EnumerateUsing (resource : 'T :> System.IDisposable) (rest: 'T -> #seq<'U>) =
            (FinallyEnumerable((fun () -> match box resource with null -> () | _ -> resource.Dispose()),
                               (fun () -> rest resource :> seq<_>)) :> seq<_>)

        [<Js>]
        let mkConcatSeq (sources: seq<'U :> seq<'T>>) =
            mkSeq (fun () -> new ConcatEnumerator<_,_>(sources) :> IEnumerator<'T>) :> seq<'T>

        type EnumerateWhileType<'T> [<Js>](g : unit -> bool, b: seq<'T>)  =
            let started = ref false
            let curr = ref None

            [<Js>]
            member this.getCurr() =
                IEnumerator.check !started;
                match !curr with None -> IEnumerator.alreadyFinished() | Some x -> x

            [<Js>]
            member this.start() = if not !started then (started := true)

            [<Js>]
            member this.finish() = (curr := None)

            interface IEnumerator<seq<'T>> with
                [<Js>]
                member this.Current = this.getCurr()
            interface IEnumerator with
                [<Js>]
                member this.Current = box (this.getCurr())
                [<Js>]
                member this.MoveNext() =
                    this.start();
                    let keepGoing = (try g() with e -> this.finish (); reraise ()) in
                    if keepGoing then
                        curr := Some(b); true
                    else
                        this.finish(); false
                [<Js>]
                member x.Reset() = IEnumerator.noReset()
            interface System.IDisposable with
                [<Js>]
                member x.Dispose() = ()

        [<Js>]
        let EnumerateWhile (g : unit -> bool) (b: seq<'T>) : seq<'T> =
            mkConcatSeq
                (mkSeq(fun () -> (new EnumerateWhileType<_>(g, b) :> IEnumerator<_>) ))

        [<Js>]
        let EnumerateThenFinally (rest : seq<'T>) (compensation : unit -> unit)  =
            (FinallyEnumerable(compensation, (fun () -> rest)) :> seq<_>)

namespace Pit.FSharp.Collections
    open Pit
    open System.Collections
    open System.Collections.Generic
    open Pit.Javascript

    module SeqModule =
        open Pit.FSharp.Core.CompilerServices.RuntimeHelpers

        [<Js>]
        let inline checkNonNull argName arg =
            match box arg with
            | null -> nullArg argName
            | _ -> ()

        // reusing mkSeq from operators
        [<Js>]
        let mkSeq f = new Pit.FSharp.Core.Operators.mkSeq<_>(f)

//        type mkSeq<'U> [<Js>](f) =
//            interface IEnumerable<'U> with
//                [<Js>]
//                member x.GetEnumerator() = f()
//            interface IEnumerable with
//                [<Js>]
//                member x.GetEnumerator() = (f() :> IEnumerator)

        type EmptyEnumerable<'T> =
            | EmptyEnumerable
            interface IEnumerable<'T> with
                [<Js>]
                member x.GetEnumerator() = IEnumerator.Empty<'T>()
            interface IEnumerable with
                [<Js>]
                member x.GetEnumerator() = (IEnumerator.Empty<'T>() :> IEnumerator)

        [<Js>]
        let mkDelayedSeq (f: unit -> IEnumerable<'T>) = mkSeq (fun () -> f().GetEnumerator())

        [<Js>]
        let mkUnfoldSeq f x = mkSeq (fun () -> IEnumerator.unfold f x)

        [<Js>]
        let Delay f = mkDelayedSeq f

        [<Js>]
        let Unfold f x = mkUnfoldSeq f x

        [<Js>]
        let Empty<'T>() = mkSeq(fun () -> IEnumerator.Empty<_>()) :> seq<'T>

        [<Js>]
        let InitializeInfinite f = mkSeq (fun () -> IEnumerator.upto None f)

        [<Js>]
        let Initialize count f =
            if count < 0 then raise (new System.InvalidOperationException("Input must be non-negative"))
            mkSeq (fun () -> IEnumerator.upto (Some (count-1)) f)

        [<Js>]
        let OfArray arr = mkSeq(fun () -> IEnumerator.ofArray arr)

        [<Js>]
        let Iterate f (source : seq<'T>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            while e.MoveNext() do
                f e.Current;

        [<Js>]
        let Get i (source : seq<'T>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            IEnumerator.nth i e

        [<Js>]
        let IterateIndexed f (source : seq<'T>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable i = 0
            while e.MoveNext() do
                f i e.Current;
                i <- i + 1;

        [<Js>]
        let Exists f (source : seq<'T>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable state = false
            while (not state && e.MoveNext()) do
                state <- f e.Current
            state

        [<Js>]
        let ForAll f (source : seq<'T>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable state = true
            while (state && e.MoveNext()) do
                state <- f e.Current
            state

        [<Js>]
        let Iterate2 f (source1 : seq<_>) (source2 : seq<_>) =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            use e1 = source1.GetEnumerator()
            use e2 = source2.GetEnumerator()
            while (e1.MoveNext() && e2.MoveNext()) do
                f e1.Current e2.Current;

        [<Js>]
        let revamp f (ie : seq<_>) = mkSeq (fun () -> f (ie.GetEnumerator()))

        [<Js>]
        let revamp2 f (ie1 : seq<_>) (source2 : seq<_>) =
            mkSeq (fun () -> f (ie1.GetEnumerator()) (source2.GetEnumerator()))

        [<Js>]
        let Filter f source =
            checkNonNull "source" source
            revamp  (IEnumerator.filter f) source

        [<Js>]
        let Map f source =
            checkNonNull "source" source
            revamp  (IEnumerator.map f) source

        [<Js>]
        let MapIndexed f source =
            checkNonNull "source" source
            revamp  (IEnumerator.mapi f) source

        [<Js>]
        let Map2 f source1 source2 =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            revamp2 (IEnumerator.map2 f) source1 source2

        [<Js>]
        let Choose f source =
            checkNonNull "source" source
            revamp  (IEnumerator.choose f) source

        [<Js>]
        let Zip source1 source2  =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            Map2 (fun x y -> x,y) source1 source2

        [<Js>]
        let Zip3 source1 source2 source3 =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            checkNonNull "source3" source3
            Map2 (fun x (y,z)-> x,y,z) source1 (Zip source2 source3)

        [<Js>]
        let TryPick f (source:seq<_>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable res = None
            while (Option.isNone res && e.MoveNext()) do
                res <- f e.Current
            res

        [<Js>]
        let Pick f source =
            checkNonNull "source" source
            match TryPick f source with
            | None      -> raise (System.InvalidOperationException("Key not found"))
            | Some x    -> x

        [<Js>]
        let TryFind f (source:seq<_>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable res = None
            while (Option.isNone res && e.MoveNext()) do
                let c = e.Current
                if f c then res <- Some(c)
            res

        [<Js>]
        let Find f source =
            checkNonNull "source" source
            match TryFind f source with
            | None      -> raise (System.InvalidOperationException("Key not found"))
            | Some x    -> x

        [<Js>]
        let IsEmpty (source:seq<'t>) =
            checkNonNull "source" source
            use ie = source.GetEnumerator()
            not(ie.MoveNext())

        [<Js>]
        let Concat sources =
            checkNonNull "sources" sources
            mkConcatSeq sources

        [<Js>]
        let Length (source:seq<_>) =
            use e = source.GetEnumerator()
            let mutable state = 0
            while e.MoveNext() do
                state <- state + 1
            state

        [<Js>]
        let Fold<'t,'state> f (x:'state) (source:seq<'t>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable state = x
            while e.MoveNext() do
                state <- f state e.Current
            state

        [<Js>]
        let Reduce f (source:seq<'t>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            if not(e.MoveNext()) then raise (System.InvalidOperationException("Input sequence is empty"))
            let mutable state = e.Current
            while e.MoveNext() do
                state <- f state e.Current;
            state

        [<Js>]
        let fromGenerator f = mkSeq(fun () -> Generator.EnumerateFromGenerator (f()))

        [<Js>]
        let toGenerator (ie : seq<_>) = Generator.GenerateFromEnumerator (ie.GetEnumerator())

        [<Js>]
        let Append (source1: seq<'T>) (source2: seq<'T>) =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            fromGenerator(fun () -> Generator.bindG (toGenerator source1) (fun () -> toGenerator source2))

        [<Js>]
        let Collect f sources = Map f sources |> Concat

        [<Js>]
        let CompareWith (f:'T->'T->int) (source1:seq<'T>) (source2:seq<'T>) =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            use e1 = source1.GetEnumerator()
            use e2 = source2.GetEnumerator()
            let rec go() =
                let e1ok = e1.MoveNext()
                let e2ok = e2.MoveNext()
                let c = (if e1ok = e2ok then 0 else if e1ok then 1 else -1)
                if c <> 0 then c else
                if not e1ok || not e2ok then 0
                else
                    let c = f e1.Current e2.Current
                    if c <> 0 then c else
                    go ()
            go()

        [<Js>]
        let Singleton x = mkSeq (fun () -> IEnumerator.Singleton x)

        [<Js>]
        let Truncate n (source:seq<_>) =
            checkNonNull "source" source
            seq { let i = ref 0
                  use ie = source.GetEnumerator()
                  while !i < n && ie.MoveNext() do
                    i := !i + 1
                    yield ie.Current }

        [<Js>]
        let Take count (source:seq<_>) =
            checkNonNull "source" source
            if count < 0 then raise (System.InvalidOperationException("Input must be non-negative"))

            if count = 0 then Empty() else
            seq { use e = source.GetEnumerator()
                  for _ in 0 .. count - 1 do
                    if not(e.MoveNext()) then
                        raise (System.InvalidOperationException("Not enough elements"))
                    yield e.Current }

        [<Js>]
        let TakeWhile p (source: seq<_>) =
            checkNonNull "source" source
            seq { use e = source.GetEnumerator()
                  let latest = ref Unchecked.defaultof<_>
                  while e.MoveNext() && (latest := e.Current; p !latest) do
                      yield !latest }

        [<Js>]
        let Skip count (source: seq<_>) =
            checkNonNull "source" source
            seq { use e = source.GetEnumerator()
                  for _ in 1 .. count do
                      if not (e.MoveNext()) then
                          raise <| System.InvalidOperationException ("Not enough elements")
                  while e.MoveNext() do
                      yield e.Current }

        [<Js>]
        let SkipWhile p (source: seq<_>) =
            checkNonNull "source" source
            seq { use e = source.GetEnumerator()
                  let latest = ref (Unchecked.defaultof<_>)
                  let ok = ref false
                  while e.MoveNext() do
                      latest := e.Current
                      if ((!ok || not (p !latest))) then
                          ok := true
                          yield !latest }

        [<Js>]
        let Pairwise (source:seq<_>) =
            checkNonNull "source" source
            seq { use ie = source.GetEnumerator()
                  if ie.MoveNext() then
                    let iref = ref ie.Current
                    while ie.MoveNext() do
                        let j = ie.Current
                        yield (!iref, j)
                        iref := j }

        [<Js>]
        let Scan<'t,'state> f (z:'state) (source:seq<'t>) =
            checkNonNull "source" source
            seq { let zref = ref z
                  yield !zref
                  use ie = source.GetEnumerator()
                  while ie.MoveNext() do
                    zref := f !zref ie.Current
                    yield !zref }

        [<Js>]
        let FindIndex f (source:seq<_>) =
            checkNonNull "source" source
            use ie = source.GetEnumerator()
            let rec loop i =
                if ie.MoveNext() then
                    if f ie.Current then
                        i
                    else
                        loop (i+1)
                else
                    raise (System.InvalidOperationException("Key not found"))
            loop 0

        [<Js>]
        let TryFindIndex f (source:seq<_>) =
            checkNonNull "source" source
            use ie = source.GetEnumerator()
            let rec loop i =
                if ie.MoveNext() then
                    if f ie.Current then
                        Some(i)
                    else loop(i+1)
                else
                    None
            loop 0

        [<Js>]
        let ToList (source : seq<'T>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable res = []
            while e.MoveNext() do
                res <- e.Current :: res
            List.rev res

        [<Js>]
        let OfList (list: 'T list) =
            (list :> seq<'T>)

        [<Js>]
        let ToArray (source : seq<'T>)  =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let res : 'T[] = [||]
            let mutable i = 0
            while e.MoveNext() do
                res.[i] <- e.Current
                i <- i + 1
            res

        [<Js>]
        let inline Sum (source: seq< (^a) >) : ^a =
            use e = source.GetEnumerator()
            let mutable acc = 0
            while e.MoveNext() do
                acc <- acc + e.Current
            acc

        [<Js>]
        let inline SumBy f (source: seq< (^a) >) : ^a =
            use e = source.GetEnumerator()
            let mutable acc = 0
            while e.MoveNext() do
                acc <- acc + (f e.Current)
            acc

        [<Js>]
        let inline Average (source: seq< (^a) >) : ^a =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable acc = 0
            let mutable count = 0
            while e.MoveNext() do
                acc <- acc + e.Current
                count <- count + 1
            if count = 0 then
                raise (System.InvalidOperationException("Input Sequence is Empty"))
            (acc / count)

        [<Js>]
        let inline AverageBy f (source: seq< (^a) >) : ^a =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            let mutable acc = 0
            let mutable count = 0
            while e.MoveNext() do
                acc <- acc + (f e.Current)
                count <- count + 1
            if count = 0 then
                raise (System.InvalidOperationException("Input Sequence is Empty"))
            (acc / count)

        [<Js>]
        let inline Min (source: seq<_>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            if not (e.MoveNext()) then
                raise (System.InvalidOperationException("Input Sequence is Empty"))
            let mutable acc = e.Current
            while e.MoveNext() do
                let curr = e.Current
                if curr < acc then
                    acc <- curr
            acc

        [<Js>]
        let inline MinBy (f : 'T -> 'U) (source: seq<'T>) : 'T =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            if not (e.MoveNext()) then
                raise (System.InvalidOperationException("Input Sequence is Empty"))
            let first = e.Current
            let mutable acc = f first
            let mutable accv = first
            while e.MoveNext() do
                let currv = e.Current
                let curr = f currv
                if curr < acc then
                    acc <- curr
                    accv <- currv
            accv

        [<Js>]
        let inline Max (source: seq<_>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            if not (e.MoveNext()) then
                raise (System.InvalidOperationException("Input Sequence is Empty"))
            let mutable acc = e.Current
            while e.MoveNext() do
                let curr = e.Current
                if curr > acc then
                    acc <- curr
            acc

        [<Js>]
        let inline MaxBy f (source: seq<_>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            if not (e.MoveNext()) then
                raise (System.InvalidOperationException("Input Sequence is Empty"))
            let first = e.Current
            let mutable acc = f first
            let mutable accv = first
            while e.MoveNext() do
                let currv = e.Current
                let curr = (f currv)
                if curr > acc then
                    acc <- curr
                    accv <- currv
            accv

        [<Js>]
        let ForAll2 p (source1: seq<_>) (source2: seq<_>) =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            use e1 = source1.GetEnumerator()
            use e2 = source2.GetEnumerator()
            let mutable ok = true
            while (ok && e1.MoveNext() && e2.MoveNext()) do
                ok <- p e1.Current e2.Current;
            ok

        [<Js>]
        let Exists2 p (source1: seq<_>) (source2: seq<_>) =
            checkNonNull "source1" source1
            checkNonNull "source2" source2
            use e1 = source1.GetEnumerator()
            use e2 = source2.GetEnumerator()
            let mutable ok = false
            while (not ok && e1.MoveNext() && e2.MoveNext()) do
                ok <- p e1.Current e2.Current;
            ok

        [<Js>]
        let Head (source : seq<_>) =
            checkNonNull "source" source
            use e = source.GetEnumerator()
            if (e.MoveNext()) then e.Current
            else raise (System.InvalidOperationException("Input Sequence is Empty"))

        [<Js>]
        let GroupBy (f:'a->'b) (source:seq<'a>) =
            let arr:('b*'a[])[] = [||]
            let array = new JsArray<_>(arr)
            use e = source.GetEnumerator()
            while e.MoveNext() do
                let item = e.Current
                let key  = f item
                match arr |> Array.tryFind(fun (k,x)-> key = k) with
                | Some(result) ->
                    let coll = new JsArray<_>(snd result)
                    coll.Push(item)
                | None         ->
                    let tuple = (key,[|item|])
                    array.Push(tuple)
            arr

        [<Js>]
        let CountBy (f:'a->obj) (source:seq<'a>) =
            let arr:(obj*int)[] = [||]
            let array = new JsArray<_>(arr)
            use e = source.GetEnumerator()
            while e.MoveNext() do
                let key  = f(e.Current)
                match arr |> Array.tryFindIndex(fun (k,x)-> key = k) with
                | Some(i) ->
                    let t = snd arr.[i]
                    arr.[i] <- (key,t+1)
                | None    ->
                    array.Push((key,1))
            arr

        [<Js>]
        let Distinct (source:seq<'a>) =
            let arr:'a[] = [||]
            let array = new JsArray<_>(arr)
            use e = source.GetEnumerator()
            while e.MoveNext() do
                let item = e.Current
                match arr |> Array.exists(fun k -> item = k) with
                | true  -> ()
                | false -> array.Push(item)
            arr

        [<Js>]
        let DistinctBy (f:'a->'b) (source:seq<'a>) =
            let keys:'b[] = [||]
            let keysArr   = new JsArray<_>(keys)
            let values:'a[] = [||]
            let valuesArr   = new JsArray<_>(values)
            use e = source.GetEnumerator()
            while e.MoveNext() do
                let item = e.Current
                let key = f item
                match keys |> Array.exists(fun k -> item = k) with
                | true -> ()
                | false ->
                    keysArr.Push(key)
                    valuesArr.Push(item)
            values

        [<Js>]
        let Sort source =
            checkNonNull "source" source
            mkDelayedSeq (fun () ->
                let array = source |> Seq.toArray
                Array.sortInPlace array
                array :> seq<_>)

        [<Js>]
        let SortBy keyf source =
            checkNonNull "source" source
            mkDelayedSeq (fun () ->
                let array = source |> Seq.toArray
                Array.sortInPlaceBy keyf array
                array :> seq<_>)

        [<Js>]
        let Windowed windowSize (source:seq<_>) =
            checkNonNull "source" source
            if windowSize <= 0 then raise(System.InvalidOperationException("Input must be non-negative"))
            seq { let arr = Array.zeroCreate windowSize
                  let r = ref (windowSize-1)
                  let i = ref 0
                  use e = source.GetEnumerator()
                  while e.MoveNext() do
                    arr.[!i] <- e.Current
                    i := (!i+1) % windowSize
                    if !r = 0 then
                        yield Array.init windowSize (fun j -> arr.[(!i+j) % windowSize])
                    else
                        r := (!r-1) }

        //let cache (source:seq<_>) =

        [<Js>]
        let ReadOnly (source:seq<_>) =
            checkNonNull "source" source
            mkSeq (fun () -> source.GetEnumerator())