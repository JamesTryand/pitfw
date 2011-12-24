namespace Pit.Javascript

open Pit

    [<JsIgnore(IgnoreNamespace=true)>]
    module JSON =

        let stringify (t:'a) = ""
        let parse (t:string) = Unchecked.defaultof<_>