namespace Pit.Compiler

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.ExprShape
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns
open System.Reflection
open Microsoft.FSharp.Reflection
open System
open Pit

module Array =

    let take (count: int) (a : 'a[]) =
        let arr : 'a[] = Array.zeroCreate count
        Array.Copy(a, arr, count)
        arr

    let skip (index:int) (a : 'a[]) =
        let arr : 'a[] = Array.zeroCreate (a.Length - index)
        Array.Copy(a, index, arr, 0, arr.Length)
        arr

[<AutoOpen>]
module Utils =

    let (++) array1 array2  = array1 |> Array.append array2
    let str format          = Printf.kprintf (sprintf "%s") format
    let (?) (t:MemberInfo) ty =
        let attr = t.GetCustomAttributes(ty,false)
        if attr.Length > 0 then (attr.[0] :?> 'a) |> Some else None

    let camelCase (input:string) =
        if input.Length = 0 then
            input
        else
            let first = (input.Substring(0, 1)).ToUpper()
            first + (input.Substring(1, input.Length - 1))

    let pascalCase (input:string) =
        if input.Length = 0 then
            input
        else
            let first = (input.Substring(0, 1)).ToLower()
            first + (input.Substring(1, input.Length - 1))

    let cleanName (n:string) = if n = null then "" else n.Replace("|", "").Replace("`", "").Replace("'", "1").Replace("''", "2").Replace("@", "").Replace("Microsoft.FSharp", "Pit.FSharp").Replace("System", "Pit")

    let removeLast (c:string) (s:string) =
        s.Substring(0,s.LastIndexOf(c))

    let getMethods (t:Type) =
        t.GetMethods(BindingFlags.Public ||| BindingFlags.NonPublic ||| BindingFlags.Static ||| BindingFlags.DeclaredOnly ||| BindingFlags.Instance)
        |> Array.filter(fun t->not(t.IsSpecialName) || (t.IsSpecialName && t.Name.Contains("op_")))

    let getOpOverload (md: MethodInfo) =
        if md.Name.Contains("op_") then
            let t = md.ReturnType |> getMethods |> Array.filter(fun ty-> ty.IsStatic)
            match t |> Array.tryFind(fun m -> m.Name.Equals(md.Name)) with
            | Some(m) -> Some(m)
            | _       -> None
        else None

    let isOpOverload (md: MethodInfo) = md |> getOpOverload |> Option.isSome

    let isOfType(t : System.Type, bType : Type) =
        let rec recOfType(t1 : System.Type) =
            if t1.IsSubclassOf(bType) then
                true
            else if t1.BaseType <> null && t1.BaseType <> typeof<Object> then
                recOfType(t1.BaseType)
            else
                false
        recOfType(t)

    let getCompileToAttribute (t:MemberInfo) =
        let c: CompileToAttribute option = t?(typeof<CompileToAttribute>)
        c

    let getMethodName (md:MethodInfo) =
        match getCompileToAttribute(md) with
        | Some(c) -> c.Name
        | _       -> md.Name
        |> cleanName

    let getPfAttributes (t: MemberInfo) =
        if t <> null then
            t.GetCustomAttributes(false) |> Array.filter(fun e -> e.GetType() = typeof<AliasAttribute> || e.GetType() = typeof<CompileToAttribute>)
        else
            [||]

    let getJsIgnoreAttr (t:MemberInfo) =
        let attr = t.GetCustomAttributes(typeof<JsIgnoreAttribute>, false)
        if attr.Length > 0 then Some(attr.[0] :?> JsIgnoreAttribute) else None

    let isIgnoreTupleArgs (md:MemberInfo) =
        match getJsIgnoreAttr(md) with
        | Some(js) -> js.IgnoreTuple
        | _        -> false

    let isIgnoreCall (md:MemberInfo) =
        match getJsIgnoreAttr(md) with
        | Some(js) -> js.IgnoreCall
        | _        -> false

    let isIgnoreItemAccess (t:Type) =
        match getJsIgnoreAttr(t) with
        | Some(js) -> js.IgnoreItemAccess
        | _        -> false

    let getIsUnionCaseNullary (t:Type) =
        let attr = t.GetCustomAttributes(typeof<CompilationRepresentationAttribute>, false)
        if attr.Length > 0 then
            let a = attr.[0] :?> CompilationRepresentationAttribute
            a.Flags = CompilationRepresentationFlags.UseNullAsTrueValue
        else
            false

    let isJsExtensionType (md:MethodInfo) =
        let attr = md.GetCustomAttributes(typeof<JsExtensionTypeAttribute>,false)
        if attr.Length > 0 then true
        else
            let attr = md.DeclaringType.GetCustomAttributes(typeof<JsExtensionTypeAttribute>,false)
            if attr.Length > 0 then true else false

    let getAliasAttr (t:Type) =
        let attr =
            t.GetCustomAttributes(false)
            |> Array.filter(fun o->o.GetType().Name = (typeof<AliasAttribute>).Name)
        match attr.Length with
        | 1 -> Some(attr.[0] :?> AliasAttribute)
        | _ -> None

    let isIgnoreTypeName (md: MemberInfo) =
        match getJsIgnoreAttr(md) with
        | Some(j) -> j.IgnoreTypeName
        | _       -> false

    let parseFullName (t: Type) name =
        let rec parseName (ty:Type) acc =
            if ty.DeclaringType <> null then
                match getAliasAttr ty.DeclaringType with
                | Some(a)   -> parseName ty.DeclaringType (if acc <> String.Empty then str "%s.%s" a.Name acc else a.Name)
                | None      -> parseName ty.DeclaringType (if acc <> String.Empty then str "%s.%s" ty.Name acc else t.Name)
            else
                match getAliasAttr ty with
                | Some(a)   -> if acc <> String.Empty then str "%s.%s" a.Name acc  else a.Name
                | None      -> if acc <> String.Empty then str "%s.%s" ty.Name acc  else ty.Name

        match getJsIgnoreAttr t with
        | Some(j) when j.IgnoreNamespace = true -> t.Name
        | Some(j) when j.IgnoreTypeName  = true -> ""
        | _ ->
            match t.Namespace with
            | null -> parseName t name
            | x -> str "%s.%s" x (parseName t name)

    let getDeclaredTypeName(t: Type, name: string) =
        let ca = getPfAttributes t |> Array.filter(fun a -> a :? AliasAttribute)
        if ca.Length > 0 then
            let a = ca.[0] :?> AliasAttribute
            // checking of IgnoreNamespace on the type
            match getJsIgnoreAttr t with
            | Some(j) when j.IgnoreNamespace = true && not(j.IgnoreTypeName) -> a.Name
            | _ ->
                if t.FullName <> null then
                    parseFullName t name
                else
                    if String.IsNullOrEmpty(name) then str "%s.%s" t.Namespace a.Name else str "%s.%s.%s" t.Namespace a.Name name
        else
            parseFullName t name |> cleanName

    let getDeclaredType (t:Type) =
        let declTy = if t.DeclaringType <> null then t.DeclaringType else t
        getDeclaredTypeName(declTy, if t.DeclaringType <> null then t.Name else String.Empty)

    let private getMemberAccess2 (name:string, t:System.Type, nameSpace:string) =
        let rec loop (typ:System.Type) =
            if typ.DeclaringType = null then
                let name = if nameSpace = "" || nameSpace = null then typ.Name else nameSpace + "." + typ.Name
                Variable(cleanName (name))
            else
                MemberAccess(cleanName typ.Name, loop typ.DeclaringType)

        if t = null then
            let name = if nameSpace = "" || nameSpace = null then name else nameSpace + "." + name
            Variable(cleanName name)
        else
            match getJsIgnoreAttr t with
            | Some(j) when j.IgnoreNamespace = true && not(j.IgnoreTypeName) -> MemberAccess(name, Variable(t.Name))
            | Some(j) when j.IgnoreTypeName  = true -> Variable(name)
            | _                                     -> MemberAccess(cleanName name, loop t)

    let private getMemberAccess3 (name:string, pType:System.Type, tType:System.Reflection.MemberInfo) =
        let ca = [| box(pType); box(tType) |] |> Array.filter(fun t -> t <> null) |> Array.map(fun t -> getPfAttributes (unbox<MemberInfo>(t))) |> Array.concat
        if  ca.Length > 0 then
            match ca.[0] with
            | :? AliasAttribute ->
                let aliasAttrib = ca.[0] :?> AliasAttribute
                let getType (m:MemberInfo) =
                    match m.MemberType with
                    | MemberTypes.Property -> (m :?> PropertyInfo).DeclaringType
                    | MemberTypes.Method   -> (m :?> MethodInfo).DeclaringType
                    | _                    -> m :?> Type
                let ty = getType (tType)
                let nameSpace =
                    match getJsIgnoreAttr(ty) with
                    | None      -> ty.Namespace
                    | Some _    -> ""
                let name = if ca.Length > 1 && tType.MemberType = MemberTypes.Method then (sprintf "%s.%s" aliasAttrib.Name ((ca.[1]:?>CompileToAttribute).Name)) else aliasAttrib.Name
                getMemberAccess2 (name, null, nameSpace)
            | :? CompileToAttribute ->
                let compileAttrib = ca.[0] :?> CompileToAttribute
                match tType.MemberType with
                | MemberTypes.Method ->
                    let md = tType :?> MethodInfo
                    let isIgnoreType = isIgnoreTypeName(md.DeclaringType)
                    let name = if md.IsStatic && not(isIgnoreType) then (sprintf "%s.%s" (getDeclaredTypeName(md.DeclaringType,"")) compileAttrib.Name) else compileAttrib.Name
                    getMemberAccess2 (name, null, null)
                | _ ->
                    getMemberAccess2 (compileAttrib.Name , null, null)
            | _ -> getMemberAccess2 (name, tType.DeclaringType, tType.DeclaringType.Namespace)
        else getMemberAccess2 (name, tType.DeclaringType, if tType.DeclaringType <> null then tType.DeclaringType.Namespace else "")

    let getMemberAccess (name:string, t:System.Reflection.MemberInfo, isUnderlyingType : bool) =
        if t <> null then
            match t.MemberType with
            | MemberTypes.Property ->
                let p = t :?> System.Reflection.PropertyInfo
                getMemberAccess3(name, p.PropertyType, p)
            | MemberTypes.Method ->
                let m = t :?>  System.Reflection.MethodInfo
                match isIgnoreTypeName(t) with
                | false -> getMemberAccess3(name, m.DeclaringType, m)
                | true  -> Variable(name)
            | MemberTypes.Constructor ->
                let c = t :?> System.Reflection.ConstructorInfo
                getMemberAccess3(name, c.DeclaringType, c)
            | MemberTypes.TypeInfo | MemberTypes.NestedType ->
                getMemberAccess3(name, t.DeclaringType, t)
            | _ ->
                if not(isUnderlyingType) then
                    getMemberAccess2 (name, t.DeclaringType, if t.DeclaringType <> null then t.DeclaringType.Namespace else "")
                else
                    let tp = t :?> Type
                    getMemberAccess2 (name, tp, tp.Namespace)
        else
            getMemberAccess2(name, null, null)

    let getClassInfo (t:Type) =
        let declTy = if t.DeclaringType <> null then t.DeclaringType else t
        getDeclaredTypeName(declTy, if t.DeclaringType <> null then t.Name else String.Empty)

    let getPropName (p:PropertyInfo) =
        let compileToAttr = p.GetCustomAttributes(typeof<CompileToAttribute>, false)
        if compileToAttr.Length > 0 then
           let c = compileToAttr.[0] :?> CompileToAttribute
           c.Name
        else
            p.Name |> cleanName

    let isGetSetIgnore (p:PropertyInfo) =
        match getJsIgnoreAttr(p) with
        | Some t -> t.IgnoreGetSet
        | _      ->
            match getJsIgnoreAttr(p.DeclaringType) with
            | Some(t) -> t.IgnoreGetSet
            | _       -> false

    let isJsObject(t : Type) =
        if t <> null then
            let jsTypeAttributes = t.GetCustomAttributes(typeof<JsObjectAttribute>, false)
            if jsTypeAttributes.Length > 0 then true else false
        else
            false

    let isTypeEqual(t: Type, compareWith: Type) =
        let interfaces = t.GetInterfaces()
        if interfaces.Length > 0 then match interfaces |> Array.tryFindIndex(fun i -> i = compareWith) with Some _ -> true | None   -> t = compareWith
        else t = compareWith

    let isUnionOrRecordType(exps:Expr[]) =
        match exps with
        | [|var1; var2|] ->
            if   FSharpType.IsRecord(var1.Type) && FSharpType.IsRecord(var2.Type) then true
            elif FSharpType.IsUnion(var1.Type) && FSharpType.IsUnion(var2.Type) then true
            else false
        | _ -> false

[<AutoOpen>]
module AstHelpers =

    let rewriteBody body =
        match body with
        | Block(children) ->
            let rec mashBlocks c =
                [| for b in c do yield! match b with
                                                | Block(l) -> mashBlocks l
                                                | _ -> [|b|]|]
            let resultBlock = Block(mashBlocks children)
            resultBlock
        | _ -> body

    let wrapReturn (n: Node) =
        match n with
        | Node.Unit                        -> n
        | Return(r)                        -> n
        | Throw(t)                         -> n
        | ForIntegerUpLoop(_, _, _, _)     -> n
        | While(_,_)                       -> n
        | _                                -> Return(n)

    let wrapClosureCall (n:Node) =
        Call(Function(None, [|Variable("thisObject")|], n) |> Closure, [|Variable("this")|])

    let wrapClosureCall2 (args: Node[]) (body:Node) =
        Call(Function(None, args ++ [|Variable("thisObject")|], body) |> Closure, args ++ [|Variable("this")|])

    let isCallPattern (e: Expr) =
        match e with
        | Patterns.Call(_, _, _)    -> true
        | _                         -> false

    let rewriteBodyWithReturn body =
        match body with
            | Block(a) ->
                let bodyNode = rewriteBody body
                match bodyNode with
                | Block(a) ->
                    let lastItem = a.[a.Length - 1] |> wrapReturn
                    Block([|lastItem|] |> Array.append (a |> Array.take(a.Length - 1)))
                | _ -> body
            | _ -> wrapReturn body

    let cleanBlock block =
        match rewriteBody block with
        | Block(a)  -> Block(a |> Array.filter(fun n -> n <> Node.Null))
        | _         -> block

    let getJsFunction func =
        match func with
        | "ToString"    -> Some "toString"
        //| "ToLower"     -> Some "toLowerCase"
        //| "ToUpper"     -> Some "toUpperCase"
        | _             -> None

    let getDefaultValue (t:PropertyInfo) =
        match t.PropertyType with
        | x when x = typeof<Int32> -> Int(Some(0))
        | x when x = typeof<float> -> Int(Some(0))
        | x when x = typeof<string> -> StringNode(Some("\"\""))
        | x when x = typeof<bool> -> BooleanNode(Some false)
        | _ -> Null

    let getBaseType (startingType:System.Type) =
        let rec innerGet (typ:System.Type) =
            if typ.BaseType = null || typ.BaseType.Name = "Object"
            then typ
            else
                innerGet typ.BaseType

        innerGet startingType

    let isJsIgnoreType (t:Type) = t |> getJsIgnoreAttr |> Option.isSome

    let isBaseTypeUnion (t:Type) =
        match t.BaseType with
        | null -> false
        | x    -> FSharpType.IsUnion(x)

    let isBaseTypeModule (t:Type) =
        match t.DeclaringType with
        | null -> false
        | x    -> FSharpType.IsModule(x)

    let isDomEntryMethod (m:MethodInfo) =
        let c = m.GetCustomAttributes(typeof<DomEntryPointAttribute>, false)
        c <> null && c.Length = 1

    let parseRec args =
        let rec loop (e:Expr) (lets:Map<Var,Expr>) =
            match e with
            | Patterns.NewRecord(r, fields) ->
                let mfields =
                    fields |> List.mapi(fun i x ->
                        match x with
                        | PropertyGet(Some(Var(v)), p, []) when lets.ContainsKey(v) ->
                            Expr.PropertyGet(lets.[v], p)
                        | Var(v) when lets.ContainsKey(v) -> lets.[v]
                        | _ -> x
                    )
                Expr.NewRecord(r, mfields)
            | Patterns.Let(x, y, z)         -> loop z (lets |> Map.add x y)
            | _                             -> e
        [for a in args -> loop a Map.empty]

    let parseValue (x:obj) (y:Type) =
        match x with
        | :? bool   -> BooleanNode(Some(x :?> bool))
        | :? int    -> Int(Some(x :?> int))
        | :? string -> StringNode(Some(x :?> string))
        | :? char   -> StringNode(Some(x :?> char |> string))
        | :? float  -> Float(Some(x :?> float))
        | _ -> if x <> null then MemberAccess(x.ToString(), getMemberAccess(y.Name, y.DeclaringType, true)) else Node.Null //failwith "Not supported literal type"

#if SILVERLIGHT
    [<AutoOpen>]
    module EnumExtensions =
        open System.Reflection

        type Enum with
            static member GetValues(ty: Type) =
                let fi = ty.GetFields(BindingFlags.Public ||| BindingFlags.Static)
                let array = Array.CreateInstance(ty, fi.Length)
                for i = 0 to fi.Length - 1 do
                    array.SetValue(fi.[i].GetValue(null), i)
                array
#endif