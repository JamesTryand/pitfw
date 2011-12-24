namespace Pit.Javascript
open Pit

    type JsArraySortDelegate = delegate of int * int -> int

    [<AbstractClass>]
    [<JsIgnore(IgnoreCtor=true,IgnoreNamespace=true,IgnoreGetSet=true,IgnoreItemAccess=true)>]
    [<AllowNullLiteral>]
    type JsArrayBase<'a>() =
        [<CompileTo("length")>]
        member x.Length
            with get() = 0

        [<CompileTo("concat")>]
        member x.Concat(other : JsArrayBase<'a>) =
            x

        member x.Item
            with get(index:int) =
                let items : 'a[] = Array.zeroCreate(0)
                items.[0]
            and set (index:int) (value:'a) = ()

        member x.Item
            with get(key:string) =
                let items : 'a[] = Array.zeroCreate(0)
                items.[0]
            and set (key:string) (value:'a) = ()

        [<CompileTo("join")>]
        member x.JoinAll() = ""

        [<CompileTo("join")>]
        member x.JoinAll(separator:string) = ""

        [<CompileTo("pop")>]
        member x.Pop() =
            let items : 'a[] = Array.zeroCreate(0)
            items.[0]

        [<CompileTo("push")>]
        member x.Push(item : 'a) =
            ()

        [<CompileTo("push")>]
        member x.Push(items : 'a[]) =
            0

        [<CompileTo("reverse")>]
        member x.Reverse() =
            x

        [<CompileTo("shift")>]
        member x.Shift() =
            let items : 'a[] = Array.zeroCreate(0)
            items.[0]

        [<CompileTo("slice")>]
        member x.Slice(startIdx : int) =
            x

        [<CompileTo("slice")>]
        member x.Slice(startIdx : int, length : int) =
            x

        [<CompileTo("sort")>]
        member x.Sort() =
            x

        [<CompileTo("remove")>] // prototype function implemented in pit.js for events
        member x.Remove(s) =
            ()

    //    [<CompileTo("sortInPlace")>]
    //    member x.SortInPlace() =
    //        ()
    //
    //    [<CompileTo("sortInPlaceBy")>]
    //    member x.SortInPlaceBy(f:'a->'a)=
    //        ()
    //
    //    [<CompileTo("sortInPlaceWith")>]
    //    member x.SortInPlaceWith(f:'a->'a->int)=
    //        ()

        [<CompileTo("sort");JsIgnore(IgnoreTuple=true)>] // F# considers a tuple as a single object passed to the function, for these kind of methods, have a wrapper in JS and use that to work
        member x.Sort(fn : JsArraySortDelegate) =
            ()

        [<CompileTo("splice")>]
        member x.Splice(index : int, removeElements : int, item:'a) =
            ()

        [<CompileTo("unshift")>]
        member x.Unshift(item:'a) = 0

        [<CompileTo("toString")>]
        override x.ToString() =
            ""

    [<Alias("Array")>]
    [<JsIgnore(IgnoreCtor=true,IgnoreNamespace=true,IgnoreGetSet=true,IgnoreItemAccess=true)>]
    [<AllowNullLiteral>]
    type JsArray<'a>(array : 'a[]) =
        inherit JsArrayBase<'a>()

    [<Alias("Array")>]
    [<JsIgnore(IgnoreCtor=true,IgnoreNamespace=true,IgnoreGetSet=true,IgnoreItemAccess=true)>]
    [<AllowNullLiteral>]
    type JsArray2D<'a>(array : 'a[,]) =
        inherit JsArrayBase<'a>()