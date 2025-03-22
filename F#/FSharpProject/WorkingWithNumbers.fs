namespace WorkingWithNumbers

module NumberOperations = 

    let rec processDigitsRecursionToDown (accum: int) (num: int) (oper: int -> int -> int)  = 
        if num < 10 then
            oper accum num
        else
            processDigitsRecursionToDown (oper accum (num % 10)) (num / 10) oper

    let rec processDigitsRecursionToTop (num: int) (oper: int -> int -> int)  = 
        if num < 10 then
            num
        else
            oper (processDigitsRecursionToTop (num / 10) oper) (num % 10)