namespace Pit.Javascript
open Pit

[<JsIgnore(IgnoreCtor=false,IgnoreNamespace=true)>]
type RegExp(patterns:string, modifier:string) =

    [<CompileTo("exec")>]
    member x.Exec(exp:string) = new JsArray<_>([|""|])

    [<CompileTo("test")>]
    member x.Test(exp:string) = true

    (*[<CompileTo("global")>]
    member x.Global = true

    [<CompileTo("ignoreCase")>]
    member x.IgnoreCase = true

    [<CompileTo("lastIndex")>]
    member x.LastIndex = 0

    [<CompileTo("multiline")>]
    member x.Multiline = true*)

    [<CompileTo("compile")>]
    static member Compile(regexp: RegExp, modifier: string) = ()