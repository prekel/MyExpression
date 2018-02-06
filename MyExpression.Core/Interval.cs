using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class Interval
	{
		public double Left { get; set; }
		public bool IsLeftOpen { get; set; }

		public double Right { get; set; }
		public bool IsRightOpen { get; set; }

		public Interval(double l, double r)
		{
			Left = l;
			Right = r;
			if (Left == Double.NegativeInfinity) IsLeftOpen = true;
			if (Right == Double.PositiveInfinity) IsRightOpen = true;
		}

		public Interval(double l, bool lo, double r, bool ro)
		{
			Left = l;
			Right = r;
			IsLeftOpen = lo;
			IsRightOpen = ro;
		}

		public override string ToString()
		{
			return $"{(IsLeftOpen ? "(" : "[")}{Left}; {Right}{(IsRightOpen ? ")" : "]")}";
		}
	}
}
