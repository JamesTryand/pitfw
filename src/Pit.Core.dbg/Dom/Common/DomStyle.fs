namespace Pit.Dom

open System
open System.Windows.Browser
open Pit

[<AllowNullLiteral>]
type DomStyle =
    val style: ScriptObject

    internal new (_style: ScriptObject) = {
        style=_style
    }

    member x.Background
        with get() = x.style.GetProperty<string>("background")
        and set(v: string) = x.style.SetProperty("background", v)

    member x.BackgroundAttachment
        with get() = x.style.GetProperty<string>("backgroundAttachment")
        and set(v: string) = x.style.SetProperty("backgroundAttachment", v)

    member x.BackgroundColor
        with get() = x.style.GetProperty<string>("backgroundColor")
        and set(v: string) = x.style.SetProperty("backgroundColor", v)

    member x.BackgroundImage
        with get() = x.style.GetProperty<string>("backgroundImage")
        and set(v: string) = x.style.SetProperty("backgroundImage", v)

    member x.BackgroundPosition
        with get() = x.style.GetProperty<string>("backgroundPosition")
        and set(v: string) = x.style.SetProperty("backgroundPosition", v)

    member x.BackgroundRepeat
        with get() = x.style.GetProperty<string>("backgroundRepeat")
        and set(v: string) = x.style.SetProperty("backgroundRepeat", v)

    member x.Border
        with get() = x.style.GetProperty<string>("border")
        and set(v: string) = x.style.SetProperty("border", v)

    member x.BorderBottom
        with get() = x.style.GetProperty<string>("borderBottom")
        and set(v: string) = x.style.SetProperty("borderBottom", v)

    member x.BorderBottomColor
        with get() = x.style.GetProperty<string>("borderBottomColor")
        and set(v: string) = x.style.SetProperty("borderBottomColor", v)

    member x.BorderBottomStyle
        with get() = x.style.GetProperty<string>("borderBottomStyle")
        and set(v: string) = x.style.SetProperty("borderBottomStyle", v)

    member x.BorderBottomWidth
        with get() = x.style.GetProperty<string>("borderBottomWidth")
        and set(v: string) = x.style.SetProperty("borderBottomWidth", v)

    member x.BorderColor
        with get() = x.style.GetProperty<string>("borderColor")
        and set(v: string) = x.style.SetProperty("borderColor", v)

    member x.BorderLeft
        with get() = x.style.GetProperty<string>("borderLeft")
        and set(v: string) = x.style.SetProperty("borderLeft", v)

    member x.BorderLeftColor
        with get() = x.style.GetProperty<string>("borderLeftColor")
        and set(v: string) = x.style.SetProperty("borderLeftColor", v)

    member x.BorderLeftStyle
        with get() = x.style.GetProperty<string>("borderLeftStyle")
        and set(v: string) = x.style.SetProperty("borderLeftStyle", v)

    member x.BorderLeftWidth
        with get() = x.style.GetProperty<string>("borderLeftWidth")
        and set(v: string) = x.style.SetProperty("borderLeftWidth", v)

    member x.BorderRight
        with get() = x.style.GetProperty<string>("borderRight")
        and set(v: string) = x.style.SetProperty("borderRight", v)

    member x.BorderRightColor
        with get() = x.style.GetProperty<string>("borderRightColor")
        and set(v: string) = x.style.SetProperty("borderRightColor", v)

    member x.BorderRightStyle
        with get() = x.style.GetProperty<string>("borderRightStyle")
        and set(v: string) = x.style.SetProperty("borderRightStyle", v)

    member x.BorderRightWidth
        with get() = x.style.GetProperty<string>("borderRightWidth")
        and set(v: string) = x.style.SetProperty("borderRightWidth", v)

    member x.BorderStyle
        with get() = x.style.GetProperty<string>("borderStyle")
        and set(v: string) = x.style.SetProperty("borderStyle", v)

    member x.BorderTop
        with get() = x.style.GetProperty<string>("borderTop")
        and set(v: string) = x.style.SetProperty("borderTop", v)

    member x.BorderTopColor
        with get() = x.style.GetProperty<string>("borderTopColor")
        and set(v: string) = x.style.SetProperty("borderTopColor", v)

    member x.BorderTopStyle
        with get() = x.style.GetProperty<string>("borderTopStyle")
        and set(v: string) = x.style.SetProperty("borderTopStyle", v)

    member x.BorderTopWidth
        with get() = x.style.GetProperty<string>("borderTopWidth")
        and set(v: string) = x.style.SetProperty("borderTopWidth", v)

    member x.BorderWidth
        with get() = x.style.GetProperty<string>("borderWidth")
        and set(v: string) = x.style.SetProperty("borderWidth", v)

    member x.Outline
        with get() = x.style.GetProperty<string>("outline")
        and set(v: string) = x.style.SetProperty("outline", v)

    member x.OutlineColor
        with get() = x.style.GetProperty<string>("outlineColor")
        and set(v: string) = x.style.SetProperty("outlineColor", v)

    member x.OutlineStyle
        with get() = x.style.GetProperty<string>("outlineStyle")
        and set(v: string) = x.style.SetProperty("outlineStyle", v)

    member x.OutlineWidth
        with get() = x.style.GetProperty<string>("outlineWidth")
        and set(v: string) = x.style.SetProperty("outlineWidth", v)

    member x.ListStyle
        with get() = x.style.GetProperty<string>("listStyle")
        and set(v: string) = x.style.SetProperty("listStyle", v)

    member x.ListStyleImage
        with get() = x.style.GetProperty<string>("listStyleImage")
        and set(v: string) = x.style.SetProperty("listStyleImage", v)

    member x.ListStylePosition
        with get() = x.style.GetProperty<string>("listStylePosition")
        and set(v: string) = x.style.SetProperty("listStylePosition", v)

    member x.ListStyleType
        with get() = x.style.GetProperty<string>("listStyleType")
        and set(v: string) = x.style.SetProperty("listStyleType", v)

    member x.Margin
        with get() = x.style.GetProperty<string>("margin")
        and set(v: string) = x.style.SetProperty("margin", v)

    member x.MarginBottom
        with get() = x.style.GetProperty<string>("marginBottom")
        and set(v: string) = x.style.SetProperty("marginBottom", v)

    member x.MarginLeft
        with get() = x.style.GetProperty<string>("marginLeft")
        and set(v: string) = x.style.SetProperty("marginLeft", v)

    member x.MarginRight
        with get() = x.style.GetProperty<string>("marginRight")
        and set(v: string) = x.style.SetProperty("marginRight", v)

    member x.MarginTop
        with get() = x.style.GetProperty<string>("marginTop")
        and set(v: string) = x.style.SetProperty("marginTop", v)

    member x.Padding
        with get() = x.style.GetProperty<string>("padding")
        and set(v: string) = x.style.SetProperty("padding", v)

    member x.PaddingBottom
        with get() = x.style.GetProperty<string>("paddingBottom")
        and set(v: string) = x.style.SetProperty("paddingBottom", v)

    member x.PaddingLeft
        with get() = x.style.GetProperty<string>("paddingLeft")
        and set(v: string) = x.style.SetProperty("paddingLeft", v)

    member x.PaddingRight
        with get() = x.style.GetProperty<string>("paddingRight")
        and set(v: string) = x.style.SetProperty("paddingRight", v)

    member x.PaddingTop
        with get() = x.style.GetProperty<string>("paddingTop")
        and set(v: string) = x.style.SetProperty("paddingTop", v)

    member x.Bottom
        with get() = x.style.GetProperty<string>("bottom")
        and set(v: string) = x.style.SetProperty("bottom", v)

    member x.Clear
        with get() = x.style.GetProperty<string>("clear")
        and set(v: string) = x.style.SetProperty("clear", v)

    member x.Clip
        with get() = x.style.GetProperty<string>("clip")
        and set(v: string) = x.style.SetProperty("clip", v)

    member x.CssFloat
        with get() = x.style.GetProperty<string>("cssFloat")
        and set(v: string) = x.style.SetProperty("cssFloat", v)

    member x.Cursor
        with get() = x.style.GetProperty<string>("cursor")
        and set(v: string) = x.style.SetProperty("cursor", v)

    member x.Display
        with get() = x.style.GetProperty<string>("display")
        and set(v: string) = x.style.SetProperty("display", v)

    member x.Height
        with get() = x.style.GetProperty<string>("height")
        and set(v: string) = x.style.SetProperty("height", v)

    member x.Left
        with get() = x.style.GetProperty<string>("left")
        and set(v: string) = x.style.SetProperty("left", v)

    member x.MaxHeight
        with get() = x.style.GetProperty<string>("maxHeight")
        and set(v: string) = x.style.SetProperty("maxHeight", v)

    member x.MaxWidth
        with get() = x.style.GetProperty<string>("maxWidth")
        and set(v: string) = x.style.SetProperty("maxWidth", v)

    member x.MinHeight
        with get() = x.style.GetProperty<string>("minHeight")
        and set(v: string) = x.style.SetProperty("minHeight", v)

    member x.MinWidth
        with get() = x.style.GetProperty<string>("minWidth")
        and set(v: string) = x.style.SetProperty("minWidth", v)

    member x.Overflow
        with get() = x.style.GetProperty<string>("overflow")
        and set(v: string) = x.style.SetProperty("overflow", v)

    member x.Position
        with get() = x.style.GetProperty<string>("position")
        and set(v: string) = x.style.SetProperty("position", v)

    member x.Right
        with get() = x.style.GetProperty<string>("right")
        and set(v: string) = x.style.SetProperty("right", v)

    member x.Top
        with get() = x.style.GetProperty<string>("top")
        and set(v: string) = x.style.SetProperty("top", v)

    member x.VerticalAlign
        with get() = x.style.GetProperty<string>("verticalAlign")
        and set(v: string) = x.style.SetProperty("verticalAlign", v)

    member x.Visibility
        with get() = x.style.GetProperty<string>("visibility")
        and set(v: string) = x.style.SetProperty("visibility", v)

    member x.Width
        with get() = x.style.GetProperty<string>("width")
        and set(v: string) = x.style.SetProperty("width", v)

    member x.ZIndex
        with get() = x.style.GetProperty<float>("zIndex") |> int
        and set(v: int) = x.style.SetProperty("zIndex", v)

    member x.BorderCollapse
        with get() = x.style.GetProperty<string>("borderCollapse")
        and set(v: string) = x.style.SetProperty("borderCollapse", v)

    member x.BorderSpacing
        with get() = x.style.GetProperty<string>("borderSpacing")
        and set(v: string) = x.style.SetProperty("borderSpacing", v)

    member x.CaptionSide
        with get() = x.style.GetProperty<string>("captionSide")
        and set(v: string) = x.style.SetProperty("captionSide", v)

    member x.EmptyCells
        with get() = x.style.GetProperty<string>("emptyCells")
        and set(v: string) = x.style.SetProperty("emptyCells", v)

    member x.TableLayout
        with get() = x.style.GetProperty<string>("tableLayout")
        and set(v: string) = x.style.SetProperty("tableLayout", v)

    member x.Color
        with get() = x.style.GetProperty<string>("color")
        and set(v: string) = x.style.SetProperty("color", v)

    member x.Direction
        with get() = x.style.GetProperty<string>("direction")
        and set(v: string) = x.style.SetProperty("direction", v)

    member x.Font
        with get() = x.style.GetProperty<string>("font")
        and set(v: string) = x.style.SetProperty("font", v)

    member x.FontFamily
        with get() = x.style.GetProperty<string>("fontFamily")
        and set(v: string) = x.style.SetProperty("fontFamily", v)

    member x.FontSize
        with get() = x.style.GetProperty<string>("fontSize")
        and set(v: string) = x.style.SetProperty("fontSize", v)

    member x.FontSizeAdjust
        with get() = x.style.GetProperty<string>("fontSizeAdjust")
        and set(v: string) = x.style.SetProperty("fontSizeAdjust", v)

    member x.FontStyle
        with get() = x.style.GetProperty<string>("fontStyle")
        and set(v: string) = x.style.SetProperty("fontStyle", v)

    member x.FontVariant
        with get() = x.style.GetProperty<string>("fontVariant")
        and set(v: string) = x.style.SetProperty("fontVariant", v)

    member x.FontWeight
        with get() = x.style.GetProperty<string>("fontWeight")
        and set(v: string) = x.style.SetProperty("fontWeight", v)

    member x.LetterSpacing
        with get() = x.style.GetProperty<string>("letterSpacing")
        and set(v: string) = x.style.SetProperty("letterSpacing", v)

    member x.LineHeight
        with get() = x.style.GetProperty<string>("lineHeight")
        and set(v: string) = x.style.SetProperty("lineHeight", v)

    member x.Quotes
        with get() = x.style.GetProperty<string>("quotes")
        and set(v: string) = x.style.SetProperty("quotes", v)

    member x.TextAlign
        with get() = x.style.GetProperty<string>("textAlign")
        and set(v: string) = x.style.SetProperty("textAlign", v)

    member x.TextDecoration
        with get() = x.style.GetProperty<string>("textDecoration")
        and set(v: string) = x.style.SetProperty("textDecoration", v)

    member x.TextIndent
        with get() = x.style.GetProperty<string>("textIndent")
        and set(v: string) = x.style.SetProperty("textIndent", v)

    member x.TextShadow
        with get() = x.style.GetProperty<string>("textShadow")
        and set(v: string) = x.style.SetProperty("textShadow", v)

    member x.TextTransform
        with get() = x.style.GetProperty<string>("textTransform")
        and set(v: string) = x.style.SetProperty("textTransform", v)

    member x.UnicodeBidi
        with get() = x.style.GetProperty<string>("unicodeBidi")
        and set(v: string) = x.style.SetProperty("unicodeBidi", v)

    member x.WhiteSpace
        with get() = x.style.GetProperty<string>("whiteSpace")
        and set(v: string) = x.style.SetProperty("whiteSpace", v)

    member x.WordSpacing
        with get() = x.style.GetProperty<string>("wordSpacing")
        and set(v: string) = x.style.SetProperty("wordSpacing", v)