namespace System.Windows.Browser

open System
open Pit

type HtmlPage() =
    
    static member Window
        with get() = new HtmlWindow()

    static member Document
        with get() = new HtmlDocument()