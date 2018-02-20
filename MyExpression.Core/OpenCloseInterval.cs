using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class OpenCloseInterval : Interval
	{
		public new double Left { get; set; }
		public bool IsLeftOpen { get; set; }

		public new double Right { get; set; }
		public bool IsRightOpen { get; set; }
		
		public OpenCloseInterval(double l, double r) : base(l, r)
		{
			if (Left == Double.NegativeInfinity) IsLeftOpen = true;
			if (Right == Double.PositiveInfinity) IsRightOpen = true;
		}

		public OpenCloseInterval(double l, bool lo, double r, bool ro) : base(l, r)
		{
			IsLeftOpen = lo;
			IsRightOpen = ro;
		}

		public OpenCloseInterval(Interval lr) : this(lr.Left, lr.Right)
		{

		}

		public override string ToString()
		{
			return $"{(IsLeftOpen ? "(" : "[")}{Left}; {Right}{(IsRightOpen ? ")" : "]")}";
		}
	}
}
