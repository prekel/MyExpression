// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

open MyExpression.FSharp.Core.Types

module Monomial =
    let create coefficient deg =
        { Coefficient = coefficient
          Degree = deg }

    let tryCreate coefficient deg =
        if deg > 0 then
            ValueSome
                { Coefficient = coefficient
                  Degree = deg }
        else
            ValueNone

    let ofTuple (coefficient, deg) = create coefficient deg

    let tryOfTuple (coefficient, deg) = tryCreate coefficient deg

    let derivative x =
        match x.Degree with
        | 0 -> create 0. 0
        | _ -> create (x.Coefficient * float x.Degree) (x.Degree - 1)

    let tryDerivative x =
        match x.Degree with
        | 0 -> ValueNone
        | _ ->
            create (x.Coefficient * float x.Degree) (x.Degree - 1)
            |> ValueSome

    let coefficient monomial = monomial.Coefficient

    let degree monomial = monomial.Degree

    let calc monomial x =
        monomial.Coefficient * x ** float monomial.Degree

    let calcByX x monomial = calc monomial x
