// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    let maxSearchPrime (x:int64) =
        if x > 10L then
            int64(System.Math.Round (sqrt (float x)))
        else 
            x

    let getFactors (x:int64) =
        if x <= 1L then
            None
        else
            let PartialDivisors = {2L..(maxSearchPrime x)}
                                |> Seq.filter (fun i -> x % i = 0L)

            let Divisors = Seq.append  (PartialDivisors|> Seq.map (fun i -> x/i)) PartialDivisors
                           |> Seq.distinctBy id
                           |> Seq.filter (fun i -> (i <> x && i <> 1L))
            if Seq.length Divisors = 0 then
                None
            else
                Some Divisors
    
    let isPrime (x:int64) =
        if (getFactors x).IsNone then
            true
        else
            false
    
    let getPrimeFactors (x:int64) =
        let factors = getFactors x
        if factors.IsNone then
            Seq.singleton x //x is a prime number
        else
            factors.Value
            |> Seq.filter (fun i -> isPrime i = true)

    //let value = Seq.max (getPrimeFactors 13195L) #Given Test Case
    let value = Seq.max (getPrimeFactors 600851475143L)
    //let value = Seq.max (getPrimeFactors 600851475149L) //Prime number

    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 310 ms

    0 // return an integer exit code
