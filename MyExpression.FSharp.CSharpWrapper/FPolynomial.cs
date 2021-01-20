// Copyright (c) 2021 Vladislav Prekel

using System.Collections;
using System.Collections.Generic;

using MyExpression.Core;


namespace MyExpression.FSharp.CSharpWrapper
{
    public class FPolynomial : IPolynomial
    {
        public double Calculate(double x) => throw new System.NotImplementedException();

        public IFunctionX Derivative { get; }
        public IEnumerator<IMonomial> GetEnumerator() => throw new System.NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        

        public FPolynomial(IPolynomial a)
        {
        }

        public FPolynomial(params double[] v)
        {
        }
    }
}
