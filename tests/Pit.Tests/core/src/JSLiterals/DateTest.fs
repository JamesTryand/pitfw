#if DOM
namespace Pit.Dom.Tests
#endif
#if AST
namespace Pit.Test
#endif
open Pit
open Pit.Javascript

    module DateTest =

        [<Js>]
        let Create() =
            let d = new Date()
            let m = d.GetDate()
            Assert.IsNotNull "Date Create Month" m
            let d1 = d.GetDay()
            Assert.IsNotNull "Date Create Day" d1
            let mn = d.GetMonth()
            Assert.IsNotNull "Date Create Month" mn
            let y = d.GetFullYear()
            Assert.IsNotNull "Date Create Year" y
            let h = d.GetHours()
            Assert.IsNotNull "Date Create Hours" h
            let mi = d.GetMinutes()
            Assert.IsNotNull "Date Create Minutes" mi
            let sec = d.GetSeconds()
            Assert.IsNotNull "Date Create Seconds" sec
            let time = d.GetTime()
            Assert.IsNotNull "Date Create Time" time
            let gmt = -d.GetTimezoneOffset() / 60.
            Assert.IsNotNull "Date Timezone offset" gmt
            let ud = d.GetUTCDate()
            Assert.IsNotNull "UTC Create Date" ud
            let udy = d.GetUTCDay()
            Assert.IsNotNull "UTC Create Day" udy
            let udm = d.GetUTCMonth()
            Assert.IsNotNull "UTC Create Month" udm
            let udy1 = d.GetUTCFullYear()
            Assert.IsNotNull "UTC Create Year" udy1
            let udh = d.GetUTCHours()
            Assert.IsNotNull "UTC Create Hours" udh
            let udmn = d.GetUTCMinutes()
            Assert.IsNotNull "UTC Create Minutes" udmn
            let udsec = d.GetUTCSeconds()
            Assert.IsNotNull "UTC Create Seconds" udsec
            let udmil = d.GetUTCMilliseconds()
            Assert.IsNotNull "UTC Create Milliseconds" udmil

        [<Js>]
        let Parse() =
            let d = Date.Parse("Jul 8, 2005")
            Assert.AreEqual "Date Parse" d 1120761000000.0

        [<Js>]
        let UTC() =
            let d = Date.UTC()
            Assert.IsNotNull "Date UTC" d

        [<Js>]
        let TestAll() =
            Create()
            Parse()
            UTC()