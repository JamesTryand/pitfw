#if DOM
namespace Pit.Dom.Tests
#endif
#if AST
namespace Pit.Test
#endif
open Pit
open Pit.Javascript

    module RegexTest =
        [<Js>]
        let Create() =
            let str = "Every man in the world! Every woman on earth!"
            let r = new RegExp("man", "g")
            let jsStr = new JsString(str)
            let str2 = jsStr.Replace(r, "person")
            Assert.AreEqual "Regex Create" str2 "Every person in the world! Every woperson on earth!"