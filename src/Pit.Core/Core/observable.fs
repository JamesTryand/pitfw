namespace Pit.FSharp.Control

open Pit
open System

    module ObservableModule =

        type BasicObserver<'T> [<Js>](next:'T->unit, error: exn->unit, completed: unit->unit) =
            let mutable stopped = false

            interface IObserver<'T> with
                [<Js>]
                member x.OnNext(args) = next args
                [<Js>]
                member x.OnError(e)   = error(e)
                [<Js>]
                member x.OnCompleted() = completed()

        type BasicObservable<'T> [<Js>](f: IObserver<'T> -> IDisposable) =

            interface IObservable<'T> with
                [<Js>]
                member x.Subscribe(observer: IObserver<'T>) = f(observer)

        [<Js>]
        let mkObservable (f: IObserver<'T> -> IDisposable) = new BasicObservable<_>(f) :> IObservable<_>

        [<Js>]
        let mkObserver (n: 'T -> unit) (e: exn -> unit) (c: unit -> unit) = new BasicObserver<_>(n, e, c) :> IObserver<_>

        [<Js>]
        let inline protect f succeed fail =
          match (try Choice1Of2 (f ()) with e -> Choice2Of2 e) with
            | Choice1Of2 x -> (succeed x)
            | Choice2Of2 e -> (fail e)

        [<CompiledName("Map")>]
        [<Js>]
        let map f (w: IObservable<'T>) =
            mkObservable(
                fun observer ->
                    let o = mkObserver
                                (fun v -> protect (fun () -> f v) observer.OnNext observer.OnError)
                                (fun e -> observer.OnError(e))
                                (fun () -> observer.OnCompleted())
                    w.Subscribe(o)
            )

        [<CompiledName("Choose")>]
        [<Js>]
        let choose f (w: IObservable<'T>) =
            mkObservable(
                fun observer ->
                    let o = mkObserver
                                (fun v -> protect (fun () -> f v) (function None -> () | Some v2 -> observer.OnNext v2) observer.OnError)
                                (fun e -> observer.OnError(e))
                                (fun () -> observer.OnCompleted())
                    w.Subscribe(o)
            )

        [<CompiledName("Filter")>]
        [<Js>]
        let filter f (w:IObservable<'T>) =
            choose (fun x -> if f x then Some x else None) w

        [<CompiledName("Partition")>]
        [<Js>]
        let partition f (w: IObservable<'T>) =
            filter f w, filter (f >> not) w

        [<CompiledName("Scan")>]
        [<Js>]
        let scan f z (w: IObservable<'T>) =
            mkObservable(
                fun observer ->
                    let state = ref z
                    let o = mkObserver
                                (fun v ->
                                    let s = !state
                                    protect (fun () -> f s v) (fun z ->
                                                state := z
                                                observer.OnNext z) observer.OnError)
                                (fun e -> observer.OnError(e))
                                (fun () -> observer.OnCompleted())
                    w.Subscribe(o)
            )

        [<CompiledName("Add")>]
        [<Js>]
        let add f (w: IObservable<'T>) = w.Add(f)

        [<CompiledName("Subscribe")>]
        [<Js>]
        let subscribe (f: 'T -> unit) (w: IObservable<'T>) = w.Subscribe(f)

        [<CompiledName("Pairwise")>]
        [<Js>]
        let pairwise (w : IObservable<'T>) : IObservable<'T * 'T> =
            mkObservable(
                fun observer ->
                    let lastArgs = ref None
                    let o =
                        mkObserver
                            (fun args2 ->
                                match !lastArgs with
                                | None          -> ()
                                | Some args1    -> observer.OnNext(args1, args2)
                                lastArgs := Some args2)
                            (fun e -> observer.OnError(e))
                            (fun () -> observer.OnCompleted())
                    w.Subscribe(o)
            )

        [<CompiledName("Merge")>]
        [<Js>]
        let merge (w1: IObservable<'T>) (w2: IObservable<'T>) =
            mkObservable(
                fun observer ->
                    let stopped = ref false
                    let completed1 = ref false
                    let completed2 = ref false
                    let obs1 =
                        mkObserver
                            (fun v  -> if not(!stopped) then observer.OnNext(v))
                            (fun e  -> if not(!stopped) then stopped := true; observer.OnError(e))
                            (fun () ->
                                if not(!stopped) then
                                    completed1 := true;
                                    if !completed1 && !completed2 then
                                        stopped := true
                                        observer.OnCompleted())
                    let h1 = w1.Subscribe(obs1)
                    let obs2 =
                        mkObserver
                            (fun v  -> if not(!stopped) then observer.OnNext(v))
                            (fun e  -> if not(!stopped) then stopped := true; observer.OnError(e))
                            (fun () ->
                                if not(!stopped) then
                                    completed2 := true;
                                    if !completed1 && !completed2 then
                                        stopped := true
                                        observer.OnCompleted())
                    let h2 = w2.Subscribe(obs2)
                    new BasicDisposable(fun () -> h1.Dispose(); h2.Dispose()) :> IDisposable
            )

        [<CompiledName("Split")>]
        [<Js>]
        let split (f : 'T -> Choice<'U1,'U2>) (w: IObservable<'T>) =
            choose (fun v -> match f v with Choice1Of2 x -> Some x | _ -> None) w,
            choose (fun v -> match f v with Choice2Of2 x -> Some x | _ -> None) w