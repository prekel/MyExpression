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
		public void Test1()
		{
			var se1 = new SquareEquation(2, 5, -3);
			Assert.AreEqual(-3, se1.XMin);
			Assert.AreEqual(0.5, se1.XMax);
		}
	}
}
