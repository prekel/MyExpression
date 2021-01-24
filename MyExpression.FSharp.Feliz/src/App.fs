module App

open System
open Feliz

open MyExpression.FSharp.Core

open Parsec

let test p str =
    match run p "" (StringSegment.ofString str) with
    | Ok (result) -> printfn "Success: %A" result
    | Error (errorMsg) -> printfn "Failure:"

[<ReactComponent>]
let HelloWorld () =
    let a, setA = React.useState ("1.")
    let b, setB = React.useState ("-2.")
    let c, setC = React.useState ("-1.")
    let d, setD = React.useState ("2.")

    let abcd, setABCD =
        React.useState ({| A = 1.; B = -2.; C = -1.; D = 2. |})

    let parsed, setParsed = React.useState (1.0)

    React.fragment [ Html.h1
                         ([ Monomial.create abcd.A 3
                            Monomial.create abcd.B 2
                            Monomial.create abcd.C 1
                            Monomial.create abcd.D 0 ]
                          |> Polynomial.ofList
                          |> Polynomial.normalize
                          |> PolynomialEquation.solve 1e-4
                          |> sprintf "%A")
                     Html.input [ prop.value a
                                  prop.onChange setA ]
                     Html.input [ prop.value b
                                  prop.onChange setB ]
                     Html.input [ prop.value c
                                  prop.onChange setC ]
                     Html.input [ prop.value d
                                  prop.onChange setD ]
                     Html.button [ prop.text "Calc"
                                   prop.onClick (fun _ ->
                                       let c1, a' = Double.TryParse(a)
                                       let c2, b' = Double.TryParse(b)
                                       let c3, c' = Double.TryParse(c)
                                       let c4, d' = Double.TryParse(d)

                                       if c1 && c2 && c3 && c4
                                       then setABCD ({| A = a'; B = b'; C = c'; D = d' |})) ]
                     Html.button [ prop.text "Parse"
                                   prop.onClick (fun _ ->
                                       let y = pfloat

                                       let u = run y "" (StringSegment.ofString a)

                                       match u with
                                       | Ok p ->
                                           let i1, i2, i3 = p
                                           setParsed i1
                                           printf "success %A" p
                                       | Error b -> printf "error %A" b) ]

                     Html.p [ prop.text parsed ] ]
