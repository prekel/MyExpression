// Copyright (c) 2021 Vladislav Prekel

using NUnit.Framework;

using MyExpression.FSharp.CSharpWrapper;

namespace MyExpression.Core.Tests.FSharp
{
    [TestFixture]
    public class FLinearEquationTests : AbstractLinearEquationTests
    {
        protected override ILinearEquation CreateEquation(double a, double b) => new FSharpLinearEquation(a, b);
    }
}
