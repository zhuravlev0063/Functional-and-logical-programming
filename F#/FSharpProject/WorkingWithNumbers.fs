namespace WorkingWithNumbers
 
 module NumberOperations = 
 
     let rec processDigits (num: int) (accum: int) (oper: int -> int -> int)  = 
         if num = 0 then accum
         else
             let digit : int = num % 10
             let new_num : int = int num / 10
             let new_accum : int = oper digit accum
             processDigits new_num new_accum oper