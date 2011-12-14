namespace PitTest
open System
open Pit
open System.Reflection
open Microsoft.FSharp.Reflection
open System.Text

module ListTests = 

    [<Js>]
    let ListDeclare1() =
        let list123 = [ 1; 2; 3 ]
        Assert.AreEqual "Declare List type 1:" list123.Head 1

    [<Js>]
    let ListDeclare2() =
        let list123 = [ 1 .. 10 ]
        Assert.AreEqual "Declare List type 2:" list123.Head 1

    [<Js>]
    let ListDeclare3() =
        let list123 = [ for i in 1 .. 10 -> i*i ]
        Assert.AreEqual "Declare List type 3:" list123.Head 1
        

    [<Js>]
    let ListAttachElements() =
        let list123 = [ for i in 1 .. 10 -> i*i ]
        let list2 = 100 :: list123
        Assert.AreEqual "Attach elements:" list2.Head 100

    [<Js>]
    let ListConcatenateElements() =
        let list1 = [ for i in 1 .. 10 -> i*i ]
        let list2 = [100]
        let list3 = list1 @ list2
        Assert.AreEqual "Concatenate elements:" list3.Head 1

        
    [<Js>]
    let ListProperties() =
        let list1 = [ 1; 2; 3 ]
        // Properties
        Assert.AreEqual "List Empty property:" list1.IsEmpty false
        Assert.AreEqual "List Length property:" list1.Length 3
        Assert.AreEqual "List Head property:" list1.Head 1
        Assert.AreEqual "List Tail property" list1.Tail.Head 2
        Assert.AreEqual "List.Tail.Tail.Head" list1.Tail.Tail.Head 3
        Assert.AreEqual "List.Item(1)" (list1.Item(1)) 2


    [<Js>]
    let ListRecursion1() =        
        let sum list =
           let rec loop list acc =
               match list with
               | head :: tail -> loop tail (acc + head)
               | [] -> acc
           loop list 0

        let list = [ 1; 2; 3]
        Assert.AreEqual "List Recursion 1" (sum list) 6

    [<Js>]
    let ListRecursion2() =        
        let IsPrimeMultipleTest n x =
           x = n || x % n <> 0

        let rec RemoveAllMultiples listn listx =
           match listn with
           | head :: tail -> RemoveAllMultiples tail (List.filter (IsPrimeMultipleTest head) listx)
           | [] -> listx


        let GetPrimesUpTo n =
            let max = int (sqrt (float n))
            RemoveAllMultiples [ 2 .. max ] [ 1 .. n ]

        let primes = (GetPrimesUpTo 100) 
        Assert.AreEqual "List Recursion 2 - First element" (List.nth primes 1) 2
        Assert.AreEqual "List Recursion 2 - 25th element" (List.nth primes 25) 2
         
    [<Js>]
    let containsNumber number list = List.exists (fun elem -> elem = number) list

    [<Js>]
    let ListBooleanOperation() =         
        let list0to3 = [0 .. 3]
        Assert.AreEqual "Boolean Operation:" (containsNumber 0 list0to3) true

    [<Js>]
    let isEqualElement list1 list2 = List.exists2 (fun elem1 elem2 -> elem1 = elem2) list1 list2

    [<Js>]
    let ListExists2() = 
       
        let list1to5 = [ 1 .. 5 ]
        let list5to1 = [ 5 .. -1 .. 1 ]
        let result = (isEqualElement list1to5 list5to1)
        if result then
            Assert.AreEqual "List.exists2 function." result true
        else
            Assert.AreEqual "List.exists2 function." result false

    [<Js>]
    let ListForAll() = 
        let isAllZeroes list = List.forall (fun elem -> elem = 0.0) list
        Assert.AreEqual "List.forall function" (isAllZeroes [0.0; 0.0]) true
        Assert.AreEqual "List.forall function" (isAllZeroes [0.0; 1.0]) false

    [<Js>]
    let listEqual list1 list2 = List.forall2 (fun elem1 elem2 -> elem1 = elem2) list1 list2

    [<Js>]
    let ListForAll2() =         
        Assert.AreEqual "List.forall2 function" (listEqual [0; 1; 2] [0; 1; 2]) true
        Assert.AreEqual "List.forall2 function" (listEqual [0; 0; 0] [0; 1; 0]) false

    [<Js>]
    let ListSort() =  
        let sortedList1 = List.sort [1; 4; 8; -2; 5]
        Assert.AreEqual "List.sort function" (List.nth sortedList1 1) -2

    [<Js>]
    let ListSortBy() =      
        let sortedList2 = List.sortBy (fun elem -> abs elem) [1; 4; 8; -2; 5]
        Assert.AreEqual "List.sortBy function" (List.nth sortedList2 1) 1

