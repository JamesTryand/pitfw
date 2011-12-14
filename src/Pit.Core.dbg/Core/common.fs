namespace Pit

    [<AutoOpen>]
    module Common =
        let toInt (o:obj) = o :?> float |> int