// Copyright (c) 2021 Vladislav Prekel

namespace global

open System

[<AutoOpen>]
module Utils =
    let inline (^) a b = a b

    let inline notImplemented message =
        match message with
        | "" -> raise (NotImplementedException())
        | _ -> raise (NotImplementedException(message))

    let inline unimplemented a =
        match a with
        | "" -> raise ^ NotImplementedException()
        | _ -> raise ^ NotImplementedException a

    let (|Float|PositiveInfinity|NegativeInfinity|NaN|) n =
        if Double.IsPositiveInfinity n then PositiveInfinity
        elif Double.IsNegativeInfinity n then NegativeInfinity
        elif Double.IsNaN n then NaN
        else Float n

    let inline unreached () = failwith "Never"
