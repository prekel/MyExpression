// Copyright (c) 2021 Vladislav Prekel

using System.Collections.Generic;

using MyExpression.Core;
using MyExpression.FSharp.Core;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FSharpLinearEquation : ILinearEquation
    {
        public double A => _equation.A;
        public double B => _equation.B;
        public double X => LinearEquationModule.solve(_equation);

        private readonly Core.LinearEquation _equation;

        public IList<double> AllRoots => new List<double>(new[] {X});
        public IList<double> Roots => AllRoots;

        public FSharpLinearEquation(double a, double b)
        {
            _equation = LinearEquationModule.create(a, b);
        }

        public void Solve()
        {
        }
    }
}
