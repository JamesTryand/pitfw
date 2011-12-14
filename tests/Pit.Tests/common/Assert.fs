namespace Pit

[<JsIgnore(IgnoreNamespace=true)>]
module Assert =

    [<JsIgnore(IgnoreTuple=true)>]
    let AreEqual (fnName:string) (a1:obj) (a2: obj) = ()

    [<JsIgnore(IgnoreTuple=true)>]
    let AreNotEqual (fnName:string) (a1:obj) (a2: obj) = ()

    [<JsIgnore(IgnoreTuple=true)>]
    let IsNull (fnName:string) (a1:obj) = ()

    [<JsIgnore(IgnoreTuple=true)>]
    let IsNotNull (fnName:string) (a1:obj) = ()