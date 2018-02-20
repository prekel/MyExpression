using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class OpenCloseInterval : Interval
	{
		public bool IsLeftOpen { get; set; }
		public bool IsRightOpen { get; set; }
		
		public OpenCloseInterval(double l, double r) : base(l, r)
		{
			if (Left == Double.NegativeInfinity) IsLeftOpen = true;
			if (Right == Double.PositiveInfinity) IsRightOpen = true;
		}

		public OpenCloseInterval(double l, bool lo, double r, bool ro) : this(l, r)
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
