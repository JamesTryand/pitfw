namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomImage =

        [<Js>]
        let DomImageSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<img id='img1'></img>"

        [<Js>]
        let Img =
            let img = document.GetElementById("img1") |> DomImage.Of
            img

        [<Js>]
        let Align() =
            Img.Align <- "left"
            Assert.AreEqual "Align Test" Img.Align "left"

        [<Js>]
        let Alt() =
            Img.Alt <- "Alt"
            Assert.AreEqual "Alt Test" Img.Alt "Alt"
//
//        [<Js>]
//        let Border() =
//            Img.Border <- SizeUnit.Initialize("5px")
//            Assert.AreEqual "Image border" Img.Border.UnitType UnitType.Pixel


//
//    member x.Complete
//        with get() = image.GetProperty<bool>("complete")
//        and set(v: bool) = image.SetProperty("complete", v)
//
//    member x.Height
//        with get() = image.GetProperty<string>("height") |> SizeUnit.Initialize
//        and set(v: SizeUnit) = image.SetProperty("height", v.ToString())
//
//    member x.HSpace
//        with get() = image.GetProperty<string>("hspace") |> SizeUnit.Initialize
//        and set(v: SizeUnit) = image.SetProperty("hspace", v.ToString())
//
//    member x.LongDesc
//        with get() = image.GetProperty<string>("longDesc")
//        and set(v: string) = image.SetProperty("longDesc", v)
//
//    member x.LowSrc
//        with get() = image.GetProperty<string>("lowsrc")
//        and set(v: string) = image.SetProperty("lowsrc", v)
//
//    member x.Name
//        with get() = image.GetProperty<string>("name")
//        and set(v: string) = image.SetProperty("name", v)
//
//    member x.Src
//        with get() = image.GetProperty<string>("src")
//        and set(v: string) = image.SetProperty("src", v)
//
//    member x.UseMap
//        with get() = image.GetProperty<string>("useMap")
//        and set(v: string) = image.SetProperty("useMap", v)
//
//    member x.VSpace
//        with get() = image.GetProperty<string>("vspace") |> SizeUnit.Initialize
//        and set(v: SizeUnit) = image.SetProperty("vspace", v.ToString())
//
//    member x.Width
//        with get() = image.GetProperty<string>("width") |> SizeUnit.Initialize
//        and set(v: SizeUnit) = image.SetProperty("width", v.ToString())
//
//    member x.OnAbort(scriptMethod: string) =
//        image.Invoke("onabort", [| box(scriptMethod) |]) |> ignore
//
//    member x.OnError(scriptMethod: string) =
//        image.Invoke("onerror", [| box(scriptMethod) |]) |> ignore
//
//    member x.OnLoad(scriptMethod: string) =
//        image.Invoke("onload", [| box(scriptMethod) |]) |> ignore