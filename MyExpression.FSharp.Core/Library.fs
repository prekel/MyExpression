namespace MyExpression.FSharp.Core

open System

module Core =

    type Monomial = { Coefficient: float; Degree: float }

    type Polynomial = Monomial list

    type Interval = { Left: float; Right: float }

    let isInInterval x interval =
        x >= interval.Left && x <= interval.Right


    type PolynomialEquation =
        { Polynomial: Polynomial
          Epsilon: float }

    type Derivativable = Monomial  | Polynomial
    
//    let derivative x =
//        match x with
//        | Monomial(y) -> y
//        | Polynomial -> failwith ""
            