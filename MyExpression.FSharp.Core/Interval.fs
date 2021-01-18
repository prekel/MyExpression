namespace MyExpression.FSharp.Core

type Interval = { Left: float; Right: float }

module Interval =
    let create left right = { Left = left; Right = right }

    let isInInterval x interval =
        x >= interval.Left && x <= interval.Right
