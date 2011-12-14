namespace TicTacToe

open Pit
open Pit.Dom

module App =
    [<DomEntryPoint>]
    [<Js>]
    let main() =        
        Game.drawBoard()
