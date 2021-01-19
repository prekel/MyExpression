using MyExpression.FSharp.CSharpWrapper;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
    [TestFixture]
    public class FSharpLinearEquationTests : AbstractLinearEquationTests
    {
        public override ILinearEquation CreateEquation(double a, double b) => new FSharpLinearEquation(a, b);
    }
}
