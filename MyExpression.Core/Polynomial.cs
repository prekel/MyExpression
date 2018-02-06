using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyExpression.Core
{
	//[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	public class Polynomial : IEnumerable<Monomial>
	{
		private SortedDictionary<double, Monomial> Data { get; private set; } = new SortedDictionary<double, Monomial>();

		public double Degree => Data.Last().Value.Degree;

		public double Evaluate(double x)
		{
			//var c = 0d;
			//foreach (var i in this)
			//{
			//	c += i.Evaluate(x);
			//}
			return Data.Values.Sum(m => m.Evaluate(x));
			//return c;
		}

		public Monomial this[double degree]
		{
			get
			{
				if (Data.ContainsKey(degree))
				{
					return Data[degree];
				}
				else
				{
					return new Monomial(0, degree);
				}
			}
		}

		//public double this[double x] => Evaluate(x);

		//public int Rank => this.Max(m => m.Count);

		//class MonomialComparer : Comparer<Monomial>
		//{
		//	public override int Compare(Monomial x, Monomial y)
		//	{
		//		return -x.CompareTo(y);
		//	}
		//}

		public Polynomial()
		{
		}

		public Polynomial(Polynomial a)
		{
			foreach (var i in a)
			{
				Data.Add(i.Degree, new Monomial(i.Coefficient, i.Degree));
			}
		}

		public void Add(Monomial a)
		{
			if (Data.ContainsKey(a.Degree))
			{
				Data[a.Degree].Add(a);
			}
			else
			{
				Data.Add(a.Degree, a);
			}
		}

		public void Sub(Monomial a)
		{
			if (Data.ContainsKey(a.Degree))
			{
				Data[a.Degree].Sub(a);
			}
			else
			{
				Data.Add(a.Degree, -a);
			}
		}

		public Polynomial Derivative
		{
			get
			{
				var d = new Polynomial();
				foreach (var i in Data.Values)
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

		public static Polynomial operator +(Polynomial a, Polynomial b)
		{
			var p = new Polynomial(a);
			foreach (var i in b)
			{
				p.Add(i);
			}
			return p;
		}

		public static Polynomial operator -(Polynomial a, Polynomial b)
		{
			var p = new Polynomial(a);
			foreach (var i in b)
			{
				p.Sub(i);
			}
			return p;
		}

		public static Polynomial operator +(Polynomial a, Monomial b)
		{
			var p = new Polynomial(a);
			p.Add(b);
			return p;
		}

		public static Polynomial operator -(Polynomial a, Monomial b)
		{
			var p = new Polynomial(a);
			p.Sub(b);
			return p;
		}

		public static Polynomial operator +(Monomial b, Polynomial a)
		{
			return a + b;
		}

		public static Polynomial operator -(Monomial b, Polynomial a)
		{
			return a - b;
		}

		public static Polynomial operator -(Polynomial a)
		{
			var p = new Polynomial(a);
			foreach (var i in p)
			{
				i.Coefficient *= -1;
			}
			return p;
		}

		public static Polynomial operator +(Polynomial a)
		{
			return a;
		}

		public static Polynomial operator *(Polynomial a, Monomial b)
		{
			var p = new Polynomial(a);
			foreach (var i in p)
			{
				i.Multiply(b);
			}
			return p;
		}

		public static Polynomial operator *(Monomial a, Polynomial b)
		{
			return b * a;
		}

		public static Polynomial operator *(Polynomial a, double b)
		{
			var p = new Polynomial(a);
			foreach (var i in p)
			{
				i.Multiply(b);
			}
			return p;
		}

		public static Polynomial operator *(double a, Polynomial b)
		{
			return b * a;
		}

		public override string ToString()
		{
			var s = "";
			foreach (var i in Data.Values.OrderByDescending(i => i))
			{
				if (i.Coefficient >= 0 && s != "")
				{
					s += "+";
				}
				s += i.ToString();
			}
			return s;
		}

		public SquareEquation SquareEquation => ToSquareEquation();

		public SquareEquation ToSquareEquation()
		{
			if (Degree != 2)
				throw new InvalidOperationException();
			return new SquareEquation(this[2].Coefficient, this[1].Coefficient, this[0].Coefficient);
		}

		public IEnumerator<Monomial> GetEnumerator()
		{
			return Data.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Data.Values.GetEnumerator();
		}
	}
}
