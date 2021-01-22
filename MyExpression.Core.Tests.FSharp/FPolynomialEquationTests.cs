// Copyright (c) 2021 Vladislav Prekel

using System.Collections.Generic;

using MyExpression.FSharp.CSharpWrapper;

using NUnit.Framework;

namespace MyExpression.Core.Tests.FSharp
{
    [TestFixture]
    public class FPolynomialEquationTests : AbstractPolynomialEquationTests
    {
        protected override IPolynomialEquation CreatePolynomialEquation(IPolynomial poly, double eps) =>
            new FPolynomialEquation(poly, eps);

        protected override IPolynomial CreatePolynomial(IEnumerable<IMonomial> a) => new FPolynomial(a);

        protected override IPolynomial CreatePolynomial(params double[] v) => new FPolynomial(v);
    }
}
