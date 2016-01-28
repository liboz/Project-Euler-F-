// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    //let baseArray = [|1..10|] //test
    let baseArray = [|1..20|] 

    let isDivisibleByAll x y = y
                               |> Array.map (fun i -> x % i)
                               |> Array.exists (fun i -> i <> 0)
                               |> not
    
    let value = Seq.unfold (fun x -> Some(x, x + 20)) 2520
                  |> Seq.filter (fun x -> (isDivisibleByAll x baseArray) = true)
                  |> Seq.item(0)

    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 180 ms

    0 // return an integer exit code
