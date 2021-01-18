namespace MyExpression.FSharp.Core

module Derivative =

    type Interval = { Left: float; Right: float }

    let isInInterval x interval =
        x >= interval.Left && x <= interval.Right


    type Derivativable =
        | Mono of Monomial
        | Poly of Polynomial

    let rec derivative x =
        match x with
        | Mono (y) -> Mono(Monomial.derivative y)
        | Poly (y) ->
            (Poly
                (y
                 |> List.map (fun t ->
                     match derivative (Mono t) with
                     | Mono (u) -> u
                     | _ -> failwith "")))
