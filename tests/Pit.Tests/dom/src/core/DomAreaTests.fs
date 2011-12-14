namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomArea =
        [<Js>]
        let DomAreaSetup() =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<area id='venus'></area>"

        [<Js>]
        let area =
            let a = document.GetElementById("venus") |> DomArea.Of
            a

        [<Js>]
        let Alt() =
            area.Alt <- "Some"
            Assert.AreEqual "Alt test" area.Alt "Some"

        [<Js>]
        let Shape() =
            area.Shape <- "circle"
            Assert.AreEqual "Shape test" area.Shape "circle"

        [<Js>]
        let Coords() =
            area.Coords <- "124,58,8"
            Assert.AreEqual "Coords test" area.Coords "124,58,8"

//
//    member x.RectCoords
//        with get() =
//            match x.Shape with
//            | "rect" ->
//                let v = x.Coords.Split([|','|])
//                if v.Length = 4 then
//                    {X1 = float(v.[0]); X2 = float(v.[1]); Y1 = float(v.[2]); Y2 = float(v.[3])}
//                else
//                    Unchecked.defaultof<Rect>
//            | _ -> Unchecked.defaultof<Rect>
//
//    member x.CircleCoords
//        with get() =
//            match x.Shape with
//            | "circle" ->
//                let v = x.Coords.Split([|','|])
//                if v.Length = 2 then
//                    { X = float(v.[0]); Y = float(v.[1]); Radius = float(v.[2]) }
//                else
//                    Unchecked.defaultof<Circle>
//            | _ -> Unchecked.defaultof<Circle>
//
//    member x.PolyCoords
//        with get() =
//            match x.Shape with
//            | "poly" ->
//                let v = x.Coords.Split([|','|])
//                if v.Length = 2 then
//                    {X1 = float(v.[0]); X2 = float(v.[1])}
//                else
//                    Unchecked.defaultof<PolyLine>
//            | _ -> Unchecked.defaultof<PolyLine>

        [<Js>]
        let Hash() =
            area.Href <- "venus.htm#description"
            Assert.AreEqual "Hash test" area.Hash "#description"


        [<Js>]
        let Host() =
            Assert.IsNull "Host test" area.Host

        [<Js>]
        let HostName() =
            Assert.IsNull "HostName test" area.HostName

        [<Js>]
        let Href() =
            Assert.AreEqual "Href test" area.Href "venus.htm#description"


        [<Js>]
        let NoHref() =
            area.NoHref <- "nohref"
            Assert.AreEqual "NoHref test" area.NoHref "nohref"
//
//    member x.NoHref
//        with get() = area.GetProperty<bool>("noHref")
//        and set(v: bool) = area.SetProperty("noHref", v)
//
//    member x.PathName
//        with get() = area.GetProperty<string>("pathName")
//        and set(v: string) = area.SetProperty("pathname", v)
//
//    member x.Port
//        with get() = area.GetProperty<string>("port")
//        and set(v: string) = area.SetProperty("port", v)
//
//    member x.Protocol
//        with get() = area.GetProperty<string>("protocol")
//        and set(v: string) = area.SetProperty("protocol", v)
//
//    member x.Search
//        with get() = area.GetProperty<string>("search")
//        and set(v: string) = area.SetProperty("search", v)
//
//    member x.Target
//        with get() = area.GetProperty<string>("target")
//        and set(v: string) = area.SetProperty("target", v)
//
