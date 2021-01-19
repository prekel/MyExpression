namespace MyExpression.FSharp.Core

open System

type PolynomialEquation =
    { Polynomial: Polynomial
      Epsilon: float }

module PolynomialEquation =
    let create eps poly = { Polynomial = poly; Epsilon = eps }

    let derivative equation =
        { equation with
              Polynomial = equation.Polynomial |> Polynomial.derivative }

    let solve equation =
        let norm =
            { equation with
                  Polynomial = equation.Polynomial |> Polynomial.normalize }

        let poly = norm.Polynomial
        let eps = norm.Epsilon

        let rec solveRec poly =
            let calc = Polynomial.calc poly

            match poly |> Polynomial.degree with
            | 1 ->
                poly
                |> LinearEquation.ofPolynomial
                |> LinearEquation.solve
                |> fun t -> [ t ]
            | _ ->
                let der = Polynomial.derivative poly
                let derRoots = solveRec der
                let intervals = Interval.ofList derRoots

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

    let calc equation = Polynomial.calc equation.Polynomial

    let check equation x =
        Math.Abs(calc equation x) < equation.Epsilon
