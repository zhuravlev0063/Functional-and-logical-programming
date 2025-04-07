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

//Задание 12. Поиск уникального элемента (11)
let findUniqueElement list =
    let rec count_in list el count =
        match list with
        | [] -> count
        | head :: tail ->
            let newCount = if head = el then count + 1 else count
            count_in tail el newCount
    let rec findUnique list =
        match list with
        | [] -> 0
        | head :: tail -> if count_in list head 0 = 1 then head else findUnique tail
    findUnique list

let findUniqueElementList list =
    list |> List.countBy id |> List.find (fun (_, count) -> count = 1) |> fst

//Задание 13. Элементы после первого максимального (21)

let elementsAfterFirstMax list =
    let maxEl = maxElement list
    let rec findFirstMaxIndex list curIndex =
        match list with
        | [] -> -1
        | head :: tail ->
            if head = maxEl then curIndex
            else findFirstMaxIndex tail (curIndex + 1)
    
    let rec takeAfterIndex list index currentIndex =
        match list with
        | [] -> []
        | head :: tail ->
            if currentIndex > index then head :: takeAfterIndex tail index (currentIndex + 1)
            else takeAfterIndex tail index (currentIndex + 1)
    
    let firstMaxIndex = findFirstMaxIndex list 0
    takeAfterIndex list firstMaxIndex 0

let elementsAfterFirstMaxList list =
    let maxEl = List.max list 
    let firstMaxIndex = list |> List.findIndex (fun x -> x = maxEl)
    list |> List.skip (firstMaxIndex + 1) 

 //Задание 14. Подсчет четных элементов

let countEvenElements list =
    let rec count list acc =
        match list with
        | [] -> acc
        | head :: tail -> 
            let newAcc = if head % 2 = 0 then acc + 1 else acc
            count tail newAcc
    count list 0

let countEvenElementsList list =
    list
    |> List.filter (fun x -> x % 2 = 0) 
    |> List.length

//Задание 15. Среднее арифметическое модулей

let average list =
    let rec sumAndCount list accSum accCount =
        match list with
        | [] -> (accSum, accCount)
        | head :: tail -> 
            let newAccSum = accSum + abs head
            let newAccCount = accCount + 1
            sumAndCount tail newAccSum newAccCount
    
    let (totalSum, totalCount) = sumAndCount list 0 0
    if totalCount = 0 then 0.0 else float totalSum / float totalCount

let averageList list =
    let sumOfAbs = list |> List.map abs |> List.sum
    let count = List.length list
    if count = 0 then 0.0 else float sumOfAbs / float count

//Задание 17. Поиск наибольшей общей подпоследовательности

let rec subseq seq1 seq2 =
    match seq1, seq2 with
    | [], _ | _, [] -> [] 
    | head1::tail1, head2::tail2 ->
        if head1 = head2 then
            head1 :: subseq tail1 tail2  
        else
            // ищем без первого элемента из первой последовательности и без первого элемента из второй
            let res1 = subseq tail1 seq2
            let res2 = subseq seq1 tail2
            if List.length res1 > List.length res2 then res1 else res2 

//Задание 18. Первернуть массив

let reverseArray arr = Array.rev arr

//Задание 19. Количество русских символов

let countRus (str: string) =
    str 
    |> Seq.filter (fun c -> (c >= 'А' && c <= 'Я') ||  (c >= 'а' && c <= 'я') )
    |> Seq.length

let main () =
     //let arr = readList 5

     let arr = [2; 3; 4; 5]
     //printList arr
     System.Console.Write("Сумма четных от 1 до 5: ")
     let result = sumEven arr
     System.Console.WriteLine(result)
     
     System.Console.Write("Минимальный от 1 до 5: ")
     let minEl = minElement arr
     System.Console.WriteLine(minEl)
 
     System.Console.Write("Количество нечетных от 1 до 5: ")
     let odds = oddCount arr
     System.Console.WriteLine(odds)

     let arr_5 = [1; 2; 3; 4; 5; 2; 3; 3;]
     System.Console.Write("Элементы списка для задания 5: ")
     printList arr_5
     System.Console.Write("Самый частый из них: ")
     System.Console.WriteLine(find_most_frequent arr_5)

     let tree = 
        node
        |> add "строка4"
        |> add "строка2"
        |> add "строка1"
        |> add "строка3"
        |> add "строка5"

     System.Console.WriteLine("Двоичное дерево с элементами строка:")
     print_tree tree

     System.Console.Write("Самый частый элемент (использование List): ")
     System.Console.WriteLine(most_frequent arr_5)

     System.Console.Write("Количество элементов, являющиеся квадратами других элементов списка: ")
     System.Console.WriteLine(count_2_elements arr)

     let listA = [20; 10; 50]   
     let listB = [91; 88; 10]
     let listC = [44; 36; 13]

     System.Console.Write("Получившиеся кортежи вида (Аi, Вi, Сi): ")
     System.Console.WriteLine(create_tuples listA listB listC)

     System.Console.WriteLine("Ввод строк:")
     let strings = sort_strings()
     System.Console.WriteLine("Строки, отсортированные по длине: ")
     strings |> List.iter (System.Console.WriteLine)

     let arr_11 = [1; 3; 7; 2; 7; 4]

     System.Console.Write("Количество элементов после максимального через списки Черча: ")
     System.Console.WriteLine(countAfterMax arr_11)

     System.Console.Write("Через List: ")
     System.Console.WriteLine(countAfterMaxList arr_11)

     let arr_12 = [2; 2; 2; 3; 2; 2; 2]

     System.Console.Write("Уникальный элемент через списки Черча: ")
     System.Console.WriteLine(findUniqueElement arr_12)

     System.Console.Write("Через List: ")
     System.Console.WriteLine(findUniqueElementList arr_12)

     System.Console.Write("Элементы после первого максимального через списки Черча: ")
     System.Console.WriteLine(elementsAfterFirstMax arr_11)

     System.Console.Write("Через List: ")
     System.Console.WriteLine(elementsAfterFirstMax arr_11)

     System.Console.Write("Количество четных через списки Черча: ")
     System.Console.WriteLine(countEvenElements arr_11)

     System.Console.Write("Через List: ")
     System.Console.WriteLine(countEvenElementsList arr_11)

     System.Console.Write("Среднее арифметическое модулей через списки Черча: ")
     System.Console.WriteLine(average arr_11)

     System.Console.Write("Через List: ")
     System.Console.WriteLine(averageList arr_11)

     let seq1 = [1; 2; 3; 4; 1]
     let seq2 = [3; 4; 1; 2; 1]

     let result = subseq seq1 seq2
     System.Console.WriteLine("Наибольшая общая подпоследовательность:")
     System.Console.WriteLine(result)

     let arr = [| 'П'; 'р'; 'и'; 'в'; 'е'; 'т' |]
     let reversedArr = reverseArray arr

     System.Console.WriteLine("Исходный массив:")
     System.Console.WriteLine(arr)

     System.Console.WriteLine("Перевернутый массив:")
     System.Console.WriteLine(reversedArr)

     let text = "Привет! Hello! 123"
     let res_19 = countRus text 
     System.Console.WriteLine("Количество русских символов в строке:")
     System.Console.WriteLine(res_19)

main()