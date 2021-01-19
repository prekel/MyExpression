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
    public abstract class AbstractMonomialTests
    {
        [TestFixture]
        public class MonomialTests : AbstractMonomialTests
        {
            protected override IMonomial CreateMonomial(double coef, double degree) => new Monomial(coef, degree);
        }

        protected abstract IMonomial CreateMonomial(double coef, double degree);

        [Test]
        public void SumTest_Random()
        {
            var r = new MyRandom();
            var a1 = r.NextDouble() * r.Next() * r.NextSign();
            var b = r.Next(1, 100);
            var a2 = r.NextDouble() * r.Next() * r.NextSign();

            var m1 = new Monomial(a1, b);
            var m2 = new Monomial(a2, b);

            var me = new Monomial(a1 + a2, b);

            var ms = m1 + m2;

            Assert.AreEqual(me, ms);
        }

        [Test]
        public void SumSubMultTest_Random()
        {
            var r = new MyRandom();
            var a1 = r.NextDouble() * r.Next() * r.NextSign();
            var b = r.Next(1, 100);
            var a2 = r.NextDouble() * r.Next() * r.NextSign();

            var m1 = new Monomial(a1, b);
            var m2 = new Monomial(a2, b);

            var me1 = new Monomial(a1 + a2, b);
            var ms1 = m1 + m2;

            var me2 = new Monomial(a1 - a2, b);
            var ms2 = m1 - m2;

            var me3 = new Monomial(a1 * a2, b + b);
            var ms3 = m1 * m2;

            Assert.AreEqual(me1, ms1);
            Assert.AreEqual(me2, ms2);
            Assert.AreEqual(me3, ms3);
        }

        [Test]
        public void CtorTest()
        {
            var r = new MyRandom();
            var a1 = r.NextDouble() * r.Next() * r.NextSign();
            var b = r.Next(1, 100);
            var a2 = r.NextDouble() * r.Next() * r.NextSign();

            var m1 = CreateMonomial(a1, b);
            var m2 = CreateMonomial(a2, b);

            Assert.That(m1.Coefficient, Is.EqualTo(a1));
            Assert.That(m1.Degree, Is.EqualTo(b));
            Assert.That(m2.Coefficient, Is.EqualTo(a2));
            Assert.That(m2.Degree, Is.EqualTo(b));

            Assert.That(m1.Calculate(0), Is.EqualTo(0));
            Assert.That(m1.Calculate(-1), Is.EqualTo(a1 * Math.Pow(-1, b)));
            Assert.That(m1.Calculate(1), Is.EqualTo(a1 * Math.Pow(1, b)));

            Assert.That(m2.Calculate(0), Is.EqualTo(0));
            Assert.That(m2.Calculate(-1), Is.EqualTo(a2 * Math.Pow(-1, b)));
            Assert.That(m2.Calculate(1), Is.EqualTo(a2 * Math.Pow(1, b)));

            var d1 = (IMonomial)m1.Derivative;
            var d2 = (IMonomial) m2.Derivative;
            
            Assert.That(d1.Coefficient, Is.EqualTo(a1 * b));
            Assert.That(d1.Degree, Is.EqualTo(b - 1));
            Assert.That(d2.Coefficient, Is.EqualTo(a2 * b));
            Assert.That(d2.Degree, Is.EqualTo(b - 1));
        }
    }
}
