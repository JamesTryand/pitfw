namespace Pit.Compiler

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.ExprShape
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns
open System.Reflection
open Microsoft.FSharp.Reflection
open System
open Pit
open Pit.Compiler.Ast

module AstParser =

    let getOperator methodName (lhs: Node) (rhs: Node) =
        match methodName with
        | "op_Addition"             -> Addition(lhs, rhs)
        | "op_Subtraction"          -> Subraction(lhs, rhs)
        | "op_Division"             -> Division(lhs, rhs)
        | "op_Multiply"             -> Multiply(lhs,rhs)
        | "op_Modulus"              -> Modulus(lhs,rhs)
        | "op_LessThan"             -> LessThan(lhs, rhs)
        | "op_GreaterThan"          -> GreaterThan(lhs, rhs)
        | "op_LessThanOrEqual"      -> LessThanOrEqual(lhs, rhs)
        | "op_GreaterThanOrEqual"   -> GreaterThanOrEqual(lhs, rhs)
        | "op_LessGreater"          -> NotEquals(lhs, rhs)
        | "op_Concatenate"          -> Concatenate(lhs, rhs)
        | "op_Equality"             -> Equality(lhs, rhs)
        | "op_Inequality"           -> NotEquals(lhs, rhs)
        | "op_LeftShift"            -> BitLeftShift(lhs, rhs)
        | "op_RightShift"           -> BitRightShift(lhs, rhs)
        | "op_ExclusiveOr"          -> BitXor(lhs, rhs)
        | "op_BitwiseAnd"           -> BitAnd(lhs, rhs)
        | "op_BitwiseOr"            -> BitOr(lhs, rhs)
        | "op_LogicalNot"           -> BitNot(lhs)
        | "op_UnaryNegation"        -> Negation(lhs)
        | _ -> Node.Ignore // return ignore so the other kind of operators are detected

    let getNameAndMap (name:string) (map:Map<string,string list>) =
        let getNextName name =
            let rec loop (name:string) num =
                if name.EndsWith(num.ToString()) then
                    loop name (num + 1)
                else
                    name + (num.ToString())
            loop name 1

        if map.ContainsKey(name) then
            let value = map.[name]
            match value with
            | h::t ->
                let nextName = getNextName h
                let map = map.Remove(name).Add(name,nextName::h::t)
                (nextName, map)
            | [] ->
                let nextName = getNextName name
                let map = map.Remove(name).Add(name,nextName::[])
                (nextName, map)
        else
            (name, map.Add(name, []))

    let getPropertyGetName (v: Var) (p: PropertyInfo) =
        if isGetSetIgnore(p) then
            Expr.Var(Var(v.Name + "." + p.Name, p.PropertyType, p.CanWrite))
        else
            Expr.Var(Var(v.Name + "." + "get_" + p.Name, p.PropertyType, p.CanWrite))

    let getArgs args' =
        let rec loop arg lets =
            match arg with
            | Patterns.Let(x,y,z) ->
                loop z (lets |> Map.add x y)
            | Patterns.Call(a,m,cargs) ->
                let newArgs = [for carg in cargs ->
                                match carg with
                                | Var x when (lets.ContainsKey x) ->
                                    let newArg = lets |> Map.find (x)
                                    newArg
                                | Coerce(Var x, _ ) when (lets.ContainsKey x) -> // when interface property is accessed we need to take it out from the Coercion
                                    lets |> Map.find (x)
                                | _ -> carg]
                if a.IsSome then
                    let a' = match a.Value with
                                | Var x when (lets.ContainsKey x) ->
                                    lets |> Map.find (x)
                                | _ -> a.Value
                    Expr.Call(a', m, newArgs)
                else
                    Expr.Call(m, newArgs)
            | Patterns.Lambda(_, Patterns.Let(_,_,_)) ->
                arg
            | Patterns.Lambda(v,e) ->
                Expr.Lambda(v, (loop e lets))
            | Patterns.NewRecord(i, args) ->
                let newArgs = [for carg in args ->
                                match carg with
                                | Var x when (lets.ContainsKey x)           -> lets |>Map.find x
                                | Coerce(Var x, _) when lets.ContainsKey x  -> lets |>Map.find x
                                | _ -> carg]
                Expr.NewRecord(i, newArgs)
            | _ -> arg

        [for a in args' -> (loop a Map.empty)]

    let transformAst (expr:Node) (projection: Node -> (Node -> Node) -> Node option) (fn: Node-> (Node -> Node) -> Node) =

        /// projection that either returns the method param or the option value
        let bind f = function
        | None      -> f()
        | Some t    -> t

        let rec transform (node:Node) =
            let projection = projection node transform
            match node with
            | DeclareStatement(l, r)                -> bind (fun() -> DeclareStatement(transform l, transform r)) projection
            | New(ctor, args)                       -> bind (fun() -> New(transform ctor, args |> Array.map(fun a -> transform a))) projection
            | Function(name, args, body)            -> bind (fun() -> Function(name, args |> Array.map(fun a -> transform a), transform body)) projection
            | Call(md, args)                        -> bind (fun() -> Call(transform md, args |> Array.map(fun m -> transform m))) projection
            | Return(n)                             -> bind (fun() -> Return(transform n)) projection
            | Block(nodes)                          -> bind (fun() -> Block( nodes |> Array.map(fun n -> transform n) )) projection
            | NewTupleNode(args)                    -> bind (fun() -> NewTupleNode(args |> Array.map transform)) projection
            | Closure(n)                            ->
                bind (fun() ->
                    Closure(
                        match n with
                        | Function(name, p, body) -> Function(name, p, transform body)
                        | _         -> n
                    )) projection
            | IfElse(cond, block, els)              -> bind (fun() -> IfElse(transform cond, transform block, if els.IsSome then Some(transform els.Value) else None)) projection
            | While(l,r)                            -> bind (fun() -> While(transform l, transform r)) projection
            | ForIntegerUpLoop(i, i1, i2, block)    -> bind (fun() -> ForIntegerUpLoop(transform i, transform i1, transform i2, transform block)) projection
            | Assign(l, r)                          -> bind (fun() -> Assign(transform l, transform r)) projection
            | Try(t, c, f)                          -> bind (fun() -> Try(transform t, (match c with | Some x -> (transform (fst x), transform (snd x)) |> Some | None -> None), match f with | Some x -> transform x |> Some | None -> None)) projection
            | IndexAccess(l, r)                     -> bind (fun() -> IndexAccess(transform l, transform r)) projection
            | Addition(l, r)                        -> bind (fun() -> Addition(transform l, transform r)) projection
            | Subraction(l, r)                      -> bind (fun() -> Subraction(transform l, transform r)) projection
            | Division(l, r)                        -> bind (fun() -> Division(transform l, transform r)) projection
            | Multiply(l, r)                        -> bind (fun() -> Multiply(transform l, transform r)) projection
            | Modulus(l, r)                         -> bind (fun() -> Modulus(transform l, transform r)) projection
            | Concatenate(l, r)                     -> bind (fun() -> Concatenate(transform l, transform r)) projection
            | LessThan(l, r)                        -> bind (fun() -> LessThan(transform l, transform r)) projection
            | GreaterThan(l, r)                     -> bind (fun() -> GreaterThan(transform l, transform r)) projection
            | LessThanOrEqual(l, r)                 -> bind (fun() -> LessThanOrEqual(transform l, transform r)) projection
            | GreaterThanOrEqual(l, r)              -> bind (fun() -> GreaterThanOrEqual(transform l, transform r)) projection
            | NotEquals(l, r)                       -> bind (fun() -> NotEquals(transform l, transform r)) projection
            | Equality(l, r)                        -> bind (fun() -> Equality(transform l, transform r)) projection
            | And(l, r)                             -> bind (fun() -> And(transform l, transform r)) projection
            | Or(l, r)                              -> bind (fun() -> Or(transform l, transform r)) projection
            | Not(l)                                -> bind (fun() -> Not(transform l)) projection
            | BitAnd(l, r)                          -> bind (fun() -> BitAnd(transform l, transform r)) projection
            | BitOr(l, r)                           -> bind (fun() -> BitOr(transform l, transform r)) projection
            | BitXor(l, r)                          -> bind (fun() -> BitXor(transform l, transform r)) projection
            | BitLeftShift(l, r)                    -> bind (fun() -> BitLeftShift(transform l, transform r)) projection
            | BitRightShift(l, r)                   -> bind (fun() -> BitRightShift(transform l, transform r)) projection
            | x                                     -> fn x transform

        transform expr

    let transformClosure node =
        transformAst(node) (fun a f -> None) (fun ast fn ->
            match ast with
            | Variable("this")      -> Variable("thisObject")
            | MemberAccess(identifier, var) ->
                match var with
                | Variable("this")                  -> MemberAccess(identifier, Variable("thisObject"))
                | MemberAccess(l, r)                ->
                    match r with
                    | Variable("this")              -> MemberAccess(identifier, MemberAccess(l, Variable("thisObject")))
                    | _                             -> MemberAccess(identifier, MemberAccess(l, r))
                | Variable(_) | MemberAccess(_,_)   -> ast
                | _                                 -> MemberAccess(identifier, fn var)
            | InstanceOf(l, r) ->
                match l with
                | Variable("this")  -> InstanceOf(Variable("thisObject"), r)
                | x                 -> InstanceOf(l, r) // return back the original object
            | x -> x)

    let getAst(quote : Expr) =
        let rec traverse node (map:Map<string, string list>) =
            match node with

            | Patterns.Value(x, y)        -> parseValue x y
            | Patterns.Let(v, value, ret) ->
                let isOperator = v.Name.Contains("op_")
                let newName,newMap = getNameAndMap (v.Name|>cleanName) map
                let vr = if not(isOperator) then Variable(newName) else MemberAccess(newName, Variable("Pit.Core.Operators"))
                match value with
                | Let(x,y,z) ->
                    if FSharpType.IsRecord v.Type then
                        let args1 = (getArgs [value]) |> List.head
                        let res = traverse args1 map
                        let decl = DeclareStatement(vr, res)
                        let after = traverse ret map
                        let afterResult =
                            match after with
                            | Block(arr) -> arr
                            | _ -> [| after |]
                        Block(afterResult |> Array.append[|decl|])
                    else
                        let getTupleExp tuple =
                            let rec getTuplePositions exp' (acc:Map<Var,Expr>) =
                                match exp' with
                                | Patterns.Let(a, Patterns.TupleGet(b, c), d) -> getTuplePositions d (acc.Add(a, Expr.TupleGet(b, c)))
                                | _ -> acc, exp'
                            let tMap, exp = getTuplePositions tuple Map.empty
                            let exp1 =
                                match exp with
                                | Patterns.Call(c,m,args) ->
                                    let rec replaceArgs (args:Expr list) (acc:Expr list) =
                                        match args with
                                        | []    -> acc
                                        | h::t  ->
                                            match h with
                                            | Patterns.Var(v) ->
                                                if tMap.ContainsKey(v) then replaceArgs t (acc @ [tMap.[v]])
                                                else replaceArgs t (acc @ [h])
                                            | _ -> replaceArgs t (acc @ [h])
                                    let args' = replaceArgs args []
                                    Expr.Call(m, args')
                                | _ -> failwith "Unrecognized sequence in let tuple args"
                            exp1
                        let secondName, secondMap = getNameAndMap (x.Name|>cleanName) newMap
                        let result =
                            if v.Type.Name.Contains("Tuple") then
                                [|DeclareStatement(vr, traverse (getTupleExp value) map)|]
                            else
                                let x1 = if not(isOperator) then Variable(secondName) else MemberAccess(secondName, Variable("Pit.Core.Operators"))
                                let le1 = DeclareStatement(x1, traverse y map)
                                let right2 =
                                    match traverse z map with
                                    | Block(a) -> Block(a) |> rewriteBodyWithReturn |> wrapClosureCall // sometimes we get a full block so wrap it inside a closure function
                                    | x        -> x
                                let le2 = DeclareStatement(vr, right2)
                                [|le1;le2|]

                        let afterResult =
                            match ret with
                            | Let(_,TupleGet(_,_),_) -> [| traverse (getTupleExp ret) map |]
                            | _ ->
                                match traverse ret secondMap with
                                | Block(arr) -> arr
                                | x          -> [|x|]
                        Block(afterResult |> Array.append result)
                | _ ->
                    let av = traverse value map
                    let retVal = traverse ret newMap
                    let afterResult =
                        match retVal with
                        | Block(arr) -> arr
                        | _ -> [|retVal|]
                    if not(isOperator) then Block(afterResult |> Array.append [|DeclareStatement(vr, av)|]) else Block(afterResult |> Array.append [| Assign(vr, av) |])

            | Patterns.LetRecursive(lets, e) ->
                let functions =[|for (v, l) in lets ->
                                        let newName,newMap = getNameAndMap v.Name map
                                        DeclareStatement(Variable(newName |> cleanName), traverse l newMap)
                               |]
                let after = traverse e map
                Block([|after|] |> Array.append functions)

            | Patterns.Lambda(v, exp) ->
                // function with no params have unitVar0 as the type name
                let newName, newMap = getNameAndMap v.Name map
                let vr = if v.Name.Contains("unitVar") then Unit else Variable(newName |> cleanName)
                let body = traverse exp newMap
                if not( isCallPattern(exp) ) then
                    // always the last block has the return variable, we take that last item and alter the list
                    match body with
                    | Block(a) -> // block is returned for a series of declarations using LET
                        Function(None, [|vr|], rewriteBodyWithReturn body)
                    | Call(_, _) ->
                        Function(None, [|vr|], Block([|body |> wrapReturn|]))
                    | Function(None, [|Unit|], body) -> //sometimes empty functions are created, wrap it with closure
                        //Function(None, [|Unit|], body |> transformClosure |> wrapClosureCall)
                        //Function(None, [|vr|], Block([|nd |> transformClosure |> wrapClosureCall |> wrapReturn|]))
                        Function(None, [|vr|], Block([|body|]))
                    | _ ->
                        Function(None, [|vr|], Block([|body |> wrapReturn|]))
                else
                    Function(None, [|vr|], Block([|body |> wrapReturn|]))

            | Patterns.Call(expr1, md, args) ->
                let args1 = (getArgs args) |> List.toArray
                let res =
                    match md.Name with
                    | var when var.StartsWith("op_") ->
                        if var = "op_Equality" && (isUnionOrRecordType args1)  then
                            Some(Call(MemberAccess("equality", (traverse args1.[0] map)), [|(traverse args1.[1] map)|]))
                        else
                            match isOpOverload md with
                            | false ->
                                match getOperator md.Name (traverse args1.[0] map) (if args1.Length > 1 then (traverse args1.[1] map) else Unit) with
                                | Node.Ignore ->
                                    if var.Equals("op_Dereference") then
                                        (traverse args1.[0] map) |> Some
                                    elif var.Equals("op_ColonEquals") then
                                        Assign(traverse args1.[0] map, traverse args1.[1] map) |> Some
                                    else
                                        None
                                | x -> Some(x)
                            | true -> None
                    | "GetArray" | "SetArray" ->
                        let lhs = traverse args1.[0] map
                        let rhs = traverse args1.[1] map
                        if args1.Length = 2 then Some(IndexAccess(lhs, rhs)) else Some(Assign(IndexAccess(lhs, rhs), traverse args1.[2] map))
                    | "Raise" | "FailWith"              -> Throw(traverse args1.[0] map) |> Some
                    | "UnboxGeneric" | "Box" | "Unbox" | "UnboxFast"  ->
                        let ty = md.ReturnType
                        (traverse args1.[0] map) |> Some
                    | "Ref" ->
                        (traverse args1.[0] map) |> Some
                    | "DefaultOf" -> Node.Null |> Some
                    | "Not" -> (traverse args1.[0] map) |> Not |> Some
                    | "Invoke" when isOfType(md.DeclaringType, typeof<Delegate>) ->
                        Call(traverse (expr1|>Option.get) map, args1 |> Array.map(fun a -> traverse a map)) |> Some
                    | x when isIgnoreCall(md) ->
                        Return(Block(args1 |> Array.map(fun a -> traverse a map))) |> Some
                    | _ -> None
                match res with
                | Some(r) -> r
                | _ ->
                    let md =
                        match getOpOverload(md) with
                        | Some(m) -> m
                        | None    -> md
                    let definition = Expr.TryGetReflectedDefinition(md)
                    let name =
                        //match getJsFunction md.Name with
                        //| Some(v)   -> v
                        //| None      ->
                        let ca  = md.GetCustomAttributes(typeof<CompileToAttribute>, true)
                        if ca <> [||] then
                            let cAttrib = ca.[0] :?> CompileToAttribute
                            cAttrib.Name |> cleanName
                        elif md.DeclaringType.IsInterface then
                            md.DeclaringType.Name + "_" + md.Name |> cleanName
                        else
                            md.Name |> cleanName
                    let createTuple args' = NewTupleNode(args')
                    let getArguments (def : Expr option, args2 : Expr[]) : Node[] =
                        match def with
                        | Some expr when not(isIgnoreTupleArgs(md)) ->
                            let rec loop exp position (acc : Node[]) =
                                match exp with
                                | Patterns.Lambda(v, x) when v.Type.Name.Contains("Tuple") ->
                                    let rec getTuplePositions exp' pos =
                                        match exp' with
                                        | Patterns.Let(a, Patterns.TupleGet(b, c), d) -> getTuplePositions d (pos + 1)
                                        (*| Patterns.Let(a,_,d) when a.Name.ToLower().Contains("item") ->
                                            // sometimes the reflecteddefinition method may only have reference to tuple as "item"
                                            getTuplePositions d (pos + 1)
                                        | Patterns.Let(a,_,d)                                 ->
                                            match d with
                                            | Patterns.Let(_,_,_) -> getTuplePositions d pos
                                            | _                   -> pos*)
                                        | _ -> pos
                                    //if not(isIgnoreTupleArgs(md))
                                    let p = (getTuplePositions x position)
                                    let lastTuplePosition = let p = (getTuplePositions x position) in if p = position then args2.Length else p
                                    let arguments = [|for pos in { position .. (lastTuplePosition) - 1 } -> traverse args2.[pos] map|]
                                    let tuple = createTuple arguments
                                    loop x lastTuplePosition ([|tuple|] |> Array.append acc)
                                    //else
                                        //loop x (args2.Length) (args2 |> Array.map(fun a -> traverse a map) |> Array.append acc)
                                | Patterns.Lambda(v, x) when v.Type.Name = "Unit" || position > (args2.Length - 1) ->
                                    //let args = args'
                                    loop x position acc
                                | Patterns.Lambda(v, x) ->
                                    //let args = args'
                                    let arg = args2.[position]
                                    //let s1 = arg.Type.GetInterfaces()
                                    //if arg.Type = v.Type then
                                    if isTypeEqual(arg.Type, v.Type) then //arg.Type = v.Type then
                                        let result = traverse arg map
                                        //loop x position ([|result|] |> Array.append acc)
                                        loop x (position + 1) ([|result|] |> Array.append acc)
                                    else
                                        loop x position acc
                                | Patterns.Let(a, Patterns.TupleGet(b, c), d) ->
                                    loop d position acc
                                | _ -> acc

                            (loop expr 0 [||])
                        | _ -> [| for a in args2 -> traverse a map |]
                    let arguments = getArguments(definition, args1)
                    let getCallNode node =
                        if arguments.Length = 0 then
                            Call(node, [|Unit|])
                        elif Microsoft.FSharp.Reflection.FSharpType.IsModule md.DeclaringType then
                            match md with
                            | x when not(isIgnoreTupleArgs md) && not(isJsExtensionType md) ->
                                let temp = ref node
                                for a in arguments do
                                    temp := Call(temp.Value, [|a|])
                                temp.Value
                            | x when isJsExtensionType(md) && arguments.Length > 0 ->
                                // example extension types for Dom types or for types which only require
                                // to generate JS properly
                                let extType   = arguments.[0] // first argument will always be the object itself in extension types
                                let arguments = arguments |> Array.skip(1)
                                Call(MemberAccessNode(node,extType), arguments)
                            | _ -> Call(node, arguments)

                            (*if not(isIgnoreTupleArgs md) then // example Math.pow(x,y)
                                let temp = ref node
                                for a in arguments do
                                    temp := Call(temp.Value, [|a|])
                                temp.Value
                            else
                                Call(node, arguments)*)
                        else
                            Call(node, arguments)

                    match expr1 with
                    | Some expr ->
                        //let left = if expr.Type = md.DeclaringType then Variable("this") else traverse expr
                        //let left = Variable("this")
                        let left = traverse expr map
                        getCallNode (MemberAccess(name, left))
                    | None ->
                        let node = getMemberAccess (name, md, false)
                        getCallNode node
                        (*match node with
                        | Variable(d) -> node
                        | _ -> getCallNode node*)

            | ShapeVar v ->
                let getVariableName (name:string) (map:Map<string,string list>) =
                    if map.ContainsKey(name) then
                        let value = map.[name]
                        match value with
                        | h::t -> h
                        | [] -> name
                    else
                        name
                let name = getVariableName v.Name map
                if not(v.Name.Contains("op_")) then Variable(name |> cleanName) else MemberAccess(name |> cleanName, Variable("Pit.Core.Operators"))

            | Patterns.Application(l,r) ->
                Call(traverse l map, [|traverse r map|])

            | Patterns.Sequential(l,r) ->
                Block([|traverse l map;traverse r map|])

            | Patterns.TupleGet(n,index) ->
                MemberAccess("Item" + (index + 1).ToString(), traverse n map)

            | Patterns.NewTuple(tup) ->
                NewTupleNode([|for t in tup do yield traverse t map|])

            | Patterns.Coerce(v, o) ->
                traverse v map

            | Patterns.Quote(x) ->
                traverse x map

            | Patterns.NewObject(i, args) ->
                match i.DeclaringType |> getJsIgnoreAttr with
                | Some(a) when a.IgnoreCtor = true ->
                    if args.Length > 1 then
                        failwith "args length is > 1 with IgnoreCtor=true;"
                    traverse args.Head map
                | j ->
                    let ctor =
                        i.DeclaringType.GetConstructors()
                        |> Array.filter(fun c -> c.DeclaringType = i.DeclaringType) // requiring only the current type
                        |> Array.toList
                    let defCtor, ctors = ctor.Head, ctor.Tail
                    let ar = [|for a in args do yield traverse a map|]
                    if i = defCtor || isJsIgnoreType(i.DeclaringType) then // if the type is declared as JsIgnore it means we dont handle custom ctor generation
                        let mi = getMemberAccess (i.DeclaringType.Name, i.DeclaringType, true)
                        match mi with
                        | Variable(name) ->
                            if j.IsSome && j.Value.IgnoreNamespace then
                                New(mi, ar)
                            else
                                //New(MemberAccess(name, Variable(i.DeclaringType.Namespace)), ar)
                                New(getDeclaredTypeName(i.DeclaringType, "") |> Variable, ar)
                        | _ -> New(mi, ar)
                    else
                        let getIndex (c:ConstructorInfo list) ctor =
                            let rec find c i =
                                match c with
                                | []    -> i
                                | h::t  -> if h = ctor then i else find t (i+1)
                            find c 0
                        let idx  = getIndex ctors i
                        let mi   = getDeclaredTypeName(i.DeclaringType,"") |> Variable
                        Call(IndexAccess(MemberAccess("ctors", mi), Variable(idx.ToString())), ar)

            | Patterns.NewRecord(i, args) ->
                if not(isJsObject(i)) then
                    let ar1 = parseRec args
                    let ar = [|for a in ar1 do yield traverse a map|]
                    //let mi = AstHelpers.getMemberAccess (AstHelpers.getDeclaredTypeName(i, String.Empty), i.DeclaringType, true)
                    New(getClassInfo(i) |> Variable, ar)
                else
                    let ar1 = parseRec args
                    let p = FSharpType.GetRecordFields(i, BindingFlags.Public ||| BindingFlags.NonPublic)
                    //let ar = args |> List.mapi(fun x a -> p.[x].Name, traverse a map) |> List.toArray
                    let ar = ar1 |> List.mapi(fun x a -> p.[x].Name, traverse a map) |> List.toArray
                    NewJsType(ar)

            | Patterns.DefaultValue(x) ->
                match x with
                | _ when x.FullName = null -> Null
                | _ when x.FullName = "String" -> Null
                | _ when x.FullName = "Int32" -> Int(Some(0))
                | _ -> Float(Some(0.))

            | Patterns.IfThenElse(s, b, e) ->
                let elseStatement =
                    match traverse e map with
                    | x when x <> Node.Unit -> rewriteBodyWithReturn x |> transformClosure |> Some
                    | _ -> None
                match traverse s map with
                | Block(st) ->
                    let statement = st |> Array.skip(st.Length - 1)
                    (*let rec getBody (b: Expr) =
                        match b with
                        | Let(p, o, i) ->
                            getBody(i)
                        | _ -> rewriteBodyWithReturn (traverse b map)
                    let body = getBody(b)*)
                    let body = rewriteBodyWithReturn (traverse b map)
                    match body with
                    | Block(b) -> // handling match conditions where match has "when" clause, intermediate variables are created here
                        let vars, body = b |> Array.partition(fun t -> match t with | DeclareStatement(_,_) -> true | _ -> false)
                        let ifStatement = IfElse(statement.[0], Block(body), elseStatement)
                        //let ifStatement = IfElse(statement.[0] , Block(body) |> transformClosure, elseStatement)
                        let after = Call(Function(None, [|Unit|], Block([|ifStatement|] |> Array.append vars) |> wrapClosureCall |> wrapReturn), [||])
                        //let after = Block([|ifStatement|] |> Array.append vars)|> wrapClosureCall |> wrapReturn
                        // need to append the vars generated in the statement too, this create duplicate variables, but its fine for use-cases where there are declarations done inside an IF block, ex: if(latest=e.GetCurrent(); (some condition){ }
                        let letvars = st |> Array.take(st.Length - 1) |> Array.rev
                        Block([|after|] |> Array.append letvars)
                    | _ ->
                        let ifStatement = IfElse(statement.[0], body, elseStatement)
                        let vars = st |> Array.take(st.Length - 1) |> Array.rev
                        let after = Call(Function(None, [|Unit|], Block([|ifStatement|])), [||])
                        Block([|after|] |> Array.append vars)
                | nd ->
                    let body = traverse b map |> rewriteBodyWithReturn |> transformClosure
                    let res = IfElse(nd |> transformClosure, body, elseStatement ) |> wrapClosureCall
                    res

            | Patterns.WhileLoop(a,b) ->
                While(traverse a map, traverse b map)

            | Patterns.ForIntegerRangeLoop(v, i1, i2, b) ->
                let var = Variable(v.Name)
                let v1 = Assign(var, traverse i1 map)
                let v2 = LessThanOrEqual(Variable(v.Name), traverse i2 map)
                let body = traverse b map
                ForIntegerUpLoop(Variable(v.Name), v1, v2, [|body |> transformClosure |> wrapClosureCall2([|var|])|] |> Block)

            | Patterns.PropertyGet(l, i, []) ->
                if l.IsSome then
                    let left = traverse l.Value map
                    if i.DeclaringType.IsInterface then
                        Call(MemberAccess(i.DeclaringType.Name + "_" + "get_" + i.Name |> cleanName, left), [|Unit|])
                    elif isJsObject(i.DeclaringType) then
                        MemberAccess(i.Name, left)
                    else
                        //Call(MemberAccess("get_" + AstHelpers.cleanName(i.Name), left), [|Unit|])
                        if isGetSetIgnore i then
                            MemberAccess(i |> getPropName, left)
                        else
                            Call(MemberAccess( "get_" + (i |> getPropName), left), [|Unit|])
                        (*let node = AstHelpers.getMemberAccess ("get_" + AstHelpers.cleanName(i.Name), i, false)
                        match node with
                        | Variable(a) -> MemberAccess(a, left)
                        |_ -> MemberAccess("get_" + AstHelpers.cleanName(i.Name), left)*)
                else
                    let node =
                        if isGetSetIgnore i then
                            getMemberAccess ( getPropName i, i, false)
                        else
                            // check if its not a module
                            // module properties would just be a declaration, static type properties are functions
                            if not(FSharpType.IsModule(i.DeclaringType)) then
                                Call(getMemberAccess ("get_" + cleanName(i.Name), i, false), [|Unit|])
                            else
                                getMemberAccess ("get_" + cleanName(i.Name), i, false)
                    match node with
                    | Variable(a) -> node
                    |_ -> node

            | Patterns.PropertyGet(l,i, args) ->
                let left = if l.IsSome then Some(traverse l.Value map) else None
                // NOTE: important that indexers are treated automatically as IndexAccess
                if i.Name = "Item" && (i.DeclaringType = typeof<Array> || isIgnoreItemAccess(i.DeclaringType)) then
                    let r = traverse args.Head map
                    IndexAccess(left |> Option.get, r)
                else
                    let ar = [|for a in args do yield traverse a map|] |> Array.rev
                    if i.DeclaringType.IsInterface then
                        Call(MemberAccess(i.DeclaringType.Name + "_" + "get_" + i.Name |> cleanName, left.Value), ar)
                    else
                        if isGetSetIgnore i then
                            MemberAccess( i |> getPropName, left.Value)
                        else
                            Call(MemberAccess("get_" + (i |> getPropName), left.Value), ar)

            | Patterns.VarSet(x, y) ->
                Assign(Variable(x.Name), traverse y map)

            | Patterns.PropertySet(var, pi, exps, c) ->
                //let memberAccess = MemberAccess("set_" + AstHelpers.cleanName(pi.Name), a')
                // setters with let, mostly mutable setters require a wrapper function to create the method
                let value =
                    match c with
                    | Patterns.Let(_,_,_) ->
                        let e = Expr.Lambda(Var("unitVar0", typeof<unit>), c)
                        let call = traverse e map
                        Call(call, [||])
                    | _            -> traverse c map
                match var with
                | Some(a) ->
                    let a' = traverse a map
                    if pi.Name = "Item" && (pi.DeclaringType = typeof<Array> || isIgnoreItemAccess(pi.DeclaringType)) then
                        let r = traverse exps.Head map
                        Assign(IndexAccess(a', r), value)
                    else
                        // property setter in types
                        let mi =
                            if isGetSetIgnore pi then
                                MemberAccess( pi |> getPropName, a')
                            else
                                MemberAccess("set_" + cleanName(pi.Name), a')
                        if isGetSetIgnore pi then
                            Assign(mi, value)
                        else
                            Call(mi, [|value|])
                | _ ->
                    // this part is for properties set in F# modules
                    if isGetSetIgnore pi then
                        let mi = MemberAccess(pi |> getPropName, getDeclaredTypeName(pi.DeclaringType, "") |> Variable)
                        Assign(mi, value)
                    else
                        let mi = MemberAccess("get_" + cleanName(pi.Name), getDeclaredTypeName(pi.DeclaringType, "") |> Variable)
                        Assign(mi, value)

            | Patterns.TryWith(a, b, c, d, e) ->
                let tryBody = traverse a map |> rewriteBodyWithReturn |> transformClosure
                let withBlock =
                    // possibility of having a reraise in a catch block
                    match traverse e map |> cleanBlock with
                    | Block(a) ->
                        let rewrite n =
                            match n with
                            | Call(MemberAccess("Reraise", _), _) -> Throw(Variable(b.Name))
                            | x                                   -> x
                        Block(a |> Array.map rewrite)
                    | x         -> x
                    |> transformClosure
                let catch = Variable(b.Name), withBlock
                Try(tryBody, Some(catch), None) |> wrapClosureCall
                //Call(Function(None, [||], Try(tryBody, Some(catch), None)), [||])

            | Patterns.TryFinally(l, r) ->
                let tryBody = traverse l map |> rewriteBodyWithReturn |> transformClosure
                let finallyBody = traverse r map |> transformClosure
                Try(tryBody, None, Some(finallyBody)) |> wrapClosureCall
                //Call(Function(None, [||], Try(tryBody, None, Some(finallyBody))), [||])

            | Patterns.NewDelegate(t, vList, e) ->
                let body = traverse e map |> rewriteBodyWithReturn
                //if not(isIgnoreTupleArgs t) then
                Function(None, vList |> List.map(fun v -> Variable(v.Name)) |> List.toArray, body)
                // NOTE: dont remove the below code, I just figured out we dont need the attribute with directly specifying in delegates, but keeping the below code if something breaks
                (*else
                    let getVars(n:Node) =
                        match n with
                        | Block(a) ->
                            let declares,rest = a |> Array.partition(fun t-> match t with | DeclareStatement(_, MemberAccess(_,Variable("tupledArg"))) -> true | _ -> false)
                            let vars = declares |> Array.map(fun t -> match t with | DeclareStatement(Variable(name), _) -> Variable(name) | _ -> Unit)
                            vars, Block(rest)
                        | _        -> failwith "Block Node expected"

                    let vars, rest = getVars(body)
                    Function(None, vars, rest)*)

            | Patterns.NewArray(t, vList) ->
                NewArray(vList |> List.map(fun v -> traverse v map) |> List.toArray)

            | Patterns.FieldGet(l,i) ->
                match l with
                | Some(f) ->
                    //let left = traverse f
                    let left = Variable("this")
                    MemberAccess(i.Name |> cleanName, left)
                | None -> getMemberAccess (i.Name, i.DeclaringType, true)

            | Patterns.FieldSet(f, pi, c) ->
                match f with
                | Some(fi) ->
                    //let left = traverse fi
                    let m = MemberAccess(pi.Name |> cleanName, Variable("this"))
                    Assign(m, traverse c map)
                | None -> Unit

            | Patterns.NewUnionCase(i, l) ->
                New(Variable(getDeclaredTypeName(i.DeclaringType, i.Name)) , [|for a in l do yield traverse a map|])

            | Patterns.UnionCaseTest(expr, info) ->
                let left = traverse expr map
                InstanceOf(left, Variable(getDeclaredTypeName(info.DeclaringType, info.Name)))

            | Patterns.TypeTest(expr, t) ->
                let left = traverse expr map
                match t.Name with
                | "Int32" | "Double" -> Equality(TypeOf(left), QuotedVariable("number"))
                | "String"           -> Equality(TypeOf(left), QuotedVariable("string"))
                | _ ->
                    if not(t.IsInterface) then
                        InstanceOf(left, Variable(getDeclaredTypeName(t, String.Empty)))
                    else
                        let intVar = QuotedVariable(("__get" + t.Name) |> cleanName)
                        Call(MemberAccess("containsInterface", left), [| intVar |])
                        (*let intTy = MemberAccessNode(Call(Variable(("__get" + t.Name) |> cleanName), [|Unit|]), left)
                        Call(MemberAccess("isInterfaceOf", intTy), [| New(Variable(getDeclaredTypeName(t, String.Empty)|> cleanName), [||]) |])*)

            | _ ->
                failwith "unrecognized sequence"
                Ignore

        traverse quote Map.empty