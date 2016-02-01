// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics
open System.Collections.Generic
open System

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    let f n k = exp (double(k)/double(n)) - 1.0

    let n = 10000
    let maxLimit = int(Math.Round(float(n)*1.5))

    let FunctionValues = [|1..maxLimit|] |> Array.map (fun k -> f n k)

    let sums = FunctionValues |> Array.map (fun i -> Array.map (fun j -> j + i) FunctionValues) |> Array.concat |> Array.sort

    let mutable (best:double) = 1.0
    let mutable bestI  = 0
    let mutable bestMid = 0

    for i in 1..(sums.Length - 1) do
        let mutable min = 0
        let mutable max = sums.Length
        let mutable mid = 0
        while (max >= min ) do 
            mid <- (min + max)/2
            if sums.[i] + sums.[mid] <= Math.PI then
                min <- mid + 1
            else
                max <- mid - 1
        let error = abs(sums.[i] + sums.[mid] - 3.14159265358979323846)
        if error < best then
            best <- error
            bestI <- i
            bestMid <- mid
    
    let mutable a = 0
    let mutable b = 0
    let mutable c = 0
    let mutable d = 0

    for i in 1..maxLimit do
        for j in i..maxLimit do
            if sums.[bestI] = f n i + f n j then
                a <- i
                b <- j
            if sums.[bestMid] = f n i + f n j then
                c <- i
                d <- j

    stopWatch.Stop()
    printfn "The values are %g %d %d %d %d and it took %f" best a b c d  stopWatch.Elapsed.TotalMilliseconds //About 700ms 
    
    0 // return an integer exit code
