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
    public abstract class AbstractIntervalTests
    {
        [TestFixture]
        public class IntervalTests : AbstractIntervalTests
        {
            protected override IInterval CreateInterval(double left, double right) => new Interval(left, right);
        }
        

        protected abstract IInterval CreateInterval(double left, double right);

        [Test]
        public void Ctor1()
        {
            var ra = new MyRandom();
            var a = ra.NextDouble() * ra.Next() * ra.NextSign();
            var b = ra.NextDouble() * ra.Next() * ra.NextSign();
            var l = Math.Min(a, b);
            var r = Math.Max(a, b);

            var iw = CreateInterval(l, r);
            Assert.AreEqual(l, iw.Left);
            Assert.AreEqual(r, iw.Right);
        }

        [Test]
        public void Ctor2()
        {
            var ra = new MyRandom();
            var a = ra.NextDouble() * ra.Next() * ra.NextSign();
            var b = ra.NextDouble() * ra.Next() * ra.NextSign();
            var l = Math.Min(a, b);
            var r = Math.Min(a, b);

            var iw = CreateInterval(l, r);
            Assert.AreEqual(l, iw.Left);
            Assert.AreEqual(r, iw.Right);
        }

        [Test]
        public void IsInTest()
        {
            var i = CreateInterval(-4, 2);
            Assert.That(i.IsInInterval(-5), Is.False);
            Assert.That(i.IsInInterval(-4), Is.True);
            Assert.That(i.IsInInterval(-3), Is.True);
            Assert.That(i.IsInInterval(-0), Is.True);
            Assert.That(i.IsInInterval(0), Is.True);
            Assert.That(i.IsInInterval(1), Is.True);
            Assert.That(i.IsInInterval(2), Is.True);
            Assert.That(i.IsInInterval(5), Is.False);
        }
    }
}
