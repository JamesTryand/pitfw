namespace Pit.Dom

open System.Windows.Browser
open Pit
open Utils

[<AllowNullLiteral>]
type DomFrameset =
    inherit DomElement
    val frameset: ScriptObject

    internal new(frameset) =
        { inherit DomElement(frameset); frameset=frameset }

    static member Of(el:DomElement) =
        new DomFrameset(el.InternalScriptObject)

    member x.Cols
        with get() = x.frameset.GetProperty<string>("cols")
        and set(v: string) = x.frameset.SetProperty("cols", v)

    member x.Rows
        with get() = x.frameset.GetProperty<string>("rows")
        and set(v: string) = x.frameset.SetProperty("rows", v)