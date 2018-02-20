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
	public class IntervalTests
	{
		[Test]
		public void Ctor1()
		{
			var ra = new MyRandom();
			var a = ra.NextDouble() * ra.Next() * ra.NextSign();
			var b = ra.NextDouble() * ra.Next() * ra.NextSign();
			var l = Math.Min(a, b);
			var r = Math.Max(a, b);

			var iw = new Interval(l, r);
			Assert.AreEqual(l, iw.Left);
			Assert.AreEqual(r, iw.Right);
		}

		[Test]
		public void Ctor2()
		{
			var ra = new MyRandom();
			var a = ra.NextDouble() * ra.Next() * ra.NextSign();
			var b = ra.NextDouble() * ra.Next() * ra.NextSign();
			var l = Math.Min(a, b);
			var r = Math.Min(a, b);

			var iw = new Interval(l, r);
			Assert.AreEqual(l, iw.Left);
			Assert.AreEqual(r, iw.Right);
		}
	}
}
