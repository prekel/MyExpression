using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class SquareEquation
	{
		public double A { get; set; }
		public double B { get; set; }
		public double C { get; set; }

		public double D => B * B - 4 * A * C;

		public int N
		{
			get
			{
				var d = D;
				if (d > 0)
					return 2;
				return d == 0 ? 1 : 0;
			}
		}

		public double X0 => -B / (2 * A);

		public double X1 => (-B + Math.Sqrt(D)) / (2 * A);

		public double X2 => (-B - Math.Sqrt(D)) / (2 * A);

		public Tuple<double, double> X => Tuple.Create(X1, X2);

		public SquareEquation(double a = 1, double b = 1, double c = 1)
		{
			A = a;
			B = b;
			C = c;
		}
	}
}
