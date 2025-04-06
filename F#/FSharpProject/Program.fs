open System

// Определение типа для списка Черча с явным указанием всех параметров типов
type ChurchList<'a, 'r> = ('a -> 'r -> 'r) -> 'r -> 'r

// Пустой список Черча
let cnil : ChurchList<'a, 'r> = fun c n -> n

// Операция cons для списка Черча
let ccons (x: 'a) (xs: ChurchList<'a, 'r>) : ChurchList<'a, 'r> = 
    fun c n -> c x (xs c n)

// Функция для преобразования списка Черча в обычный список
let toList (cl: ChurchList<'a, 'a list>) : 'a list =
    cl (fun x xs -> x :: xs) []

// Функция для чтения n элементов с клавиатуры и создания списка Черча
let readChurchList (n: int) : ChurchList<int, int list> =
    let rec readElements count acc =
        if count <= 0 then 
            acc
        else
            printfn "Введите число %d:" (n - count + 1)
            let num = Console.ReadLine() |> int
            readElements (count - 1) (ccons num acc)
    readElements n cnil

// Главная программа
[<EntryPoint>]
let main argv =
    printfn "Введите количество элементов n:"
    let n = Console.ReadLine() |> int
    let churchList = readChurchList n
    let regularList = toList churchList
    printfn "Полученный список: %A" regularList
    0