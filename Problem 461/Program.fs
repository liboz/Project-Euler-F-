// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics
open System


let f n k = exp (double(k)/double(n)) - 1.0

let n = 1000
let maxLimit = int(Math.Round(float(n)*1.5))

let FunctionValues = [|1..maxLimit|] |> Array.map (fun k -> f n k)

let sums = FunctionValues |> Array.map (fun i -> Array.map (fun j -> j + i) FunctionValues) |> Array.concat |> Array.sort

let findBest (sums:double[]) = 
    let mutable (best:double) = 1.0
    let mutable bestI  = 0
    let mutable bestMid = 0

    for i in 1..(sums.Length - 1) do
        let mutable min = 0
        let mutable max = sums.Length - 1
        let mutable mid = 0
        while (max >= min ) do 
            mid <- (min + max)/2
            if sums.[i] + sums.[mid] <= Math.PI then
                min <- mid + 1
            else
                max <- mid - 1

        let mutable error = abs(sums.[i] + sums.[mid] - Math.PI)
        if error > abs(sums.[i] + sums.[(min + max)/2] - Math.PI) then
            mid <- (min + max)/2
            error <- abs(sums.[i] + sums.[mid] - Math.PI)
        if error < best then
            best <- error
            bestI <- i
            bestMid <- mid
    best, bestI, bestMid

let findBestAlg2 (sums:double[]) =
    let mutable (best:double) = 1.0
    let mutable bestI  = 0
    let mutable bestMid = 0

    let rec doBisect i min max = 
        let mid = (min + max)/2
        if max >= min then
            if sums.[i] + sums.[mid] <= Math.PI then doBisect i (mid + 1) max
            else doBisect i min (mid - 1)
        else
            mid

    let mutateBest i error mid =
            if error < best then
                best <- error
                bestI <- i
                bestMid <- mid
    let find = 
        for i in 1..(sums.Length - 1) do
            let mutable mid = doBisect i 0 (sums.Length - 1)
            let mutable error = abs(sums.[i] + sums.[mid] - Math.PI)
            if mid + 1 < sums.Length && error > abs(sums.[i] + sums.[mid + 1] - Math.PI) then
                mid <- mid + 1
                error <- abs(sums.[i] + sums.[mid] - Math.PI)
            if error < best then
                best <- error
                bestI <- i
                bestMid <- mid

        best, bestI, bestMid
    find 
(*
let findIntAlg1 bestI bestMid = 
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
    a, b, c, d


let findIntAlg2 bestI bestMid = 
    let findNM x = 
        let seq = {1..maxLimit} |> Seq.map (fun k -> (f n k, k))
        let get2nd3rd (a, b, c) = (b, c)
        seq |> Seq.map (fun (i, n) -> Seq.map (fun (j, m) -> (j + i, n, m) ) seq) 
            |> Seq.concat |> Seq.find (fun (i, n, m) -> i = x)
            |>  get2nd3rd

    let digitsBestI = findNM sums.[bestI]
    let digitsBestMid = findNM sums.[bestMid]

    let a = fst digitsBestI
    let b = snd digitsBestI
    let c = fst digitsBestMid
    let d = snd digitsBestMid
    a, b, c, d
    *)
let findIntAlg3 bestI bestMid = 

    let rec find target i j = 
        if target = f n i + f n j then i, j
        elif i = maxLimit && j = maxLimit then 0, 0
        elif j = maxLimit then find target (i+1) (i+1)
        else find target i (j+1)

    let a, b = find sums.[bestI] 1 1
    let c, d = find sums.[bestMid] 1 1
    a, b, c, d

(*
let algo4 bestI bestMid =
  let rec findI bestI mid i j = 
    if bestI = f n i + f n j then 
      let x, y = mid
      i, j, x, y
    elif i = maxLimit && j = maxLimit then 0, 0, 0, 0
    elif j = maxLimit then findI bestI mid (i + 1) ( i + 1)
    else findI bestI mid i (j + 1)

  let rec findMid ii bestMid i j = 
    if bestMid = f n i + f n j then 
      let x, y = ii
      x, y, i, j
    elif i = maxLimit && j = maxLimit then 0, 0, 0, 0
    elif j = maxLimit then findMid ii bestMid (i + 1) ( i + 1)
    else findMid ii bestMid i (j + 1)

  let rec find bestI bestMid i j = 
    if bestI = f n i + f n j then findMid (i, j) bestMid i j
    elif bestMid = f n i + f n j then findI bestI (i, j) i j
    elif i = maxLimit && j = maxLimit then 0, 0, 0, 0
    elif j = maxLimit then find bestI bestMid (i + 1) ( i + 1)
    else find bestI bestMid i (j + 1)

  find sums.[bestI] sums.[bestMid] 1 1
  *)

  
let outer   = 1000
let random = Random 19740531

let bests = 
  {500..outer}|> Seq.map (fun z ->  
     let maxLimit = int(Math.Round(float(z)*1.5))
     let FunctionValues = [|1..maxLimit|] |> Array.map (fun k -> f n k)
     FunctionValues |> Array.map (fun i -> Array.map (fun j -> j + i) FunctionValues) |> Array.concat |> Array.sort
    )

let stopWatch = 
  let sw = System.Diagnostics.Stopwatch ()
  sw.Start ()
  sw

let timeIt (name : string) (a : double[] -> 'T) : unit = 
  let t = stopWatch.ElapsedMilliseconds
  let v = a (bests|> Seq.item 0)
  for i = 1 to (outer - 500 - 1) do
    a (bests|> Seq.item i) |> ignore
  let d = stopWatch.ElapsedMilliseconds - t
  printfn "%s, elapsed %d ms, result %A" name d v

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    timeIt "alg1" findBest
    timeIt "alg2" findBestAlg2
    let algtime = stopWatch.Elapsed.TotalMilliseconds
    let best, bestI, bestMid = findBest sums
    printfn "Finding took %f and the result was %g %d %d" (stopWatch.Elapsed.TotalMilliseconds - algtime) best bestI bestMid
    let alg1time = stopWatch.Elapsed.TotalMilliseconds
    let best, bestI, bestMid = findBestAlg2 sums
    printfn "Finding Way 2 took %f and the result was %g %d %d" (stopWatch.Elapsed.TotalMilliseconds - alg1time) best bestI bestMid
    (*
    let time1= stopWatch.Elapsed.TotalMilliseconds
    let a, b, c, d = findIntAlg1 bestI bestMid
    printfn "Alg1 Took %f" (stopWatch.Elapsed.TotalMilliseconds - time1)
    let time2 = stopWatch.Elapsed.TotalMilliseconds
    let a, b, c, d = findIntAlg2 bestI bestMid
    printfn "Alg2 Took %f" (stopWatch.Elapsed.TotalMilliseconds - time2)
    *)
    let time3 = stopWatch.Elapsed.TotalMilliseconds
    let a, b, c, d = findIntAlg3 bestI bestMid
    printfn "Alg3 Took %f" (stopWatch.Elapsed.TotalMilliseconds - time3)
    (*
    let time4 = stopWatch.Elapsed.TotalMilliseconds
    let a, b, c, d = algo4 bestI bestMid
    printfn "Alg4 Took %f" (stopWatch.Elapsed.TotalMilliseconds - time4)
    *)
    stopWatch.Stop()
    printfn "The values are %g %d %d %d %d and it took %f" best a b c d  stopWatch.Elapsed.TotalMilliseconds //About 700ms 
    
    0 // return an integer exit code
