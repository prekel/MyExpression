module App

open System
open Feliz

open MyExpression.FSharp.Core

open Parsec

type private State = { Polynomial: Polynomial }

let private test p str =
    match run p "" (StringSegment.ofString str) with
    | Ok (result) -> printfn "Success: %A" result
    | Error (errorMsg) -> printfn "Failure:"

type private Msg =
    | SetABCD of float * float * float * float
    | SetMonomial of Monomial

let private update (state: State) b =
    match b with
    | SetABCD (a, b, c, d) ->
        { Polynomial =
              [ Monomial.create a 3
                Monomial.create b 2
                Monomial.create c 1
                Monomial.create d 0 ]
              |> Polynomial.ofList }
    | SetMonomial m ->
        { Polynomial =
              state.Polynomial
              |> List.filter (fun m -> m.Degree <= 3)
              |> List.append [ m ] }

let tryParseFloat (s: string) =  
    let c, a = Double.TryParse(s)

    match c with
    | true -> Some a
    | false -> None

[<ReactComponent>]
let HelloWorld () =
    let a, setA = React.useState ("1.")
    let b, setB = React.useState ("-2.")
    let c, setC = React.useState ("-1.")
    let d, setD = React.useState ("2.")
    let m, setM = React.useState ("2.0x^4")

    let state, dispatch =
        React.useReducer
            (update,
             { Polynomial =
                   [ Monomial.create 1. 3
                     Monomial.create -2. 2
                     Monomial.create -1. 1
                     Monomial.create 2. 0 ]
                   |> Polynomial.ofList })

    React.useEffect
        ((fun _ ->
            let y = ()

            SetABCD(Double.Parse(a), Double.Parse(b), Double.Parse(c), Double.Parse(d))
            |> dispatch),
         [| a :> obj
            b :> obj
            c :> obj
            d :> obj |])

    let abcd, setABCD =
        React.useState ({| A = 1.; B = -2.; C = -1.; D = 2. |})

    let parsed, setParsed = React.useState (1.0)

    React.fragment [ Html.h1
                         (state.Polynomial
                          |> PolynomialEquation.solve 1e-4
                          |> sprintf "%A")
                     Html.input [ prop.onChange setM ]
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

                                       let u =
                                           run pairOfNumbers "" (StringSegment.ofString m)

                                       match u with
                                       | Ok p ->
                                           let i1, i2, i3 = p
                                           SetMonomial i1 |> dispatch
                                           printf "success %A" p
                                       | Error b -> printf "error %A" b) ]

                     Html.p [ prop.text parsed ] ]
