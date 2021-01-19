namespace MyExpression.FSharp.Core

type Interval = { Left: float; Right: float }

module Interval =
    let create left right = { Left = left; Right = right }

    let isInInterval x interval =
        x >= interval.Left && x <= interval.Right

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
