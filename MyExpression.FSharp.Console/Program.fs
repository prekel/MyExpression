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


    let le = LinearEquation.create 12. 3.

    let lea = le |> LinearEquation.solve
    let check = le |> LinearEquation.check
    let check1 = check 1.
    let checka = check lea

//    let p =
//        [ Monomial.create 12.3 1
//          Monomial.create 2.0 0
//          Monomial.create 2.0 0 ]
//        |> Polynomial.ofList
//        |> Polynomial.normalize
//
//    let exn = p |> PolynomialEquation.create 1e-8
//    let exn1 = exn |> PolynomialEquation.solve

    let p =
        [ Monomial.create 1. 3
          Monomial.create -2.0 2
          Monomial.create -1.0 1
          Monomial.create 2.0 0 ]
        |> Polynomial.ofList
        |> Polynomial.normalize
        |> PolynomialEquation.solve 1e-12

    0 // return an integer exit code
