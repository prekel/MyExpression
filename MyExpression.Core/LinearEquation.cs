using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class LinearEquation : IEquation
	{
		public double A { get; set; }
		public double B { get; set; }

		public double X => -B / A;

		public IList<double> Roots { get => new List<double>(new double[] { X }); }

		public LinearEquation(double a, double b)
		{
			A = a;
			B = b;
		}

		public LinearEquation(PolynomialEquation p)
		{
			if (p.Polynomial.Degree != 1) throw new InvalidOperationException();
			A = p.Polynomial[1].Coefficient;
			B = p.Polynomial[0].Coefficient;
		}
	}
}
