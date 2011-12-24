namespace Pit
open System.Windows.Browser

    [<AutoOpen>]
    module CoreUtils =

        let invoke (fn:unit->unit) =
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(
                new System.Action(fun () -> fn())) |> ignore

        let eval (js:string) =
            System.Windows.Browser.HtmlPage.Window.Eval(js) |> ignore

    [<ScriptableType>]
    type ScriptableInvoker(fn:unit->unit) =
        [<ScriptableMember>]
        member x.Invoke() =
            fn()

    [<ScriptableType>]
    type ScriptableInvokerArgs<'T>(fn:'T->unit) =
        [<ScriptableMember>]
        member x.Invoke(args:'T) =
            fn(args)