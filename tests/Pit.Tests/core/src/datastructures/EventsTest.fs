namespace Pit.Test
open Pit

    module EventsTest =

        type EventTest [<Js>]() =
            let evt = new Event<int>()
            let evt2 = new Event<int>()
            let mutable i = 0
            let mutable i2 = 0

            [<Js>]
            member this.FakeCall() =
                evt.Trigger(i)
                i <- i + 1

            [<Js>]
            member this.FakeCall2() =
                evt2.Trigger(i2)
                i2 <-  i2 + 1

            [<Js>]
            member this.Evt with get() = evt.Publish

            [<Js>]
            member this.Evt2 with get() = evt2.Publish

        [<Js>]
        let Add() =
            let e = new EventTest()
            e.Evt
            |> Event.add(fun i ->
                Assert.AreEqual "Event Add test" i 0
            )

            e.FakeCall()


        [<Js>]
        let Map() =
            let e = new EventTest()
            e.Evt
            |> Event.map(fun i -> (i, i+1))
            |> Event.add(fun (k, l) ->
                Assert.AreEqual "Event Map test" k 0
                Assert.AreEqual "Event Map test" l 1
            )

            e.FakeCall()

        [<Js>]
        let Choose() =
            let e = new EventTest()
            e.Evt
            |> Event.choose(fun i ->
                    if i = 1 then
                        Some(i, i + 1)
                    else None)
            |> Event.add(fun (k, l) ->
                Assert.AreEqual "Event Choose test" k 1
                Assert.AreEqual "Event Choose test" l 2
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Filter() =
            let e = new EventTest()
            e.Evt
            |> Event.filter(fun i ->
                    if i = 1 then
                        true
                    else false)
            |> Event.add(fun k ->
                Assert.AreEqual "Event Filter test" k 1
            )

            e.FakeCall()
            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Merge() =
            let e = new EventTest()
            e.Evt
            |> Event.merge(e.Evt2)
            |> Event.add(fun k ->
                Assert.AreEqual "Event Merge test" k 0
            )

            e.FakeCall()
            e.FakeCall2()


        [<Js>]
        let PairWise() =
            let e = new EventTest()
            e.Evt
            |> Event.pairwise
            |> Event.add(fun (k , l) ->
                Assert.AreEqual "Event PairWise test" k 0
                Assert.AreEqual "Event PairWise test" l 1
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Partition() =
            let e = new EventTest()
            let (e1, e2) =
                e.Evt
                |> Event.partition (fun l -> l = 1)

            e1 |> Event.add(fun l ->
                Assert.AreEqual "Event Partition test" l 1
            )

            e2 |> Event.add(fun l ->
                Assert.AreEqual "Event Partition test" l 0
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Scan() =
            let initialState = 0
            let i = ref 1 // initial state starts from (0+1)
            let e = new EventTest()
            e.Evt
            |> Event.scan (fun state _ -> state + 1) initialState
            |> Event.add(fun l ->
                Assert.AreEqual "Event Scan Test" i l
                i := !i + 1
            )

            e.FakeCall()
            e.FakeCall()


        [<Js>]
        let (|Odd|Even|) (i) =
            if (i % 2 = 0) then Odd(i)
            else Even(i)

        [<Js>]
        let Split() =
            let initialState = 0
            let e = new EventTest()
            let (OddResult, EvenResult) = Event.split (|Odd|Even|) e.Evt

            OddResult |> Event.add (fun k ->
                Assert.AreEqual "Event Split test" k  0 )

            EvenResult |> Event.add (fun k ->
                Assert.AreEqual "Event Split test" k 1 )

            e.FakeCall()
            e.FakeCall()

    module Event2Tests =
        type Args [<Js>](x:int) =
            [<Js>]
            member this.XValue = x

        type Delegate1 = delegate of obj * Args -> unit

        type Event2Test [<Js>]() =
            let ev = new Event<Delegate1, Args>()
            let ev2 = new Event<Delegate1, Args>()
            let mutable i = 0
            let mutable i2 = 0

            [<Js>]
            member this.FakeCall() =
                ev.Trigger(this, new Args(i))
                i <- i + 1

            [<Js>]
            member this.FakeCall2() =
                ev2.Trigger(this, new Args(i2))
                i2 <- i2 + 1

            [<Js>]
            member this.Evt = ev.Publish

            [<Js>]
            member this.Evt2 = ev2.Publish

        [<Js>]
        let Add() =
            let e = new Event2Test()
            e.Evt
            |> Event.add(fun arg ->
                Assert.AreEqual "Event2 add test" arg.XValue 0
            )
            e.FakeCall()

        [<Js>]
        let Map() =
            let e = new Event2Test()
            e.Evt
            |> Event.map(fun a -> (a.XValue, a.XValue + 1))
            |> Event.add(fun (k, l) ->
                Assert.AreEqual "Event2 Map test" k 0
                Assert.AreEqual "Event2 Map test" l 1
            )
            e.FakeCall()

        [<Js>]
        let Choose() =
            let e = new Event2Test()
            e.Evt
            |> Event.choose(fun arg ->
                if arg.XValue = 1 then
                    Some(arg.XValue, arg.XValue + 1)
                else
                    None
            )
            |> Event.add(fun (k,l) ->
                Assert.AreEqual "Event2 Choose test" k 1
                Assert.AreEqual "Event2 Choose test" l 2
            )
            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Filter() =
            let e = new Event2Test()
            e.Evt
            |> Event.filter(fun arg-> if arg.XValue = 1 then true else false)
            |> Event.add(fun k->
                Assert.AreEqual "Event2 filter test" k.XValue 1
            )
            e.FakeCall()
            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Merge() =
            let e = new Event2Test()
            e.Evt
            |> Event.merge(e.Evt2)
            |> Event.add(fun arg ->
                Assert.AreEqual "Event2 Merge test" arg.XValue 0
            )
            e.FakeCall()
            e.FakeCall2()

        [<Js>]
        let PairWise() =
            let e = new Event2Test()
            e.Evt
            |> Event.pairwise
            |> Event.add(fun (k,l) ->
                Assert.AreEqual "Event2 Pairwise test" k.XValue 0
                Assert.AreEqual "Event2 Pairwise test" l.XValue 1
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Partition() =
            let e = new Event2Test()
            let (e1, e2) =
                e.Evt
                |> Event.partition(fun l -> l.XValue = 1)

            e1 |> Event.add(fun l ->
                Assert.AreEqual "Event2 Partition test" l.XValue 1
            )

            e2 |> Event.add(fun l ->
                Assert.AreEqual "Event2 Partition test" l.XValue 0
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Scan() =
            let initialState = 0
            let i = ref 1 // initial state starts from (0+1)
            let e = new Event2Test()
            e.Evt
            |> Event.scan (fun state _ -> state + 1) initialState
            |> Event.add(fun l ->
                Assert.AreEqual "Event2 Scan Test" i l
                i := !i + 1
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let (|Odd|Even|) (i:Args) =
            if (i.XValue % 2 = 0) then Odd(i.XValue)
            else Even(i.XValue)

        [<Js>]
        let Split() =
            let initialState = 0
            let e = new Event2Test()
            let (OddResult, EvenResult) = Event.split (|Odd|Even|) e.Evt

            OddResult |> Event.add (fun k ->
                Assert.AreEqual "Event2 Split test" k  0 )

            EvenResult |> Event.add (fun k ->
                Assert.AreEqual "Event2 Split test" k 1 )

            e.FakeCall()
            e.FakeCall()

    module ObservableTests =

        type EventTest [<Js>]() =
            let evt = new Event<int>()
            let evt2 = new Event<int>()
            let mutable i = 0
            let mutable i2 = 0

            [<Js>]
            member this.FakeCall() =
                evt.Trigger(i)
                i <- i + 1

            [<Js>]
            member this.FakeCall2() =
                evt2.Trigger(i2)
                i2 <-  i2 + 1

            [<Js>]
            member this.Evt with get() = evt.Publish

            [<Js>]
            member this.Evt2 with get() = evt2.Publish

        [<Js>]
        let Add() =
            let e = new EventTest()
            let n = ref 0
            e.Evt
            |> Observable.add(fun i ->
                Assert.AreEqual "Observable Add Test" n i
                n := !n + 1
            )
            e.FakeCall()
            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Map() =
            let e = new EventTest()
            e.Evt
            |> Observable.map(fun i -> (i, i+1))
            |> Observable.add(fun (k,l) ->
                Assert.AreEqual "Observable Map Test" k 0
                Assert.AreEqual "Observable Map Test" l 1
            )
            e.FakeCall()

        [<Js>]
        let Choose() =
            let e = new EventTest()
            e.Evt
            |> Observable.choose(fun i -> if i = 1 then Some(i, i+1) else None)
            |> Observable.add(fun (k,l) ->
                Assert.AreEqual "Observable Choose Test" k 1
                Assert.AreEqual "Observable Choose Test" l 2
            )
            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Filter() =
            let e = new EventTest()
            e.Evt
            |> Observable.filter(fun i -> if i = 1 then true else false)
            |> Observable.add(fun k ->
                Assert.AreEqual "Observable Filter Test" k 1)

            e.FakeCall()
            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Merge() =
            let e = new EventTest()
            e.Evt
            |> Observable.merge(e.Evt2)
            |> Observable.add(fun k ->
                Assert.AreEqual "Event Merge test" k 0
            )
            e.FakeCall()
            e.FakeCall2()

        [<Js>]
        let PairWise() =
            let e = new EventTest()
            e.Evt
            |> Observable.pairwise
            |> Observable.add(fun (k,l) ->
                Assert.AreEqual "Observable Pairwise Test" k 0
                Assert.AreEqual "Observable Pairwise Test" l 1
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Partition() =
            let e = new EventTest()
            let (e1, e2) = e.Evt |> Observable.partition(fun l -> l = 1)
            e1 |> Observable.add(fun l ->
                Assert.AreEqual "Observable partition test" l 1
            )
            e2 |> Observable.add(fun k ->
                Assert.AreEqual "Observable parition test" k 0
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let Scan() =
            let initialState = 0
            let i = ref 1 // initial state starts from (0+1)
            let e = new EventTest()
            e.Evt
            |> Observable.scan (fun state _ -> state + 1) initialState
            |> Observable.add(fun l ->
                Assert.AreEqual "Event Scan Test" i l
                i := !i + 1
            )

            e.FakeCall()
            e.FakeCall()

        [<Js>]
        let (|Odd|Even|) (i) =
            if (i % 2 = 0) then Odd(i)
            else Even(i)

        [<Js>]
        let Split() =
            let initialState = 0
            let e = new EventTest()
            let (OddResult, EvenResult) = Observable.split (|Odd|Even|) e.Evt

            OddResult |> Observable.add (fun k ->
                Assert.AreEqual "Event Split test" k  0 )

            EvenResult |> Observable.add (fun k ->
                Assert.AreEqual "Event Split test" k 1 )

            e.FakeCall()
            e.FakeCall()
