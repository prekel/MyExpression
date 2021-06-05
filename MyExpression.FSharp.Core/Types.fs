module MyExpression.FSharp.Core.Types

[<Struct>]
type Interval = Interval of struct (float * float)

[<Struct>]
type Monomial = { Degree: int; Coefficient: float }

[<Struct>]
type Polynomial = Polynomial of Monomial list

[<Struct>]
type LinearEquation = { A: float; B: float }
