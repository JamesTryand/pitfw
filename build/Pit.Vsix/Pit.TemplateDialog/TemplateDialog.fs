namespace Pit.TemplateDialog
open System
open System.Windows
open System.Windows.Controls
open System.Runtime.InteropServices

module  WindowExtensions =     

        [<DllImport("user32.dll")>]
        extern int SetWindowLong(IntPtr hwnd, int index, int value)

        [<DllImport("user32.dll")>]
        extern int GetWindowLong(IntPtr hwnd, int index)
        
        let hideMinMinMax (win:Window) =
            let GWL_STYLE = -16
            let hwnd = (new System.Windows.Interop.WindowInteropHelper(win)).Handle
            let value = GetWindowLong(hwnd, GWL_STYLE)
            SetWindowLong(hwnd, GWL_STYLE, (int)(value &&& -131073 &&& -65537)) |> ignore
            

module Main =
    let getMainDialog(viewModel:MainViewModel) = 
        let win = Application.LoadComponent(new System.Uri("/Pit.TemplateDialog;component/MainPage.xaml", UriKind.Relative)) :?> Window
        win.DataContext <- viewModel
        let cb = win.FindName("webProj") :?> ComboBox
        cb.SelectionChanged.Add(fun e -> 
            if e.AddedItems.[0].ToString() = "<New Web Project>" then viewModel.ShouldAddNewWebApp <- true
            else viewModel.ShouldAddNewWebApp <- false
            )
        win.SourceInitialized.Add( fun _ -> WindowExtensions.hideMinMinMax(win))
        let okButton = win.FindName("OKButton") :?> Button
        okButton.Click.Add(fun _ ->
            win.DialogResult <- Nullable true 
            win.Close()
            )
        let cancelButton = win.FindName("CancelButton") :?> Button
        cancelButton.Click.Add(fun _ ->
            win.DialogResult <- Nullable false
            win.Close()
            )        
        win
