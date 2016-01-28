// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let value = (1, 1)
                |>Seq.unfold (fun (current, old) -> 
                    if (current > 4000000) then None 
                    else Some(current + old, (current + old, current))) 
                |> Seq.filter (fun x -> x % 2 = 0)
                |> Seq.sum
    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 4 ms

    stopWatch.Reset()

    let rec fibonacci x = 
        if x = 0 then
            1
        elif x = 1 then
            1
        else
            fibonacci (x-1) + fibonacci (x-2)

    stopWatch.Start()
    let value2 =[1..32] 
                |> List.map (fun x -> fibonacci x)
                |> List.filter (fun x -> x % 2 = 0)
                |> List.sum

    stopWatch.Stop()
    printfn "The value2 is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 92 ms
    0 // return an integer exit code
