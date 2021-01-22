// Copyright (c) 2021 Vladislav Prekel

using NUnit.Framework;

using MyExpression.FSharp.CSharpWrapper;

namespace MyExpression.Core.Tests.FSharp
{
    [TestFixture]
    public class FMonomialTests : AbstractMonomialTests
    {
        protected override IMonomial CreateMonomial(double coef, int degree) => new FMonomial(coef, degree);
    }
}
