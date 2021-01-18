namespace MyExpression.FSharp.Core

type Monomial = { Coefficient: float; Degree: int }

module Monomial =
    let create coef deg = { Coefficient = coef; Degree = deg }

    let derivative x =
        create (x.Coefficient * float x.Degree) (x.Degree - 1)
