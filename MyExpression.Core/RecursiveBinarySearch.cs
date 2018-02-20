﻿// Copyright (c) 2018 Vladislav Prekel

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
		public bool IsSolved { get; private set; }

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

		private void Init(Interval lr, double eq, double eps)
		{
			StartInterval = new Interval(lr.Left, lr.Right);
			Interval = new Interval(lr.Left, lr.Right);
			EqualValue = eq;
			Epsilon = eps;
		}

		public RecursiveBinarySearch(Func<double, double> f, Interval lr, double eq, double eps)
		{
			Init(lr, eq, eps);
			Function = x => f(x) - eq;
		}

		public RecursiveBinarySearch(Func<double, double> f, Interval lr, double eps)
		{
			Init(lr, 0, eps);
			Function = f;
		}

		public double Solve()
		{
			InitBinarySearch();
			if (IsSolved) return Answer;
			BinarySearch();
			return Answer;
		}

		private void InitBinarySearch()
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
					}
					if (IsNegative)
					{
						Left = 0;
						Right = Increaser(1, 0, u => u < 0);
					}
				}
				if (z < 0)
				{
					if (IsPositive)
					{
						Left = 0;
						Right = Increaser(1, 0, u => u > 0);
					}
					if (IsNegative)
					{
						Left = Increaser(-1, 0, u => u > 0);
						Right = 0;
					}
				}
			}

			var l = Function(Left);
			var r = Function(Right);

			if (Math.Abs(l) < Epsilon) { Answer = Left; IsSolved = true; return; }
			if (Math.Abs(r) < Epsilon) { Answer = Right; IsSolved = true; return; }

			if (Left == Double.NegativeInfinity && IsPositive)
			{
				Left = Increaser(-1, Right, u => u < 0);
				Right = Right;
			}
			if (Left == Double.NegativeInfinity && IsNegative)
			{
				Left = Increaser(-1, Right, u => u > 0);
				Right = Right;
			}
			if (Right == Double.PositiveInfinity && IsPositive)
			{
				Left = Left;
				Right = Increaser(1, Left, u => u > 0);
			}
			if (Right == Double.PositiveInfinity && IsNegative)
			{
				Left = Left;
				Right = Increaser(1, Left, u => u < 0);
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
		}

		private void BinarySearch()
		{
			if (Math.Abs(Left - Right) < Epsilon) return;
			if (Comparer(MedianValue, 0) > 0)
			{
				Right = Median;
				BinarySearch();
			}
			else
			{
				Left = Median;
				BinarySearch();
			}
		}
	}
}
