namespace Pit.Compiler

open System
open System.Reflection
open System.Collections.Generic
open System.ComponentModel
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Reflection
open Pit

module TypeParser =

    let getFilteredTypes (types: Type[]) =
        types |> Array.filter(fun t -> not(isOfType(t, typeof<Delegate>)) && not(isOfType(t, typeof<Attribute>)) && not(isJsIgnoreType(t)) && not(isJsObject(t)) && not(isBaseTypeUnion(t)))

    let getAllFilteredTypes (types: Type[]) =
        types |> getFilteredTypes |> Array.filter(fun t-> not(isBaseTypeModule(t)))

    let getNestedTypes (t: Type) =
        t.GetNestedTypes(BindingFlags.Instance ||| BindingFlags.Static ||| BindingFlags.Public) |> getFilteredTypes

    let getNamespaces (types:System.Type[])=
        // get unique namespaces
        let moduleTypes, normalTypes =
            types
            |> Array.filter(fun t -> t.Namespace <> null && not(isJsIgnoreType t))
            |> Array.partition(fun t -> FSharpType.IsModule(t) || FSharpType.IsUnion(t))

        moduleTypes |> Seq.map(fun t -> getDeclaredTypeName(t, String.Empty))
        |> Seq.append (normalTypes |> Seq.map(fun t->t.Namespace))
        |> Seq.distinct
        |> Seq.map(fun t->
            Block( [| Call (Variable ("registerNamespace"), [|QuotedVariable(t)|]) |])
        )
        |> Seq.toArray

    let getQuotedExprForMethods(md: MethodInfo[]) =
        [| for m in md do
            match Microsoft.FSharp.Quotations.Expr.TryGetReflectedDefinition(m) with
            | Some(rd) -> yield (m,rd)
            | _        -> ()
        |]

    let getEqualityFunction (memberAccess : Node) (parameters: string[]) =
        let initStatement = DeclareStatement(Variable("result"), BooleanNode(Some true))
        let typeStatemember = Assign(Variable("result"), And(Variable("result"), InstanceOf(Variable("compareTo"), memberAccess)))
        let getBlock (p:string) = Assign(Variable("result"),
                                            And(Variable("result"),
                                                Equality( MemberAccess("get_" + camelCase p + "()", Variable("this")), MemberAccess("get_" + camelCase p + "()", Variable("compareTo")))))
        let blocks = parameters |> Array.map getBlock
        Function(None, [|Variable("compareTo")|], Block( [|Return(Variable("result"))|] |> Array.append blocks |> Array.append [|initStatement|] ))

    let getFields (t: Type) =
        t.GetFields(BindingFlags.DeclaredOnly|||BindingFlags.Public|||BindingFlags.Instance|||BindingFlags.NonPublic)

    let getFieldMembers (t:Type) =
        let getDefValue (fi: FieldInfo) =
            match fi.FieldType with
            | x when x = typeof<Int32>  -> Int(Some(0))
            | x when x = typeof<float>  -> Int(Some(0))
            | x when x = typeof<string> -> StringNode(Some("\"\""))
            | x when x = typeof<bool>   -> BooleanNode(Some false)
            | _ -> Null
        getFields t
        |> Array.map(fun fi->
            Assign(MemberAccess(fi.Name |> cleanName, Variable("this")), getDefValue(fi))
        )

    module Property =
        // strip extension method type property gets
        let strip (exp: Expr) =
            match exp with
            | Patterns.Lambda(v, e) when not(v.Name.Contains("unitVar")) -> e
            | _ -> exp

        let createGet  (useFullNamespace:bool) (t:System.Type) (property:PropertyInfo)=
            let def = (Microsoft.FSharp.Quotations.Expr.TryGetReflectedDefinition(property.GetGetMethod()))
            let ast =
                if def.IsSome then
                    def.Value
                    |> strip
                    |> AstParser.getAst
                    |> Some
                else None
            let memberAccess =
                if useFullNamespace then
                    match getMemberAccess(t.Name, t, false) with
                    | Variable(_) -> Variable(getDeclaredTypeName(t, "")) // we can also get a variable, get the proper type for this
                    | x           -> x
                else Variable(t.Name |> cleanName)
            let node =
                if ast.IsSome then
                    ast.Value
                else
                    Function(None, [|Unit|], Block([|Return(Variable("this." + property.Name))|]))
            if not(property.GetGetMethod().IsStatic) then
                Assign(MemberAccess("get_" + property.Name, MemberAccess("prototype", memberAccess)), node)
            else
                Assign(MemberAccess("get_" + property.Name, memberAccess), node)

        let createSet  (t:System.Type) (property:PropertyInfo)=
            let setMethod = property.GetSetMethod()
            let setMethod = if setMethod = null then None else Some setMethod
            let def = if setMethod.IsSome then (Microsoft.FSharp.Quotations.Expr.TryGetReflectedDefinition(setMethod.Value)) else None
            let ast =
                if def.IsSome then
                    def.Value
                    |> strip
                    |> AstParser.getAst
                    |> Some
                else None
            Assign(MemberAccess("set_" + property.Name,
                                        MemberAccess("prototype",
                                                Variable(cleanName(t.Name)))),
                                                if ast.IsSome then
                                                    ast.Value
                                                else
                                                    Function(None, [|Variable("x")|], Block([|Assign(Variable("this." + property.Name), Variable("x"))|])))
    module Interface =
        module Empty =
            let getAst (t:Type) =
                // interface AST just contains empty function structure
                // interface methods
                let md = t.GetMethods(BindingFlags.Public ||| BindingFlags.NonPublic ||| BindingFlags.Static ||| BindingFlags.DeclaredOnly ||| BindingFlags.Instance)
                let q =
                    [| for m in md do
                        yield (
                            Assign(MemberAccess(cleanName(m.Name), MemberAccess("prototype",Variable(cleanName(t.Name)))),
                                Function(None, [||], Block([| Unit |])) )
                        )
                    |]
                let classType = if t.DeclaringType <> null then t.DeclaringType else t
                let classInfo = getMemberAccess (getDeclaredTypeName(t, String.Empty), classType, true)
                [|Class(cleanName(t.Name), "", classInfo, [||], [||], q)|]

        let getAst (declType: Type) (t: Type) (useFullName:bool) =
            let interfaceCreate = "__create"
            let interfaceGet = "__get"
            let interfaceName = t.Name |> cleanName
            let className = (if useFullName then getDeclaredTypeName(declType, "") else declType.Name) |> cleanName
            let map = declType.GetInterfaceMap(t)
            // get quoted expressions for all interface methods
            let qm = getQuotedExprForMethods map.TargetMethods
            let interfaceTargetMethodMap = map.TargetMethods |> Array.mapi(fun i m -> (m, map.InterfaceMethods.[i]))

            let fnAst =
                [| for (m,q) in qm do
                    yield (
                        let ast =
                            match q with
                                | Patterns.Lambda(v, exp) -> exp |> ClassParser.parse m
                                | _ -> AstParser.getAst q
                        let interfaceMethod = interfaceTargetMethodMap |> Array.find(fun i -> fst i = m)
                        ast |> InterfaceParser.transformAst, interfaceMethod
                    )
                |]

            let functions =
                [| for (ast,interfaceMethod) in fnAst do
                    yield Assign(MemberAccess(cleanName((snd interfaceMethod).Name), MemberAccess("prototype", Variable(interfaceName))), ast)
                |]

            let interfaceType =
                DeclareStatement(
                    Variable(interfaceName.ToLower()),
                        Class(interfaceName, "", Unit,
                            [|Assign(MemberAccess("x", Variable("this")), Variable("thisObject"))|], // assigning the global function object to x, this.x=thisObject
                                [|Variable("thisObject")|], functions))

            //AST for create function
            let createFun =
                Function(
                    Some(interfaceCreate+interfaceName),
                        [|Variable("thisObject")|],
                            Block([|interfaceType; Return(New(Variable(interfaceName.ToLower()), [|Variable("thisObject")|]))|]))

            //AST for get interface function
            let getFunWrapperAst (fntyp:MethodInfo) pr =
                let fnAst = Function(None, pr, Return( MemberAccessNode(Call(Variable(fntyp.Name), pr), Variable("this." + interfaceName))))
                // generating wrappers as interfacename_methodname
                Assign(MemberAccess(fntyp.DeclaringType.Name + "_" + fntyp.Name |> cleanName, MemberAccess("prototype", Variable(className))), fnAst)
            // checking if the Js marked functions and interfaces declared are same,
            if fnAst.Length = map.InterfaceMethods.Length then
                //AST for wrapper functions in the main class
                let wrapFuns =
                    map.InterfaceMethods
                    |> Array.mapi(fun i m ->
                            let fnPr, _ = fnAst.[i]
                            match fnPr with
                            | Function(s, pr, body) -> getFunWrapperAst m pr
                            | _ -> failwith "no function body"
                    )

                let ctorMember = Assign( MemberAccess(interfaceName, Variable(("this"))), Call(Variable(interfaceCreate+interfaceName), [|Variable("this")|]))

                let getInterfaceFun =
                    Assign(
                        MemberAccess(interfaceGet+interfaceName,
                            MemberAccess("prototype", Variable(className))),
                                Function(None, [|Unit|], Return( MemberAccess(interfaceName, Variable("this")))))

                (ctorMember, wrapFuns ++ [| createFun;getInterfaceFun |]) |> Some
            else
                None

    module Class =

        let getAst (t:Type) nestedTypes =
            let interfaces = t.GetInterfaces()
            let ifm =
                interfaces
                |> Array.map(fun i -> t.GetInterfaceMap(i).TargetMethods)
                |> Array.concat
            let ictors, imembers =
                interfaces
                |> Array.choose(fun i -> Interface.getAst t i false)
                |> Array.unzip
            let imembers = imembers |> Array.concat

            // ctors
            let ctors = t.GetConstructors() |> Array.toList
            let defCtor = if ctors.Length > 0 then Some(ctors.Head) else None
            let ctorParameters = if defCtor.IsSome then [|for p in defCtor.Value.GetParameters() do yield Variable(p.Name)|] else [||]
            let ctorsAst =
                if ctors.Length > 1 then
                    if not(FSharpType.IsRecord(t)) then Some(CtorParser.createCtorsAst(ctors.Tail, t)) else None
                else None

            let properties = t.GetProperties(BindingFlags.DeclaredOnly|||BindingFlags.Public|||BindingFlags.Instance|||BindingFlags.Static)
            let defCtorMember =
                if not(FSharpType.IsRecord(t)) then
                    match defCtor with
                    | Some(d) ->
                        match Expr.TryGetReflectedDefinition(d) with
                        | Some(q) ->
                            match q with
                            | Patterns.Lambda(v, exp) ->
                                CtorParser.getCtorBody exp
                                |> CtorParser.getAst true
                                |> CtorParser.transformDefaultCtor
                                |> cleanBlock
                            | _                       -> failwith "un-recognizable ctor"
                        | None -> CtorParser.getCtorMembers ctorParameters properties
                    | None -> Unit
                else
                    CtorParser.getCtorMembers ctorParameters (FSharpType.GetRecordFields(t, BindingFlags.Public ||| BindingFlags.NonPublic))
            let ctorMembers = [|defCtorMember|] |> Array.append ictors

            // class methods
            let clm =
                t
                |> getMethods
                |> Array.filter(fun m1 -> ifm |> Array.tryFind(fun t -> t = m1) |> Option.isNone)
            let quotesAndMethods = getQuotedExprForMethods clm
            let q =
                    [|for (m,q) in quotesAndMethods |> Array.filter(fun (m1, q1) -> not(m1.IsStatic)) do
                        yield(
                                let ast =
                                    match q with
                                    | Patterns.Lambda(v, exp) -> exp |> ClassParser.parse m
                                    | _                       -> AstParser.getAst q
                                Assign(MemberAccess(getMethodName m, MemberAccess("prototype",Variable(cleanName(t.Name)))), ast)
                    )|]

            let declTy = if t.DeclaringType <> null then t.DeclaringType else t
            let classInfo = getDeclaredTypeName(declTy, if t.DeclaringType <> null then t.Name else String.Empty) |> Variable //AstHelpers.getMemberAccess (AstHelpers.getDeclaredTypeName(declTy, if t.DeclaringType <> null then t.Name else String.Empty), declTy, true)
            let staticMembers =
                [| for (m, q) in quotesAndMethods |> Seq.filter(fun (m1, q1) -> m1.IsStatic) do
                    yield Assign(MemberAccess(getMethodName m, classInfo), ClassParser.parse m q)
                |]
            let getProps = properties |> Array.map (Property.createGet false t)
            let setProps = properties |> Array.filter(fun p -> p.CanWrite) |> Array.map (Property.createSet t)
            let block    = getProps |> Array.append setProps |> Array.append imembers |> Array.append q
            let baseType = getBaseType t
            let inherits = if baseType = t then String.Empty else baseType.FullName |> cleanName //_getAccessVariable(baseType.Name, baseType.DeclaringType, baseType.Namespace)
            let getCtors = if ctorsAst.IsSome then ctorsAst.Value else [||]

            staticMembers |> Array.append getCtors |> Array.append [|Class(cleanName(t.Name), inherits, classInfo, ctorMembers, ctorParameters, block)|] |> Array.append nestedTypes

    module Module =

        let getAst (t:Type) nty =
            let baseType = getBaseType t
            let inherits = if baseType = t then String.Empty else baseType.FullName
            let quotesAndMethods = getQuotedExprForMethods(t.GetMethods(BindingFlags.Public ||| BindingFlags.NonPublic ||| BindingFlags.Static ||| BindingFlags.DeclaredOnly ||| BindingFlags.Instance))
            let moduleName = getDeclaredTypeName(t, String.Empty)
            let result =
                [|for (m, q) in quotesAndMethods do
                    yield(
                        // always the first parameter is the extension object
                        match AstParser.getAst q with
                        | Function(None, args, b) when m.Name.Contains(".") ->
                            // extension methods support
                            // first param is always the object itself
                            let m1 = m.GetParameters().[0].ParameterType
                            let split = m.Name.Split('.')
                            // stripping unwanted Block(return)
                            let func = match b with
                                            | Block([|Return x|]) ->
                                                match x with
                                                    | Function(None, t, y) ->
                                                        Function(None, t |> Array.append args, y)
                                                    | _ -> x
                                            | _ -> Function(None, args, b)
                            let declTy = if m1.DeclaringType <> null then m1.DeclaringType else m1
                            let mi = MemberAccess(m1.Name, MemberAccess(moduleName, Variable(declTy.Name)))
                            Assign(MemberAccess(split.[1], mi), func)
                        | Function(None, args, b) when isDomEntryMethod(m)=true ->
                            Block([|Call(MemberAccess ("domReady",Variable ("DOM")),[|Function (None, [||], b)|])|])
                        | Function(None, args, b) when m.Name.Contains("|") -> // active pattern support
                            let name = m.Name.Replace("|", "")
                            Assign(MemberAccess(name, Variable(moduleName)), Function(None, args, b))
                        | x ->
                            let mi = MemberAccess(m.Name, Variable(moduleName))
                            Assign(mi, x)
                    )
                 |]

            let typesInModule =
                quotesAndMethods
                |> Seq.map fst
                |> Seq.filter(fun m -> m.Name.Contains("."))
                |> Seq.distinctBy(fun m -> m.GetParameters().[0].ParameterType)
                |> Seq.map(fun m ->
                    let m1 = m.GetParameters().[0].ParameterType
                    let declTy = if m1.DeclaringType <> null then m1.DeclaringType else m1
                    let mi = MemberAccess(m1.Name, MemberAccess(moduleName, Variable(declTy.Name)))
                    Class(m1.Name, inherits, mi, [||], [||], [||])
                )
                |> Seq.toArray

            [|nty; typesInModule; result|] |> Array.concat

    module Union =
        let getAst (t:Type) =
            let accessType =
                match getMemberAccess(t.Name, t, false) with
                | Variable(_) -> Variable(getDeclaredTypeName(t, "")) // we can also get a variable, get the proper type for this
                | x           -> x
            let unionCases = FSharpType.GetUnionCases(t)
            let rdr = [| for c in unionCases do yield FSharpValue.PreComputeUnionConstructorInfo c |]
            let rd  = [|for r in rdr do yield (r, r.GetParameters())|]

            let cleanName (name:string) =
                name.Replace("New", "").Replace("get_", "")

            let createPropertyGet2 (property:ParameterInfo, r:Node) =
                Assign(MemberAccess("get_" + camelCase property.Name,
                        MemberAccess("prototype", r)),
                            Function(None, [|Unit|], Block([|Return(Variable("this." + camelCase property.Name))|])))

            let createPropertyGet3 (property:PropertyInfo, t:Type) =
                let u = unionCases |> Seq.find(fun uty -> property.Name.Contains(uty.Name)) // get the union type here
                let isUnionCaseNullary = getIsUnionCaseNullary(u.DeclaringType)
                let ast =
                    match (Microsoft.FSharp.Quotations.Expr.TryGetReflectedDefinition(property.GetGetMethod())) with
                    | Some(d) -> d |> AstParser.getAst |> Some
                    | _       -> None
                Assign(
                    MemberAccess("get_" + property.Name, if not(isUnionCaseNullary) then MemberAccess("prototype", accessType) else accessType), //no prototype assignment here
                        if ast.IsSome then
                            ast.Value
                        else
                            Function(None, [|Unit|], Block([| Return(InstanceOf(Variable("this"), MemberAccessNode(Variable(u.Name), accessType))) |])))

            // main type properties, filter out the properties that is already generated in union cases
            let props = t.GetProperties() |> Array.filter( fun e -> unionCases |> Array.filter(fun e1 -> e1.Name = e.Name) |> Array.length = 0 )
            // create union properties as InstaceOf properties, and normal properties with normal expression
            let unionProps, normalprops = props |> Array.partition(fun p -> unionCases |> Array.exists(fun u -> p.Name.Contains(u.Name)))
            let propNodes =
                unionProps
                |> Array.map(fun u -> createPropertyGet3(u, t))
                |> Array.append (normalprops |> Array.map (Property.createGet true t))
            let ictors, imembers =
                t.GetInterfaces()
                |> Array.choose(fun i -> Interface.getAst t i true)
                |> Array.unzip
            let createFuns, imembers = // separating create functions and members for proper setup in the ctor body
                imembers
                |> Array.concat
                |> Array.partition(fun i -> match i with | Function(_,_,_) -> true | _ -> false)

            let unions =
                [| for (r, parameters) in rd do
                    yield!(
                        let name = cleanName r.Name
                        let values = [| for p in parameters do yield (Variable(p.Name), Assign(MemberAccess(camelCase p.Name, Variable("this")), Variable(p.Name))) |]
                        let ctor = Assign(MemberAccess(name, accessType),
                                        Function(None, [| for (par, _) in values do yield par |], Block( [| for(_, prop) in values do yield prop |] ++ ictors ++ createFuns))) //also appending interface initializations
                        let inheritance = Assign(MemberAccess("prototype", MemberAccess(name, accessType)),
                                                    New(accessType, [||]))
                        let memberAccess = MemberAccess(cleanName r.Name, accessType)
                        let equals = Assign(MemberAccess("equality", MemberAccess("prototype", memberAccess)),
                                            getEqualityFunction memberAccess (parameters |> Array.map(fun p -> p.Name)))
                        let props = parameters |> Array.map(fun p -> createPropertyGet2(p, memberAccess))
                        props ++ [|ctor; inheritance; equals;|]
                    )
               |]
            // get op_ overload methods, static, normal methods
            let ifm =
                t.GetInterfaces()
                |> Array.map(fun i -> t.GetInterfaceMap(i).TargetMethods)
                |> Array.concat
            let methods =
                getMethods t
                //|> Array.filter(fun m -> m.IsStatic && m.Name.StartsWith("op"))
                |> Array.filter(fun m1 -> ifm |> Array.tryFind(fun t -> t = m1) |> Option.isNone)
                |> getQuotedExprForMethods
                |> Array.map(fun (m,q) ->
                    Assign(MemberAccess(cleanName(m.Name), accessType), AstParser.getAst q)
                )

            // the main union type which is extended (acts as the abstract function type)
            let uType = [| Assign(accessType, Function(None, [|Unit|], Block([| for p in props do yield Assign(MemberAccess(camelCase p.Name, Variable("this")), getDefaultValue p)|]) ))|]
            // appending property nodes after union declarations as their might be extended properties implemented for a union type
            // Important point, don't append nestedtypes, since we evaluate the whole Union case expression including the nested types
            [|uType; imembers; unions; propNodes; methods|] |> Array.concat

    let getAstFromType (mo:System.Type) =
        let rec loop (t:Type) =
            if t.BaseType = typeof<Enum> then
                let values = Enum.GetValues(t) :?> int[]
                let enums = values |> Array.map (fun v -> Enum.GetName(t,v))
                let m = getMemberAccess(t.Name, t.DeclaringType, true)
                [|EnumNode(m, enums)|]

            elif FSharpType.IsModule t then
                let nestedTypes =
                    let nty =
                        getNestedTypes t in
                        [|getNamespaces nty; [|for ty in nty do yield! loop ty |];|]
                        |> Array.concat
                Module.getAst t nestedTypes

            elif FSharpType.IsUnion(t) then
                Union.getAst t

            elif t.IsInterface then
                Interface.Empty.getAst t
            else
                let nestedTypes = [|for ty in getNestedTypes t do yield! loop ty |]
                Class.getAst t nestedTypes

        loop mo

    let getAst(types : System.Type[]) =
        let fty = getAllFilteredTypes types
        let namespaces = getNamespaces fty
        [| for t in fty do
            yield getAstFromType t
        |]
        |> Array.append [|namespaces|]
        |> Array.concat