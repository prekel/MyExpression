using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class PolynomialEquation
	{
		public double Epsilon { get; set; }
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

		public PolynomialEquation(Polynomial p, double eps = 0.00001)
		{
			Polynomial = p;
			Epsilon = eps;
		}

		public List<double> Roots { get; private set; } = new List<double>();

		public bool IsSolved { get; private set; }

		public void Solve()
		{

		}

		public class Interval : Core.Interval
		{
			public double P { get; set; }
			public bool Increasing => P > 0;
			public bool Decreasing => P < 0;

			public Interval(double l, double r) : base(l, r)
			{
			}

			public Interval(double l, double r, double p) : base(l, r)
			{
				P = p;
			}
		}

		public class Intervals : List<Interval>
		{
			public Intervals(List<double> r, Polynomial p)
			{
				if (r.Count == 0)
				{
					Add(new Interval(Double.NegativeInfinity, Double.PositiveInfinity, p.Evaluate(0)));
				}
				else if (r.Count == 1)
				{
					Add(new Interval(Double.NegativeInfinity, r[0], p.Evaluate(r[0] - 1)));
					Add(new Interval(r[0], Double.PositiveInfinity, p.Evaluate(r[0] + 1)));
				}
				else
				{
					Add(new Interval(Double.NegativeInfinity, r[0], p.Evaluate(r[0] - 1)));
					for (var i = 0; i < r.Count - 1; i++)
					{
						Add(new Interval(r[i], r[i + 1], p.Evaluate((r[i] + r[i + 1]) / 2)));
					}
					Add(new Interval(r[r.Count - 1], Double.PositiveInfinity, p.Evaluate(r[r.Count - 1] + 1)));
				}
			}
		}
	}
}
