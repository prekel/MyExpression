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
	public class OpenCloseIntervalTests
	{
		[Test]
		public void Ctor1()
		{
			var ra = new MyRandom();
			var a = ra.NextDouble() * ra.Next() * ra.NextSign();
			var b = ra.NextDouble() * ra.Next() * ra.NextSign();
			var l = Math.Min(a, b);
			var r = Math.Min(a, b);

			var iw = new OpenCloseInterval(l, r);
			Assert.AreEqual(l, iw.Left);
			Assert.AreEqual(r, iw.Right);
			Assert.IsFalse(iw.IsLeftOpen);
			Assert.IsFalse(iw.IsRightOpen);
		}

		[Test]
		public void Ctor2()
		{
			var ra = new MyRandom();
			var a = ra.NextDouble() * ra.Next() * ra.NextSign();
			var b = ra.NextDouble() * ra.Next() * ra.NextSign();
			var l = Math.Min(a, b);
			var r = Math.Min(a, b);
			var lo = ra.NextBool();
			var ro = ra.NextBool();

			var iw = new OpenCloseInterval(l, lo, r, ro);
			Assert.AreEqual(l, iw.Left);
			Assert.AreEqual(r, iw.Right);
			Assert.AreEqual(lo, iw.IsLeftOpen);
			Assert.AreEqual(ro, iw.IsRightOpen);
		}
	}
}
