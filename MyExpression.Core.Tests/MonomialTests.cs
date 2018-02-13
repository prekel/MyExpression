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
	public class MonomialTests
	{
		[Test]
		public void SumTest_Random()
		{
			var r = new MyRandom();
			var a1 = r.NextDouble() * r.Next() * r.NextSign();
			var b = r.Next(1, 100);
			var a2 = r.NextDouble() * r.Next() * r.NextSign();

			var m1 = new Monomial(a1, b);
			var m2 = new Monomial(a2, b);

			var me = new Monomial(a1 + a2, b);

			var ms = m1 + m2;

			Assert.AreEqual(me, ms);
		}

		[Test]
		public void SumSubMultTest_Random()
		{
			var r = new MyRandom();
			var a1 = r.NextDouble() * r.Next() * r.NextSign();
			var b = r.Next(1, 100);
			var a2 = r.NextDouble() * r.Next() * r.NextSign();

			var m1 = new Monomial(a1, b);
			var m2 = new Monomial(a2, b);

			var me1 = new Monomial(a1 + a2, b);
			var ms1 = m1 + m2;

			var me2 = new Monomial(a1 - a2, b);
			var ms2 = m1 - m2;

			var me3 = new Monomial(a1 * a2, b + b);
			var ms3 = m1 * m2;

			Assert.AreEqual(me1, ms1);
			Assert.AreEqual(me2, ms2);
			Assert.AreEqual(me3, ms3);
		}

	}
}
