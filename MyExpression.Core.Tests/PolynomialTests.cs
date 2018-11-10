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
	public class PolynomialTests
	{
		[Test]
		public void ParseTest1()
		{
			var s = "x^3+4*x^2+5*x+2";
			var p = Polynomial.Parse(s);

			Assert.AreEqual(new Monomial(1, 3), p[3]);
			Assert.AreEqual(new Monomial(4, 2), p[2]);
			Assert.AreEqual(new Monomial(5, 1), p[1]);
			Assert.AreEqual(new Monomial(2, 0), p[0]);

			var pe = new Polynomial()
			{
				new Monomial(1, 3),
				new Monomial(4, 2),
				new Monomial(5, 1),
				new Monomial(2, 0)
			};
			Assert.AreEqual(pe, p);
		}

		[Test]
		public void Ctor()
		{
			var a = new double[] { 1.234, -1.234, 2.345, -2.345 };
			var b = new Polynomial(a);
			Assert.AreEqual(a[0], b[3].Coefficient);
			Assert.AreEqual(a[1], b[2].Coefficient);
			Assert.AreEqual(a[2], b[1].Coefficient);
			Assert.AreEqual(a[3], b[0].Coefficient);

			var d = new Polynomial(b);
			Assert.AreEqual(a[0], d[3].Coefficient);
			Assert.AreEqual(a[1], d[2].Coefficient);
			Assert.AreEqual(a[2], d[1].Coefficient);
			Assert.AreEqual(a[3], d[0].Coefficient);
			Assert.AreEqual(b[3].Coefficient, d[3].Coefficient);
			Assert.AreEqual(b[2].Coefficient, d[2].Coefficient);
			Assert.AreEqual(b[1].Coefficient, d[1].Coefficient);
			Assert.AreEqual(b[0].Coefficient, d[0].Coefficient);
		}

		[Test]
		public void SumTest()
		{
			var a = new Polynomial(1, 2, 3, 4);
			var b = new Polynomial(1, 2, -3, -4);
			var c = a + b;
			Assert.AreEqual(2, c[3].Coefficient);
			Assert.AreEqual(4, c[2].Coefficient);
			Assert.AreEqual(0, c[1].Coefficient);
			Assert.AreEqual(0, c[0].Coefficient);

			a = new Polynomial(1, 2, 3, 4);
			b = new Polynomial(-1, 2, -3, -4);
			c = a + b;
			Assert.AreEqual(2, c.Degree);
			Assert.AreEqual(0, c[3].Coefficient);
			Assert.AreEqual(4, c[2].Coefficient);
			Assert.AreEqual(0, c[1].Coefficient);
			Assert.AreEqual(0, c[0].Coefficient);
		}

		[Test]
		public void SubTest()
		{
			var a = new Polynomial(1, 2, 3, 4);
			var b = new Polynomial(1, 2, -3, -4);
			var c = a - b;
			Assert.AreEqual(1, c.Degree);
			Assert.AreEqual(0, c[3].Coefficient);
			Assert.AreEqual(0, c[2].Coefficient);
			Assert.AreEqual(6, c[1].Coefficient);
			Assert.AreEqual(8, c[0].Coefficient);

			a = new Polynomial(1, 2, 3, 4);
			b = new Polynomial(-1, 2, -3, -4);
			c = a - b;
			Assert.AreEqual(3, c.Degree);
			Assert.AreEqual(2, c[3].Coefficient);
			Assert.AreEqual(0, c[2].Coefficient);
			Assert.AreEqual(6, c[1].Coefficient);
			Assert.AreEqual(8, c[0].Coefficient);
		}

		[Test]
		public void FromRootsEquationTest()
		{
			var r = new MyRandom();
			for (var j = 0; j < 100; j++)
			{
				var n = r.Next(3, 8);
				var roots = new double[n];
				for (var i = 0; i < n; i++)
				{
					roots[i] = r.Next(-20, 20);
				}

				Array.Sort(roots);
				var p = Polynomial.FromRoots(roots);
				var eq = new PolynomialEquation(p, 1e-12);
				eq.Solve();
				for (var i = 0; i < n; i++)
				{
					if (Math.Abs(roots[i] - eq.AllRoots[i]) > 1e-3)
					{
						throw new ApplicationException($"{String.Join(", ", roots)}\n{String.Join(", ", eq.AllRoots)}");
					}

					Assert.AreEqual(roots[i], eq.AllRoots[i], 1e-3);
				}
			}
		}
	}
}
