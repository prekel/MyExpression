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
			var a = r.NextDouble() * r.Next() * r.NextSign();
		}
	}
}
