// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class PolynomialEquation : IEquation
	{
		public double Epsilon { get; set; }
		public Polynomial Polynomial { get; private set; }

		public Polynomial Derivative => Polynomial.Derivative;

		private PolynomialEquation polynomialEquation;
		public PolynomialEquation DerivativeEquation
		{
			get
			{
				if (polynomialEquation == null)
				{
					polynomialEquation = new PolynomialEquation(Derivative);
				}
				return polynomialEquation;
			}
		}

		public PolynomialEquation(Polynomial p, double eps = 0.00001)
		{
			Polynomial = p;
			Epsilon = eps;
		}

		public IList<double> AllRoots { get; private set; } = new List<double>();

		public IList<double> Roots { get => new SortedSet<double>(AllRoots).ToList(); }

		public bool IsSolved { get; private set; }

		public void Solve()
		{
			if (Polynomial.Degree == 1)
			{
				var le = new LinearEquation(this);
				AllRoots.Add(le.X);
				IsSolved = true;
				return;
			}
			DerivativeEquation.Solve();
			var intr = DerivativeEquation.MonotonyIntervals;
			foreach (var i in intr)
			{
				var a = Polynomial.Calculate(i.Left);
				var b = Polynomial.Calculate(i.Right);
				if (a * b > 0) continue;
				AllRoots.Add(BinarySearch(i));
			}
			IsSolved = true;
		}

		public double BinarySearch(Interval a)
		{
			if (a.Left == Double.NegativeInfinity && a.Right == Double.PositiveInfinity)
			{
				var z = Polynomial.Calculate(0);
				if (z >= 0)
				{
					if (a.IsPositive) return BinarySearch(new Interval(Increaser(-1, 0, u => u < 0), 0), (x, y) => x.CompareTo(y));
					if (a.IsNegative) return BinarySearch(new Interval(0, Increaser(1, 0, u => u < 0)), (x, y) => -x.CompareTo(y));
				}
				if (z < 0)
				{
					if (a.IsPositive) return BinarySearch(new Interval(0, Increaser(1, 0, u => u > 0)), (x, y) => x.CompareTo(y));
					if (a.IsNegative) return BinarySearch(new Interval(Increaser(-1, 0, u => u > 0), 0), (x, y) => -x.CompareTo(y));
				}
			}

			var l = Polynomial.Calculate(a.Left);
			var r = Polynomial.Calculate(a.Right);

			if (Math.Abs(l) < Epsilon) return a.Left;
			if (Math.Abs(r) < Epsilon) return a.Right;

			if (a.Left == Double.NegativeInfinity && a.IsPositive)
			{
				return BinarySearch(new Interval(Increaser(-1, a.Right, u => u < 0), a.Right), (x, y) => x.CompareTo(y));
			}
			if (a.Left == Double.NegativeInfinity && a.IsNegative)
			{
				return BinarySearch(new Interval(Increaser(-1, a.Right, u => u > 0), a.Right), (x, y) => -x.CompareTo(y));
			}
			if (a.Right == Double.PositiveInfinity && a.IsPositive)
			{
				return BinarySearch(new Interval(a.Left, Increaser(1, a.Left, u => u > 0)), (x, y) => x.CompareTo(y));
			}
			if (a.Right == Double.PositiveInfinity && a.IsNegative)
			{
				return BinarySearch(new Interval(a.Left, Increaser(1, a.Left, u => u < 0)), (x, y) => -x.CompareTo(y));
			}

			double Increaser(int k, double rl, Func<double, bool> cnd)
			{
				var rl1 = 0.0;
				while (true)
				{
					rl1 = rl + k;
					if (cnd(Polynomial.Calculate(rl1)))
					{
						return rl1;
					}
					k *= 2;
				}
			}

			if (l < r)
			{
				return BinarySearch(a, (x, y) => x.CompareTo(y));
			}

			return BinarySearch(a, (x, y) => -x.CompareTo(y));
		}

		public double BinarySearch(Interval a, Func<double, double, int> comp)
		{
			var m = a.Left / 2 + a.Right / 2;
			if (Math.Abs(a.Left - a.Right) < Epsilon) return m;
			var p = Polynomial.Calculate(m);
			if (comp(p, 0) == 1)
			{
				return BinarySearch(new Interval(a.Left, m), comp);
			}
			if (comp(p, 0) == -1)
			{
				return BinarySearch(new Interval(m, a.Right), comp);
			}
			return m;
		}

		private Intervals intervals;
		public Intervals MonotonyIntervals
		{
			get
			{
				if (!IsSolved) return null;
				if (intervals == null) intervals = new Intervals(AllRoots, Polynomial);
				return intervals;
			}
		}

		public class Interval : Core.Interval
		{
			public double P { get; set; }
			public bool IsPositive => P > 0;
			public bool IsNegative => P < 0;

			public Interval(double l, double r) : base(l, r)
			{
			}

			public Interval(double l, double r, double p) : base(l, r)
			{
				P = p;
			}
		}

		public class Intervals : List<Interval>
		{
			public Intervals(IList<double> r, Polynomial p)
			{
				if (r.Count == 0)
				{
					Add(new Interval(Double.NegativeInfinity, Double.PositiveInfinity, p.Calculate(0)));
				}
				else if (r.Count == 1)
				{
					Add(new Interval(Double.NegativeInfinity, r[0], p.Calculate(r[0] - 1)));
					Add(new Interval(r[0], Double.PositiveInfinity, p.Calculate(r[0] + 1)));
				}
				else
				{
					Add(new Interval(Double.NegativeInfinity, r[0], p.Calculate(r[0] - 1)));
					for (var i = 0; i < r.Count - 1; i++)
					{
						Add(new Interval(r[i], r[i + 1], p.Calculate((r[i] + r[i + 1]) / 2)));
					}
					Add(new Interval(r[r.Count - 1], Double.PositiveInfinity, p.Calculate(r[r.Count - 1] + 1)));
				}
			}
		}

		public override string ToString()
		{
			return $"{Polynomial} = 0 IsSolved = {IsSolved}" + (Roots.Count > 0 ? $" Roots = {String.Join(" ", Roots)}" : "");
		}
	}
}
