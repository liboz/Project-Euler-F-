// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let rec fibonacci x = 
        if x = 0 then
            1
        elif x = 1 then
            1
        else
            fibonacci (x-1) + fibonacci (x-2)
    
    let value = [1..32]
                |> List.map (fun x -> fibonacci x)
                |> List.filter (fun x -> x % 2 = 0)
                |> List.sum
    
    printfn "The value is %d." value
    0 // return an integer exit code
