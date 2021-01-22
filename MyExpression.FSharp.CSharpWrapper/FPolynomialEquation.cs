// Copyright (c) 2021 Vladislav Prekel

using System.Collections.Generic;
using System.Linq;

using MyExpression.Core;
using MyExpression.FSharp.Core;

using PolynomialEquation = MyExpression.FSharp.Core.PolynomialEquation;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FPolynomialEquation : IPolynomialEquation
    {
        public IList<double>? AllRoots { get; private set; }
        public IList<double>? Roots { get; private set; }

        public void Solve()
        {
            var roots = PolynomialEquation.solve(Epsilon,
                Polynomial
                    .Select(u => MonomialModule.create(u.Coefficient, u.Degree)).ToList().ToFSharpList());
            AllRoots = roots.ToList();
            Roots = AllRoots.Distinct().ToList();
        }

        public double Epsilon { get; }
        public IPolynomial Polynomial { get; }

        public FPolynomialEquation(IPolynomial poly, double eps)
        {
            Polynomial = poly;
            Epsilon = eps;
        }
    }
}
