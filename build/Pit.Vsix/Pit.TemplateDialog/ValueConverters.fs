namespace Pit.TemplateDialog
open System
open System.ComponentModel
open System.Windows.Data
open System.Windows

type BooleanToVisibilityConverter() =
    interface IValueConverter with
        member x.Convert(value, targetType, parameter, culture) =
            let getResult = 
                if value <> null  then
                    let res = value.ToString() |> bool.Parse
                    if res then Visibility.Collapsed |> box
                    else Visibility.Visible |> box
                else Visibility.Collapsed |> box
            
            getResult

        member x.ConvertBack(value, targetType, parameter, culture) = 
            raise (new System.NotImplementedException())



        
        

