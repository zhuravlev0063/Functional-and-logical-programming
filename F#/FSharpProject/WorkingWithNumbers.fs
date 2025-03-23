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

    (*let rec processDigits (num: int) (accum: int) =
        if num < 10 then
            oper accum num
        else
            processDigitsRecursionToDown oper (oper accum (num % 10)) (num / 10) 
    *)
    let rec bypassDigits (num: int) func (accum: int) =
        match num with
        0 -> accum
        | _ -> bypassDigits (int num / 10) func (func (num % 10) accum)