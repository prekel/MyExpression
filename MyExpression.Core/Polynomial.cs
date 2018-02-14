using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyExpression.Core
{
	public class Polynomial : IEnumerable<Monomial>
	{
		private SortedDictionary<double, Monomial> Data { get; set; } = new SortedDictionary<double, Monomial>();

		public double Degree => Data.Last().Value.Degree;

		public double Calculate(double x) => Data.Values.Sum(m => m.Calculate(x));

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

		public Polynomial(double[] v)
		{
			for (var i = v.Length - 1; i >= 0; i--)
			{
				Add(new Monomial(v[v.Length - i - 1], i));
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

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Polynomial)) return false;
			var p = (Polynomial)obj;
			if (p.Degree != p.Degree) return false;
			for (var i = 0d; i <= Math.Max(Degree, p.Degree); i++)
			{
				if (this[i].Coefficient != p[i].Coefficient)
					return false;
			}
			return true;
		}

		public bool Equals(Polynomial p, double epscoef = 0)
		{
			if (p.Degree != p.Degree) return false;
			for (var i = 0d; i <= Math.Max(Degree, p.Degree); i++)
			{
				if (Math.Abs(this[i].Coefficient - p[i].Coefficient) > epscoef)
					return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
