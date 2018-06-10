using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class QuadraticParabola : SquareEquation, IFunctionX, IDerivativable
	{
		public QuadraticParabola(double a, double b, double c) : base(a, b, c) { }

		public IFunctionX Derivative => new Straight(2 * A, B);

		public double Calculate(double x) => A * x * x + B * x + C;
	}
}
