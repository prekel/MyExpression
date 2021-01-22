open FParsec

open MyExpression.FSharp.Core

open MyExpression.FSharp.FParsec

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



    let str s = pstring s
    let monomial = pfloat >>. str "x^" >>. pint32


    let ws = spaces // whitespace parser

    let str_ws str =
        parse {
            do! skipString str
            return ()
        }

    let number_ws =
        parse {
            let! number = pfloat
            return number
        }

    let pairOfNumbers =
        parse {
            let! number1 = pfloat
            do! str_ws "x^"
            let! number2 = pint32
            return Monomial.create number1 number2
        }
        
    Say.test pairOfNumbers "2x^3"

    0
