namespace MyExpression.FSharp.Core

type Polynomial = Monomial list

module Polynomial =
    let create (x: Monomial list): Polynomial = x
