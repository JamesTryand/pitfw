namespace Pit.Test
open Pit

    module OverloadedCtorsTests =

        type t =
            val x   : int
            val msg : string

            [<Js>] new()   = {x=0;msg=""}
            [<Js>] new(x)  = {x=x;msg=""}
            [<Js>] new(x,msg) = {x=x;msg=msg}

        [<Js>]
        let TestRecordLikeCtor() =
            let v = new t()
            Assert.AreEqual "Record Like Ctor Exp1" v.x 0
            let v1 = new t(10)
            Assert.AreEqual "Record Like Ctor Exp2" v1.x 10
            let v2 = new t(20, "Hello World")
            Assert.AreEqual "Record Like Ctor Exp3" v2.x 20
            Assert.AreEqual "Record Like Ctor Exp3" v2.msg "Hello World"

        type t1 [<Js>](x, msg) =
            let xval    = x
            let msgval  = msg

            [<Js>] member this.XVal = xval
            [<Js>] member this.MsgVal = msgval

            [<Js>] new() = new t1(0,"")

        [<Js>]
        let TestNormalTypes() =
            let v  = new t1(10, "Hello World")
            Assert.AreEqual "Normal Type Ctor Exp1" v.XVal 10
            Assert.AreEqual "Normal Type Ctor Exp1" v.MsgVal "Hello World"
            let v1 = new t1()
            Assert.AreEqual "Normal Type Ctor Exp2" v1.XVal 0