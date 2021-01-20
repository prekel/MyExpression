// Copyright (c) 2018 Vladislav Prekel

using System.Collections.Generic;

using NUnit.Framework;

using MyExpression.FSharp.CSharpWrapper;

namespace MyExpression.Core.Tests.FSharp
{
    [TestFixture]
    public class FPolynomialTests : AbstractPolynomialTests
    {
        protected override IPolynomial CreatePolynomial(IEnumerable<IMonomial> a) => new FPolynomial(a);
        protected override IPolynomial CreatePolynomial(params double[] v) => new FPolynomial(v);
    }
}
