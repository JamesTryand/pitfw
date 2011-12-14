namespace Pit.Dom

open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomTableData =
    inherit DomElement
    val td : ScriptObject

    internal new (td) =
        { inherit DomElement(td); td = td }

    static member Of(el:DomElement) =
        new DomTableData(el.InternalScriptObject)

    member x.Abbr
        with get() = x.td.GetProperty<string>("abbr")
        and set(v: string) = x.td.SetProperty("abbr", v)

    member x.Axis
        with get() = x.td.GetProperty<string>("axis")
        and set(v: string) = x.td.SetProperty("axis", v)

    member x.CellIndex
        with get() = x.td.GetProperty<float>("cellIndex") |> int
        and set(v: int) = x.td.SetProperty("cellIndex", v)

    member x.Ch
        with get() = x.td.GetProperty<string>("ch")
        and set(v: string) = x.td.SetProperty("ch", v)

    member x.ChOff
        with get() = x.td.GetProperty<string>("chOff")
        and set(v: string) = x.td.SetProperty("chOff", v)

    member x.ColSpan
        with get() = x.td.GetProperty<float>("colSpan") |> int
        and set(v: int) = x.td.SetProperty("colSpan", v)

    member x.Headers
        with get() =
            let headers = x.td.GetProperty<ScriptObjectCollection>("headers")
            headers |> Seq.map(fun h -> new DomTableHeader(h)) |> Seq.toArray

    member x.RowSpan
        with get() = x.td.GetProperty<float>("rowSpan") |> int
        and set(v: int) = x.td.SetProperty("rowSpan", v)

    member x.VAlign
        with get() =
            x.td.GetProperty<string>("vAlign")
        and set(v) = x.td.SetProperty("vAlign", v.ToString().ToLower())

and DomTableHeader =
    inherit DomTableData
    val th: ScriptObject

    internal new (th) =
        { inherit DomTableData(th); th = th }

    static member Of(el:DomElement) =
        new DomTableHeader(el.InternalScriptObject)

and DomTableRow =
    inherit DomElement
    val tr: ScriptObject

    internal new (tr) =
        { inherit DomElement(tr); tr=tr}

    static member Of(el:DomElement) =
        new DomTableRow(el.InternalScriptObject)

    member x.Ch
        with get() = x.tr.GetProperty<string>("ch")
        and set(v: string) = x.tr.SetProperty("ch", v)

    member x.ChOff
        with get() = x.tr.GetProperty<string>("chOff")
        and set(v: string) = x.tr.SetProperty("chOff", v)

    member x.RowIndex
        with get() = x.tr.GetProperty<float>("rowIndex") |> int
        and set(v: int) = x.tr.SetProperty("rowIndex", v)

    member x.SectionRowIndex
        with get() = x.tr.GetProperty<float>("sectionRowIndex") |> int
        and set(v: int) = x.tr.SetProperty("sectionRowIndex", v)

    member x.VAlign
        with get() =
            x.tr.GetProperty<string>("vAlign")
        and set(v) = x.tr.SetProperty("vAlign", v.ToString().ToLower())

    member x.DeleteCell(index: int) =
        x.tr.Invoke("deleteCell", [| box(index) |]) |> ignore

    member x.InsertCell(index: int) =
        x.tr.Invoke("insertCell", [| box(index) |]) |> ignore
        
    member x.Cells
        with get() =
            let items = x.tr.GetProperty<ScriptObjectCollection>("cells")
            items |> Seq.map(fun i -> new DomTableData(i)) |> Seq.toArray

and
    [<AllowNullLiteral>]
    DomTableCaption =
        inherit DomElement
        val tc: ScriptObject
        internal new (tc) =
            { inherit DomElement(tc); tc=tc }

        static member Of(el: DomElement) =
            new DomTableCaption(el.InternalScriptObject)

