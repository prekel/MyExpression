// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

open System

module PolynomialEquation =

    let solve equation eps =
        let norm = equation |> Polynomial.normalize

        let poly = norm

        let rec solveRec poly =
            let calc = Polynomial.calc poly

            match poly |> Polynomial.degree with
            | 1 ->
                poly
                |> LinearEquation.ofPolynomial
                |> LinearEquation.solve
                |> List.singleton
            | _ ->
                let der = Polynomial.derivative poly
                let derRoots = solveRec der
                let intervals = Interval.intervalsOfList derRoots

                let filteredIntervals =
                    intervals
                    |> List.filter (fun i ->
                        not
                            (match i with
                             | (NegativeInfinity, right) ->
                                 let a, b = (calc (right - 1.), calc right)
                                 a > b && b > eps || a < b && b < -eps
                             | (left, PositiveInfinity) ->
                                 let a, b = (calc left, calc (left + 1.))
                                 a > b && a < -eps || a < b && a > eps
                             | (left, right) ->
                                 let a, b = (calc left, calc right)
                                 sign (a) * sign (b) > 0 && abs (a * b) >= eps))

                let roots =
                    filteredIntervals
                    |> List.map (BinarySearch.search calc eps)
                    |> List.sort

                roots

        solveRec poly

    let check poly eps x = Math.Abs(Polynomial.calc poly x) < eps
