namespace Pit.Dom.Tests

    open System
    open Pit
    open Pit.Dom
    open Pit.Dom.Html5
    open HtmlModule

    module CommonTests =

        [<Js>]
        let DomArticleSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<article id='art'></article>"

        [<Js>]
        let DomArticleCheck() =
            let div  = document.GetElementById("art") |> DomArticle.Of
            Assert.IsNotNull "DomArticle check" div


        [<Js>]
        let DomAsideSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<aside id='asid'></aside>"

        [<Js>]
        let DomAsideCheck() =
            let div  = document.GetElementById("asid") |> DomAside.Of
            Assert.IsNotNull "Dom aside check" div

        [<Js>]
        let DomFigureCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<figure id ='fig'><img src='PIT.PNG' alt='Pit Smaple icon' width='304' height='228'/></figure>"
            let fig = document.GetElementById("fig") |> DomFigure.Of
            Assert.IsNotNull "Dom Figure check" fig


        [<Js>]
        let DomFigureCaptionCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<figure id ='fig'><img src='PIT.PNG' alt='Pit Smaple icon' width='304' height='228'/><figcaption id='figc'>Pit icon</figcaption></figure>"
            let fig = document.GetElementById("figc") |> DomFigCaption.Of
            Assert.IsNotNull "Dom Figure Caption check" fig


        [<Js>]
        let DomFooterCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<footer id='foot'>Copyright 1999-2050.</footer>"
            let fig = document.GetElementById("foot") |> DomFooter.Of
            Assert.AreEqual "Dom Footer check" fig.InnerHTML "Copyright 1999-2050."

        [<Js>]
        let DomHeaderCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<header id='head'></header>"
            let fig = document.GetElementById("head") |> DomHeader.Of
            Assert.IsNotNull "Dom Header check" fig


        [<Js>]
        let DomHGroupCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<hgroup id='hg'></hgroup>"
            let fig = document.GetElementById("hg") |> DomHGroup.Of
            Assert.IsNotNull "Dom HGroup check" fig

        [<Js>]
        let DomMarkCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<mark id='hg'>new</mark>"
            let fig = document.GetElementById("hg") |> DomMark.Of
            Assert.IsNotNull "Dom Mark check" fig

        [<Js>]
        let DomNavCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<nav id='hg'></nav>"
            let fig = document.GetElementById("hg") |> DomNav.Of
            Assert.IsNotNull "Dom Nav check" fig


        (*[<Js>]
        let DomProgressCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<progress id='hg' value='22' max='100'></progress>"
            let fig = document.GetElementById("hg") |> DomProgress.Of
            Assert.AreEqual "Dom Progress check" fig.Max 100
            Assert.AreEqual "Dom Progress check" fig.Value 22*)


        [<Js>]
        let DomRubyCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<ruby id='hg'></ruby>"
            let fig = document.GetElementById("hg") |> DomRuby.Of
            Assert.IsNotNull "Dom Ruby check" fig


        [<Js>]
        let DomRTCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<ruby>漢 <rt id='hg'> ㄏㄢˋ </rt></ruby>"
            let fig = document.GetElementById("hg") |> DomRT.Of
            Assert.IsNotNull "Dom RT check" fig


        [<Js>]
        let DomRPCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<ruby>漢 <rt><rp id='hg'>(</rp>ㄏㄢˋ<rp id='hg1'>)</rp></rt></ruby>"
            let fig = document.GetElementById("hg") |> DomRP.Of
            Assert.IsNotNull "Dom RP check" fig
            let fig1 = document.GetElementById("hg1") |> DomRP.Of
            Assert.AreEqual "Dom RP check" fig1.InnerHTML ")"

        [<Js>]
        let DomSectionCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<section id='hg'></section>"
            let fig = document.GetElementById("hg") |> DomSection.Of
            Assert.IsNotNull "Dom Section check" fig

        [<Js>]
        let DomEmbedCheck() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<embed id='hg' type='application/x-shockwave-flash' src='helloworld.swf' />"
            let fig = document.GetElementById("hg") |> DomEmbed.Of
            Assert.AreEqual "Dom Embed check" fig.Src "helloworld.swf"
            Assert.AreEqual "Dom Embed check" fig.Type "application/x-shockwave-flash"

