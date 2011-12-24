namespace Pit.Test
open Pit

module RecordsTests =

    type MyRecord = {
        X: int;
        Y: int;
        Z: int
        }

    [<Js>]
    let RecordDeclare() =
        let myRecord1 = { X = 1; Y = 2; Z = 3; }
        Assert.AreEqual "Record Declare 1" myRecord1.X 1
        Assert.AreEqual "Record Declare 1" myRecord1.Y 2


    [<Js>]
    let RecordDeclare2() =
        let myRecord2 = { MyRecord.X = 1; MyRecord.Y = 2; MyRecord.Z = 3 }
        Assert.AreEqual "Record Declare 2" myRecord2.Y 2


    [<Js>]
    let RecordDeclare3() =
        let myRecord2 = { MyRecord.X = 1; MyRecord.Y = 2; MyRecord.Z = 3 }
        let myRecord3 = { myRecord2 with Y = 100; Z = 2 }
        Assert.AreEqual "Record Declare 3" myRecord3.Y 100


    type Car = {
        Make : string
        Model : string
        mutable Odometer : int
        }

    [<Js>]
    let RecordDeclare4() =
        let myCar = { Make = "Fabrikam"; Model = "Coupe"; Odometer = 108112 }
        myCar.Odometer <- myCar.Odometer + 21
        Assert.AreEqual "Record Declare 4" myCar.Odometer (108112 + 21)


    type Point3D = { x: float; y: float; z: float }
    [<Js>]
    let RecordPatternMatching() =
        let evaluatePoint (point: Point3D) =
            match point with
            | { x = 0.0; y = 0.0; z = 0.0 } -> "Point is at the origin."
            | _ -> "Point at other location"

        let r = evaluatePoint { x = 0.0; y = 0.0; z = 0.0 }
        Assert.AreEqual "Record Pattern Matching" r "Point is at the origin."
        let r1 = evaluatePoint { x = 10.0; y = 0.0; z = -1.0 }
        Assert.AreEqual "Record Pattern Matching" r1 "Point at other location"

    [<JsObject>]
    type Employee = {
        id      : int
        name    : string
    }

    [<Js>]
    let RecordJsObjectTest() =
        let emp = { id = 0; name = "Robert" }
        Assert.AreEqual "RecordJsObject Test" 0 emp.id
        Assert.AreEqual "RecordJsObject Test" "Robert" emp.name

    [<Js>]
    let RecordJsObjectEqualityTest() =
        let emp = {id = 1; name = "Robert" }
        let isEqual =
            match emp with
            | { id = 1; name = "Robert" } -> true
            | _                           -> false
        Assert.AreEqual "Record Equality Test" true isEqual

    [<Js>]
    let RecordJsObjectEqualityTest2() =
        let emp1 = {id = 1; name = "Robert" }
        let emp2 = {id = 1; name = "Robert" }
        let res = emp1 = emp2
        Assert.AreEqual "Record Equality Test2" true res

    [<JsObject>]
    type Address = {
        street: string
        pincode : int
    }

    [<JsObject>]
    type Employee1 = {
        id      : int
        name    : string
        address : Address
    }

    [<JsObject>]
    type Manager = {
        employee : Employee1
        division : string
    }

    [<Js>]
    let NestedRecord() =
        let man:Manager = { employee = { id = 0; name = "Robert" ; address = { street = "Green street" ; pincode = 420 } }  ; division = "HR" }
        let man1:Manager = { man with employee = { man.employee with address = { street = "Red street" ; pincode = 428 } }  ; }
        let man2:Manager = {man1 with employee = { id = 1; name ="Dilbert";address = man.employee.address } }
        Assert.AreEqual "Manager1 name" man1.employee.name "Robert"
        Assert.AreEqual "Manager1 address" man1.employee.address.street "Red street"
        Assert.AreEqual "Manager2 name" man2.employee.name "Dilbert"
        Assert.AreEqual "Manager2 address" man2.employee.address.street "Green street"

    [<Js>]
    let NestedRecordEquality() =
        let man1:Manager = { employee = { id = 0; name = "Robert" ; address = { street = "Green street" ; pincode = 420 } }  ; division = "HR" }
        let man2:Manager = { employee = { id = 0; name = "Robert" ; address = { street = "Green street" ; pincode = 420 } }  ; division = "HR" }
        let res = man1 = man2
        Assert.AreEqual "Nested Record Equality" true res

    type Address1 = {
        street: string
        pincode : int
    }

    type Employee2 = {
        id      : int
        name    : string
        address : Address1
    }

    [<JsObject>]
    type Manager1 = {
        employee : Employee2
        division : string
    }

    [<Js>]
    let NestedRecord2() =
        let man:Manager1 = { employee = { id = 0; name = "Robert" ; address = { street = "Green street" ; pincode = 420 } }  ; division = "HR" }
        let man1:Manager1 = { man with employee = { man.employee with address = { street = "Red street" ; pincode = 428 } }  ; }
        let man2:Manager1 = {man1 with employee = { id = 1; name ="Dilbert";address = man.employee.address } }
        Assert.AreEqual "Manager1 name" man1.employee.name "Robert"
        Assert.AreEqual "Manager1 address" man1.employee.address.street "Red street"
        Assert.AreEqual "Manager2 name" man2.employee.name "Dilbert"
        Assert.AreEqual "Manager2 address" man2.employee.address.street "Green street"

    type CustomPoint = {
        x : int
        y : int
    } with
        [<Js>]
        member this.xy = this.x * this.y

    [<Js>]
    let RecordExtendedTypeTest() =
        let p : CustomPoint = { x = 10; y = 20}
        let xy = p.xy
        Assert.AreEqual "Record Member XY" xy 200

    module SomeObject =
        type t = {
            connected : bool
        }

    module State =
        [<JsObject>]
        type s = {
            current : SomeObject.t
            last    : SomeObject.t option
        }

        [<JsObject>]
        type t = {
            current : s option
        }

    [<Js>]
    let handle (t:State.t) =
        match t.current with
        | Some(current) ->
            let t = {t with current = Some({current with last = Some(current.current)})}
            t
        | None          -> t

    [<Js>]
    let RecordWithSomeNone() =
        let t : State.t = { current = Some({current ={connected=false}; last = None}) }
        let t = handle(t)
        Assert.AreEqual "Record With Option and Match" t.current.Value.current.connected false