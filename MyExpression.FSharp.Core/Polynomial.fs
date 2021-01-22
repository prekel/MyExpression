// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

type Polynomial = Monomial list

module Polynomial =
    let ofList (x: Monomial list): Polynomial = x

    let derivative (xs: Polynomial) =
        xs |> List.map Monomial.derivative |> ofList

    let normalize (xs: Polynomial) =
        xs
        |> List.sortByDescending (fun m -> m.Degree)
        |> List.groupBy (fun m -> m.Degree)
        |> List.map (fun t ->
            snd t
            |> List.reduce (fun m1 m2 -> (Monomial.create (m1.Coefficient + m2.Coefficient) (fst t))))
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
        poly |> List.find (fun m -> m.Degree = degree) |> Monomial.coefficient
