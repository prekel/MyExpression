// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open MyExpression.FSharp.Core.Core

// Define a function to construct a message to print
let from whom = sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message

    let y =
        { Left = -1.0; Right = 4.0 }
        |> isInInterval 3.0

    printfn "%A" y
    
    0 // return an integer exit code
