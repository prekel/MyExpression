namespace MyExpression.FSharp.Core

type Derivativable =
    | Mono of Monomial
    | Poly of Polynomial

module Derivative =

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

    let derivative1 =
        function
        | Mono (y) -> Mono(Monomial.derivative y)
        | Poly (y) -> Poly(Polynomial.derivative y)
