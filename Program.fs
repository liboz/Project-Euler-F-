// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let divisibleby3or5 x = 
        if x % 3 = 0 || x % 5 = 0 then
            x
        else
            0
     
    let sumValue = 
        [1..999]
        |> List.map(divisibleby3or5)
        |> List.sum
    printfn "The sum total is %d" sumValue
    0 // return an integer exit code
