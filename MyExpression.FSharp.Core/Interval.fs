// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

open MyExpression.FSharp.Core.Types

module Interval =
    let create left right = Interval(left, right)
    let ofTuple (left, right) = Interval(left, right)
    let toTuple (Interval (left, right)) = struct (left, right)

    let ofList list =
        create (list |> List.head) (list |> List.tail |> List.head)

    let tryOfList list =
        match list with
        | [ left; right ] -> Some ^ create left right
        | _ -> None

    let isInInterval x (Interval (left, right)) = left <= x && x <= right

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
        | _ ->
            let l =
                roots |> List.take (List.length roots - 1)

            let r = roots |> List.tail

            let first =
                (negativeInfinityInterval (roots |> List.head))

            let list = List.map2 create l r

            let last =
                (positiveInfinityInterval (roots |> List.last))

            [ first ] @ list @ [ last ]

    let left (Interval (left, _)) = left
    let right (Interval (_, right)) = right

    let difference (Interval (left, right)) = abs (left - right)
