namespace Pit.Compiler
open Pit
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.ExprShape
open Microsoft.FSharp.Reflection
open System
open System.Reflection

    module CtorParser =

        let getCtorMembers (ctorParameters: Node[]) (properties: PropertyInfo[]) =
            let getCtorParam (n:Node) (p:string) =
                match n with
                | Variable(t) -> t.ToLower() = p.ToLower()
                | _           -> false

            let members = [|for p in properties do yield
                                                    Assign(MemberAccess(p.Name, Variable("this")),
                                                    match ctorParameters |> Array.tryFind(fun t -> getCtorParam t p.Name) with
                                                    | Some(d)   -> d
                                                    | _         -> Unit
                                                    )|]
            Block(members)

        let getCtorBody e =
            let rec isSequential (e:Expr) =
                match e with
                | Patterns.Sequential(l,r) -> true
                | Patterns.Let(v,t,i)      -> isSequential(i)
                | _                        -> false

            if isSequential(e) then
                let rec findCtorBody (e:Expr) =
                    match e with
                    | Patterns.Sequential(l, r) -> r
                    | Patterns.Let(v, t, i)     -> findCtorBody i
                    | _                         -> e
                findCtorBody e
            else
                // sometimes there is no sequential in the expr body
                // example: FSharpSet overloaded ctor
                e


        let transformDefaultCtor (node:Node) =
            AstParser.transformAst(node) (fun p f -> None) (fun ast f ->
                match ast with
                | DeclareStatement(l,r) ->
                    match l with
                    | Variable(x) -> Assign(MemberAccess(x |> cleanName, Variable("this")), r)
                    | _           -> failwith "not recognizable node in ctor transform"
                | x -> x)

        let getAst (isDefaultCtor:bool) (e:Expr) =
            match e with
            | Patterns.NewRecord(r, args) ->
                (* when using record expressions to create ctors handle it separately,
                    type Exception  =
                        val message : string
                        [<Js>] new ()       = { message = ""  }
                        [<Js>] new (msg)    = { message = msg }*)
                let firstChar (s:string) = s.Substring(0,1).ToLower()
                let var = if isDefaultCtor then Variable("this") else Variable(r.Name |> firstChar)
                let ar1 = parseRec args
                let p   =
                    r.GetProperties()
                    |> Array.sortBy(fun pi ->
                        let compileMappingAttr = pi.GetCustomAttributes(typeof<CompilationMappingAttribute>, false)
                        if compileMappingAttr.Length > 0 then
                            let cmp = compileMappingAttr.[0] :?> CompilationMappingAttribute
                            cmp.SequenceNumber
                        else 0
                    )
                let ar  =
                    ar1
                    |> List.mapi(fun i exp ->
                        match exp with
                        | Patterns.Value(x,y) ->
                            let v = parseValue x y
                            Assign(MemberAccess(p.[i].Name |> pascalCase, var), v)
                        | ShapeVar v ->
                            Assign(MemberAccess(p.[i].Name |> pascalCase, var), Variable(v.Name))
                        | _ -> failwith "Unrecognized sequence in ctor new record construction"
                    )
                    |> List.toArray
                if isDefaultCtor then Block(ar)
                else
                    // for record type expressions it is safe to assume we initialize all properties with a default initialization
                    // of the object itself
                    let dec  = DeclareStatement(var, New(getDeclaredType(r) |> Variable,[|Unit|]))
                    [|dec;Block(ar);Return(var)|] |> Block
            | _  ->
                let ast = (AstParser.getAst e)
                if isDefaultCtor then ast else ast |> rewriteBodyWithReturn

        let createCtorsAst(ctors : seq<ConstructorInfo>, t : Type) =
            let ctorStr = ".__init__ =function(idx, lambda){if (typeof this['ctors'] == 'undefined') {this['ctors'] = [];}var ctors = this['ctors'];ctors[idx] = lambda;};"
            let tAccess = getDeclaredType(t) //parseFullName t //_getAccessVariable(t.Name, t.DeclaringType, t.Namespace)
            let ctorInit = tAccess + ctorStr
            let quotesAndCtors =
                [| for c in ctors do
                        match Microsoft.FSharp.Quotations.Expr.TryGetReflectedDefinition(c) with
                        | Some(rd) -> yield (c, rd)
                        | _        -> () |]
            (*let getCtorBody e =
                match e with
                | Patterns.Sequential(l,r) -> r
                | x                        -> x*)
            let ctors =
                quotesAndCtors
                |> Array.mapi(fun i ctorExpr ->
                        let ctor, e = ctorExpr
                        match e with
                        | Patterns.Lambda(v, exp) ->
                            let ast =
                                exp
                                |> getCtorBody
                                |> getAst false
                            let func =
                                match ast with
                                | Function(None, args, body) -> body
                                | x                          -> x
                            Block(
                                [|Call
                                    (MemberAccess
                                        ("__init__", Variable(tAccess)),
                                    [|Int(Some i);
                                        Function(None, ctor.GetParameters() |> Array.map(fun p -> Variable(p.Name)), func)|])|])
                        | _ -> failwith "un-recognizable ctor"
                )

            ctors |> Array.append [|StringLiteral(ctorInit)|]

    module ClassParser =
        let parse (md:MethodInfo) (exp:Expr) =
            match exp with
            | Patterns.Lambda(v, x) when isIgnoreTupleArgs(md) && v.Type.Name.Contains("Tuple") ->
                let rec getTuplePositions exp' acc =
                    match exp' with
                    | Patterns.Let(a, Patterns.TupleGet(b, c), d) -> getTuplePositions d ([|Variable(a.Name |> cleanName)|] ++ acc)
                    | _ -> acc, exp'
                let args, exp' = getTuplePositions x [||]
                Function(None, args, AstParser.getAst exp' |> rewriteBodyWithReturn)
            | _ -> AstParser.getAst exp

    module InterfaceParser =

        let transformAst(node: Node) =

            let transform astNode fn =
                match astNode with
                | MemberAccess(identifier, var) ->
                    match var with
                    | Variable("this") -> MemberAccess(identifier, MemberAccess("x", var)) // only replacing "this" member access
                    | Variable("thisObject") -> MemberAccess(identifier, MemberAccess("x", var)) // only replacing "this" closure member access
                    | MemberAccess(l, r) ->
                        match r with
                        | Variable("this") -> MemberAccess(identifier, MemberAccess(l, MemberAccess("x", r))) // only replacing "this" member access
                        | Variable("thisObject") -> MemberAccess(identifier, MemberAccess(l, MemberAccess("x", r))) // only replacing "this" closure member access
                        | _ -> MemberAccess(l, r)
                    | Variable(_) | MemberAccess(_,_) -> astNode
                    | _ -> MemberAccess(identifier, fn var)
                | Variable("thisObject") -> MemberAccess("x", Variable("thisObject"))
                | x -> x

            let projection (astNode:Node) (fn: Node->Node) =
                match astNode with
                | Call(md, margs)                       ->
                    match md with
                    | Closure(Function(n, p, body))     -> Call(fn md, margs) |> Some // no need to process the closure arguments, it will already be "this" or "thisObject"
                    | _                                 -> None
                | New(n, args) ->
                    let transformArg (n:Node) =
                        match n with
                        | Variable("this") -> Variable("this.x")
                        | _                -> n
                    match (args |> Array.exists(fun a -> match a with | Variable("this") -> true | _ -> false)) with
                    | true  -> New(n, args |> Array.map transformArg) |> Some
                    | false -> None
                | _ -> None

            AstParser.transformAst node projection transform