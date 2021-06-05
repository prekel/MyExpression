// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

open MyExpression.FSharp.Core.Types

module Polynomial =
    let ofList (x: Monomial list) = Polynomial x

    let normalize (Polynomial xs) =
        xs
        |> List.sortByDescending (fun m -> m.Degree)
        |> List.groupBy (fun m -> m.Degree)
        |> List.map
            (fun t ->
                snd t
                |> List.reduce (fun m1 m2 -> Monomial.create (m1.Coefficient + m2.Coefficient) (fst t)))
        |> ofList

    let isValid (Polynomial poly) =
        poly |> List.forall (fun m -> m.Degree > 0)

    let isNormalized poly = poly = (poly |> normalize)

    let derivative (Polynomial xs) =
        xs
        |> List.map Monomial.derivative
        |> Polynomial
        |> normalize

    let head (Polynomial xs) = List.head xs

    let degree poly =
        poly |> normalize |> head |> Monomial.degree

    let byDegree degree (Polynomial poly) =
        let mono =
            poly |> List.tryFind (fun m -> m.Degree = degree)

        match mono with
        | Some m -> m
        | None -> Monomial.create 0. degree

    let calc (Polynomial poly) x =
        poly |> List.map (Monomial.calcByX x) |> List.sum

    let coefficient (Polynomial poly) degree =
        poly
        |> List.find (fun m -> m.Degree = degree)
        |> Monomial.coefficient

    let sum (Polynomial a) (Polynomial b) = a @ b |> Polynomial |> normalize

    let multiplyWithLinearBinomial (Polynomial poly) x0 =
        let byX =
            poly
            |> List.map (fun m -> Monomial.create m.Coefficient (m.Degree + 1))
            |> Polynomial

        let byX0 =
            poly
            |> List.map (fun m -> Monomial.create (m.Coefficient * -x0) m.Degree)
            |> Polynomial

        sum byX byX0

    let rec ofRoots roots =
        match roots with
        | [] -> unreached ()
        | [ x ] ->
            [ Monomial.create 1.0 1
              Monomial.create -x 0 ]
            |> ofList
        | x :: xs -> multiplyWithLinearBinomial (ofRoots xs) x
