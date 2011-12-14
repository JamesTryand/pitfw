namespace Pit.Javascript
open Pit

    [<Alias("String")>]
    [<JsIgnore(IgnoreCtor=true,IgnoreNamespace=true,IgnoreGetSet=true)>]
    type JsString(str: string) =
        [<CompileTo("length")>]
        member x.Length = 0

        [<CompileTo("charAt")>]
        member x.CharAt(index:int) = ""

        [<CompileTo("charCodeAt")>]
        member x.CharCodeAt(index:int) = 0

        [<CompileTo("concat")>]
        member x.Concat(other:JsString) = JsString("")

        [<CompileTo("indexOf")>]
        member x.IndexOf(searchString:string) = 0

        [<CompileTo("indexOf")>]
        member x.IndexOf(searchString:string, start:int) = 0

        [<CompileTo("lastIndexOf")>]
        member x.LastIndexOf(searchString:string) = 0

        [<CompileTo("lastIndexOf")>]
        member x.LastIndexOf(searchString:string, start:int) = 0

        [<CompileTo("match")>]
        member x.Match(regex: RegExp) = new JsArray<string>([|""|])

        [<CompileTo("replace")>]
        member x.Replace(from:string, tostr:string) = ""

        [<CompileTo("replace")>]
        member x.Replace(regex:RegExp, tostr:string) = ""

        [<CompileTo("search")>]
        member x.Search(searchString:string) = 0

        [<CompileTo("search")>]
        member x.Search(regex:RegExp) = 0

        [<CompileTo("slice")>]
        member x.Slice(startIndex:int) = ""

        [<CompileTo("slice")>]
        member x.Slice(startIndex:int, endIndex:int) = ""

        [<CompileTo("split")>]
        member x.Split() = new JsArray<string>([|""|])

        [<CompileTo("split")>]
        member x.Split(separator:string) = new JsArray<string>([|""|])

        [<CompileTo("split")>]
        member x.Split(separator:string, limit:int) = new JsArray<string>([|""|])

        [<CompileTo("substring")>]
        member x.Substring(start:int) = ""

        [<CompileTo("substr")>]
        member x.Substring(start:int, length:int) = ""

        [<CompileTo("toLower")>]
        member x.ToLower() = ""

        [<CompileTo("toUpper")>]
        member x.ToUpper() = ""

        [<CompileTo("fromCharCode")>]
        static member FromCharCode(n1:int) = ""