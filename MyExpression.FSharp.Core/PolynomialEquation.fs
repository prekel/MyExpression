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

        let rec solveRec poly =
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
                
                []  

        solveRec poly

    let calc equation = Polynomial.calc equation.Polynomial

    let check equation x =
        Math.Abs(calc equation x) < equation.Epsilon
