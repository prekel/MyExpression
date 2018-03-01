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
					polynomialEquation = new PolynomialEquation(Derivative, Epsilon);
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

		public IList<double> Roots => new SortedSet<double>(AllRoots).ToList();

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
				double a, b;
				if (i.Left == Double.NegativeInfinity)
				{
					a = Polynomial.Calculate(i.Right - 1);
					b = Polynomial.Calculate(i.Right);
					if (a > b && b > Epsilon) continue;
					if (a < b && b < -Epsilon) continue;
				}
				else if (i.Right == Double.PositiveInfinity)
				{
					a = Polynomial.Calculate(i.Left);
					b = Polynomial.Calculate(i.Left + 1);
					if (a > b && a < -Epsilon) continue;
					if (a < b && a > Epsilon) continue;
				}
				else
				{
					a = Polynomial.Calculate(i.Left);
					b = Polynomial.Calculate(i.Right);
					if (Math.Sign(a) * Math.Sign(b) > 0 && Math.Abs(a * b) >= Epsilon) continue;
				}
				var bs = new RecursiveBinarySearch(Polynomial.Calculate, i, Epsilon);
				AllRoots.Add(bs.Solve());
			}
			IsSolved = true;
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

		public class Intervals : List<Interval>
		{
			public Intervals(IList<double> r, Polynomial p)
			{
				if (r.Count == 0)
				{
					Add(new Interval(Double.NegativeInfinity, Double.PositiveInfinity));
				}
				else if (r.Count == 1)
				{
					Add(new Interval(Double.NegativeInfinity, r[0]));
					Add(new Interval(r[0], Double.PositiveInfinity));
				}
				else
				{
					Add(new Interval(Double.NegativeInfinity, r[0]));
					for (var i = 0; i < r.Count - 1; i++)
					{
						Add(new Interval(r[i], r[i + 1]));
					}
					Add(new Interval(r[r.Count - 1], Double.PositiveInfinity));
				}
			}
		}

		public override string ToString()
		{
			return $"{Polynomial} = 0 IsSolved = {IsSolved}" + (Roots.Count > 0 ? $" Roots = {{{String.Join(" ", Roots)}}}" : "");
		}
	}
}
