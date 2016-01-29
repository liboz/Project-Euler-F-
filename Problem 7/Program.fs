// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics


type PrimeFactor = {number: int; count: int}

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    let maxSearchPrime x = int (System.Math.Round (sqrt (float x)))
    
    let isPrime (x) = not ({3..(maxSearchPrime x)} |> Seq.exists (fun i -> x % i = 0))

    let value = Seq.unfold (fun x -> Some(x, x + 2)) 3
                |> Seq.filter (fun i -> isPrime i)
                |> Seq.item(9999) //This is 10000th item instead of 10001 because we skip the number 2. Note 0-index

    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 220ms 
    0 // return an integer exit code
