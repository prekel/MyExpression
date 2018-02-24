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

		public static double CubicVietaB(double a, params double[] x)
		{
			return -(x[0] + x[1] + x[2]) * a;
		}

		public static double CubicVietaC(double a, params double[] x)
		{
			return (x[0] * x[1] + x[1] * x[2] + x[0] * x[2]) * a;
		}

		public static double CubicVietaD(double a, params double[] x)
		{
			return -x[0] * x[1] * x[2] * a;
		}

		[Test]
		public void Cubic_Vieta_Dgt0_Random()
		{
			var r = new MyRandom();
			double a, b, c, d;
			while (true)
			{
				a = r.Next(1, 100) * r.NextDouble() * r.NextSign();
				b = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				c = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				d = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				if (CubicDiscriminant(a, b, c, d) > 0 && a != 0) break;
			}

			var p = new Polynomial
			{
				new Monomial(a, 3),
				new Monomial(b, 2),
				new Monomial(c, 1),
				new Monomial(d, 0),
			};
			var pe = new PolynomialEquation(p, 1e-8);
			pe.Solve();

			var x1 = pe.Roots[0];
			var x2 = pe.Roots[1];
			var x3 = pe.Roots[2];

			Assert.AreEqual(-b / a, x1 + x2 + x3, 1e-4);
			Assert.AreEqual(c / a, x1 * x2 + x2 * x3 + x1 * x3, 1e-4);
			Assert.AreEqual(-d / a, x1 * x2 * x3, 1e-4);

			if (Math.Abs(d) >= 0.01)
			{
				Assert.AreEqual(-c / d, 1 / x1 + 1 / x2 + 1 / x3, 1e-3);
				Assert.AreEqual(b / d, 1 / (x1 * x2) + 1 / (x2 * x3) + 1 / (x1 * x3), 1e-3);
			}
		}

		[Test]
		public void Cubic_Manual()
		{
			var e = new List<double[]>();
			var a = new List<double[]>();
			e.Add(new double[] { 8, -36, 54, -27 });
			a.Add(new double[] { 1.5 });
			e.Add(new double[] { 1, -2, -16, 32 });
			a.Add(new double[] { -4, 2, 4 });
			e.Add(new double[] { 2, -7, 4, -14 });
			a.Add(new double[] { 3.5 });
			e.Add(new double[] { -1, -5, 4, 20 });
			a.Add(new double[] { -5, -2, 2 });
			e.Add(new double[] { 3, -3, -0.75, 0.75 });
			a.Add(new double[] { -0.5, 0.5, 1 });
			e.Add(new double[] { 1, -2, -1, 1 });
			a.Add(new double[] { -0.801937735804838, 0.5549581320873701, 2.246979603717467 });

			for (var i = 0; i < e.Count; i++)
			{
				var pe = new PolynomialEquation(new Polynomial(e[i]), 1e-9);
				pe.Solve();
				for (var j = 0; j < pe.Roots.Count; j++)
				{
					Assert.AreEqual(a[i][j], pe.Roots[j], 1e-5);
				}
			}
		}

		[Test]
		public void Cubic_Int_Deq0_Random()
		{
			var r = new MyRandom();
			double a, b, c, d;
			while (true)
			{
				a = r.Next(1, 100) * r.NextSign();
				b = r.Next(0, 100) * r.NextSign();
				c = r.Next(0, 100) * r.NextSign();
				d = r.Next(0, 100) * r.NextSign();
				if (CubicDiscriminant(a, b, c, d) == 0) break;
			}

			var p = new Polynomial
			{
				new Monomial(a, 3),
				new Monomial(b, 2),
				new Monomial(c, 1),
				new Monomial(d, 0),
			};
			var pe = new PolynomialEquation(p, 1e-8);
			pe.Solve();

			Assert.AreEqual(3, pe.AllRoots.Count);
			Assert.IsTrue(new Interval(1, 2).IsInInterval(pe.Roots.Count), pe.Roots.Count.ToString());

			foreach (var i in pe.AllRoots)
			{
				Assert.AreEqual(0, p.Calculate(i), 1e-6);
			}
		}

		[Test]
		public void Cubic_Int_Dlt0_Random()
		{
			var r = new MyRandom();
			double a, b, c, d;
			while (true)
			{
				a = r.Next(1, 100) * r.NextSign();
				b = r.Next(0, 100) * r.NextSign();
				c = r.Next(0, 100) * r.NextSign();
				d = r.Next(0, 100) * r.NextSign();
				if (CubicDiscriminant(a, b, c, d) < 0) break;
			}

			var p = new Polynomial
			{
				new Monomial(a, 3),
				new Monomial(b, 2),
				new Monomial(c, 1),
				new Monomial(d, 0),
			};
			var pe = new PolynomialEquation(p, 1e-8);
			pe.Solve();

			Assert.AreEqual(1, pe.AllRoots.Count);
			Assert.AreEqual(1, pe.Roots.Count);

			Assert.AreEqual(0, p.Calculate(pe.Roots[0]), 1e-6);
		}

		[Test]
		public void Cubic_Vieta_Dlt0_Random()
		{
			var r = new MyRandom();
			double a, b, c, d;
			while (true)
			{
				a = r.Next(1, 100) * r.NextDouble() * r.NextSign();
				b = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				c = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				d = r.Next(0, 100) * r.NextDouble() * r.NextSign();
				if (CubicDiscriminant(a, b, c, d) < 0 && a != 0) break;
			}

			var p = new Polynomial
			{
				new Monomial(a, 3),
				new Monomial(b, 2),
				new Monomial(c, 1),
				new Monomial(d, 0),
			};
			var pe = new PolynomialEquation(p, 1e-8);
			pe.Solve();

			Assert.AreEqual(1, pe.AllRoots.Count);
			Assert.AreEqual(1, pe.Roots.Count);

			Assert.AreEqual(0, p.Calculate(pe.Roots[0]), 1e-6);
		}

		//[Test]
		//public void Cubic_Deq0_Random()
		//{
		//	var r = new MyRandom();
		//	double a, b, c, d;
		//	while (true)
		//	{
		//		//a = 2 * r.NextDouble() * r.NextSign();
		//		a = 5 * r.NextDouble() * r.NextSign();
		//		b = 5 * r.NextDouble() * r.NextSign();
		//		c = 5 * r.NextDouble() * r.NextSign();
		//		d = 5 * r.NextDouble() * r.NextSign();
		//		if (Math.Abs(CubicDiscriminant(a, b, c, d)) < 1e-7 && Math.Abs(a) >= 1e-2) break;
		//	}

		//	var p = new Polynomial
		//	{
		//		new Monomial(a, 3),
		//		new Monomial(b, 2),
		//		new Monomial(c, 1),
		//		new Monomial(d, 0),
		//	};
		//	var pe = new PolynomialEquation(p, 1e-9);
		//	pe.Solve();

		//	Assert.AreEqual(3, pe.AllRoots.Count);
		//	Assert.IsTrue(new Interval(1, 2).IsInInterval(pe.Roots.Count), pe.Roots.Count.ToString());

		//	foreach (var i in pe.AllRoots)
		//	{
		//		Assert.AreEqual(0, p.Calculate(i), 1e-5);
		//	}
		//}

		[Test]
		public void Cubic_Vieta_RootsToCoef_Random()
		{
			var r = new MyRandom();
			var x = new double[3];
			while (true)
			{
				x[0] = r.Next(0, 50) * r.NextDouble() * r.NextSign();
				x[1] = r.Next(0, 50) * r.NextDouble() * r.NextSign();
				x[2] = r.Next(0, 50) * r.NextDouble() * r.NextSign();
				if (!(x[0] == x[1] || x[1] == x[2] || x[2] == x[0])) break;
			}
			Array.Sort(x);
			double a, b, c, d;

			a = r.Next(1, 3) * r.NextSign();
			b = CubicVietaB(a, x);
			c = CubicVietaC(a, x);
			d = CubicVietaD(a, x);
			Assert.Greater(CubicDiscriminant(a, b, c, d), 0);
			var pe1 = new PolynomialEquation(new Polynomial(a, b, c, d), 1e-6);
			pe1.Solve();
			Assert.AreEqual(3, pe1.AllRoots.Count);
			Assert.AreEqual(3, pe1.Roots.Count);
			Assert.AreEqual(x[0], pe1.AllRoots[0], 1e-5);
			Assert.AreEqual(x[1], pe1.AllRoots[1], 1e-5);
			Assert.AreEqual(x[2], pe1.AllRoots[2], 1e-5);
		}
	}
}
