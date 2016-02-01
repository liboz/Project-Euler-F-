// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics
open System.Collections.Generic
open System

[<EntryPoint>]
let main argv = 
    let max = 2000000L

    let stopWatch = Stopwatch.StartNew()
    //Using Recursion
    let rec sieve list = 
        match list with 
        | head::tail  when head*head < max ->  head :: (sieve  <|  List.except [head*head..head..max] tail)
        | head::tail -> head::tail
        | [] -> []
    
    let value = 
        let allbut2 = sieve [3L..2L..max] 
                            |> List.sum
        allbut2 + 2L

    stopWatch.Stop()
    printfn "The values are %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 10 seconds 
    

    stopWatch.Reset()
    stopWatch.Start() 
    //Using Dictionary
    let sieve (n:int64) =
        let primes = new Dictionary<int64, bool>()
        for i in 3L..2L..n do primes.Add(i, true)
        for i in 3L..2L..int64(sqrt(double(n))) do //We only need to go up to the sqrt of the max
            if primes.[i] = true then
                for j in i*i..i..n do //Start at i squared to do the sieve properly!
                    primes.[j] <- false //Set non-primes to false
        let value = seq { for i in 3L..2L..n do
                            if primes.[i] = true then
                                yield i }
                        |> Seq.sum
        value + 2L
    let testvalue2 = sieve max
    stopWatch.Stop()
    printfn "The values are %d and it took %f" testvalue2 stopWatch.Elapsed.TotalMilliseconds //About 700ms 
    
    0 // return an integer exit code
