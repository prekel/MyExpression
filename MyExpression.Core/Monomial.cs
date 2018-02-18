using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyExpression.Core
{
	public class Monomial : IComparable
	{
		public double Coefficient { get; set; }
		public double Degree { get; set; }

		public double Calculate(double x) => Coefficient * Math.Pow(x, Degree);

		public double this[double x] => Calculate(x);

		public void Add(Monomial a)
		{
			if (a.Degree != Degree) throw new InvalidOperationException();
			Coefficient += a.Coefficient;
		}

		public void Sub(Monomial a)
		{
			if (a.Degree != Degree) throw new InvalidOperationException();
			Coefficient -= a.Coefficient;
		}

		public void Multiply(Monomial a)
		{
			Degree += a.Degree;
			Coefficient *= a.Coefficient;
		}

		public void Multiply(double a)
		{
			Coefficient *= a;
		}

		public static Monomial operator +(Monomial a, Monomial b)
		{
			if (a.Degree != b.Degree) throw new InvalidOperationException();
			return new Monomial(a.Coefficient + b.Coefficient, a.Degree);
		}

		public static Monomial operator -(Monomial a, Monomial b)
		{
			if (a.Degree != b.Degree) throw new InvalidOperationException();
			return new Monomial(a.Coefficient - b.Coefficient, a.Degree);
		}

		public static Monomial operator -(Monomial a)
		{
			return new Monomial(-a.Coefficient, a.Degree);
		}

		public static Monomial operator +(Monomial a)
		{
			return new Monomial(a.Coefficient, a.Degree);
		}

		public static Monomial operator *(Monomial a, Monomial b)
		{
			return new Monomial(a.Coefficient * b.Coefficient, a.Degree + b.Degree);
		}

		public static Monomial operator *(Monomial a, double b)
		{
			return new Monomial(a.Coefficient * b, a.Degree);
		}

		public Monomial(double coef = 1, double degree = 1)
		{
			Coefficient = coef;
			Degree = degree;
		}

		public Monomial Derivative
		{
			get
			{
				if (Degree == 0) return new Monomial(0, 0);
				return new Monomial(Coefficient * Degree, Degree - 1);
			}
		}

		private static readonly Regex Pattern = new Regex("([+,-]{0,1})([0-9,.]{0,})[*,]{0,1}([x]{0,1})([^,]{0,1})([0-9,-]{0,})");

		public static Monomial Parse(string p)
		{
			var m = Pattern.Match(p);
			if (m.Groups[1].Value.Length > 1) throw new FormatException();
			if (m.Groups[4].Value.Length == 0 && m.Groups[5].Value.Length > 0) throw new FormatException();
			if (m.Groups[4].Value.Length > 0 && m.Groups[5].Value.Length == 0) throw new FormatException();
			var f = m.Groups[1].Length == 1 ? Double.Parse(m.Groups[1] + "1") : 1;
			var c = m.Groups[2].Length > 0 ? Double.Parse(m.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture) : 1;
			var d = m.Groups[5].Length > 0 ? Double.Parse(m.Groups[5].Value, System.Globalization.CultureInfo.InvariantCulture) : 1;
			if (m.Groups[3].Length == 0) d = 0;
			return new Monomial(f * c, d);
		}

		public static bool TryParse(string p, out Monomial result)
		{
			try
			{
				result = Parse(p);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}

		public override string ToString()
		{
			return $"{Coefficient}x^{Degree}";
		}

		public int CompareTo(object obj)
		{
			return Degree.CompareTo(((Monomial)obj).Degree);
		}

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Monomial)) return false;
			var o = (Monomial)obj;
			return Coefficient == o.Coefficient && Degree == o.Degree;
		}

		public bool Equals(Monomial m, double epscoef, double epsdegree = 0)
		{
			if (epsdegree == 0)
			{
				return Math.Abs(Coefficient - m.Coefficient) <= epscoef && Degree == m.Degree;
			}
			return Math.Abs(Coefficient - m.Coefficient) <= epscoef && Math.Abs(Degree - m.Degree) <= epsdegree;
		}

		public override int GetHashCode()
		{
			return Coefficient.GetHashCode() ^ Degree.GetHashCode();
		}
	}
}
