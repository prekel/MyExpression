// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.FSharp.Core

open System

[<AutoOpen>]
module Global =
    let notImplemented message =
        match message with
        | "" -> raise (NotImplementedException())
        | _ -> raise (NotImplementedException(message))

    let never () = failwith "Never"

#if FABLE_COMPILER
    open Fable.Core
    open Fable.Core.JsInterop
    
    [<Emit("isNaN($0)")>]
    let isNaN (x: 'a) = jsNative

    let (|Float|PositiveInfinity|NegativeInfinity|NaN|) n =
        if (fun n -> n = infinity) n then PositiveInfinity
        elif (fun n -> n = -infinity) n then NegativeInfinity
        elif isNaN n then NaN
        else Float n
#else
    let (|Float|PositiveInfinity|NegativeInfinity|NaN|) n =
        if Double.IsPositiveInfinity n then PositiveInfinity
        elif Double.IsNegativeInfinity n then NegativeInfinity
        elif Double.IsNaN n then NaN
        else Float n
#endif
