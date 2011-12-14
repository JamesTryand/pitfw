namespace Pit

    [<AutoOpen>]
    module CoreUtils =

        let invoke (fn:unit->unit) =
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(
                new System.Action(fun () -> fn())) |> ignore

        let eval (js:string) =
            System.Windows.Browser.HtmlPage.Window.Eval(js) |> ignore