namespace Pit

[<Alias("Array")>]
[<JsIgnore>]
type JsArray<'a>(array : 'a[]) =

    [<CompileTo("length")>]
    member x.Length
        with get() = 0

    [<CompileTo("concat")>]
    member x.Concat(other : JsArray<'a>) =
        x

    [<Js>]
    member x.Item
        with get(index:int) =
            let items : 'a[] = Array.zeroCreate(0)
            items.[0]
        and set (index:int) (value:'a) = ()

    [<CompileTo("pop")>]
    member x.Pop() =
        let items : 'a[] = Array.zeroCreate(0)
        items.[0]

    [<CompileTo("push")>]
    member x.Push(item : 'a) =
        0

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
    member x.Slice(startIdx : int, endIdx : int) =
        x

    [<CompileTo("sort")>]
    member x.Sort() =
        x

    [<CompileTo("sort")>]
    member x.Sort(fn : ('a * 'a) -> int) =
        x

    [<CompileTo("splice")>]
    member x.Splice(index : int, removeElements : int) =
        x

    [<CompileTo("splice")>]
    member x.Splice(index : int, removeElements : int, newItems : 'a[]) =
        x

    [<CompileTo("toString")>]
    override x.ToString() =
        ""