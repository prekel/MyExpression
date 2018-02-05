using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class Monomial : SortedList<string, double>, IComparable
	{
		public double Ratio { get; set; }
		//public double Degree { get; set; }
		public string Var { get; set; }

		public double Evaluate(double x)
		{
			if (Count == 0)
				return Ratio;
			return Ratio * this.Sum(i => Math.Pow(x, i.Value));
		}

		public double this[double x] => Evaluate(x);

		//public Monomial(double ratio, string variable, double degree)
		//{
		//	Ratio = ratio;
		//	this[variable] = degree;
		//}

		public Monomial(double ratio, double degree, string variable = "x")
		{
			Ratio = ratio;
			this[variable] = degree;
		}

		public Monomial(double ratio)
		{
			Ratio = ratio;
		}

		public Monomial Derivative
		{

			get
			{
				if (Count > 1) 
					throw new NotImplementedException();
				return new Monomial(Ratio * Values[0], Values[0] - 1);
			}
		}

		public override string ToString()
		{
			return $"{Ratio}{Var}^{Values[0]}";
		}

		public int CompareTo(object obj)
		{
			return Values.Sum().CompareTo(((Monomial)obj).Values.Sum());
		}
	}
}
