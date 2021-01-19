// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

open System

[<AutoOpen>]
module Global =
    let notImplemented message =
        match message with
        | "" -> raise (NotImplementedException())
        | _ -> raise (NotImplementedException(message))

    let (|Float|PositiveInfinity|NegativeInfinity|NaN|) n =
        if Double.IsPositiveInfinity n then PositiveInfinity
        elif Double.IsNegativeInfinity n then NegativeInfinity
        elif Double.IsNaN n then NaN
        else Float n

    let never () = failwith "Never"