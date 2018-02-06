using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class PolynomialEquation
	{
		public Polynomial Polynomial { get; private set; }

		public Polynomial Derivative => Polynomial.Derivative;

		private PolynomialEquation polynomialEquation;
		public PolynomialEquation DerivativeEquation
		{
			get
			{
				if (polynomialEquation == null)
				{
					polynomialEquation = new PolynomialEquation(Derivative);
				}
				return polynomialEquation;
			}
		}

		public PolynomialEquation(Polynomial p)
		{
			Polynomial = p;
		}

		public List<double> Roots { get; private set; } = new List<double>();

		public bool IsSolved { get; private set; }

		public void Solve()
		{

		}
	}
}
