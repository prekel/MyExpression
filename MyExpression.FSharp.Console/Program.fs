open MyExpression.FSharp.Core

// Define a function to construct a message to print
let from whom = sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message

//    let y =
//        { Left = -1.0; Right = 4.0 } |> isInInterval 3.0
//
//    printfn "%A" y
//
//    let mon = Monomial.create 3.0 2 
//
//    let d = derivative (Monomial mon)
//
//
//    let p =
//        Polynomial [ mon
//                     Monomial.create 12.3 3
//                     Monomial.create 2.0 4 ]

    //let pd = derivative p

    0 // return an integer exit code
