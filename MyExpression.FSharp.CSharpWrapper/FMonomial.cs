using System;

using MyExpression.Core;
using MyExpression.FSharp.Core;

using Monomial = MyExpression.FSharp.Core.Monomial;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FMonomial : IMonomial
    {
        public double Calculate(double x) => MonomialModule.calc(_monomial, x); 

        public IFunctionX Derivative => new FMonomial(MonomialModule.derivative(_monomial));
        public double Coefficient => _monomial.Coefficient;
        public int Degree => _monomial.Degree;

        private readonly Monomial _monomial;

        public FMonomial(double coef, int degree) => _monomial = MonomialModule.create(coef, (int) degree);

        private FMonomial(Monomial monomial) => _monomial = monomial;
    }
}
