using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyExpression.Core
{
	//[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	public class Polynomial : MySortedList<Monomial>
	{
		public double Degree => this.First().Degree;

		public double Evaluate(double x)
		{
			//var c = 0d;
			//foreach (var i in this)
			//{
			//	c += i.Evaluate(x);
			//}
			return this.Sum(m => m.Evaluate(x));
			//return c;
		}

		//public double this[double x] => Evaluate(x);

		//public int Rank => this.Max(m => m.Count);

		class MonomialComparer : Comparer<Monomial>
		{
			public override int Compare(Monomial x, Monomial y)
			{
				return -x.CompareTo(y);
			}
		}

		public Polynomial() : base(new MonomialComparer())
		{

		}

		public Polynomial Derivative
		{
			get
			{
				var d = new Polynomial();
				foreach (var i in this)
				{
					d.Add(i.Derivative);
				}
				return d;
			}
		}

		public static Polynomial Parse(string p)
		{
			if (p[0] != '-' && p[0] != '+') p = "+" + p;
			var s1 = p.Split(new char[] { '-', '+' }, StringSplitOptions.RemoveEmptyEntries);

			var j = 0;
			for (var i = 0; i < s1.Length; i++)
			{
				s1[i] = p[j] + s1[i];
				j += s1[i].Length;
			}

			var pl = new Polynomial();
			foreach (var i in s1)
			{
				pl.Add(Monomial.Parse(i));
			}
			return pl;
		}

		public override string ToString()
		{
			var s = "";
			foreach (var i in this)
			{
				if (i.Coefficient > 0 && s != "")
				{
					s += "+";
				}
				s += i.ToString();
			}
			return s;
		}

		public SquareEquation ToSquareEquation()
		{
			if (Count > 3)
				throw new InvalidOperationException();
			return new SquareEquation(base[0].Coefficient, base[1].Coefficient, base[2].Coefficient);
		}
	}
}
