open System
open WorkingWithNumbers


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

(*
    System.Console.WriteLine("Введите коэффициенты квадратного уравения a, b, c:")
    let a = Double.Parse(System.Console.ReadLine())
    let b = Double.Parse(System.Console.ReadLine())
    let c = Double.Parse(System.Console.ReadLine())

    let roots = solve_quadr a b c
    match roots with
        None -> System.Console.WriteLine("Нет решений")
        | Linear(x) -> System.Console.WriteLine("Единственный корень: {0}", x)
        | Quadratic(x, y) -> System.Console.WriteLine("Корни: {0} {1}", x, y)


    System.Console.WriteLine("Введите радиус и высоту цилиндра:")
    let r = Double.Parse(System.Console.ReadLine())
    let h = Double.Parse(System.Console.ReadLine())
    
    let volume_superpos = volume_cylinder_through_superpos (r, h)
    System.Console.WriteLine("(Суперпозиция) Объем цилиндра с радиусом основания {0} и высотой {1}: {2}", r, h, volume_superpos)

    let volume_carry = volume_cylinder_through_carry r h
    System.Console.WriteLine("(Каррирование) Объем цилиндра с радиусом основания {0} и высотой {1}: {2}", r, h, volume_carry)

    System.Console.WriteLine("Введите число:")
    let num = System.Int32.Parse(Console.ReadLine())

    System.Console.WriteLine("Рекурсия вверх")
    System.Console.WriteLine("Сумма цифр числа: {0}", (NumberOperations.processDigitsRecursionToTop num (+) ) )
    System.Console.WriteLine("Прозведение цифр числа: {0}", (NumberOperations.processDigitsRecursionToTop num (*) ) )

    System.Console.WriteLine("Рекурсия вниз")
    System.Console.WriteLine("Сумма цифр числа: {0}", (NumberOperations.processDigitsRecursionToDown (+) 0 num  ) )
    System.Console.WriteLine("Прозведение цифр числа: {0}", (NumberOperations.processDigitsRecursionToDown (*) 1 num ) )


    let factor = NumberOperations.chooseFunction false
    Console.WriteLine("Результат: {0}", (factor 1 6))
    Console.WriteLine("Результат: {0}", (factor 1 5))

    let factor1 = NumberOperations.chooseFunction true
    Console.WriteLine("Результат: {0}", (factor1 0 12345))
    Console.WriteLine("Результат: {0}", (factor1 0 1236781))

    let min_function = fun a b -> if a < b then a else b
    let min_digit = NumberOperations.bypassDigits 1234 min_function 10
    System.Console.WriteLine("Минимальная цифра числа: {0}", min_digit)

    let max_function = fun a b -> if a > b then a else b
    let max_digit = NumberOperations.bypassDigits 1234 max_function 0
    System.Console.WriteLine("Максимальная цифра числа: {0}", max_digit)

    let plus = fun a b -> a + b
    let plus_digits = NumberOperations.bypassDigits 1234 plus 0
    System.Console.WriteLine("Сумма цифр числа: {0}", plus_digits)

    let mult = fun a b -> a * b
    let mult_digits = NumberOperations.bypassDigits 1234 mult 1
    System.Console.WriteLine("Произведение цифр числа: {0}", mult_digits)
*)

    let min_function = fun a b -> if a < b then a else b
    let evenCondition = fun a -> if a % 2 = 0 then true else false
    let min_digit = NumberOperations.bypassDigitsWithCondition 1234 min_function 10 evenCondition
    System.Console.WriteLine("Минимальная четная цифра числа: {0}", min_digit)

    let max_function = fun a b -> if a > b then a else b
    let oddCondition = fun a -> if a % 2 <> 0 then true else false
    let max_digit = NumberOperations.bypassDigitsWithCondition 1234 max_function 0 oddCondition
    System.Console.WriteLine("Максимальная нечетная цифра числа: {0}", max_digit)

    let plus = fun a b -> a + b
    let notOne = fun a -> if a <> 1 then true else false
    let plus_digits = NumberOperations.bypassDigitsWithCondition 1234 plus 0 notOne
    System.Console.WriteLine("Сумма цифр числа, которые не равны 1: {0}", plus_digits)

    let mult = fun a b -> a * b
    let notThree = fun a -> if a <> 3 then true else false
    let mult_digits = NumberOperations.bypassDigitsWithCondition 1234 mult 1 notThree
    System.Console.WriteLine("Произведение цифр числа, которые не равны 3: {0}", mult_digits)


    0