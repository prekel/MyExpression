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
			var r = new Random();
			var a = r.Next(1, 100000) * (r.Next() % 2 == 0 ? -1 : 1);
			var b = r.Next(0, 100000) * (r.Next() % 2 == 0 ? -1 : 1);

			var le = new LinearEquation(a, b);

			var x = -b / (double)a;

			Assert.AreEqual(x, le.X);
		}
	}
}