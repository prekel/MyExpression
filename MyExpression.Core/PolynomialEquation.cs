using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class PolynomialEquation
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

		public List<double> Roots { get; private set; } = new List<double>();

		public bool IsSolved { get; private set; }

		public void Solve()
		{
			if (Polynomial.Degree == 1)
			{
				Roots.Add(-Polynomial[0].Coefficient / Polynomial[1].Coefficient);
				IsSolved = true;
				return;
			}
			DerivativeEquation.Solve();
			var intr = DerivativeEquation.RIntervals;
			foreach (var i in intr)
			{
				var a = Polynomial.Evaluate(i.Left);
				var b = Polynomial.Evaluate(i.Right);
				if (a * b > 0) continue;
				Roots.Add(BinarySearch(i));
			}
			IsSolved = true;
		}

		public double BinarySearch(Interval a)
		{
			if (a.Left == Double.NegativeInfinity && a.Right == Double.PositiveInfinity)
			{
				return 0;
			}
			var l = Polynomial.Evaluate(a.Left);
			var r = Polynomial.Evaluate(a.Right);

			if (Math.Abs(l) < Epsilon) return a.Left;
			if (Math.Abs(r) < Epsilon) return a.Right;

			if (a.Left == Double.NegativeInfinity && a.IsPositive)
			{
				return BinarySearch(new Interval(Dasdas(-1, a.Right, u => u < 0), a.Right), (x, y) => x.CompareTo(y));
			}
			if (a.Left == Double.NegativeInfinity && a.IsNegative)
			{
				return BinarySearch(new Interval(Dasdas(-1, a.Right, u => u > 0), a.Right), (x, y) => -x.CompareTo(y));
			}
			if (a.Right == Double.PositiveInfinity && a.IsPositive)
			{
				return BinarySearch(new Interval(a.Left, Dasdas(1, a.Left, u => u > 0)), (x, y) => x.CompareTo(y));
			}
			if (a.Right == Double.PositiveInfinity && a.IsNegative)
			{
				return BinarySearch(new Interval(a.Left, Dasdas(1, a.Left, u => u < 0)), (x, y) => -x.CompareTo(y));
			}

			double Dasdas(int k, double rl, Func<double, bool> cnd)
			{
				var rl1 = 0.0;
				while (true)
				{
					rl1 = rl + k;
					if (cnd(Polynomial.Evaluate(rl1)))
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
			var p = Polynomial.Evaluate(m);
			if (comp(p, 0) == 1)
			{
				return BinarySearch(new Interval(a.Left, m));
			}
			if (comp(p, 0) == -1)
			{
				return BinarySearch(new Interval(m, a.Right));
			}
			return m;
		}

		private Intervals intervals;
		public Intervals RIntervals
		{
			get
			{
				if (!IsSolved) return null;
				if (intervals == null) intervals = new Intervals(Roots, Polynomial);
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
			public Intervals(List<double> r, Polynomial p)
			{
				if (r.Count == 0)
				{
					Add(new Interval(Double.NegativeInfinity, Double.PositiveInfinity, p.Evaluate(0)));
				}
				else if (r.Count == 1)
				{
					Add(new Interval(Double.NegativeInfinity, r[0], p.Evaluate(r[0] - 1)));
					Add(new Interval(r[0], Double.PositiveInfinity, p.Evaluate(r[0] + 1)));
				}
				else
				{
					Add(new Interval(Double.NegativeInfinity, r[0], p.Evaluate(r[0] - 1)));
					for (var i = 0; i < r.Count - 1; i++)
					{
						Add(new Interval(r[i], r[i + 1], p.Evaluate((r[i] + r[i + 1]) / 2)));
					}
					Add(new Interval(r[r.Count - 1], Double.PositiveInfinity, p.Evaluate(r[r.Count - 1] + 1)));
				}
			}
		}
	}
}
