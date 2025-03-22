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
    System.Console.WriteLine("Сумма цифр числа: {0}", (processDigitsRecursionToTop num (+) ) )
    System.Console.WriteLine("Прозведение цифр числа: {0}", (processDigitsRecursionToTop num (*) ) )

    System.Console.WriteLine("Рекурсия вниз")
    System.Console.WriteLine("Сумма цифр числа: {0}", (processDigitsRecursionToDown 0 num (+) ) )
    System.Console.WriteLine("Прозведение цифр числа: {0}", (processDigitsRecursionToDown 1 num (*) ) )

    0