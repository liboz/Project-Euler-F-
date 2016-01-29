// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics


type PrimeFactor = {number: int; count: int}

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    let baseArray = [|1..100|]

    let sumofSquares x = x 
                        |> Array.map (fun i -> i*i)
                        |> Array.sum

    let squareofSum x =  pown (Array.sum x) 2

    let value = (squareofSum baseArray) - (sumofSquares baseArray)

    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 0.9ms 
    0 // return an integer exit code
