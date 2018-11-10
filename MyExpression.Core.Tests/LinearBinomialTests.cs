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
		public void MultTest1()
		{
			var p = Polynomial.Parse("x-3");
			var b = new LinearBinomial(-4);
			var m1 = p * b;
			var m2 = b * p;
			var expected = Polynomial.Parse("x^2+x-12");
			Assert.AreEqual(expected, m1);
			Assert.AreEqual(expected, m2);
		}
	}
}