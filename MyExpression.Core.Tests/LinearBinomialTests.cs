// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using MyExpression.Core;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class LinearBinomialTests
	{
		[Test]
		public void MultPolyBin()
		{
			var p = Polynomial.Parse("x-3");
			var b = new LinearBinomial(-4);
			var m1 = p * b;
			var m2 = b * p;
			var expected = Polynomial.Parse("x^2+x-12");
			Assert.AreEqual(expected, m1);
			Assert.AreEqual(expected, m2);
		}

		[Test]
		public void MultBinBin()
		{
			var a = new LinearBinomial(3);
			var b = new LinearBinomial(-4);
			var m1 = a * b;
			var m2 = b * a;
			var expected = Polynomial.Parse("x^2+x-12");
			Assert.AreEqual(expected, m1);
			Assert.AreEqual(expected, m2);
		}
		
		private Polynomial _polynomial;

		[SetUp]
		public void Setup()
		{
			var a1 = new LinearBinomial(1);
			var a2 = new LinearBinomial(2);
			var a3 = new LinearBinomial(3);
			var a4 = new LinearBinomial(4);
			_polynomial = a1 * a2 * a3 * a4;
		}
		
		[Test]
		public void Mult_4()
		{
			var exp = Polynomial.Parse("x^4-10x^3+35x^2-50x+24");
			Assert.AreEqual(exp, _polynomial);
		}
		
		[Test]
		public void Equation()
		{
			var eq = new PolynomialEquation(_polynomial, 1e-10);
			eq.Solve();
			Assert.AreEqual(1, eq.Roots[0], 1e-9);
			Assert.AreEqual(2, eq.Roots[1], 1e-9);
			Assert.AreEqual(3, eq.Roots[2], 1e-9);
			Assert.AreEqual(4, eq.Roots[3], 1e-9);
		}
	}
}