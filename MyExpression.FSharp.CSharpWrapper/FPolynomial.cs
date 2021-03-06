// Copyright (c) 2021 Vladislav Prekel

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.FSharp.Collections;

using MyExpression.Core;
using MyExpression.FSharp.Core;

using Monomial = MyExpression.FSharp.Core.Monomial;

namespace MyExpression.FSharp.CSharpWrapper
{
    public class FPolynomial : IPolynomial
    {
        public double Calculate(double x) => PolynomialModule.calc(_polynomial, x);

        public IFunctionX Derivative => new FPolynomial(PolynomialModule.derivative(_polynomial));

        // TODO: сделать отдельный список
        public IEnumerator<IMonomial> GetEnumerator() =>
            _polynomial.Select(u => new FMonomial(u.Coefficient, u.Degree)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly FSharpList<Monomial> _polynomial;

        private FPolynomial(FSharpList<Monomial> polynomial) => _polynomial = polynomial;


        public FPolynomial(params double[] v)
        {
            var list = new List<Monomial>();
            for (var i = v.Length - 1; i >= 0; i--)
            {
                list.Add(MonomialModule.create(v[v.Length - i - 1], i));
            }

            list.Reverse();
            _polynomial = PolynomialModule.ofList(Interop.ToFSharpList(list));
        }

        public FPolynomial(IEnumerable<IMonomial> a)
        {
            _polynomial = PolynomialModule.ofList(
                Interop.ToFSharpList(new List<IMonomial>(a)
                    .Select(u => MonomialModule.create(u.Coefficient, (int) u.Degree)).ToList()));
        }

        public IMonomial this[int degree] => this.ToList()[degree];
        public int Degree => PolynomialModule.degree(_polynomial);
    }
}
