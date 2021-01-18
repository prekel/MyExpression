namespace MyExpression.FSharp.Core

type Polynomial = Monomial list

module Polynomial =
    let create (x: Monomial list): Polynomial = x

    let derivative (xs: Polynomial) =
        xs |> List.map Monomial.derivative |> create

    let normalize (xs: Polynomial) =
        xs
        |> List.sortByDescending (fun m -> m.Degree)
        |> List.groupBy (fun t -> t.Degree)
        |> List.map (fun t ->
            snd t
            |> List.reduce (fun s t1 -> (Monomial.create (s.Coefficient + t1.Coefficient) (fst t))))
        |> create
