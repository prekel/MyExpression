namespace MyExpression.FSharp.Core

type PolynomialEquation =
    { Polynomial: Polynomial
      Epsilon: float }

module PolynomialEquation =
    let create poly eps = { Polynomial = poly; Epsilon = eps }
