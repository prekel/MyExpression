// Copyright (c) 2021 Vladislav Prekel

using System;

using MyExpression.Core;
using MyExpression.FSharp.Core;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FSharpInterval : IInterval
    {
        public double Left => _interval.Item1;
        public double Right => _interval.Item2;
        public bool IsInInterval(double x) => IntervalModule.isInInterval(x, _interval.Item1, _interval.Item2);

        private readonly Tuple<double, double> _interval;

        public FSharpInterval(double l, double r) => _interval = IntervalModule.create(l, r);
    }
}
