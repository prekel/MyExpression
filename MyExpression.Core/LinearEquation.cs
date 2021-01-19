// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class LinearEquation : ILinearEquation
	{
		public double A { get; set; }
		public double B { get; set; }

		public double X => -B / A;

		public IList<double> AllRoots => new List<double>(new double[] { X });
		public IList<double> Roots => AllRoots;

		public LinearEquation(double a, double b)
		{
			A = a;
			B = b;
		}

		public LinearEquation(PolynomialEquation p)
		{
			if (p.Polynomial.Degree != 1) throw new InvalidOperationException();
			A = p.Polynomial[1].Coefficient;
			B = p.Polynomial[0].Coefficient;
		}

		public Polynomial ToPolynomial()
		{
			var p = new Polynomial
			{
				new Monomial(A, 1),
				new Monomial(B, 0),
			};
			return p;
		}

		public static LinearEquation Parse(string s)
		{
			var p = Polynomial.Parse(s);
			return new LinearEquation(p[1].Coefficient, p[0].Coefficient);
		}

		public override string ToString()
		{
			var k = A.ToString();
			var m = B.ToString();
			var x = "x";
			if (A == 0) return m;
			if (A == 1) k = "";
			if (A == -1) k = "-";
			if (B > 0) m = "+" + m;
			if (B == 0) m = "";
			return $"{k}{x}{m}";
		}

		public void Solve()
		{
			
		}
	}
}
