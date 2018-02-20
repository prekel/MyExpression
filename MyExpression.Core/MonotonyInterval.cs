using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class MonotonyInterval : Interval
	{
		public double P { get; set; }
		public bool IsPositive => P > 0;
		public bool IsNegative => P < 0;

		public MonotonyInterval(double l, double r) : base(l, r)
		{
		}

		public MonotonyInterval(double l, double r, double p) : base(l, r)
		{
			P = p;
		}

		public MonotonyInterval(Interval lr) : this(lr.Left, lr.Right)
		{
			
		}
	}
}
