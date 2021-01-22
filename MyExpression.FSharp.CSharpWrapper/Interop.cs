// Copyright (c) 2021 Vladislav Prekel

using System.Collections.Generic;

using Microsoft.FSharp.Collections;

namespace MyExpression.FSharp.CSharpWrapper
{
    public static class Interop
    {
        public static FSharpList<T> ToFSharpList<T>(this IList<T> input) => CreateFSharpList(input, 0);

        private static FSharpList<T> CreateFSharpList<T>(IList<T> input, int index) => index >= input.Count
            ? FSharpList<T>.Empty
            : FSharpList<T>.Cons(input[index], CreateFSharpList(input, index + 1));
    }
}
