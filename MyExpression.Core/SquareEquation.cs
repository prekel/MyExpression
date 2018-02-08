using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class SquareEquation : IEquation
	{
		public double A { get; set; }
		public double B { get; set; }
		public double C { get; set; }

		public double D => B * B - 4 * A * C;

		public double this[double x] => A * x * x + B * x + C;

		public int N
		{
			get
			{
				var d = D;
				if (d > 0)
					return 2;
				return d == 0 ? 1 : 0;
			}
		}

		public double X0 => -B / (2 * A);

		public double Y0 => this[X0];

		public double X1 => (-B + Math.Sqrt(D)) / (2 * A);

		public double X2 => (-B - Math.Sqrt(D)) / (2 * A);

		public Tuple<double, double> X => Tuple.Create(X1, X2);

		public IList<double> Roots
		{
			get
			{
				var ret = new List<double>(2);
				if (N == 1) ret.Add(X0);
				if (N == 2) ret.AddRange(new double[] { X1, X2 });
				return ret;
			}
		}

		public SquareEquation(double a = 1, double b = 0, double c = 0)
		{
			A = a;
			B = b;
			C = c;
		}

		public SquareEquation(PolynomialEquation p)
		{
			if (p.Polynomial.Degree != 2) throw new InvalidOperationException();
			A = p.Polynomial[2].Coefficient;
			B = p.Polynomial[1].Coefficient;
			C = p.Polynomial[0].Coefficient;
		}

		public Polynomial ToPolynomial()
		{
			var p = new Polynomial
			{
				new Monomial(A, 2),
				new Monomial(B, 1),
				new Monomial(C, 0)
			};
			return p;
		}

		public override string ToString()
		{
			return $"{A}x^2{(B >= 0 ? "+" + B : B.ToString())}{(C >= 0 ? "+" + C : C.ToString())}";
		}
	}
}
