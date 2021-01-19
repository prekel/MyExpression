// Copyright (c) 2021 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
    [TestFixture]
    public abstract class AbstractBinarySearchTests
    {
        [TestFixture]
        public class RecursiveBinarySearchTests : AbstractBinarySearchTests
        {
            protected override IBinarySearch CreateBinarySearch(Func<double, double> f, Interval lr, double eps) =>
                new RecursiveBinarySearch(f, lr, eps);

            protected override IBinarySearch CreateBinarySearch(Func<double, double> f, Interval lr, double eq,
                double eps) => new RecursiveBinarySearch(f, lr, eq, eps);
        }

        protected abstract IBinarySearch CreateBinarySearch(Func<double, double> f, Interval lr, double eps);
        protected abstract IBinarySearch CreateBinarySearch(Func<double, double> f, Interval lr, double eq, double eps);


        [Test]
        public void Xpow3()
        {
            var bs01 = CreateBinarySearch(x => x * x * x, new Interval(-1, 1), 1e-7);
            var bs02 = CreateBinarySearch(x => x * x * x, new Interval(-1, 1), 1, 1e-7);
            var bs03 = CreateBinarySearch(x => x * x * x, new Interval(-1, 1), -1, 1e-7);
            var bs04 = CreateBinarySearch(x => -x * x * x, new Interval(-1, 1), 1e-7);
            var bs05 = CreateBinarySearch(x => -x * x * x, new Interval(-1, 1), 1, 1e-7);
            var bs06 = CreateBinarySearch(x => -x * x * x, new Interval(-1, 1), -1, 1e-7);
            var bs07 = CreateBinarySearch(x => x * x * x,
                new Interval(Double.NegativeInfinity, Double.PositiveInfinity), -1, 1e-7);
            var bs08 = CreateBinarySearch(x => x * x * x,
                new Interval(Double.NegativeInfinity, Double.PositiveInfinity), 0, 1e-7);
            var bs09 = CreateBinarySearch(x => x * x * x,
                new Interval(Double.NegativeInfinity, Double.PositiveInfinity), 1e-7);
            var bs10 = CreateBinarySearch(x => x * x * x,
                new Interval(Double.NegativeInfinity, Double.PositiveInfinity), 1, 1e-7);
            var bs11 = CreateBinarySearch(x => x * x * x, new Interval(0, Double.PositiveInfinity), 1, 1e-7);
            var bs12 = CreateBinarySearch(x => x * x * x, new Interval(0, Double.PositiveInfinity), 0, 1e-7);
            var bs13 = CreateBinarySearch(x => x * x * x, new Interval(0, Double.PositiveInfinity), 1e-7);
            var bs14 = CreateBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, 0), -1, 1e-7);
            var bs15 = CreateBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, 0), 0, 1e-7);
            var bs16 = CreateBinarySearch(x => x * x * x, new Interval(Double.NegativeInfinity, 0), 1e-7);
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
