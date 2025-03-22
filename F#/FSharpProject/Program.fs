
open System


let solve_quadr a b c =
        let D = b * b - 4. * a * c
        ( (-b + sqrt(D)) / (2. * a), (-b - sqrt(D)) / (2. * a) )


[<EntryPoint>]
let main (args : string[]) =
    printfn "Hello from F#"

    printfn "Введите коэффициенты квадратного уравения a, b, c:"    
    let a = Double.Parse(Console.ReadLine())
    let b = Double.Parse(Console.ReadLine())
    let c = Double.Parse(Console.ReadLine())

    let roots = solve_quadr a b c
    printfn "Корни уравнения: %A" roots

    0
