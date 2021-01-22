// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

type Monomial = { Degree: int; Coefficient: float }

module Monomial =
    let create coefficient deg =
        { Coefficient = coefficient
          Degree = deg }

    let ofTuple (coefficient, deg) = create coefficient deg

    let derivative x =
        match x.Degree with
        | 0 -> create 0. 0
        | _ -> create (x.Coefficient * float x.Degree) (x.Degree - 1)

    let coefficient monomial = monomial.Coefficient

    let degree monomial = monomial.Degree

    let calc monomial x =
        monomial.Coefficient * x ** float monomial.Degree

    let calcByX x monomial = calc monomial x
