
let readList n =
    let rec readListRec n acc =
        match n with
            |0 ->acc
            |k ->
                let el = System.Console.ReadLine() |> int //преобразовали в число
                let newAcc = acc@[el] //el добавляется в конец acc, причем оператор @ создает новый список
                readListRec (k-1) newAcc
    readListRec n []
 
let rec printList list = 
     match list with
         | [] -> ignore
         | head::tail -> //первый + остальные
             System.Console.WriteLine(head.ToString())
             printList tail
 
let rec reduce list (f:(int->int->int)) (predicate:(int->bool)) acc =
     match list with
         | [] -> acc
         | head::tail ->
             let newAcc = if predicate head then f acc head else acc
             reduce tail f predicate newAcc
 
let minElement list =
     match list with
         | [] -> 0
         | h::t -> reduce t min (fun a -> true) h 

let maxElement list =
     match list with
         | [] -> 0
         | h::t -> reduce t max (fun a -> true) h 
 

let sumEven list = 
     reduce list (+) (fun a -> a % 2 = 0) 0
 
let oddCount list = 
     reduce list (fun a b -> a + 1) (fun a -> a % 2 = 1) 0

let rec frequency list n k =
    match list with
    | [] -> k
    | head::tail -> 
        let new_k = if head = n then k+1 else k
        frequency tail n new_k

//Задание 5. Найти наиболее часто встречающийся элемент списка

let rec frequency_list list main_list cur_list = 
    match list with
    | [] -> cur_list
    | head::tail -> 
        let freq_elem = frequency main_list head 0
        let new_list = cur_list@[freq_elem]
        frequency_list tail main_list new_list

let pos list el = 
    let rec pos_inner list el p = 
        match list with
            |[] -> 0
            | head::tail -> 
                if (head = el) then p
                else 
                    let p_next = p + 1
                    pos_inner tail el p_next
    pos_inner list el 1

let get_from_list list pos = 
    let rec get list p cur_p = 
        match list with 
            |[] -> 0
            |head::tail -> 
                if p = cur_p then head
                else 
                    let p_next = cur_p + 1
                    get tail p p_next
    get list pos 1

let find_most_frequent list = 
    let fL = frequency_list list list []
    (maxElement fL) |> (pos fL) |> (get_from_list list)   

//Задание 6. Реализация двоичного дерева с элементом строка

type Tree =
    | Empty
    | Node of string * Tree * Tree

let node = Empty

let rec add value tree =
    match tree with
    | Empty -> Node(value, Empty, Empty)
    | Node(v, left, right) ->
        if value < v then Node(v, add value left, right)
        elif value > v then Node(v, left, add value right)
        else tree

let rec traverse tree =
    match tree with
    | Empty -> []
    | Node(v, left, right) -> traverse left @ [v] @ traverse right

let print_tree tree =
    let rec print tree_arr =
        match tree_arr with
        | [] -> ignore
        | head::tail ->
            System.Console.WriteLine(head.ToString())
            print tail
    print (traverse tree)


 //Задание 7. ПОиск самого частого встречающегося элемента через класс List

let most_frequent list =
    list
    |> List.countBy id // сгруппировали, посчитали кол-во, получили список пар (элемент, кол-во)
    |> List.sortByDescending snd //отсортировали по убыванию по 2 элементу
    |> List.head //первый кортеж отсортированного
    |> fst //перый элемент

//Задание 8. Сколько элементов из списка могут быть квадратом какого-то элемента из списка

let count_2_elements (list: int list) =
    let unique_el = List.distinct list
    list
    |> List.filter (fun x -> unique_el  |> List.exists (fun y -> y * y = x))
    |> List.length
    
// Задание 9. Реализовать функцию, которая по трем спискам составляет список
let digit_sum n:int =
    let rec digit_sum_in n curSum =
        if n = 0 then curSum
        else
            let n_new = n/10
            let digit = n%10
            let sum = curSum + digit
            digit_sum_in n_new sum
    digit_sum_in n 0

let count_div n =
    match n with
    |0->0
    |_ ->
        let n_new = abs n
        [1..n_new] |> List.filter (fun x -> n_new % x = 0) |> List.length

let create_tuples (listA: int list) (listB: int list) (listC: int list) =

    let sortedA = listA |> List.sortByDescending id
    let sortedB = listB |> List.sortBy (fun x -> (digit_sum x, abs x))
    let sortedC = listC |> List.sortByDescending (fun x -> (count_div x, abs x))
    
    List.zip3 sortedA sortedB sortedC

//Задание 10. Отсортировать строки по длине

let sort_strings () =
    let rec readLines acc =
        let line = System.Console.ReadLine()
        if  line = "" then acc
        else readLines (line :: acc)
    
    let lines = readLines [] |> List.rev
    lines |> List.sortBy (fun s -> s.Length)

//Задание 11. Количество элементов после максимального (1)

let countAfterMax list =
    let maxEl = maxElement list
    let rec findLastMaxIndex list curIndex index =
        match list with
        | [] -> index
        | head :: tail ->
            let newLastIndex = if head = maxEl then curIndex else index
            findLastMaxIndex tail (curIndex + 1) newLastIndex
    let lastMaxIndex = findLastMaxIndex list 0 (-1)
    List.length list - lastMaxIndex - 1

let countAfterMaxList list =
    let maxEl = List.max list
    let lastIndex = 
        list 
        |> List.mapi (fun i x -> i, x) //список пар индекс + элемент
        |> List.filter (fun (_, x) -> x = maxEl) //оставили макс пары
        |> List.map fst
        |> List.last
    List.length list - lastIndex - 1
