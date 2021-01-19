// Copyright (c) 2021 Vladislav Prekel

using System;

using Microsoft.FSharp.Core;

using MyExpression.Core;
using MyExpression.FSharp.Core;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FBinarySearch : IBinarySearch
    {
        public IInterval StartInterval { get; }
        public double Epsilon { get; }
        public double Answer { get; private set; }
        public Func<double, double> Function { get; }

        public double Solve()
        {
            Answer = BinarySearch.search(
                FSharpFunc<double, double>.FromConverter(x => Function(x)),
                Epsilon,
                StartInterval.Left, StartInterval.Right);
            return Answer;
        }

        public FBinarySearch(Func<double, double> f, IInterval lr, double eps)
        {
            Function = f;
            StartInterval = lr;
            Epsilon = eps;
        }
    }
}
