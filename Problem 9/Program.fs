// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics

[<EntryPoint>]
let main argv = 
    
    let stopWatch = Stopwatch.StartNew()

    let isPythagoreanTriple a b c = (a*a + b*b = c*c) = true
    let findPossibleTripletPairs i array givenConstraint = array|> Array.filter (fun j -> j > i) 
                                                                |> Array.map (fun j -> (j, givenConstraint - j - i))
                                                                |> Array.filter (fun (j, k) -> j < k && isPythagoreanTriple i j k)

    let baseArray = [|1..1000|]
    let additionConstraint = 1000

    let value = baseArray
                |> Array.map (fun i -> (i, findPossibleTripletPairs i baseArray additionConstraint))
                |> Array.filter (fun (i, jk) -> (jk.Length <> 0))
                |> Array.map (fun (i, jk) -> 
                    (i, fst jk.[0], snd jk.[0]))

    let val1, val2, val3 = value.[0]

    stopWatch.Stop()
    printfn "The values are %d, %d, %d and it took %f" val1 val2 val3 stopWatch.Elapsed.TotalMilliseconds //About 25ms 

    0 // return an integer exit code
