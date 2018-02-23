using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class CodeDomEvalTests
	{
		[Test]
		public void RandomSinCos()
		{
			Func<double, double> evalsin = new CodeDomEval("Math.Sin(x)").Calculate;
			double sin(double x) => Math.Sin(x);
			Func<double, double> evalcos = new CodeDomEval("Math.Cos(x)").Calculate;
			double cos(double x) => Math.Cos(x);
			var r = new MyRandom();
			for (var i = 0; i < 100; i++)
			{
				var x = r.Next(100) * r.NextDouble();
				Assert.AreEqual(sin(x), evalsin(x));
				Assert.AreEqual(cos(x), evalcos(x));
			}
		}

		[Test]
		public void Random()
		{
			Func<double, double> f1 = new CodeDomEval("Math.Sin(x)*1/x*4383+2143/1414+141-1.2*23*x*Math.Abs(x*Math.Sin(x))").Calculate;
			double f(double x) => Math.Sin(x) * 1 / x * 4383 + 2143 / 1414 + 141 - 1.2 * 23 * x * Math.Abs(x * Math.Sin(x));
			var r = new MyRandom();
			for (var i = 0; i < 1000; i++)
			{
				var x = r.Next(100) * r.NextDouble();
				Assert.AreEqual(f(x), f1(x), 1e-8);
			}
		}
	}
}