//    type Widget = { ID: int; Rev: int }
//Issue in js generation
//    [<Js>]
//    let ListSortWith() = 
//
//        let compareWidgets widget1 widget2 =
//           if widget1.ID < widget2.ID then -1 else
//           if widget1.ID > widget2.ID then 1 else
//           if widget1.Rev < widget2.Rev then -1 else
//           if widget1.Rev > widget2.Rev then 1 else
//           0
//
//        let listToCompare = [
//            { ID = 92; Rev = 1 }
//            { ID = 110; Rev = 1 }
//            { ID = 100; Rev = 5 }
//            { ID = 100; Rev = 2 }
//            { ID = 92; Rev = 1 }
//            ]
//
//        let sortedWidgetList = List.sortWith compareWidgets listToCompare
//        Assert.AreEqual "List.sortWith function" sortedWidgetList.Head.ID 92
//        Assert.AreEqual "List.sortWith function" sortedWidgetList.Head.Rev 1


    [<Js>]
    let ListFind() = 
        let isDivisibleBy number elem = elem % number = 0
        let result = List.find (isDivisibleBy 5) [ 1 .. 100 ]
        Assert.AreEqual "List.find function" result 5

    [<Js>]
    let ListPick() =         
        let valuesList = [ ("a", 1); ("b", 2); ("c", 3) ]

        let resultPick = List.pick (fun elem ->
                            match elem with
                            | (value, 2) -> Some value
                            | _ -> None) valuesList
        Assert.AreEqual "List.pick function"  resultPick "b"

    [<Js>]
    let ListTryFind() =   
        let list1d = [1; 3; 7; 9; 11; 13; 15; 19; 22; 29; 36]
        let isEven x = x % 2 = 0
        match List.tryFind isEven list1d with
        | Some value -> Assert.AreEqual "List.tryFind function" value 22
        | None -> ()

        match List.tryFindIndex isEven list1d with
        | Some value -> Assert.AreEqual "List.tryFindIndex function" value 8
        | None -> ()

    [<Js>]
    let ListArithemeticOperations() =
        // Compute the sum of the first 10 integers by using List.sum.
        let sum1 = List.sum [1 .. 10]
        Assert.AreEqual "List.sum function" sum1 55

        // Compute the sum of the squares of the elements of a list by using List.sumBy.
        let sum2 = List.sumBy (fun elem -> elem*elem) [1 .. 10]
        Assert.AreEqual "List.sumBy function" sum2 385


        // Compute the average of the elements of a list by using List.average.
        let avg1 = List.average [0.0; 1.0; 1.0; 2.0]
        Assert.AreEqual "List.average function" avg1 1.0

        
        let avg2 = List.averageBy (fun elem -> float elem) [1 .. 10]
        Assert.AreEqual "List.averageBy function" avg2 5.5



    [<Js>]
    let ListZip() =
        let list1 = [ 1; 2; 3 ]
        let list2 = [ -1; -2; -3 ]
        let listZip = List.zip list1 list2
        let f = fst listZip.Head
        Assert.AreEqual "List.zip function" f 1
        Assert.AreEqual "List.zip function" (snd listZip.Head) -1

    [<Js>]
    let fst3 (a,_,_) = a

    [<Js>]
    let snd3 (_,a,_) = a
    
    [<Js>]
    let trd (_,_,a) = a


    [<Js>]
    let ListZip3() =        
        let list1 = [ 1; 2; 3 ]
        let list2 = [ -1; -2; -3 ]
        let list3 = [ 0; 0; 0]
        let listZip3 = List.zip3 list1 list2 list3
        let f = fst3 listZip3.Head
        Assert.AreEqual "List.zip function" f 1
        Assert.AreEqual "List.zip function" (snd3 listZip3.Head) -1
        Assert.AreEqual "List.zip function" (trd listZip3.Head) 0

    [<Js>]
    let ListUnZip() = 
        let lists = List.unzip [(1,2); (3,4)]
        Assert.AreEqual "List.unzip function" (fst lists).Head 1
        Assert.AreEqual "List.unzip function" (snd lists).Head 2

    [<Js>]
    let ListUnZip3() =   
        let listsUnzip3 = List.unzip3 [(1,2,3); (4,5,6)]
        Assert.AreEqual "List.unzip3 function" (fst3 listsUnzip3).Head 1
        Assert.AreEqual "List.unzip3 function" (snd3 listsUnzip3).Head 4

