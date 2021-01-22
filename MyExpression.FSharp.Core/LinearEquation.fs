// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

type LinearEquation = { A: float; B: float }

module LinearEquation =
    let create a b = { A = a; B = b }

    let ofTuple (a, b) = create a b

    let solve linear = -linear.B / linear.A

    let ofPolynomial poly =
        create
            (poly
             |> Polynomial.byDegree 1
             |> Monomial.coefficient)
            (poly
             |> Polynomial.byDegree 0
             |> Monomial.coefficient)

    let calc linear x = linear.A * x + linear.B

    let checkAccurate linear x = (calc linear x) = 0.
    
    let check linear eps x = abs (calc linear x) < eps
