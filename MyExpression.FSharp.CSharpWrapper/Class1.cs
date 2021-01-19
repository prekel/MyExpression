using System;
using System.Collections.Generic;

using MyExpression.Core;
using MyExpression.FSharp.Core;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FSharpLinearEquation : IEquation
    {
        public IList<double> AllRoots { get; private set; }
        public IList<double> Roots => AllRoots;

        private MyExpression.FSharp.Core.LinearEquation _equation;

        public FSharpLinearEquation(double a, double b)
        {
            _equation = LinearEquationModule.create(a, b);
        }

        public void Solve()
        {
            AllRoots = new List<double>(new[] {LinearEquationModule.solve(_equation)});
        }
    }
}
