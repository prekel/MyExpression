// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

module BinarySearch =
    let search f eps interval =
        let median interval =
            match interval with
            | (NegativeInfinity, PositiveInfinity) -> 0.
            | (NegativeInfinity, right) -> right - 1.
            | (left, PositiveInfinity) -> left + 1.
            | (left, right) -> (left + right) / 2.

        let globalMedian = median interval

        let endsDifference =
            match interval with
            | (NegativeInfinity, PositiveInfinity) -> f 1.0 - f -1.
            | (NegativeInfinity, right) -> f right - f globalMedian
            | (left, PositiveInfinity) -> f globalMedian - f left
            | (left, right) -> f right - f left

        let isPositive = endsDifference > 0.
        let isNegative = endsDifference < 0.

        let comparer = if isPositive then (>) else (<)

        let isIntervalValueLessEps =
            match interval with
            | (left, _) when abs (f left) < eps -> Some left
            | (_, right) when abs (f right) < eps -> Some right
            | _ -> None

        let increaseInterval f interval =
            let increase k rl cnd f =
                let rec increaseRec k =
                    let rl1 = rl + k
                    if (f >> cnd) rl1 then rl1 else increaseRec k * 2.

                increaseRec k

            match interval with
            | (NegativeInfinity, PositiveInfinity) ->
                match f 0. with
                | z when z >= 0. && isPositive -> (increase -1. 0. (fun u -> u < 0.) f, 0.)
                | z when z >= 0. && isNegative -> (0., increase 1. 0. (fun u -> u < 0.) f)
                | z when z < 0. && isPositive -> (0., increase 1. 0. (fun u -> u > 0.) f)
                | z when z < 0. && isNegative -> (increase -1. 0. (fun u -> u > 0.) f, 0.)
                | _ -> never ()
            | (NegativeInfinity, right) when isPositive -> (increase -1. right (fun u -> u < 0.) f, right)
            | (NegativeInfinity, right) when isNegative -> (increase -1. right (fun u -> u > 0.) f, right)
            | (left, PositiveInfinity) when isPositive -> (left, increase 1. left (fun u -> u > 0.) f)
            | (left, PositiveInfinity) when isNegative -> (left, increase 1. left (fun u -> u < 0.) f)
            | x -> x

        let increasedInterval = increaseInterval f interval

        let rec recSearch (interval: Interval) =
            let localMedian = median interval
            let medianValue = f localMedian

            if Interval.difference interval < eps
            then localMedian
            elif comparer medianValue 0.
            then recSearch (interval |> Interval.left, localMedian)
            else recSearch (localMedian, interval |> Interval.right)

        match isIntervalValueLessEps with
        | Some x -> x
        | _ -> recSearch increasedInterval
