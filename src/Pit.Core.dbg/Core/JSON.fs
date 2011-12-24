namespace Pit.Javascript

open System
open System.Windows.Browser
open Pit

    module JSON =

        let private json() = HtmlPage.Window.GetProperty("JSON") :?> ScriptObject

        let stringify (t:'a) = json().Invoke("stringify", t) :?> string
        let parse (t:string) = json().Invoke("parse",t) :?> 'a