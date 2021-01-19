﻿// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

using MyExpression.FSharp.CSharpWrapper;

//using MyExpression.Core;
using NUnit.Framework;

namespace MyExpression.Core.Tests
{
    [TestFixture]
    public class LinearEquationTests
    {
        private static LinearEquation ctor1(double a, double b) => new(a, b);

        private static FSharpLinearEquation ctor2(double a, double b) => new(a, b);

        private static Func<double, double, ILinearEquation>[] ctors = new Func<double, double, ILinearEquation>[]
            {ctor1, ctor2};

        [Test]
        public void X_IntRandom([ValueSource(nameof(ctors))] Func<double, double, ILinearEquation> ctor)
        {
            var r = new MyRandom();
            var a = r.Next(1, 100000) * r.NextSign();
            var b = r.Next(0, 100000) * r.NextSign();

            var le = new FSharpLinearEquation(a, b);

            var x = -b / (double) a;

            Assert.AreEqual(x, le.X);
        }

        [Test]
        public void ParseTest_Random()
        {
            var r = new MyRandom();
            var a = r.Next(1, 100000) * r.NextSign();
            var b = r.Next(0, 100000) * r.NextSign();

            var s = $"{a}x{(b >= 0 ? "+" : "")}{b}";

            var le = LinearEquation.Parse(s);

            Assert.AreEqual(a, le.A);
            Assert.AreEqual(b, le.B);
        }

        [Test]
        public void ToStringTest()
        {
            var l = new LinearEquation(1, 1);
            Assert.AreEqual("x+1", l.ToString());
            l = new LinearEquation(0, 1);
            Assert.AreEqual("1", l.ToString());
            l = new LinearEquation(1, 0);
            Assert.AreEqual("x", l.ToString());
            l = new LinearEquation(-1, 0);
            Assert.AreEqual("-x", l.ToString());
            l = new LinearEquation(-1, -1);
            Assert.AreEqual("-x-1", l.ToString());
            l = new LinearEquation(2, 2);
            Assert.AreEqual("2x+2", l.ToString());
            l = new LinearEquation(0, 2);
            Assert.AreEqual("2", l.ToString());
            l = new LinearEquation(2, 0);
            Assert.AreEqual("2x", l.ToString());
            l = new LinearEquation(-2, 0);
            Assert.AreEqual("-2x", l.ToString());
            l = new LinearEquation(-2, -2);
            Assert.AreEqual("-2x-2", l.ToString());
        }
    }
}
