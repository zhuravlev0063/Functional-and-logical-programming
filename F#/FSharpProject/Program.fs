open System

open WorkingWithNumbers.NumberOperations


type SolveQuadratic =
    None
    | Linear of float
    | Quadratic of float * float

let solve_quadr a b c =
        let D = b * b - 4. * a * c
        if a = 0. then
            if b = 0. then None
            else Linear(-c / b)
        else
            if D < 0. then None
            else Quadratic(( (-b + sqrt(D)) / (2. * a), (-b - sqrt(D)) / (2. * a) ))

let square_circle r = 
    (System.Math.PI * r ** 2.0)

let volume_cylinder_through_superpos (r, h) =
    let square_cylinder_base = square_circle r
    h * square_cylinder_base

let volume_cylinder_through_carry r h =
    let square_cylinder_base = square_circle r
    h * square_cylinder_base


[<EntryPoint>]
let main (args : string[]) =
    printfn "Hello, World"

    printfn "Введите число:"
    let num = System.Int32.Parse(Console.ReadLine())

    printfn "Сумма цифр числа: %d" (processDigits num 0 (+) )
    printfn "Прозведение цифр числа: %d" (processDigits num 1 (*) )

    0