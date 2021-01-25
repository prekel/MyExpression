// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

type Interval = float * float

module Interval =
    let create left right: Interval = left, right
    let ofTuple (left, right) = create left right

    let ofList list =
        create (list |> List.head) (list |> List.tail |> List.head)

    let isInInterval x (interval: Interval) = fst interval <= x && x <= snd interval

    let infinityInterval = create -infinity infinity

    let negativeInfinityInterval a = create -infinity a

    let positiveInfinityInterval a = create a infinity

    let intervalsOfList roots =
        match roots with
        | [] -> [ infinityInterval ]
        | [ a ] ->
            [ negativeInfinityInterval a
              positiveInfinityInterval a ]
        | a :: xs ->
            let w =
                roots |> List.windowed 2 |> List.map ofList

            (negativeInfinityInterval a) :: w
            @ [ positiveInfinityInterval (xs |> List.last) ]

    let left (interval: Interval) = fst interval
    let right (interval: Interval) = snd interval

    let difference ((left, right): Interval) = abs (left - right)
