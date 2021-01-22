open MyExpression.FSharp.Core

[<EntryPoint>]
let main argv =
    let le = LinearEquation.create 12. 3.

    let lea = le |> LinearEquation.solve
    let check = le |> LinearEquation.check
    let check1 = check 1.
    let checka = check lea

    let p =
        [ Monomial.create 1. 3
          Monomial.create -2.0 2
          Monomial.create -1.0 1
          Monomial.create 2.0 0 ]
        |> Polynomial.ofList
        |> Polynomial.normalize
        |> PolynomialEquation.solve 1e-12

    let r = [ 1.; 2.; 3.; 4.; 5. ]
    let p1 = Polynomial.ofRoots r
    let r1 = PolynomialEquation.solve 1e-5 p1

    let tr = [ -15. .. 15. ]
    let tp1 = Polynomial.ofRoots tr
    let tr1 = PolynomialEquation.solve 1e-8 tp1

    0
