// Copyright (c) 2021 Vladislav Prekel

using System;

using MyExpression.FSharp.CSharpWrapper;

using NUnit.Framework;

namespace MyExpression.Core.Tests.FSharp
{
    [TestFixture]
    public class FBinarySearchTests : AbstractBinarySearchTests
    {
        protected override IBinarySearch CreateBinarySearch(Func<double, double> f, Interval lr, double eps) =>
            new FBinarySearch(f, lr, eps);

        protected override IBinarySearch CreateBinarySearch(Func<double, double> f, Interval lr, double eq,
            double eps)
        {
            Func<double, double> f1 = (x) => f(x) - eq;
            return new FBinarySearch(f1, lr, eps);
        }
    }
}
