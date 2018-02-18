// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyExpression.Core;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class SquareEquationTests
	{
		[Test]
		public void XMinXmax_Int1()
		{
			var se1 = new SquareEquation(2, 5, -3);
			Assert.AreEqual(-3, se1.XMin);
			Assert.AreEqual(0.5, se1.XMax);
		}

		[Test]
		public void XMinXmax_Int2()
		{
			var se1 = new SquareEquation(4, 21, 5);
			Assert.AreEqual(-5, se1.XMin);
			Assert.AreEqual(-0.25, se1.XMax);
		}

		[Test]
		public void XMinXmax_Int3()
		{
			var se1 = new SquareEquation(3, -10, 3);
			Assert.AreEqual(1 / 3.0, se1.XMin, 0.0001);
			Assert.AreEqual(3, se1.XMax);
		}

		[Test]
		public void Roots_Int1()
		{
			var se1 = new SquareEquation(2, 5, -3);
			Assert.AreEqual(-3, se1.Roots[0]);
			Assert.AreEqual(0.5, se1.Roots[1]);
		}

		[Test]
		public void Roots_Int4()
		{
			var se1 = new SquareEquation(-2, 5, -3);
			Assert.AreEqual(1, se1.Roots[0]);
			Assert.AreEqual(1.5, se1.Roots[1]);
		}

		[Test]
		public void XMinXMax_Int4()
		{
			var se1 = new SquareEquation(-2, 5, -3);
			Assert.AreEqual(1, se1.XMin);
			Assert.AreEqual(1.5, se1.XMax);
		}

		[Test]
		public void X1X2_Int4()
		{
			var se1 = new SquareEquation(-2, 5, -3);
			Assert.AreEqual(1, se1.X2);
			Assert.AreEqual(1.5, se1.X1);
		}

		[Test]
		public void ToPolynomialIntRandom()
		{
			var r = new MyRandom();
			var a = r.Next(1, 10000) * r.NextSign();
			var b = r.Next(0, 10000) * r.NextSign();
			var c = r.Next(0, 10000) * r.NextSign();

			var se = new SquareEquation(a, b, c);

			var p = se.ToPolynomial();

			Assert.AreEqual(se.A, p[2].Coefficient);
			Assert.AreEqual(se.B, p[1].Coefficient);
			Assert.AreEqual(se.C, p[0].Coefficient);

			Assert.AreEqual(a, p[2].Coefficient);
			Assert.AreEqual(b, p[1].Coefficient);
			Assert.AreEqual(c, p[0].Coefficient);
		}

		[Test]
		public void AllRoots_Manual()
		{
			var a = new SquareEquation(1, 4, 4);
			Assert.AreEqual(-2, a.X0);
			Assert.AreEqual(-2, a.X.Item1);
			Assert.AreEqual(-2, a.X.Item1);
			Assert.AreEqual(-2, a.X1);
			Assert.AreEqual(-2, a.X2);
			Assert.AreEqual(1, a.Roots.Count);
			Assert.AreEqual(2, a.AllRoots.Count);
			Assert.AreEqual(-2, a.Roots[0]);
			Assert.AreEqual(-2, a.AllRoots[0]);
			Assert.AreEqual(-2, a.AllRoots[1]);
		}

		private double Discriminant(double a, double b, double c) => b * b - 4 * a * c;

		[Test]
		public void Wieth_Random()
		{
			var r = new MyRandom();
			double a, b, c;
			while (true)
			{
				a = r.Next(1, 1000) * r.NextDouble() * r.NextSign();
				b = r.Next(0, 1000) * r.NextDouble() * r.NextSign();
				c = r.Next(0, 1000) * r.NextDouble() * r.NextSign();
				if (Discriminant(a, b, c) > 0 && a != 0) break;
			}

			var se = new SquareEquation(a, b, c);

			Assert.AreEqual(-b / a, se.X1 + se.X2, 1e-8);
			Assert.AreEqual(c / a, se.X1 * se.X2, 1e-8);
		}
	}
}
