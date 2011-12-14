namespace PitTest
open System
open Pit
open System.Reflection
open Microsoft.FSharp.Reflection
open System.Text
open ListTests
open LetTests

module TestInit =

    [<Js>]
    let DoForTests() =
        ForTests.ForSimple()
        ForTests.ForFunctions()
        ForTests.ForInDeclare()
        ForTests.ForInDeclare2()


    [<Js>]
    let DoWhileTests() =
        WhileTests.WhileDeclare()

    [<Js>]
    let DoTupleTests() =
        TupleTests.TupleDecalre()
        TupleTests.TupleFst()
        TupleTests.TupleSnd()
        TupleTests.MixedTuple()
        TupleTests.TupleFunctions()
        TupleTests.TupleFunctions2()
        TupleTests.TupleFunctions3()
        TupleTests.TupleArrays()
        //TupleTests.TupleRecords()

//
//    [<Js>]
//    let DoRecordsTests() =
//        RecordsTests.RecordDeclare()
//        RecordsTests.RecordDeclare2()
//        RecordsTests.RecordDeclare3()
//        RecordsTests.RecordDeclare4()
//        RecordsTests.RecordPatternMatching()

    [<Js>]
    let DoUnionTests() =
        UnionTests.UnionDeclare()

    [<Js>]
    let DoPatternTests() =
        PatternTests.ConstantMatchTest()
        PatternTests.ConstantMatchTest2()
        PatternTests.IdentifierPattern()
        PatternTests.VariablePatternTest()
        PatternTests.AsPatternTest()
        PatternTests.OrPatternTest()
        PatternTests.AndPatternTest()
        PatternTests.ConsPatternTest()
        PatternTests.ListPatternTest()
        PatternTests.ParanthesizedPatternTest()
        PatternTests.TuplePatternTest()
        //PatternTests.RecordPatternTest()
        PatternTests.WildCardPatternTest()

    [<Js>]
    let DoDelegateTests() =
        DelegateTests.DeclareTest()
        
 

    [<Js>]
    let DoLetTests() =
        LetTests.LetDeclare()
        LetTests.Let()
        LetTests.Let3()
        LetTests.LetRecursive()
        LetTests.LetRecursive2()
        LetTests.LetFunctionValues()
        LetTests.LetLambdaFunctions()
        LetTests.LetFunctionComposition()
        LetTests.LetTuple()
        LetTests.LetTuple2()
        LetTests.LetMutable()

    [<Js>]
    let DoListTests() =    
        ListTests.ListDeclare1()
        ListTests.ListDeclare2()
        ListTests.ListDeclare3()
        ListTests.ListAttachElements()
        ListTests.ListConcatenateElements()
        ListTests.ListProperties()
        ListTests.ListRecursion1()
        ListTests.ListRecursion2()
        ListTests.ListBooleanOperation()
        ListTests.ListExists2()
        ListTests.ListForAll()
        ListTests.ListForAll2()
        ListTests.ListSort()
        ListTests.ListSortBy()
        //ListTests.ListSortWith()
        ListTests.ListFind()
        ListTests.ListPick()
        ListTests.ListTryFind()
        ListTests.ListArithemeticOperations()
        ListTests.ListZip()
        ListTests.ListZip3()
        ListTests.ListUnZip()
        ListTests.ListUnZip3()
        ListTests.ListMap()
        ListTests.ListMap2()
        ListTests.ListMap3()
        ListTests.ListMapi()
        ListTests.ListMapi2()
        ListTests.ListCollect()
        ListTests.ListFilter()
        ListTests.ListChoose()
        ListTests.ListAppendConcat()
        ListTests.ListFold()
        ListTests.ListFold2()
        ListTests.ListFold2_2()
        ListTests.ListFoldBack()
        ListTests.ListFoldBack2()
        ListTests.ListReduce()