namespace TicTacToe
open Pit
open Pit.Dom

module Game =

    type State =
        | X
        | O
        | None

    [<Js>]
    let wins = [|(0,1,2);(3,4,5);(6,7,8);(0,3,6);(1,4,7);(2,5,8);(0,4,8);(2,4,6);|]

    [<Js>]
    let gameState = [| State.None; State.None; State.None; State.None; State.None; State.None; State.None; State.None; State.None  |];

    [<Js>]
    let mutable lstState = State.None

    [<Js>]
    let resetBoard() =
        let tab = document.GetElementsByTagName("table").[0]  |> DomTable.Of
        for i = 0 to 2 do
            for j = 0 to 2 do
                tab.Rows.[i].Cells.[j].InnerHTML <- ""
                gameState.[(i * 3) + j] <- State.None
        lstState <- State.None

    [<Js>]
    let checkWinner(i, j) =
        let index = (i * 3) + j
        wins
        |> Array.filter (fun (a,b,c) -> if (a = index || b = index || c = index) then true else false )
        |> Array.map ( fun (d,e,f) -> (gameState.[d], gameState.[e], gameState.[f]) )
        |> Array.iter (fun (g,h,i) ->
                                match (g,h,i) with
                                | (State.O, State.O, State.O) ->
                                    alert("O is Winner")
                                    resetBoard()
                                | (State.X, State.X, State.X) ->
                                    alert("X is Winner")
                                    resetBoard()
                                | _ -> ())
        let l = gameState |> Array.filter (fun k -> k = State.None) |> Array.length
        if l = 0 then
            alert("Match draw")
            resetBoard()

    [<Js>]
    let hook(td:DomElement) (i:int) (j:int) =
        td
        |> Event.click
        |> Event.add(fun _ ->
                    match gameState.[(i * 3) + j] with
                    | State.None ->
                        match lstState with
                        | State.None | State.X ->
                            gameState.[(i * 3) + j] <- State.O
                            td.InnerHTML <- "O"
                            lstState <- State.O
                        | State.O ->
                            gameState.[(i * 3) + j] <- State.X
                            td.InnerHTML <- "X"
                            lstState <- State.X
                        checkWinner(i,j)
                    | _ -> alert("Click on another cell")
            )

    [<Js>]
    let drawBoard() =
        let div = document.GetElementById("ttt")
        let tab = document.CreateElement("table") |> DomTable.Of
        tab.Rules <- "all"
        for i = 0 to 2 do
            let tr = document.CreateElement("tr")
            tr.SetAttribute("height", "200")
            for j = 0 to 2 do
                let td = document.CreateElement("td")
                td.SetAttribute("width", "200")
                td.SetAttribute("align", "center")
                td.SetAttribute("valign", "center")
                hook td i j
                tr.AppendChild(td)
            tab.AppendChild(tr)
        div.AppendChild(tab)