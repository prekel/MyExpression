// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

type Polynomial = Monomial list

module Polynomial =
    let ofList (x: Monomial list): Polynomial = x

    let normalize (xs: Polynomial) =
        xs
        |> List.sortByDescending (fun m -> m.Degree)
        |> List.groupBy (fun m -> m.Degree)
        |> List.map (fun t ->
            snd t
            |> List.reduce (fun m1 m2 -> Monomial.create (m1.Coefficient + m2.Coefficient) (fst t)))
        |> ofList

    let isValid (poly: Polynomial) =
        poly |> List.forall (fun m -> m.Degree > 0)

    let isNormalized poly = poly = (poly |> normalize)

    let derivative (xs: Polynomial) =
        xs
        |> List.map Monomial.derivative
        |> normalize
        |> ofList

    let degree poly =
        poly |> normalize |> List.head |> Monomial.degree

    let byDegree degree (poly: Polynomial) =
        let mono =
            poly |> List.tryFind (fun m -> m.Degree = degree)

        match mono with
        | Some (m) -> m
        | None -> Monomial.create 0. degree

    let calc (poly: Polynomial) x =
        poly |> List.map (Monomial.calcByX x) |> List.sum

    let coefficient (poly: Polynomial) degree =
        poly
        |> List.find (fun m -> m.Degree = degree)
        |> Monomial.coefficient

    let sum (a: Polynomial) (b: Polynomial) = a @ b |> normalize

    let multiplyWithLinearBinomial (poly: Polynomial) x0 =
        let byX =
            poly
            |> List.map (fun m -> Monomial.create m.Coefficient (m.Degree + 1))

        let byX0 =
            poly
            |> List.map (fun m -> Monomial.create (m.Coefficient * -x0) m.Degree)

        sum byX byX0

    let ofRoots roots =
        let rec recOfRoots roots =
            match roots with
            | [] -> never ()
            | [ x ] ->
                [ Monomial.create 1.0 1
                  Monomial.create -x 0 ]
                |> ofList
            | x :: xs -> multiplyWithLinearBinomial (recOfRoots xs) x

        recOfRoots roots
