module App

open Feliz

open MyExpression.FSharp.Core

[<ReactComponent>]
let HelloWorld () =
    Html.h1
        ([ Monomial.create 1. 3
           Monomial.create -2. 2
           Monomial.create -1. 1
           Monomial.create 2. 0 ]
         |> Polynomial.ofList
         |> Polynomial.normalize
         |> PolynomialEquation.solve 1e-4
         |> sprintf "%A")
