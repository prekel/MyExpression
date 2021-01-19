namespace MyExpression.FSharp.Core


open System

type Interval = float * float

module Interval =
    let create left right = Interval(left, right)
    let ofTuple (left, right) = Interval(left, right)

    let isInInterval x interval = fst interval <= x && x <= snd interval

    let infinityInterval = create -infinity infinity

    let positiveInfinityInterval a = create -infinity a

    let negativeInfinityInterval a = create a infinity

    let ofList roots =
        match roots with
        | [] -> [ infinityInterval ]
        | [ a ] ->
            [ negativeInfinityInterval a
              positiveInfinityInterval a ]
        | [ a; b ] ->
            [ negativeInfinityInterval a
              create a b
              positiveInfinityInterval b ]
        | [ a; b; c ] ->
            [ negativeInfinityInterval a
              create a b
              create b c
              positiveInfinityInterval c ]
        | _ -> notImplemented ""

    let left (interval: Interval) = fst interval
    let right (interval: Interval) = snd interval

    let difference ((left, right): Interval) = Math.Abs(left - right)
