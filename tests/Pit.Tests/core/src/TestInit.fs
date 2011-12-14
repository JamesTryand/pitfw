namespace Pit.Test
open Pit

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

    [<Js>]
    let DoRecordsTests() =
        RecordsTests.RecordDeclare()
        RecordsTests.RecordDeclare2()
        RecordsTests.RecordDeclare3()
        RecordsTests.RecordDeclare4()
        RecordsTests.RecordPatternMatching()

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
        DelegateTests.Declare1()
        DelegateTests.Declare2()

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