and
    [<AllowNullLiteral>]
    DomTableBody =
        inherit DomElement
        val tb: ScriptObject

        internal new(tb) =
            { inherit DomElement(tb); tb=tb }

        static member Of(el: DomElement) =
            new DomTableBody(el.InternalScriptObject)

and
    [<AllowNullLiteral>]
    DomTableFoot =
        inherit DomElement
        val tf: ScriptObject

        internal new(tf) =
            { inherit DomElement(tf); tf=tf }

        static member Of(el: DomElement) =
            new DomTableFoot(el.InternalScriptObject)

and
    [<AllowNullLiteral>]
    DomTableHead =
        inherit DomElement
        val th: ScriptObject

        internal new(th) =
            { inherit DomElement(th); th=th }

        static member Of(el: DomElement) =
            new DomTableHead(el.InternalScriptObject)

[<AllowNullLiteral>]
type DomTable =
    inherit DomElement
    val tb: ScriptObject

    internal new (tb) =
        { inherit DomElement(tb);tb=tb }

    static member Of(el:DomElement) =
        new DomTable(el.InternalScriptObject)

    member x.Cells
        with get() =
            let items = x.tb.GetProperty<ScriptObjectCollection>("cells")
            items |> Seq.map(fun i -> new DomTableData(i)) |> Seq.toArray

    member x.Rows
        with get() =
            let items = x.tb.GetProperty<ScriptObjectCollection>("rows")
            items |> Seq.map(fun i -> new DomTableRow(i)) |> Seq.toArray

    member x.TBodies
        with get() =
            let items = x.tb.GetProperty<ScriptObjectCollection>("tBodies")
            items |> Seq.map(fun i -> new DomTableBody(i)) |> Seq.toArray

    member x.Caption
        with get() =
            let tc = x.tb.GetProperty<ScriptObject>("caption")
            new DomTableCaption(tc)

    member x.CellPadding
        with get() = x.tb.GetProperty<string>("cellPadding")
        and set(v: string) = x.tb.SetProperty("cellPadding", v.ToString())

    member x.CellSpacing
        with get() = x.tb.GetProperty<string>("cellSpacing")
        and set(v: string) = x.tb.SetProperty("cellSpacing", v.ToString())

    member x.Frame
        with get() =
            let f = x.tb.GetProperty<ScriptObject>("frame")
            new DomFrame(f)

    member x.Rules
        with get() = x.tb.GetProperty<string>("rules")
        and set(v: string) = x.tb.SetProperty("rules", v)

    member x.Summary
        with get() = x.tb.GetProperty<string>("summary")
        and set(v: string) = x.tb.SetProperty("summary", v)

    member x.TFoot
        with get() =
            let tf = x.tb.GetProperty<ScriptObject>("tFoot")
            new DomTableFoot(tf)

    member x.THead
        with get() =
            let th = x.tb.GetProperty<ScriptObject>("tHead")
            new DomTableHead(th)

    member x.CreateCaption() =
        let c = x.tb.Invoke("createCaption", [||]) :?> ScriptObject
        new DomTableCaption(c)

    member x.CreateTFoot() =
        let tf = x.tb.Invoke("createTFoot", [||]) :?> ScriptObject
        new DomTableFoot(tf)

    member x.CreateTHead() =
        let th = x.tb.Invoke("createTHead", [||]) :?> ScriptObject
        new DomTableHead(th)

    member x.DeleteCaption() =
        x.tb.Invoke("deleteCaption", [||]) |> ignore

    member x.DeleteRow(index: int) =
        x.tb.Invoke("deleteRow", [| box(index) |]) |> ignore

    member x.DeleteTFoot() =
        x.tb.Invoke("deleteTFoot", [||]) |> ignore

    member x.DeleteTHead() =
        x.tb.Invoke("deleteTHead", [||]) |> ignore

    member x.InsertRow(index: int) =
        x.tb.Invoke("insertRow", [| box(index) |]) |> ignore