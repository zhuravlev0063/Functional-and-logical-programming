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