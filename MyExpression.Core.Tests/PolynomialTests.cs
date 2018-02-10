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
	}
}
