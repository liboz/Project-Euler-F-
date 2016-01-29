// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics


type PrimeFactor = {number: int; count: int}

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    //let baseArray = [|1..10|] //test
    let baseArray = [|11L..20L|] //Can start with 11 because 2520 is divisible by 1 to 10

    let isDivisibleByAll x y = y
                               |> Array.map (fun i -> x % i)
                               |> Array.exists (fun i -> i <> 0L)
                               |> not
    let rec gcd x y = 
        if y = 0L then x
        else gcd y (x % y)

    let lcm x y = x*y/(gcd x y)

    let value = baseArray
                |> Array.fold (fun x y -> lcm x y) 1L
    
    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 2ms with int64

    stopWatch.Reset()
    stopWatch.Start()
    
    let value = Seq.unfold (fun x -> Some(x, x + 2520L)) 2520L //We have to be divisible by 2520 because 1-10 is a subset of 1-20.
                  |> Seq.filter (fun x -> (isDivisibleByAll x baseArray) = true)
                  |> Seq.item(0)

    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 30ms with int64, but much longer when using bigger range 

    0 // return an integer exit code
