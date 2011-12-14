namespace Pit.Dom
open System
open System.Windows.Browser

[<AutoOpen>]
module Utils =

    let invoke (name: string) (args: obj[]) (ctx: ScriptObject) =
        ctx.Invoke(name, args) |> ignore

    let invoke2 (name: string) (args: obj[]) (ctx: ScriptObject) =
        ctx.Invoke(name, args)

    type ScriptObject with
        member x.GetProperty<'T>(name : string) =
            x.GetProperty(name) |> unbox<'T>
