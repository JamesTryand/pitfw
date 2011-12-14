namespace Pit.Rx

    module Observable =
        open Pit.Dom
        open Pit

        /// Simple implementation of IObserver<'T>, Since we are using ReflectedDefinitionAttribute, we won't be able to use Object expressions
        type BasicObserver<'T> [<Js>](next:'T->unit, error: exn->unit, completed: unit->unit) =
            let mutable stopped = false
            interface IObserver<'T> with
                [<Js>]
                member x.OnNext(args)   = next args
                [<Js>]
                member x.OnError(e)     = error(e)
                [<Js>]
                member x.OnCompleted()  = completed()

        /// Simple implementation of IObservable<'T>
        type BasicObservable<'T> [<Js>](f: IObserver<'T> -> IDisposable) =
            interface IObservable<'T> with
                [<Js>]
                member x.Subscribe(observer: IObserver<'T>) = f(observer)

        /// Wrapper function to create an IObservable
        [<Js>]
        let mkObservable (f: IObserver<'T> -> IDisposable) = new BasicObservable<_>(f) :> IObservable<_>

        /// Wrapper function to create an IObserver
        [<Js>]
        let mkObserver (n: 'T -> unit) (e: exn -> unit) (c: unit -> unit) = new BasicObserver<_>(n, e, c) :> IObserver<_>

        /// Observable function that will invoke the lambda function.
        [<Js>]
        let invoke f (w:IObservable<'T>) =
            mkObservable(
                fun observer ->
                    let obs =
                        mkObserver
                            (fun v  -> f(fun () -> observer.OnNext(v)) )
                            (fun e  -> f(fun () -> observer.OnError(e)))
                            (fun () -> f(fun () -> observer.OnCompleted()))
                    w.Subscribe(obs)
            )

        /// Delays the execution of the Observable using window.SetTimeout
        /// Usefule in creating a simple time delays to the observable events
        [<Js>]
        let delay milliseconds (w:IObservable<'T>) =
            let f g = (window.SetTimeout((fun () -> g()), milliseconds) |> ignore)
            invoke f w