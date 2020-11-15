// Copyright (c) 2019 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class CodeAnalysisEvalTests
	{
		[Test]
		public void RandomSinCos()
		{
			Func<double, double> evalcos = new CodeAnalysisEval("cos(x)").Calculate;
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
			Func<double, double> f1 = new CodeAnalysisEval("sin(x)*1/x*4383+2143/1414+141-1.2*23*x*Math.Abs(x*Math.Sin(x))").Calculate;
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
			var s = "sin(x)";
			var c = new CodeAnalysisEval(s);
			Func<double, double> f = c.Calculate;
			Assert.AreEqual(0, f(0), 1e-7);
			Assert.AreEqual(1, f(Math.PI / 2), 1e-7);
			Assert.AreEqual(0, f(Math.PI), 1e-7);
			Assert.AreEqual(Math.Sqrt(2) / 2, f(Math.PI / 4), 1e-7);
		}

		[Test]
		public void AtanAsinAcosTest()
		{
			Func<double, double> evalf = new CodeAnalysisEval("atan(x) + asin(x) + acos(x)").Calculate;
			double f(double x) => Math.Atan(x) + Math.Asin(x) + Math.Acos(x);
			var r = new MyRandom();
			for (var i = 0; i < 10; i++)
			{
				var x = r.Next(100) * r.NextDouble();
				Assert.AreEqual(f(x), evalf(x));
			}
		}
	}
}
