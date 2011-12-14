#if DOM
namespace Pit.Dom.Tests
#endif
#if AST
namespace Pit.Test
#endif
open Pit
open Pit.Javascript

    module MathTest =
        [<Js>]
        let PI() =
            let p1 = Math.PI
            Assert.IsNotNull "PI" p1

        [<Js>]
        let abs() =
            let a = Math.abs(-30.)
            Assert.AreEqual "Abs" 30. 30.

        [<Js>]
        let TestAll() =
            PI()
            abs()
