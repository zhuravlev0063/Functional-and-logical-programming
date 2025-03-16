printfn "Hello from F#"

let solve_quadr (a, b, c) =
    let D = b * b - 4. * a * c
    ( (-b + sqrt(D)) / (2. * a), (-b - sqrt(D)) / (2. * a) )

let roots = solve_quadr (1.0, 2.0, -3.0)

printfn "Корни уравнения: %A" roots