//    [<Js>]
//    let ListIter() =  
//        let list1 = [1; 2; 3]
//        let list2 = [4; 5; 6]
//        List.iter (fun x -> printfn "List.iter: element is %d" x) list1
//        List.iteri(fun i x -> printfn "List.iteri: element %d is %d" i x) list1
//        List.iter2 (fun x y -> printfn "List.iter2: elements are %d %d" x y) list1 list2
//        List.iteri2 (fun i x y ->
//                       printfn "List.iteri2: element %d of list1 is %d element %d of list2 is %d"
//                         i x i y)
//            list1 list2


    [<Js>]
    let ListMap() =
        let list1 = [1; 2; 3]
        let newList = List.map (fun x -> x + 1) list1
        Assert.AreEqual "List.map function" newList.Head 2
        
    [<Js>]
    let ListMap2() =        
        let list1 = [1; 2; 3]
        let list2 = [4; 5; 6]
        let sumList = List.map2 (fun x y -> x + y) list1 list2
        Assert.AreEqual "List.map2 function" sumList.Head 5

    [<Js>]
    let ListMap3() =              
        let list1 = [1; 2; 3]
        let list2 = [4; 5; 6]   
        let newList2 = List.map3 (fun x y z -> x + y + z) list1 list2 [2; 3; 4]
        Assert.AreEqual "List.map3 function" newList2.Head 7

    [<Js>]
    let ListMapi() =    
        let list1 = [1; 2; 3]
        let newListAddIndex = List.mapi (fun i x -> x + i) list1
        Assert.AreEqual "List.mapi function" newListAddIndex.Head 1

    [<Js>]
    let ListMapi2() =   
        let list1 = [1; 2; 3]
        let list2 = [4; 5; 6]    
        let listAddTimesIndex = List.mapi2 (fun i x y -> (x + y) * i) list1 list2
        Assert.AreEqual "List.mapi2 function" listAddTimesIndex.Head 0


    [<Js>]
    let ListCollect() = 
        let list1 = [1; 2; 3]  
        let collectList = List.collect (fun x -> [for i in 1..3 -> x * i]) list1
        Assert.AreEqual "List.collect function" collectList.Head 1

    [<Js>]
    let ListFilter() = 
        let evenOnlyList = List.filter (fun x -> x % 2 = 0) [1; 2; 3; 4; 5; 6]
        Assert.AreEqual "List.filter function" evenOnlyList.Head 2


    [<Js>]
    let ListChoose() = 
        let listWords = [ "and"; "Rome"; "Bob"; "apple"; "zebra" ]
        let isCapitalized (string1:string) = System.Char.IsUpper string1.[0]
        let results = List.choose (fun elem ->
            match elem with
            | elem when isCapitalized elem -> Some(elem + "'s")
            | _ -> None) listWords
        Assert.AreEqual "List.choose function" results.Head "Rome's"

    [<Js>]
    let ListAppendConcat() = 
        let list1to10 = List.append [1; 2; 3] [4; 5; 6; 7; 8; 9; 10]
        let listResult = List.concat [ [1; 2; 3]; [4; 5; 6]; [7; 8; 9] ]
        Assert.AreEqual "List.append function" list1to10.Head 1
        Assert.AreEqual "List.concat function" listResult.Head 1
    
    [<Js>]
    let reverseList list = List.fold (fun acc elem -> elem::acc) [] list    
        
    [<Js>]
    let ListFold() = 
        let sumList list = List.fold (fun acc elem -> acc + elem) 0 list
        Assert.AreEqual "List.fold function: SumTest" (sumList [ 1 .. 3 ]) 6
        let averageList list = (List.fold (fun acc elem -> acc + float elem) 0.0 list / float list.Length)
        let stdDevList list =
            let avg = averageList list
            sqrt (List.fold (fun acc elem -> acc + (float elem - avg) ** 2.0 ) 0.0 list / float list.Length)
        Assert.AreEqual "List.fold function: AverageTest" (averageList [ 1; 2; 1] ) 1.333333
        Assert.AreEqual "List.fold function: StdDevTest" (sumList [ 1; 2; 1] ) 0.471405
        Assert.AreEqual "List.fold function: ReverseTest" (reverseList [1 .. 10] ).Head 10

    [<Js>]
    let ListFold2() = 
        let sumGreatest list1 list2 = List.fold2 (fun acc elem1 elem2 ->
                                                      acc + max elem1 elem2) 0 list1 list2
        let sum = sumGreatest [1; 2; 3] [3; 2; 1]
        Assert.AreEqual "List.fold2 function" sum 8



    // Discriminated union type that encodes the transaction type.
    type Transaction =
        | Deposit
        | Withdrawal

    [<Js>]
    let ListFold2_2() = 
        let transactionTypes = [Deposit; Deposit; Withdrawal]
        let transactionAmounts = [100.00; 1000.00; 95.00 ]
        let initialBalance = 200.00

        // Use fold2 to perform a calculation on the list to update the account balance.
        let endingBalance = List.fold2 (fun acc elem1 elem2 ->
                                        match elem1 with
                                        | Deposit -> acc + elem2
                                        | Withdrawal -> acc - elem2)
                                        initialBalance
                                        transactionTypes
                                        transactionAmounts
        Assert.AreEqual "List.fold2 function" endingBalance 1205.000000
         
    [<Js>]
    let copyList list = List.foldBack (fun elem acc -> elem::acc) list []
    
    [<Js>]
    let ListFoldBack() =          
        let sumListBack list = List.foldBack (fun acc elem -> acc + elem) list 0
        Assert.AreEqual "List.foldBack function: Sum List" (sumListBack [1; 2; 3]) 6
        Assert.AreEqual "List.foldBack function: Copy List" (copyList [1 .. 10]).Head 1

    type Transaction2 =
        | Deposit
        | Withdrawal
        | Interest
    
    [<Js>]
    let ListFoldBack2() =          
        let transactionTypes2 = [Deposit; Deposit; Withdrawal; Interest]
        let transactionAmounts2 = [100.00; 1000.00; 95.00; 0.05 / 12.0 ]
        let initialBalance2 = 200.00

        // Because fold2 processes the lists by starting at the head element,
        // the interest is calculated last, on the balance of 1205.00.
        let endingBalance2 = List.fold2 (fun acc elem1 elem2 ->
                                        match elem1 with
                                        | Deposit -> acc + elem2
                                        | Withdrawal -> acc - elem2
                                        | Interest -> acc * (1.0 + elem2))
                                        initialBalance2
                                        transactionTypes2
                                        transactionAmounts2
        
        Assert.AreEqual "List.fold2 function:" endingBalance2 1210.020833
        
        // Because foldBack2 processes the lists by starting at end of the list,
        // the interest is calculated first, on the balance of only 200.00.
        let endingBalance3 = List.foldBack2 (fun elem1 elem2 acc ->
                                        match elem1 with
                                        | Deposit -> acc + elem2
                                        | Withdrawal -> acc - elem2
                                        | Interest -> acc * (1.0 + elem2))
                                        transactionTypes2
                                        transactionAmounts2
                                        initialBalance2
        Assert.AreEqual "List.foldBack2 function:" endingBalance3 1205.833333


    
    [<Js>]
    let ListReduce() =  

        let sumAList list =
            try
                List.reduce (fun acc elem -> acc + elem) list
            with
               | :? System.ArgumentException as exc -> 0

        let resultSum = sumAList [2; 4; 10]
        Assert.AreEqual "List.reduce function:" resultSum 16

    
        
        


