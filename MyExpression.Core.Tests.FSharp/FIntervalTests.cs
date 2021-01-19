// Copyright (c) 2018 Vladislav Prekel

using NUnit.Framework;

using MyExpression.FSharp.CSharpWrapper;

namespace MyExpression.Core.Tests.FSharp
{
    [TestFixture]
    public class FIntervalTests : AbstractIntervalTests
    {
        protected override IInterval CreateInterval(double left, double right) => new FInterval(left, right);
    }
}
