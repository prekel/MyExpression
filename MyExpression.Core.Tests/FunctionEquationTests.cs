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
	public class FunctionEquationTests
	{
		[Test]
		public void CustomTest()
		{
			var e1 = new FunctionEquation((x) => Math.Sin(x) * Math.Sin(x) * Math.Cos(x) / Math.Tan(x), new Interval(-5, 5), 1e-3, 1e-5);
			e1.Solve();
		}
	}
}
