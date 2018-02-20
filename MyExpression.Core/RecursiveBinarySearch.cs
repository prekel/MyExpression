// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class RecursiveBinarySearch : IBinarySearch
	{
		public Interval StartInterval { get; private set; }
		public Interval Interval { get; private set; }
		public double Epsilon { get; private set; }
		public double Answer { get; private set; }
		public Func<double, double> Function { get; private set; }
		public double EqualValue { get; private set; }

		public double EndsDifference
		{
			get
			{
				if (Interval.Left == Double.NegativeInfinity && Interval.Right == Double.PositiveInfinity) return Function(1) - Function(-1);
				if (Interval.Left == Double.NegativeInfinity) return Function(Interval.Right) - Function(Median);
				if (Interval.Right == Double.PositiveInfinity) return Function(Median) - Function(Interval.Left);
				return Function(Interval.Right) - Function(Interval.Left);
			}
		}
		public bool IsPositive => EndsDifference > 0;
		public bool IsNegative => EndsDifference < 0;

		private double Median
		{
			get
			{
				if (Interval.Left == Double.NegativeInfinity && Interval.Right == Double.PositiveInfinity) return 0;
				if (Interval.Left == Double.NegativeInfinity) return Interval.Right - 1;
				if (Interval.Right == Double.PositiveInfinity) return Interval.Left + 1;
				return (Interval.Left + Interval.Right) / 2;
			}
		}
		private double MedianValue => Function(Median);
		private double Left { get => Interval.Left; set => Interval.Left = value; }
		private double Right { get => Interval.Right; set => Interval.Right = value; }

		private static Func<double, double, int> LessComparer = (x, y) => x.CompareTo(y);
		private static Func<double, double, int> GreaterComparer = (x, y) => -x.CompareTo(y);
		private Func<double, double, int> Comparer => IsPositive ? LessComparer : GreaterComparer;

		public RecursiveBinarySearch(Func<double, double> f, Interval lr, double eq, double eps)
		{
			Function = x => f(x) - eq;
			StartInterval = new Interval(lr.Left, lr.Right);
			Interval = new MonotonyInterval(lr.Left, lr.Right);
			EqualValue = eq;
			Epsilon = eps;
		}

		public double Solve()
		{
			throw new NotImplementedException();
		}

		private double BinarySearch()
		{
			if (Left == Double.NegativeInfinity && Right == Double.PositiveInfinity)
			{
				var z = Function(0);
				if (z >= 0)
				{
					if (IsPositive)
					{
						Left = Increaser(-1, 0, u => u < 0);
						Right = 0;
						//Comparer = (x, y) => x.CompareTo(y);
					}
					if (IsNegative)
					{
						Left = 0;
						Right = Increaser(1, 0, u => u < 0);
						//Comparer = (x, y) => -x.CompareTo(y);
					}
				}
				if (z < 0)
				{
					if (IsPositive)
					{
						Left = 0;
						Right = Increaser(1, 0, u => u > 0);
						//Comparer = (x, y) => x.CompareTo(y);
					}
					if (IsNegative)
					{
						Left = Increaser(-1, 0, u => u > 0);
						Right = 0;
						//Comparer = (x, y) => -x.CompareTo(y);
					}
				}
			}

			var l = Function(Left);
			var r = Function(Right);

			if (Math.Abs(l) < Epsilon) return Left;
			if (Math.Abs(r) < Epsilon) return Right;

			if (Left == Double.NegativeInfinity && IsPositive)
			{
				Left = Increaser(-1, Right, u => u < 0);
				Right = Right;
				//Comparer = (x, y) => x.CompareTo(y);
			}
			if (Left == Double.NegativeInfinity && IsNegative)
			{
				Left = Increaser(-1, Right, u => u > 0);
				Right = Right;
				//Comparer = (x, y) => -x.CompareTo(y);
			}
			if (Right == Double.PositiveInfinity && IsPositive)
			{
				Left = Left;
				Right = Increaser(1, Left, u => u > 0);
				//Comparer = (x, y) => x.CompareTo(y);
			}
			if (Right == Double.PositiveInfinity && IsNegative)
			{
				Left = Left;
				Right = Increaser(1, Left, u => u < 0);
				//Comparer = (x, y) => -x.CompareTo(y);
			}

			double Increaser(int k, double rl, Func<double, bool> cnd)
			{
				var rl1 = 0.0;
				while (true)
				{
					rl1 = rl + k;
					if (cnd(Function(rl1)))
					{
						return rl1;
					}
					k *= 2;
				}
			}

			if (l < r)
			{
				return BinarySearch((x, y) => x.CompareTo(y));
			}

			return BinarySearch((x, y) => -x.CompareTo(y));
		}

		public double BinarySearch(Func<double, double, int> comp)
		{
			var m = a.Left / 2 + a.Right / 2;
			if (Math.Abs(a.Left - a.Right) < Epsilon) return m;
			var p = Function(m);
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
	}
}
