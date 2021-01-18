namespace MyExpression.FSharp.Core

open System

module Core =

    type Monomial = { Coefficient: float; Degree: int }


    type Polynomial = Monomial list

    type Interval = { Left: float; Right: float }

    let isInInterval x interval =
        x >= interval.Left && x <= interval.Right

    let monomial coef deg = { Coefficient = coef; Degree = deg }

    type PolynomialEquation =
        { Polynomial: Polynomial
          Epsilon: float }

    type Derivativable =
        | Monomial of Monomial
        | Polynomial of Polynomial

    let rec derivative x =
        match x with
        | Monomial (y) ->
            Monomial
                { Coefficient = y.Coefficient * float y.Degree
                  Degree = y.Degree - 1 }
        | Polynomial (y) ->
            (Polynomial
                (y
                 |> List.map (fun t ->
                     match derivative (Monomial t) with
                     | Monomial (u) -> u
                     | _ -> failwith "")))
