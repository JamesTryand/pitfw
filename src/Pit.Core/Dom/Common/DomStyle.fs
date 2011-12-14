namespace Pit.Dom
open Pit


[<AllowNullLiteral;JsIgnore(IgnoreGetSet=true)>]
type DomStyle() =

    [<CompileTo("background")>]
    member x.Background
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("backgroundAttachment")>]
    member x.BackgroundAttachment
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("backgroundColor")>]
    member x.BackgroundColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("backgroundImage")>]
    member x.BackgroundImage
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("backgroundPosition")>]
    member x.BackgroundPosition
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("backgroundRepeat")>]
    member x.BackgroundRepeat
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("border")>]
    member x.Border
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderBottom")>]
    member x.BorderBottom
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderBottomColor")>]
    member x.BorderBottomColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderBottomStyle")>]
    member x.BorderBottomStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderBottomWidth")>]
    member x.BorderBottomWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderColor")>]
    member x.BorderColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderLeft")>]
    member x.BorderLeft
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderLeftColor")>]
    member x.BorderLeftColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderLeftStyle")>]
    member x.BorderLeftStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderLeftWidth")>]
    member x.BorderLeftWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderRight")>]
    member x.BorderRight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderRightColor")>]
    member x.BorderRightColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderRightStyle")>]
    member x.BorderRightStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderRightWidth")>]
    member x.BorderRightWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderStyle")>]
    member x.BorderStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderTop")>]
    member x.BorderTop
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderTopColor")>]
    member x.BorderTopColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderTopStyle")>]
    member x.BorderTopStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderTopWidth")>]
    member x.BorderTopWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderWidth")>]
    member x.BorderWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("outline")>]
    member x.Outline
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("outlineColor")>]
    member x.OutlineColor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("outlineStyle")>]
    member x.OutlineStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("outlineWidth")>]
    member x.OutlineWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("listStyle")>]
    member x.ListStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("listStyleImage")>]
    member x.ListStyleImage
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("listStylePosition")>]
    member x.ListStylePosition
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("listStyleType")>]
    member x.ListStyleType
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("margin")>]
    member x.Margin
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("marginBottom")>]
    member x.MarginBottom
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("marginLeft")>]
    member x.MarginLeft
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("marginRight")>]
    member x.MarginRight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("marginTop")>]
    member x.MarginTop
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("padding")>]
    member x.Padding
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("paddingBottom")>]
    member x.PaddingBottom
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("paddingLeft")>]
    member x.PaddingLeft
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("paddingRight")>]
    member x.PaddingRight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("paddingTop")>]
    member x.PaddingTop
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("bottom")>]
    member x.Bottom
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("clear")>]
    member x.Clear
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("clip")>]
    member x.Clip
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("cssFloat")>]
    member x.CssFloat
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("cursor")>]
    member x.Cursor
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("display")>]
    member x.Display
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("height")>]
    member x.Height
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("left")>]
    member x.Left
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("maxHeight")>]
    member x.MaxHeight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("maxWidth")>]
    member x.MaxWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("minHeight")>]
    member x.MinHeight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("minWidth")>]
    member x.MinWidth
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("overflow")>]
    member x.Overflow
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("position")>]
    member x.Position
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("right")>]
    member x.Right
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("top")>]
    member x.Top
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("verticalAlign")>]
    member x.VerticalAlign
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("visibility")>]
    member x.Visibility
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("width")>]
    member x.Width
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("zIndex")>]
    member x.ZIndex
        with get() = 0
        and set(v: int) = ()

    [<CompileTo("borderCollapse")>]
    member x.BorderCollapse
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("borderSpacing")>]
    member x.BorderSpacing
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("captionSide")>]
    member x.CaptionSide
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("emptyCells")>]
    member x.EmptyCells
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("tableLayout")>]
    member x.TableLayout
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("color")>]
    member x.Color
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("direction")>]
    member x.Direction
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("font")>]
    member x.Font
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fontFamily")>]
    member x.FontFamily
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fontSize")>]
    member x.FontSize
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fontSizeAdjust")>]
    member x.FontSizeAdjust
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fontStyle")>]
    member x.FontStyle
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fontVariant")>]
    member x.FontVariant
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("fontWeight")>]
    member x.FontWeight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("letterSpacing")>]
    member x.LetterSpacing
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("lineHeight")>]
    member x.LineHeight
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("quotes")>]
    member x.Quotes
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textAlign")>]
    member x.TextAlign
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textDecoration")>]
    member x.TextDecoration
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textIndent")>]
    member x.TextIndent
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textShadow")>]
    member x.TextShadow
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("textTransform")>]
    member x.TextTransform
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("unicodeBidi")>]
    member x.UnicodeBidi
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("whiteSpace")>]
    member x.WhiteSpace
        with get() = ""
        and set(v: string) = ()

    [<CompileTo("wordSpacing")>]
    member x.WordSpacing
        with get() = ""
        and set(v: string) = ()