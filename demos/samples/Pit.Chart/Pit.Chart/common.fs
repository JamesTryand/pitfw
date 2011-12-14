namespace Pit.Chart
open Pit

/// <summary>
/// Colors generator module
/// </summary>
module Colors =
    /// <summary>
    /// Color combinations as default palette in Charts.
    /// </summary
    [<Js>]
    let c =
        [|  "#63AA00"
            "#FBB340"
            "#00AADE"
            "#976290"
            "#71C200"
            "#FF8000"
        |]

    /// <summary>
    /// Returns a set of colors for any combination
    /// </summary>
    [<Js>]
    let get (count) =
        let len = c.Length
        Array.init(count + 1) (fun i ->
            c.[i % len]
        )