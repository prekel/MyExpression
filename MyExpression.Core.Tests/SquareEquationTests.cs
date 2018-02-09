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
		public void ToPolynomialRandom()
		{
			var r = new Random();
			var a = r.Next(1, 10000) * (r.Next() % 2 == 0 ? -1 : 1);
			var b = r.Next(0, 10000) * (r.Next() % 2 == 0 ? -1 : 1);
			var c = r.Next(0, 10000) * (r.Next() % 2 == 0 ? -1 : 1);

			var se = new SquareEquation(a, b, c);

			var p = se.ToPolynomial();

			Assert.AreEqual(se.A, p[2].Coefficient);
			Assert.AreEqual(se.B, p[1].Coefficient);
			Assert.AreEqual(se.C, p[0].Coefficient);
		}
	}
}
