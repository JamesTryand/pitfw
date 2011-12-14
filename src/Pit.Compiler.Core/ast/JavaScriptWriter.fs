namespace Pit.Compiler

open System
open System.Text
open Pit.Compiler.Ast

module JavaScriptWriter =
    let sb(str:String) = new StringBuilder(str)
    let (@@) (str:string) (sb:StringBuilder) = sb.Append(str) |> ignore

    let append (str : String) (sb : StringBuilder) =
        sb.Append(str) |> ignore

    let appendLine (str : String) (sb : StringBuilder) =
        sb.AppendLine(str) |> ignore

    let trim (x:string) =
        x.Trim([|' '; '\n'; '\t'; '\r'; ','|])

    let writeToJS(astNode : Node) =
        let rec traverse node =
            match node with
            | Int(i) ->
                match i with
                | Some(v) -> v.ToString()
                | _ -> "0"
            | Float(f) ->
                match f with
                | Some(v) -> v.ToString()
                | _ -> "0"
            | StringNode(s) ->
                match s with
                | Some(s) ->
                    if s <> String.Empty && s <> "\"\"" then
                        String.Format("\"{0}\"", s)
                    else "''"
                | _ -> ""
            | BooleanNode(b) ->
                match b with
                | Some(b) -> b.ToString().ToLower()
                | _ -> "false"
            | Variable(n) -> n
            | QuotedVariable(n)     -> String.Format("\"{0}\"", n)
            | Addition(l, r)        -> str "(%s + %s)" (traverse l) (traverse r)
            | Subraction(l, r)      -> str "(%s - %s)" (traverse l) (traverse r)
            | Division(l, r)        -> str "(%s / %s)" (traverse l) (traverse r)
            | Multiply(l, r)        -> str "(%s * %s)" (traverse l) (traverse r)
            | Modulus(l, r)         -> str "(%s %s %s)" (traverse l) "%" (traverse r)
            | Negation(l)           -> str "-%s" (traverse l)
            | Concatenate(l, r)     -> str "%s + %s" (traverse l) (traverse r)
            | LessThan(l, r)        -> str "%s < %s" (traverse l) (traverse r)
            | LessThanOrEqual(l, r) -> str "%s <= %s" (traverse l) (traverse r)
            | GreaterThan(l, r)     -> str "%s > %s" (traverse l) (traverse r)
            | GreaterThanOrEqual(l, r) -> str "%s >= %s" (traverse l) (traverse r)
            | NotEquals(l, r)       -> str "%s != %s" (traverse l) (traverse r)
            | Equality(l, r)        -> str "%s == %s" (traverse l) (traverse r)
            | IndexAccess(l, r)     -> str "%s[%s]" (traverse l) (traverse r)
            | And(l, r)             -> str "%s && %s" (traverse l) (traverse r)
            | Or(l, r)              -> str "%s || %s" (traverse l) (traverse r)
            | Not(l)                -> str "!%s" (traverse l)
            | BitAnd(l, r)          -> str "%s & %s" (traverse l) (traverse r)
            | BitOr(l, r)           -> str "%s | %s" (traverse l) (traverse r)
            | BitXor(l, r)          -> str "%s ^ %s" (traverse l) (traverse r)
            | BitNot(l)             -> str "~%s" (traverse l)
            | BitLeftShift(l, r)    -> str "%s << %s" (traverse l) (traverse r)
            | BitRightShift(l, r)   -> str "%s >> %s" (traverse l) (traverse r)

            | DeclareStatement(n1, n2) ->
                let d = sb(str "var %s=" (traverse n1))
                let rhs = traverse n2
                (if not(rhs.EndsWith(";")) then rhs + ";" else rhs) @@ d
                d.ToString()
            | New(node, args) ->
                let n = sb("")
                args |> Array.iteri(fun i vr ->
                    (traverse vr) @@ n
                    if i < args.Length - 1 then "," @@ n
                )
                let p = (traverse node)
                let p1 = (n.ToString())
                str "new %s(%s)" p p1
            | Function(name, p, body) ->
                let vars = sb("")
                p |> Array.iteri(fun i vr ->
                    if i > 0 then "," @@ vars
                    (traverse vr) @@ vars
                )
                let prs = vars.ToString()
                let fn = sb("")
                match name with
                | Some(n) ->
                    (str "function %s" n) @@ fn
                    (if prs <> String.Empty then (str "(%s)" prs) else "()") @@ fn
                | None -> (if prs <> String.Empty then (str "function(%s)" prs) else "function()") @@ fn
                "{" + (traverse body) + "}" @@ fn
                fn.ToString()
            | Node.Call(n, p) ->
                let c = sb("")
                let isEmptyParam = p = [||]
                let isNullParam = p.Length = 1 && p.[0] = Node.Null
                if not(isEmptyParam) && not(isNullParam) then
                    p |> Array.iteri(fun i r ->
                        if i > 0 then "," @@ c
                        (traverse r) @@ c
                    )
                let md = traverse n
                let prs = c.ToString()
                if prs <> String.Empty then
                    str "%s(%s)" md prs
                elif isEmptyParam then
                    str "(%s)()" md
                else str "%s()" md
            | Return(n) ->
                let ret = traverse n
                if ret <> String.Empty then
                    if not(ret.EndsWith(";")) then str "return %s;" ret else str "return %s" ret
                else ""
            | Block(nodes) ->
                let blk = sb("")
                for n in nodes do
                    let b = traverse n
                    (if not(b.EndsWith(";")) && b <> String.Empty then b + ";" else b) @@ blk
                blk.ToString()
            | NewTupleNode(nodes) ->
                let nt = sb("{")
                nodes |> Array.iteri(fun i n ->
                    (str "Item%d : %s" (i+1) (traverse n)) @@ nt
                    if i < nodes.Length - 1 then "," @@ nt
                )
                "}" @@ nt
                nt.ToString()
            | NewJsType(nodes) ->
                let nt = sb("{")
                nodes |> Array.iteri(fun i n ->
                    (str "%s : %s" (fst n) (traverse (snd n))) @@ nt
                    if i < nodes.Length - 1 then "," @@ nt
                )
                "}" @@ nt
                nt.ToString()
            | MemberAccess(vr, n) ->
                str "%s.%s" (traverse n) vr
            | MemberAccessNode(vr, n) ->
                str "%s.%s" (traverse n) (traverse vr)
            | Closure(n) ->
                str "(%s)" (traverse n)
            | IfElse(cond, b, e) ->
                let ife = sb((str "if (%s)" (traverse cond)) + "{" + (traverse b))
                match e with
                | Some(el) ->
                    "} else {" + (traverse el) + "}" @@ ife
                | _ -> "}" @@ ife
                ife.ToString()
            | While(l, r) ->
                str "while (%s)" (traverse l) + "{" + (traverse r) + "}"
            | ForIntegerUpLoop(i, i1, i2, b) ->
                str "for(var %s %s; %s++)" (traverse i1) (traverse i2) (traverse i) + "{" + traverse b + "}"
            | Assign(lhs , rhs) ->
                let blk = sb(traverse lhs + "=")
                let r = traverse rhs
                (if not(r.EndsWith(";")) then r + ";" else r) @@ blk
                blk.ToString()
            | Class(cName, bName, ci, ctorVars, ctorArgs, body) ->
                let blockSb = new StringBuilder()
                let v = traverse ci
                if v <> String.Empty then // sometimes we won't have an object to assign the function to
                    blockSb |> append(str "%s = " v)
                blockSb |> append("(function(){")
                if not(String.IsNullOrEmpty(bName)) then
                    blockSb |> append(str "__extends(%s,%s);" cName bName)
                blockSb |> append("function ")
                blockSb |> append(cName)
                blockSb |> append("(")
                ctorArgs |> Array.iteri(fun i vr ->
                    let pres = traverse vr
                    blockSb |> append(pres)
                    if i < ctorArgs.Length - 1 then blockSb |> append(",")
                )
                blockSb |> append(")")
                blockSb |> append("{")
                if not(String.IsNullOrEmpty(bName)) then
                    blockSb |> append(str "%s.__super__.constructor.apply(this, arguments);" cName)
                for c in ctorVars do
                    blockSb |> append(traverse c)
                blockSb |> append("}")
                for n in body do
                    blockSb |> append(traverse n)
                blockSb |> append("return ")
                blockSb |> append(cName)
                blockSb |> append(";})();")
                blockSb.ToString()
            | EnumNode(m, v) ->
                let eb = sb(str "var %s=" (traverse m) + "{")
                v |> Array.iteri(fun i e ->
                    str "%s:" e + "{}" @@ eb
                    if i < v.Length - 1 then "," @@ eb
                )
                "};" @@ eb
                eb.ToString()
            | StringLiteral(s) -> s
            | Try(t, c, f) ->
                let tb = sb("try {" + traverse t + "}")
                match c with
                | Some(ct) ->
                    str "catch(%s)" (traverse (fst ct)) + "{" + traverse (snd ct) + "}" @@ tb
                | _ -> ()
                match f with
                | Some(fn) ->
                    "finally {" + (traverse fn) + "}" @@ tb
                | _ -> ()
                tb.ToString()
            | NewArray(n) ->
                let a = sb("[")
                n |> Array.iteri(fun i e ->
                    (str "%s" (traverse e)) @@ a
                    if i < n.Length - 1 then "," @@ a
                )
                "]" @@ a
                a.ToString()
            | InstanceOf(l, r) ->
                str "%s instanceof %s" (traverse l) (traverse r)
            | TypeOf(l) ->
                str "typeof %s" (traverse l)
            | Node.Unit -> ""
            | Node.Null -> "null"
            | Node.Empty -> "{}"
            | Node.Throw(e) -> str "throw %s" (traverse e)
            | _ -> failwith "unrecognized sequence"

        traverse astNode

    let getJS(nodes : Node[]) =
        use sw = new System.IO.StringWriter()
        for n in nodes do
            writeToJS n
            |> sw.Write
        sw.GetStringBuilder().ToString()