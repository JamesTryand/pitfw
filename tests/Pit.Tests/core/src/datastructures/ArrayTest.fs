namespace Pit.Test
open Pit
open Pit.Javascript

module ArrayTest =

    [<Js>]
    let Declare() =
        let arr1 = [|1;2;3|]
        Assert.AreEqual "Declare Array" arr1.[0] 1

    [<Js>]
    let Length() =
        let array = [|1;2;3;4;5|]
        let len = Array.length array
        Assert.AreEqual "Array Length" len 5

    [<Js>]
    let DeclareRange() =
        let array = [| for i in 1 .. 10 -> i * i |]
        Assert.AreEqual "Declare Array Range" array.[4] 25

    [<Js>]
    let ZeroCreate() =
        let array : int array = Array.zeroCreate 10
        Assert.AreEqual "Array Zero Create" array.[0] 0

    [<Js>]
    let CreateGetSet() =
        let array1 = Array.create 5 ""
        for i in 0 .. array1.Length - 1 do
            Array.set array1 i (i.ToString())
        Assert.AreEqual "Array Create/Get/Set" array1.[0] (Array.get array1 0)

    [<Js>]
    let Init() =
        let array = (Array.init 5 (fun index -> index * index))
        Assert.AreEqual "Array Init" array.[4] 16

    [<Js>]
    let Copy() =
        let array1 = Array.init 5 (fun index -> index * index)
        let array2 = Array.copy array1
        Assert.AreEqual "Array Copy" array1.[0] array2.[0]

    [<Js>]
    let Sub() =
        let a1 = [|1..10|]
        let a2 = Array.sub a1 2 6
        Assert.AreEqual "Array Sub" 6 a2.Length

    [<Js>]
    let Append() =
        let m = Array.append [| 1; 2; 3|] [| 4; 5; 6|]
        Assert.AreEqual "Array append" 6 m.Length

    [<Js>]
    let Choose() =
        let k =
            Array.choose (fun elem ->
                if elem % 2 = 0 then
                    Some(float (elem*elem - 1))
                else
                    None) [| 1 .. 10 |]
        Assert.AreEqual "Array Choose" k.[0] 3.0

    [<Js>]
    let Collect() =
        let k = Array.collect (fun elem -> [| 0 .. elem |]) [| 1; 5; 10|]
        Assert.AreEqual "Array Collect" 19 k.Length

    [<Js>]
    let Concat() =
        let multiplicationTable max = seq { for i in 1 .. max -> [| for j in 1 .. max -> (i, j, i*j) |] }
        let array = Array.concat (multiplicationTable 3)
        let i,j,k = array.[3]
        Assert.AreEqual "Array Concat - i" i 2
        Assert.AreEqual "Array Concat - j" j 1
        Assert.AreEqual "Array Concat - k" k 2

    [<Js>]
    let Filter() =
        let k = Array.filter (fun elem -> elem % 2 = 0) [| 1 .. 10|]
        Assert.AreEqual "Array Filter" k.[0] 2

    [<Js>]
    let Rev() =
        let a = Array.rev [|3;2;1;|]
        Assert.AreEqual "Array Reverse" a.[0] 1

    [<Js>]
    let FilterChooseRev() =
        let a =
            [| 1 .. 10 |]
            |> Array.filter (fun elem -> elem % 2 = 0)
            |> Array.choose (fun elem -> if (elem <> 8) then Some(elem*elem) else None)
            |> Array.rev
        Assert.AreEqual "Array Filter Choose Reverse" a.[0] 100

    [<Js>]
    let Exists1() =
        let allNegative = Array.exists (fun elem -> abs (elem) = elem) >> not
        let res = allNegative [| -1; -2; -3 |]
        Assert.AreEqual "Array Exists Equal" res true

    [<Js>]
    let Exists2() =
        let allNegative = Array.exists (fun elem -> abs (elem) = elem) >> not
        let res = allNegative [| -1; 2; -3 |]
        Assert.AreNotEqual "Array Exists Not Equal" res false

    [<Js>]
    let Exists2Function() =
        let haveEqualElement = Array.exists2 (fun elem1 elem2 -> elem1 = elem2)
        let res = haveEqualElement [| 1; 2; 3 |] [| 3; 2; 1|]
        Assert.AreEqual "Array Exists2" res true

    [<Js>]
    let ForAll() =
        let allPositive = Array.forall (fun elem -> elem >= 0)
        let res = allPositive [| 0; 1; 2; 3 |]
        Assert.AreEqual "Array For All" res true

    [<Js>]
    let ForAll2() =
        let allEqual = Array.forall2 (fun elem1 elem2 -> elem1 = elem2)
        let res = allEqual [| 1; 2 |] [| 1; 2 |]
        Assert.AreEqual "Array ForAll2" res true

    [<Js>]
    let FindAndFindIndex() =
        let a1 = [| 1.. 10 |]
        let i = a1 |> Array.find(fun a -> a = 5)
        Assert.AreEqual "Array Find" 5 i
        let i2 = a1 |> Array.findIndex(fun a -> a = 5)
        Assert.AreEqual "Array Find" 4 i2

    [<Js>]
    let TryFind() =
        let array = [|1..10|]
        let item = array |> Array.tryFind(fun i -> i = 2)
        match item with
        | Some(i) -> Assert.AreEqual "Array Try Find" i 2
        | None    -> failwith "Item Not Found"

    [<Js>]
    let Fill() =
        let arrayFill1 = [| 1 .. 10 |]
        Array.fill arrayFill1 3 5 0
        Assert.AreEqual "Array Fill" arrayFill1.[3] 0

    [<Js>]
    let Iterate() =
        let array = [|1|]
        array |> Array.iter(fun i -> Assert.AreEqual "Array Iterate" i 1)

    [<Js>]
    let IterateIndexed() =
        let array = [|1;2;|]
        let array2 = [|1;2;|]
        array
        |> Array.iteri(fun idx i ->
            Assert.AreEqual "Array Iterate Indexed" i array2.[idx]
        )

    [<Js>]
    let IterateIndexed2() =
        let array = [|1;|]
        let array2 = [|3;|]
        array2
        |> Array.iteri2(fun idx i1 i2 ->
            Assert.AreEqual "Array Iterate Indexed2" i1 1
            Assert.AreEqual "Array Iterate Indexed2" i2 3
        ) array

    [<Js>]
    let Map() =
        let array = [|1;2;|]
        let array2 = array |> Array.map(fun i -> i + i)
        Assert.AreEqual "Array Map" array2.[1] 4

    [<Js>]
    let MapIndexed() =
        let array = [|1;2;|]
        let array2 = array |> Array.mapi(fun idx i -> idx + i)
        Assert.AreEqual "Array MapIndexed" array2.[1] 3

    [<Js>]
    let Map2() =
        let array = [|1;2|]
        let array2 = [|3;4|]
        let resultArray = array2 |> Array.map2 (fun i1 i2 -> i1+i2) array
        Assert.AreEqual "Array Map2" resultArray.[1] 6

    [<Js>]
    let MapIndexed2() =
        let array = [|1;2|]
        let array2 = [|3;4|]
        let resultArray = array2 |> Array.mapi2 (fun idx i1 i2 -> idx+i1+i2) array
        Assert.AreEqual "Array MapIndexed2" resultArray.[1] 7

    [<Js>]
    let Pick() =
        let values = [| ("a", 1); ("b", 2); ("c", 3) |]
        let resultPick = Array.pick (fun elem ->
                            match elem with
                            | (value, 2) -> Some value
                            | _ -> None) values
        Assert.AreEqual "Array Pick" "b" resultPick

    [<Js>]
    let TryPick() =
        let values = [| ("a", 1); ("b", 2); ("c", 3) |]
        let resultPick = Array.tryPick (fun elem ->
                            match elem with
                            | (value, 2) -> Some value
                            | _ -> None) values
        match resultPick with
        | Some(t) -> Assert.AreEqual "Array TryPick" "b" t
        | None    -> failwith "TryPick failed"

    [<Js>]
    let Partition() =
        let array = [|-2;-1;1;2;|]
        let n,p = array |> Array.partition(fun t -> t < 0)
        Assert.AreEqual "Array Partition" 2 n.Length

    [<Js>]
    let Zip() =
        let array1 = [| 1; 2; 3 |]
        let array2 = [| -1; -2; -3 |]
        let arrayZip = Array.zip array1 array2
        let item1,item2 = Array.get arrayZip 1
        Assert.AreEqual "Array Zip" 2 item1
        Assert.AreEqual "Array Zip" -2 item2

    [<Js>]
    let Zip3() =
        let array1 = [| 1; 2; 3 |]
        let array2 = [| -1; -2; -3 |]
        let array3 = [| -1; -2; -3 |]
        let arrayZip = Array.zip3 array1 array2 array3
        let item1,item2,item3 = Array.get arrayZip 1
        Assert.AreEqual "Array Zip3" 2 item1
        Assert.AreEqual "Array Zip3" -2 item2
        Assert.AreEqual "Array Zip3" -2 item3

    [<Js>]
    let Unzip() =
        let array1, array2 = Array.unzip [| (1, 2); (3, 4) |]
        Assert.AreEqual "Array Unzip" 2 array1.Length
        Assert.AreEqual "Array Unzip" 2 array2.Length

    [<Js>]
    let Unzip3() =
        let array1, array2, array3 = Array.unzip3 [| (1, 2, 3); (3, 4, 3) |]
        Assert.AreEqual "Array Unzip3" 2 array1.Length
        Assert.AreEqual "Array Unzip3" 2 array2.Length
        Assert.AreEqual "Array Unzip3" 2 array3.Length

    [<Js>]
    let Fold() =
        let sumArray array = Array.fold (fun acc elem -> acc + elem) 0 array
        let a = [|1;2;3|]
        let res = sumArray a
        Assert.AreEqual "Array Fold" 6 res

    [<Js>]
    let FoldBack() =
        let subtractArrayBack array1 = Array.foldBack (fun elem acc -> elem - acc) array1 0
        let a = [|1;2;3|]
        let res = subtractArrayBack a
        Assert.AreEqual "Array FoldBack" 2 res

    [<Js>]
    let Fold2() =
        let sumGreatest array1 array2 =
            Array.fold2 (fun acc elem1 elem2 ->
                acc + max elem1 elem2) 0 array1 array2
        let sum = sumGreatest [| 1; 2; 3 |] [| 3; 2; 1 |]
        Assert.AreEqual "Array Fold2" 8 sum

    [<Js>]
    let FoldBack2() =
        let subtractArrayBack array1 array2 = Array.foldBack2 (fun elem acc1 acc2 -> elem - (acc1 - acc2)) array1 array2 0
        let a1 = [|1;2;3|]
        let a2 = [|4;5;6|]
        let res = subtractArrayBack a1 a2
        Assert.AreEqual "Array FoldBack2" -9 res

    [<Js>]
    let Scan() =
        let initialBalance = 1122.73
        let transactions = [| -100.00; +450.34; -62.34; -127.00; -13.50; -12.92 |]
        let balances =
            Array.scan (fun balance transactionAmount -> balance + transactionAmount) initialBalance transactions
        Assert.AreEqual "Array Scan" 1022.73 balances.[1]

    [<Js>]
    let ScanBack() =
        let subtractArrayBack array1 = Array.scanBack (fun elem acc -> elem - acc) array1 0
        let a = [|1;2;3|]
        let res = subtractArrayBack a
        Assert.AreEqual "Array ScanBack" 2 res.[0]

    [<Js>]
    let Reduce() =
        let names = [| "A"; "man"; "landed"; "on"; "the"; "moon" |]
        let sentence = names |> Array.reduce (fun acc item -> acc + " " + item)
        Assert.AreEqual "Array Reduce" sentence "A man landed on the moon"

    [<Js>]
    let ReduceBack() =
        let res = Array.reduceBack (fun elem acc -> elem - acc) [| 1; 2; 3; 4 |]
        Assert.AreEqual "Array Reduce Back" res -2

    [<Js>]
    let SortInPlace() =
        let array = [|10;2;4;1|]
        Array.sortInPlace array
        Assert.AreEqual "Array SortInPlace" 1 array.[0]

    [<Js>]
    let SortInPlaceBy() =
        let array1 = [|1; 4; 8; -2; 5|]
        Array.sortInPlaceBy (fun elem -> abs elem) array1
        Assert.AreEqual "Array SortInPlaceBy" 1 array1.[0]

    [<Js>]
    let SortInPlaceWith() =
        let array1 = [|1; 4; 8; -2; 5|]
        Array.sortInPlaceWith (fun e1 e2 -> e1-e2) array1
        Assert.AreEqual "Array SortInPlaceWith" -2 array1.[0]

    [<Js>]
    let SortWith() =
        let array1 = [|1; 4; 8; -2; 5|]
        let array2 = Array.sortWith(fun e1 e2 -> e1-e2) array1
        Assert.AreEqual "Array SortWith" -2 array2.[0]

    [<Js>]
    let Sort() =
        let array1 = [|1; 4; 8; -2; 5|]
        let array2 = Array.sort array1
        Assert.AreEqual "Array Sort" -2 array2.[0]

    [<Js>]
    let Sort2() =
        let array1 = [|"Womciw"; "Beosudo"; "Guyx"; "Rouh"; "Iibow"; "Tae"; "Ebiucly"; "Gonumaf";  "Hiowvivb"; |]
        let array2 = [|"Pye"; "Gyhsy"; "Lhfi"; "Ouqilfo"; "Ymukoed"; "Nhap"; "Aguccet"; "Hahd"; "Debcok" |]
        let names = Array.zip array1 array2 |> Array.map(fun (f,s)-> f + " " + s) |> Array.sort
        Assert.AreEqual "Array Sort2" "Iibow Ymukoed" names.[5]

    [<Js>]
    let Permute() =
        let array1 = [|1;2;3;4;5|]
        let n = array1.Length
        let permute = Array.permute(fun idx -> idx % n) array1
        Assert.AreEqual "Array Permute" 1 permute.[0]

    [<Js>]
    let Sum() =
        let a = [|1;2;3;4;5|]
        let s = Array.sum a
        Assert.AreEqual "Array Sum" s 15

    [<Js>]
    let SumBy() =
        let s =
            [| 1 .. 10 |]
            |> Array.sumBy (fun x -> x * x)
        Assert.AreEqual "Array Sumby" 385 s

    [<Js>]
    let Min() =
        let a = [|1;2;3;4|]
        let s = Array.min a
        Assert.AreEqual "Array Min" 1 s

    [<Js>]
    let Max() =
        let a = [|1;2;3;4|]
        let s = Array.max a
        Assert.AreEqual "Array Max" 4 s

    [<Js>]
    let MinBy() =
        let r =
            [| -10 .. 10 |]
            |> Array.minBy (fun x -> x * x - 1)
        Assert.AreEqual "Array MinBy" 0 r

    [<Js>]
    let MaxBy() =
        let r =
            [| -10 .. 10 |]
            |> Array.maxBy (fun x -> x * x - 1)
        Assert.AreEqual "Array MaxBy" -10 r

    [<Js>]
    let Average() =
        let r = [|1.0 .. 10.0|] |> Array.average
        Assert.AreEqual "Array Average" 5.5 r

    [<Js>]
    let AverageBy() =
        let avg2 = Array.averageBy (fun elem -> float elem) [|1 .. 10|]
        Assert.AreEqual "Array AverageBy" 5.5 avg2

    [<Js>]
    let ToList() =
        let array = [|1;2;3|]
        let list = Array.toList array
        Assert.AreEqual "Array ToList" 1 list.Head

    [<Js>]
    let OfList() =
        let list = [1;2;3]
        let a = Array.ofList list
        Assert.AreEqual "Array OfList" 1 a.[0]

    [<Js>]
    let ToSeq() =
        let a = [|1;2;3|]
        let sequence = Array.toSeq a
        use e = sequence.GetEnumerator()
        e.MoveNext() |> ignore
        Assert.AreEqual "Array ToSeq" 1 e.Current

    [<Js>]
    let OfSeq() =
        let sequence = seq { 1..5 }
        let array = Array.ofSeq sequence
        Assert.AreEqual "Array OfSeq" 1 array.[0]