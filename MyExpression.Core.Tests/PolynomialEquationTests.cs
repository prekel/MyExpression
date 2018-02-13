// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class PolynomialEquationTests
	{
		private static double CubicDiscriminant(double a, double b, double c, double d)
		{
			var c1 = -4 * b * b * b * d;
			var c2 = b * b * c * c;
			var c3 = -4 * a * c * c * c;
			var c4 = 18 * a * b * c * d;
			var c5 = -27 * a * a * d * d;
			return c1 + c2 + c3 + c4 + c5;
		}

		[Test]
		public void CubicEquation_Random_3Roots()
		{
			var r = new MyRandom();
			double a, b, c, d;
			while (true)
			{
				a = r.Next(1, 100) * r.NextDouble() * r.NextSign();
				b = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				c = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				d = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				if (CubicDiscriminant(a, b, c, d) > 0) break;
			}

			var p = new Polynomial
			{
				new Monomial(a, 3),
				new Monomial(b, 2),
				new Monomial(c, 1),
				new Monomial(d, 0),
			};
			var pe = new PolynomialEquation(p, 1e-7);
			pe.Solve();

			var x1 = pe.Roots[0];
			var x2 = pe.Roots[1];
			var x3 = pe.Roots[2];

			Assert.AreEqual(-b / a, x1 + x2 + x3, 1e-4);
			Assert.AreEqual(c / a, x1 * x2 + x2 * x3 + x1 * x3, 1e-4);
			Assert.AreEqual(-d / a, x1 * x2 * x3, 1e-4);

			if (Math.Abs(d) >= 0.01)
			{
				Assert.AreEqual(-c / d, 1 / x1 + 1 / x2 + 1 / x3, 1e-4);
				Assert.AreEqual(b / d, 1 / (x1 * x2) + 1 / (x2 * x3) + 1 / (x1 * x3), 1e-4);
			}
		}
	}
}
