// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class FunctionEquation : IEquation
	{
		public Func<double, double> Function { get; private set; }

		public Interval Interval { get; private set; }
		public double Step { get; private set; }
		public double Epsilon { get; private set; }

		public FunctionEquation(Func<double, double> function, Interval interval, double step = 1e-3, double eps = 1e-8)
		{
			Function = function;
			Interval = interval;
			Step = step;
			Epsilon = eps;
		}

		public FunctionEquation(IFunctionX functionx, Interval interval, double step = 1e-3, double eps = 1e-8)
			: this(functionx.Calculate, interval, step, eps) { }

		public IList<double> AllRoots { get; private set; }

		public IList<double> Roots { get; private set; }

		public void Solve()
		{
			AllRoots = new List<double>();
			var x0 = Interval.Left;
			var y0 = Function(x0);
			var y = 0d;
			for (var x = Interval.Left + Step; x <= Interval.Right; x += Step)
			{
				y = Function(x);

				if (y0 * y < 0)
				{
					var bs = new RecursiveBinarySearch(Function, new Interval(x0, x), Epsilon);
					AllRoots.Add(bs.Solve());
				}

				y0 = y;
				x0 = x;
			}
		}
	}
}
