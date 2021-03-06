// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

module PolynomialEquation =
    let private filterIntervals eps calc =
        List.filter (fun (i: Interval) ->
            match i with
            | (NegativeInfinity, right) ->
                let a, b = (calc (right - 1.), calc right)
                not (a > b && b > eps || a < b && b < -eps)
            | (left, PositiveInfinity) ->
                let a, b = (calc left, calc (left + 1.))
                not (a > b && a < -eps || a < b && a > eps)
            | (left, right) ->
                let a, b = (calc left, calc right)
                not (sign (a) * sign (b) > 0 && abs (a * b) >= eps))

    let solve eps poly =
        let rec solveRec poly =
            match poly |> Polynomial.degree with
            | 1 ->
                poly
                |> LinearEquation.ofPolynomial
                |> LinearEquation.solve
                |> List.singleton
            | _ ->
                let calc = Polynomial.calc poly

                poly
                |> Polynomial.derivative
                |> solveRec
                |> Interval.intervalsOfList
                |> filterIntervals eps calc
                |> List.map (BinarySearch.search calc eps)
                |> List.sort

        poly |> Polynomial.normalize |> solveRec

    let check poly eps x = abs (Polynomial.calc poly x) < eps
