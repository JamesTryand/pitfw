namespace Pit.Dom
open System
open System.Windows.Browser
open Utils

[<AutoOpen>]
module HtmlModule =
    let document    = new DomDocument(HtmlPage.Document)
    let window      = new DomWindow(HtmlPage.Window)
    let alert s     = HtmlPage.Window.Alert(s)