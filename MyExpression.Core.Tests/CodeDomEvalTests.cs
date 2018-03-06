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
			Func<double, double> evalcos = new CodeDomEval("cos(x)").Calculate;
			double cos(double x) => Math.Cos(x);
			var r = new MyRandom();
			for (var i = 0; i < 10; i++)
			{
				var x = r.Next(100) * r.NextDouble();
				Assert.AreEqual(cos(x), evalcos(x));
			}
		}

		[Test]
		public void Random()
		{
			Func<double, double> f1 = new CodeDomEval("sin(x)*1/x*4383+2143/1414+141-1.2*23*x*Math.Abs(x*Math.Sin(x))").Calculate;
			double f(double x) => Math.Sin(x) * 1 / x * 4383 + 2143 / 1414 + 141 - 1.2 * 23 * x * Math.Abs(x * Math.Sin(x));
			var r = new MyRandom();
			for (var i = 0; i < 100; i++)
			{
				var x = r.Next(100) * r.NextDouble();
				Assert.AreEqual(f(x), f1(x), 1e-8);
			}
		}

		[Test]
		public void ReadmeTest()
		{
			var s = "Math.Sin(x)";
			var c = new CodeDomEval(s);
			Func<double, double> f = c.Calculate;
			Assert.AreEqual(0, f(0), 1e-7);
			Assert.AreEqual(1, f(Math.PI / 2), 1e-7);
			Assert.AreEqual(0, f(Math.PI), 1e-7);
			Assert.AreEqual(Math.Sqrt(2) / 2, f(Math.PI / 4), 1e-7);
		}
	}
}
