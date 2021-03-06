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
        
        if (globalMedian = -1.) then ()

        let endsDifference =
            match interval with
            | (NegativeInfinity, PositiveInfinity) -> f 1. - f -1.
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
            let increase k rl cnd =
                let rec increaseRec k =
                    let rl1 = rl + k
                    if cnd (f rl1) then rl1 else increaseRec (k * 2.)

                let t = increaseRec k
                if (System.Double.IsInfinity(t)) then never () else t

            let ltZero = (>) 0.
            let gtZero = (<) 0.

            match interval with
            | (NegativeInfinity, PositiveInfinity) ->
                match f 0. with
                | z when z >= 0. && isPositive -> (increase -1. 0. ltZero, 0.)
                | z when z >= 0. && isNegative -> (0., increase 1. 0. ltZero)
                | z when z < 0. && isPositive -> (0., increase 1. 0. gtZero)
                | z when z < 0. && isNegative -> (increase -1. 0. gtZero, 0.)
                | _ -> never ()
            | (NegativeInfinity, right) when isPositive -> (increase -1. right ltZero, right)
            | (NegativeInfinity, right) when isNegative -> (increase -1. right gtZero, right)
            | (left, PositiveInfinity) when isPositive -> (left, increase 1. left gtZero)
            | (left, PositiveInfinity) when isNegative -> (left, increase 1. left ltZero)
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
