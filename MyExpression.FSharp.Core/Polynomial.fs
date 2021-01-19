namespace MyExpression.FSharp.Core

type Polynomial = Monomial list

module Polynomial =
    let ofList (x: Monomial list): Polynomial = x

    let derivative (xs: Polynomial) =
        xs |> List.map Monomial.derivative |> ofList

    let normalize (xs: Polynomial) =
        xs
        |> List.sortByDescending (fun m -> m.Degree)
        |> List.groupBy (fun t -> t.Degree)
        |> List.map (fun t ->
            snd t
            |> List.reduce (fun s t1 -> (Monomial.create (s.Coefficient + t1.Coefficient) (fst t))))
        |> ofList

    let degree poly =
        poly |> normalize |> List.head |> Monomial.degree

    let byDegree degree (poly: Polynomial) =
        let mono =
            poly |> List.tryFind (fun m -> m.Degree = degree)

        match mono with
        | Some (m) -> m
        | None -> Monomial.create 0. degree

    let calc (poly: Polynomial) x =
        poly |> List.map (Monomial.calcByX x) |> List.sum
