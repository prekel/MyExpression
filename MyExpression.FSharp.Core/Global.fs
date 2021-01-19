namespace MyExpression.FSharp.Core

open System

[<AutoOpen>]
module Global =
    let notImplemented message =
        match message with
        | "" -> raise (NotImplementedException())
        | _ -> raise (NotImplementedException(message))
