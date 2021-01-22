namespace MyExpression.FSharp.FParsec

open FParsec

module Say =
    let hello name = printfn "Hello %s" name

    let test p str =
        match run p str with
        | Success (result, _, _) -> printfn "Success: %A" result
        | Failure (errorMsg, _, _) -> printfn "Failure: %s" errorMsg

    let str s = pstring s
    let floatBetweenBrackets () = str "[" >>. pfloat .>> str "]"
