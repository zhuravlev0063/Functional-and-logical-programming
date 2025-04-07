open System


// Чтение списка с клавиатуры
let rec readList n =
    // Считываем n чисел с клавиатуры
    let list1 = fun f -> fun x -> x
    let cons head tail = fun f -> fun x -> f head (tail f x)
    System.Console.WriteLine("введите элемент списка:")
    match n with
    | 1 -> cons (System.Console.ReadLine()) list1
    | _ -> cons (System.Console.ReadLine()) (readList(n-1))

let printList lst =
        lst (fun head tail -> printf "%A " head; tail) ()
        printfn "" 

[<EntryPoint>]  
let main (args : string[]) =

    System.Console.WriteLine("введите количество элементов списка:")
    let n = int (System.Console.ReadLine())
    printList (readList n)
    0
