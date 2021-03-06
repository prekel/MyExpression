﻿// Copyright (c) 2018 Vladislav Prekel

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

		public double X1 => (-B - Math.Sqrt(D)) / (2 * A);

		public double X2 => (-B + Math.Sqrt(D)) / (2 * A);

		public double XMin => A > 0 ? X1 : X2;

		public double XMax => A < 0 ? X1 : X2;

		public Tuple<double, double> X => Tuple.Create(X1, X2);

		public Tuple<double, double> XMinMax => Tuple.Create(XMin, XMax);

		public IList<double> AllRoots => new List<double>(new double[] { XMin, XMax });
		public IList<double> Roots
		{
			get
			{
				var ret = new List<double>(2);
				if (N == 1) ret.Add(X0);
				if (N == 2) ret.AddRange(new double[] { XMin, XMax });
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
			var a = A.ToString();
			var b = B.ToString();
			var c = C.ToString();
			if (A == 0) return new Straight(B, C).ToString();
			var x2 = "x^2";
			var x = "x";
			if (A == 1) a = "";
			if (A == -1) a = "-";
			if (B == 0) b = x = "";
			if (B == 1) b = "";
			if (B > 0) b = "+" + b;
			if (B == -1) b = "-";
			if (B == 1) b = "";
			if (B == -1) b = "-";
			if (C == 0) c = "";
			if (C > 0) c = "+" + c;
			return $"{a}{x2}{b}{x}{c}";
		}

		public void Solve()
		{
			
		}
	}
}
