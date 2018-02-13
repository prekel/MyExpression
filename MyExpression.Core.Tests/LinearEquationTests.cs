// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using MyExpression.Core;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class LinearEquationTests
	{
		[Test]
		public void X_IntRandom()
		{
			var r = new MyRandom();
			var a = r.Next(1, 100000) * r.NextSign();
			var b = r.Next(0, 100000) * r.NextSign();

			var le = new LinearEquation(a, b);

			var x = -b / (double)a;

			Assert.AreEqual(x, le.X);
		}

		[Test]
		public void ParseTest_Random()
		{
			var r = new MyRandom();
			var a = r.Next(1, 100000) * r.NextSign();
			var b = r.Next(0, 100000) * r.NextSign();

			var s = $"{a}x{(b >= 0 ? "+" : "")}{b}";

			var le = LinearEquation.Parse(s);

			Assert.AreEqual(a, le.A);
			Assert.AreEqual(b, le.B);
		}
	}
}