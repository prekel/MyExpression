using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MyExpression.Core.Tests
{
	[TestFixture]
	public class RecursiveBinarySearchTests
	{
		[Test]
		public void Xpow3()
		{
			var bs01 = new RecursiveBinarySearch(x => x * x * x, new Interval(-1, 1), 1e-7);
			var bs02 = new RecursiveBinarySearch(x => x * x * x, new Interval(-1, 1), 1, 1e-7);
			var bs03 = new RecursiveBinarySearch(x => x * x * x, new Interval(-1, 1), -1, 1e-7);
			var bs04 = new RecursiveBinarySearch(x => -x * x * x, new Interval(-1, 1), 1e-7);
			var bs05 = new RecursiveBinarySearch(x => -x * x * x, new Interval(-1, 1), 1, 1e-7);
			var bs06 = new RecursiveBinarySearch(x => -x * x * x, new Interval(-1, 1), -1, 1e-7);
			var bs07 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, Double.PositiveInfinity), -1, 1e-7);
			var bs08 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, Double.PositiveInfinity), 0, 1e-7);
			var bs09 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, Double.PositiveInfinity), 1e-7);
			var bs10 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, Double.PositiveInfinity), 1, 1e-7);
			var bs11 = new RecursiveBinarySearch(x => x * x * x, new Interval(0, Double.PositiveInfinity), 1, 1e-7);
			var bs12 = new RecursiveBinarySearch(x => x * x * x, new Interval(0, Double.PositiveInfinity), 0, 1e-7);
			var bs13 = new RecursiveBinarySearch(x => x * x * x, new Interval(0, Double.PositiveInfinity), 1e-7);
			var bs14 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, 0), -1, 1e-7);
			var bs15 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, 0), 0, 1e-7);
			var bs16 = new RecursiveBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, 0), 1e-7);
			Assert.AreEqual(0, bs01.Solve(), 1e-6);
			Assert.AreEqual(1, bs02.Solve(), 1e-6);
			Assert.AreEqual(-1, bs03.Solve(), 1e-6);
			Assert.AreEqual(0, bs04.Solve(), 1e-6);
			Assert.AreEqual(-1, bs05.Solve(), 1e-6);
			Assert.AreEqual(1, bs06.Solve(), 1e-6);
			Assert.AreEqual(-1, bs07.Solve(), 1e-6);
			Assert.AreEqual(0, bs08.Solve(), 1e-6);
			Assert.AreEqual(0, bs09.Solve(), 1e-6);
			Assert.AreEqual(1, bs10.Solve(), 1e-6);
			Assert.AreEqual(1, bs11.Solve(), 1e-6);
			Assert.AreEqual(0, bs12.Solve(), 1e-6);
			Assert.AreEqual(0, bs13.Solve(), 1e-6);
			Assert.AreEqual(-1, bs14.Solve(), 1e-6);
			Assert.AreEqual(0, bs15.Solve(), 1e-6);
			Assert.AreEqual(0, bs16.Solve(), 1e-6);
		}
	}
}
