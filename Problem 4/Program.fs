// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.Diagnostics

[<EntryPoint>]
let main argv = 
    let stopWatch = Stopwatch.StartNew()
    
    let isPalindrome (x:int) = 
        let chars = x.ToString().ToCharArray()
        Array.rev chars = chars

    let multiplyByVector x y = y
                                |> Array.filter (fun i -> i > x) // To not do extra multiplications
                                |> Array.map (fun i -> i * x)

    let ThreeDigitNumbers = [|100..999|]
    let value = ThreeDigitNumbers
                |> Array.map (fun i -> multiplyByVector i ThreeDigitNumbers)
                |> Array.collect id
                |> Array.filter (fun i -> isPalindrome i)
                |> Array.max

    stopWatch.Stop()
    printfn "The value is %d and it took %f" value stopWatch.Elapsed.TotalMilliseconds //About 180 ms

    0 // return an integer exit code
