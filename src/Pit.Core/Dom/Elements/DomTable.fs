namespace Pit.Dom

open System
open Pit

[<AllowNullLiteral;JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomTableData() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
        new DomTableData()

    [<CompileTo("abbr")>]
    member x.Abbr
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("axis")>]
    member x.Axis
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("cellIndex")>]
    member x.CellIndex
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("ch")>]
    member x.Ch
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("chOff")>]
    member x.ChOff
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("colSpan")>]
    member x.ColSpan
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("headers")>]
    member x.Headers
        with get() =
            let i : DomTableHeader[] = Array.zeroCreate 0
            i

    [<CompileTo("rowSpan")>]
    member x.RowSpan
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("vAlign")>]
    member x.VAlign
        with get() = ""
        and set(v: string) = ()

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    DomTableHeader() =
        inherit DomTableData()

        [<JsIgnore(IgnoreCall=true)>]
        static member Of(el:DomElement) =
            new DomTableHeader()


and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    DomTableRow() =
        inherit DomElement()

        [<JsIgnore(IgnoreCall=true)>]
        static member Of(el:DomElement) =
            new DomTableRow()

        [<CompileTo("ch")>]
        member x.Ch
            with get() = ""
            and set(v : string) = ()

        [<CompileTo("chOff")>]
        member x.ChOff
            with get() = ""
            and set(v: string) = ()

        [<CompileTo("rowIndex")>]
        member x.RowIndex
            with get() = 0
            and set(v: int) = ()

        [<CompileTo("sectionRowIndex")>]
        member x.SectionRowIndex
            with get() = 0
            and set(v: int) = ()

        [<CompileTo("vAlign")>]
        member x.VAlign
            with get() = ""
            and set(v: string) = ()

        [<CompileTo("deleteCell")>]
        member x.DeleteCell(index: int) =
            ()

        [<CompileTo("insertCell")>]
        member x.InsertCell(index : int) =
            ()

        [<CompileTo("cells")>]
        member x.Cells
            with get() =
                let i : DomTableData[] = Array.zeroCreate 0
                i

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    [<AllowNullLiteral>]
    DomTableCaption() =
        inherit DomElement()

        [<JsIgnore(IgnoreCall=true)>]
        static member Of(el:DomElement) =
            new DomTableCaption()


and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    [<AllowNullLiteral>]
    DomTableBody() =
        inherit DomElement()

        [<JsIgnore(IgnoreCall=true)>]
        static member Of(el:DomElement) =
            new DomTableBody()

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    [<AllowNullLiteral>]
    DomTableFoot() =
        inherit DomElement()

        [<JsIgnore(IgnoreCall=true)>]
        static member Of() =
            new DomTableFoot()

and
    [<JsIgnoreAttribute(IgnoreGetSet=true)>]
    [<AllowNullLiteral>]
    DomTableHead() =
        inherit DomElement()

        [<JsIgnore(IgnoreCall=true)>]
        static member Of() =
            new DomTableHead()

[<AllowNullLiteral;JsIgnoreAttribute(IgnoreGetSet=true)>]
type DomTable() =
    inherit DomElement()

    [<JsIgnore(IgnoreCall=true)>]
    static member Of(el:DomElement) =
            new DomTable()

    [<CompileTo("cells")>]
    member x.Cells
        with get() =
            let i : DomTableData[] = Array.zeroCreate 0
            i

    [<CompileTo("rows")>]
    member x.Rows
        with get() =
            let i : DomTableRow[] = Array.zeroCreate 0
            i

    [<CompileTo("tBodies")>]
    member x.TBodies
        with get() =
            let i : DomTableBody[] = Array.zeroCreate 0
            i

    [<CompileTo("caption")>]
    member x.Caption
          with get() = DomTableCaption()

    [<CompileTo("cellPadding")>]
    member x.CellPadding
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("cellSpacing")>]
    member x.CellSpacing
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("frame")>]
    member x.Frame
        with get() = DomFrame()

    [<CompileTo("rules")>]
    member x.Rules
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("summary")>]
    member x.Summary
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("tFoot")>]
    member x.TFoot
        with get() = Unchecked.defaultof<DomTableFoot>

    [<CompileTo("tHead")>]
    member x.THead
        with get() = Unchecked.defaultof<DomTableHead>

    [<CompileTo("createCaption")>]
    member x.CreateCaption() =
        DomTableCaption()

    [<CompileTo("createTFoot")>]
    member x.CreateTFoot() =
        DomTableFoot()

    [<CompileTo("createTHead")>]
    member x.CreateTHead() =
        DomTableHead()

    [<CompileTo("deleteCaption")>]
    member x.DeleteCaption() =
        ()

    [<CompileTo("deleteRow")>]
    member x.DeleteRow(index : int) =
        ()

    [<CompileTo("deleteTFoot")>]
    member x.DeleteTFoot() =
        ()

    [<CompileTo("deleteTHead")>]
    member x.DeleteTHead() =
        ()

    [<CompileTo("insertRow")>]
    member x.InsertRow(index : int) =
        ()