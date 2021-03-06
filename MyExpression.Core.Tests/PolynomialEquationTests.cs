﻿// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace MyExpression.Core.Tests
{
    [TestFixture]
    public abstract class AbstractPolynomialEquationTests
    {
        [TestFixture]
        public class PolynomialEquationTests : AbstractPolynomialEquationTests
        {
            protected override IPolynomialEquation CreatePolynomialEquation(IPolynomial poly, double eps) =>
                new PolynomialEquation(poly, eps);

            protected override IPolynomial CreatePolynomial(IEnumerable<IMonomial> a) => new Polynomial(a);
            protected override IPolynomial CreatePolynomial(params double[] v) => new Polynomial(v);
        }


        protected abstract IPolynomialEquation CreatePolynomialEquation(IPolynomial poly, double eps);
        protected abstract IPolynomial CreatePolynomial(IEnumerable<IMonomial> a);
        protected abstract IPolynomial CreatePolynomial(params double[] v);

        public static double CubicDiscriminant(double a, double b, double c, double d)
        {
            var c1 = -4 * b * b * b * d;
            var c2 = b * b * c * c;
            var c3 = -4 * a * c * c * c;
            var c4 = 18 * a * b * c * d;
            var c5 = -27 * a * a * d * d;
            return c1 + c2 + c3 + c4 + c5;
        }

        public static double CubicVietaB(double a, params double[] x)
        {
            return -(x[0] + x[1] + x[2]) * a;
        }

        public static double CubicVietaC(double a, params double[] x)
        {
            return (x[0] * x[1] + x[1] * x[2] + x[0] * x[2]) * a;
        }

        public static double CubicVietaD(double a, params double[] x)
        {
            return -x[0] * x[1] * x[2] * a;
        }

        [Test]
        public void Cubic_Vieta_Dgt0_Random()
        {
            var r = new MyRandom();
            double a, b, c, d;
            while (true)
            {
                a = r.Next(1, 100) * r.NextDouble() * r.NextSign();
                b = r.Next(0, 100) * r.NextDouble() * r.NextSign();
                c = r.Next(0, 100) * r.NextDouble() * r.NextSign();
                d = r.Next(0, 100) * r.NextDouble() * r.NextSign();
                if (CubicDiscriminant(a, b, c, d) > 0 && a != 0) break;
            }

            var p = CreatePolynomial(a, b, c, d);
            var pe = CreatePolynomialEquation(p, 1e-11);
            pe.Solve();

            Assert.AreEqual(3, pe.AllRoots.Count);
            Assert.AreEqual(3, pe.Roots.Count);
            var x = pe.Roots.ToArray();
            Assert.AreEqual(b, CubicVietaB(a, x), 1e-6);
            Assert.AreEqual(c, CubicVietaC(a, x), 1e-6);
            Assert.AreEqual(d, CubicVietaD(a, x), 1e-6);

            var x1 = pe.Roots[0];
            var x2 = pe.Roots[1];
            var x3 = pe.Roots[2];
            Assert.AreEqual(0, p.Calculate(x1), 1e-6);
            Assert.AreEqual(0, p.Calculate(x2), 1e-6);
            Assert.AreEqual(0, p.Calculate(x3), 1e-6);

            if (Math.Abs(d) <= 1e-4) return;
            Assert.AreEqual(-c / d, 1 / x1 + 1 / x2 + 1 / x3, 1e-3);
            Assert.AreEqual(b / d, 1 / (x1 * x2) + 1 / (x2 * x3) + 1 / (x1 * x3), 1e-3);
        }

        [Test]
        public void Cubic_Manual()
        {
            var e = new List<double[]>();
            var a = new List<double[]>();
            e.Add(new double[] {8, -36, 54, -27});
            a.Add(new double[] {1.5});
            e.Add(new double[] {1, -2, -16, 32});
            a.Add(new double[] {-4, 2, 4});
            e.Add(new double[] {2, -7, 4, -14});
            a.Add(new double[] {3.5});
            e.Add(new double[] {-1, -5, 4, 20});
            a.Add(new double[] {-5, -2, 2});
            e.Add(new double[] {3, -3, -0.75, 0.75});
            a.Add(new double[] {-0.5, 0.5, 1});
            e.Add(new double[] {1, -2, -1, 1});
            a.Add(new double[] {-0.801937735804838, 0.5549581320873701, 2.246979603717467});

            for (var i = 0; i < e.Count; i++)
            {
                var pe = CreatePolynomialEquation(CreatePolynomial(e[i]), 1e-10);
                pe.Solve();
                for (var j = 0; j < pe.Roots.Count; j++)
                {
                    Assert.AreEqual(a[i][j], pe.Roots[j], 1e-5);
                }
            }
        }

        [Test]
        public void Cubic_Int_Deq0_Random()
        {
            var r = new MyRandom();
            double a, b, c, d;
            while (true)
            {
                a = r.Next(1, 100) * r.NextSign();
                b = r.Next(0, 100) * r.NextSign();
                c = r.Next(0, 100) * r.NextSign();
                d = r.Next(0, 100) * r.NextSign();
                if (CubicDiscriminant(a, b, c, d) == 0) break;
            }

            var p = new Polynomial
            {
                new Monomial(a, 3),
                new Monomial(b, 2),
                new Monomial(c, 1),
                new Monomial(d, 0),
            };
            var pe = CreatePolynomialEquation(p, 1e-11);
            pe.Solve();

            Assert.AreEqual(3, pe.AllRoots.Count);
            Assert.IsTrue(new Interval(1, 2).IsInInterval(pe.Roots.Count), pe.Roots.Count.ToString());

            foreach (var i in pe.AllRoots)
            {
                Assert.AreEqual(0, p.Calculate(i), 1e-5);
            }
        }

        [Test]
        public void Cubic_Int_Dlt0_Random()
        {
            var r = new MyRandom();
            double a, b, c, d;
            while (true)
            {
                a = r.Next(1, 100) * r.NextSign();
                b = r.Next(0, 100) * r.NextSign();
                c = r.Next(0, 100) * r.NextSign();
                d = r.Next(0, 100) * r.NextSign();
                if (CubicDiscriminant(a, b, c, d) < 0) break;
            }

            var p = CreatePolynomial(new[]
            {
                new Monomial(a, 3),
                new Monomial(b, 2),
                new Monomial(c, 1),
                new Monomial(d, 0),
            });
            var pe = CreatePolynomialEquation(p, 1e-10);
            pe.Solve();

            Assert.AreEqual(1, pe.AllRoots.Count);
            Assert.AreEqual(1, pe.Roots.Count);

            Assert.AreEqual(0, p.Calculate(pe.Roots[0]), 1e-4);
        }

        [Test]
        public void Cubic_Vieta_Dlt0_Random()
        {
            var r = new MyRandom();
            double a, b, c, d;
            while (true)
            {
                a = r.Next(1, 100) * r.NextDouble() * r.NextSign();
                b = r.Next(0, 100) * r.NextDouble() * r.NextSign();
                c = r.Next(0, 100) * r.NextDouble() * r.NextSign();
                d = r.Next(0, 100) * r.NextDouble() * r.NextSign();
                if (CubicDiscriminant(a, b, c, d) < 0 && a != 0) break;
            }

            var p = CreatePolynomial( new []
            {
                new Monomial(a, 3),
                new Monomial(b, 2),
                new Monomial(c, 1),
                new Monomial(d, 0),
            });
            var pe = CreatePolynomialEquation(p, 1e-9);
            pe.Solve();

            Assert.AreEqual(1, pe.AllRoots.Count);
            Assert.AreEqual(1, pe.Roots.Count);

            Assert.AreEqual(0, p.Calculate(pe.Roots[0]), 1e-6);
        }

        [Test]
        public void Cubic_Vieta_RootsToCoef_Random()
        {
            var r = new MyRandom();
            var x = new double[3];
            while (true)
            {
                x[0] = r.Next(0, 500) * r.NextDouble() * r.NextSign();
                x[1] = r.Next(0, 500) * r.NextDouble() * r.NextSign();
                x[2] = r.Next(0, 500) * r.NextDouble() * r.NextSign();
                if (!(x[0] == x[1] || x[1] == x[2] || x[2] == x[0])) break;
            }

            Array.Sort(x);
            double a, b, c, d;
            IPolynomialEquation pe;

            a = r.Next(1, 10) * r.NextSign();
            b = CubicVietaB(a, x);
            c = CubicVietaC(a, x);
            d = CubicVietaD(a, x);
            Assert.Greater(CubicDiscriminant(a, b, c, d), 0);
            pe = CreatePolynomialEquation(CreatePolynomial(a, b, c, d), 1e-9);
            pe.Solve();
            Assert.AreEqual(3, pe.AllRoots.Count);
            Assert.AreEqual(3, pe.Roots.Count);
            Assert.AreEqual(x[0], pe.AllRoots[0], 1e-5);
            Assert.AreEqual(x[1], pe.AllRoots[1], 1e-5);
            Assert.AreEqual(x[2], pe.AllRoots[2], 1e-5);

            List<double[]> zz = new List<double[]>
            {
                new double[] {0, 0, 0},
                new double[] {0, 0, 1},
                new double[] {0, 0, 2},
                new double[] {0, 1, 1},
                new double[] {0, 2, 2},
                new double[] {1, 1, 1},
                new double[] {1, 1, 2},
                new double[] {1, 2, 2},
                new double[] {2, 2, 2},
            };

            foreach (var z in zz)
            {
                a = r.Next(1, 10) * r.NextSign();
                b = CubicVietaB(a, z);
                c = CubicVietaC(a, z);
                d = CubicVietaD(a, z);
                Assert.AreEqual(0, CubicDiscriminant(a, b, c, d));
                pe = CreatePolynomialEquation(CreatePolynomial(a, b, c, d), 1e-8);
                pe.Solve();
                Assert.AreEqual(3, pe.AllRoots.Count);
                Assert.IsTrue(new Interval(1, 2).IsInInterval(pe.Roots.Count), pe.Roots.Count.ToString());
                Assert.AreEqual(z[0], pe.AllRoots[0], 1e-5);
                Assert.AreEqual(z[1], pe.AllRoots[1], 1e-5);
                Assert.AreEqual(z[2], pe.AllRoots[2], 1e-5);
            }
        }

        [Test]
        public void ReadmeTest()
        {
            var s = "x^3-2x^2-x+2";
            var eps = 1e-8;
            var p = Polynomial.Parse(s);
            var e = CreatePolynomialEquation(p, eps);
            e.Solve();
            Assert.AreEqual(-1, e.Roots[0], 1e-7);
            Assert.AreEqual(0, p.Calculate(e.Roots[0]), 1e-7);
            Assert.AreEqual(1, e.Roots[1], 1e-7);
            Assert.AreEqual(0, p.Calculate(e.Roots[1]), 1e-7);
            Assert.AreEqual(2, e.Roots[2], 1e-7);
            Assert.AreEqual(0, p.Calculate(e.Roots[2]), 1e-7);
        }
    }
}
