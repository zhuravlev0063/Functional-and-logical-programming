namespace WorkingWithNumbers

module NumberOperations = 

    let rec processDigitsRecursionToDown (oper: int -> int -> int) (accum: int) (num: int)   = 
        if num < 10 then
            oper accum num
        else
            processDigitsRecursionToDown oper (oper accum (num % 10)) (num / 10) 

    let rec processDigitsRecursionToTop (num: int) (oper: int -> int -> int)  = 
        if num < 10 then
            num
        else
            oper (processDigitsRecursionToTop (num / 10) oper) (num % 10)

    let rec factorialRecursionToTop (num: int) =
        if num <= 1 then
            1
        else
            num * factorialRecursionToTop(num - 1)

    let rec factorialRecursionToDown (accum: int) (num: int) =
        if num <= 1 then
            accum
        else
            factorialRecursionToDown (accum * num) (num - 1)

    let chooseFunction (digitSum: bool) =
        match digitSum with
        true -> processDigitsRecursionToDown (+)
        | false -> factorialRecursionToDown

    let rec bypassDigits (num: int) (func: int -> int -> int) (accum: int) : int =
        match num with
        0 -> accum
        | _ -> bypassDigits (int num / 10) func (func (num % 10) accum) 

    let rec bypassDigitsWithCondition (num: int) (twoArgFunc: int -> int -> int) (accum: int) (condition: int -> bool) : int =
        match num with
        0 -> accum
        | _ when (condition (num % 10)) = true -> bypassDigitsWithCondition (num / 10) twoArgFunc (twoArgFunc (num % 10) accum) condition
        | _ -> bypassDigitsWithCondition (num / 10) twoArgFunc accum condition

    let rec GCD (a: int, b: int) : int =
        match b with
        0 -> a
        | _ -> GCD (b, a % b)

    let rec bypassMutuallyPrimeComponentsInNumber (current: int) (num: int) (func: int -> int -> int) (accum: int) : int =
        match current with
        x when x >= num -> accum
        | x when GCD(num, x) = 1 -> bypassMutuallyPrimeComponentsInNumber (current+1) num func (func current accum)
        | _ -> bypassMutuallyPrimeComponentsInNumber (current+1) num func accum

    let EulerFunction (num: int) : int =
        bypassMutuallyPrimeComponentsInNumber 1 num (fun x acc -> acc + 1) 0

    let rec bypassMutuallyPrimeWithCondition (current: int) (num: int) (func: int -> int -> int) (accum: int) (condition: int -> bool) =
        match current with
        x when x >= num -> accum
        | x when GCD (num, x) = 1 && condition x -> 
            bypassMutuallyPrimeWithCondition (current + 1) num func (func accum current) condition
        | _ -> bypassMutuallyPrimeWithCondition (current + 1) num func accum condition

    let isPrime n =
        let rec check i =
            if i * i > n then true
            else if n % i = 0 then false
            else check (i + 1)
        if n <= 1 then false else check 2

    let maxPrimeDivisor n =
        let rec findDivisor i maxDiv =
            if i > n / 2 then maxDiv
            elif n % i = 0 && isPrime i then findDivisor (i + 1) i
            else findDivisor (i + 1) maxDiv
        findDivisor 2 1
    
    let productDigitsNotDividesOn5 num =
        let rec product acc num =
            match num with
            | 0 -> acc
            | _ ->
                let digit = num % 10
                let newAcc = if digit % 5 <> 0 then acc * digit else acc
                product newAcc (num / 10)
        product 1 num

    let max_odd_non_prime_divisor n =
        let rec find_divisor i maxDiv =
            if i > n / 2 then maxDiv
            elif n % i = 0 && i % 2 <> 0 && not (isPrime i) then find_divisor (i + 1) i
            else find_divisor (i + 1) maxDiv
        find_divisor 2 1

    let product_of_digits num =
        let rec product acc num =
            match num with
            | 0 -> acc
            | _ -> product (acc * (num % 10)) (num / 10)
        product 1 num

    let gcd_of_max_odd_non_prime_and_product n =
        let maxOddNonPrime = max_odd_non_prime_divisor n
        let productDigits = product_of_digits n
        GCD (maxOddNonPrime, productDigits)

module ListOperations = 
    let readList (n: int) =
         let rec readNumbers remaining accum =
             match remaining with
             | 0 -> List.rev accum
             | x when x > 0 ->
                 printf "Введите число: "
                 let newElem = System.Console.ReadLine() |> int
                 readNumbers (remaining - 1) (newElem :: accum)
             | _ -> failwith "Ошибка в рекурсивной функции"
 
         match n with 
         | x when x > 0 -> readNumbers n []
         | x when x < 0 -> failwith "Количество элементов не может быть отрицательным"
         | 0 -> []
         | _ -> failwith "Непредвиденная ошибка"

    let rec writeList list =
         match list with
         | [] -> ()
         | head :: tail -> 
             System.Console.WriteLine(head.ToString())
             writeList tail
    let rec reduceListWithCondition list (func: int -> int -> int) (condition: int -> bool) (accum: int) =
         match list with
         [] -> accum
         | head :: tail when condition head -> reduceListWithCondition tail func condition (func head accum)
         | head :: tail when (condition head) = false  -> reduceListWithCondition tail func condition accum
         | _ -> failwith "Непредвиденная ошибка"