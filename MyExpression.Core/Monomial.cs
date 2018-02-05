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
		//public string Var { get; set; }

		public double Evaluate(double x)
		{
			//if (Count == 0)
			//	return Ratio;
			//return Ratio * this.Sum(i => Math.Pow(x, i.Value));
			return Coefficient * Math.Pow(x, Degree);
		}

		public double this[double x] => Evaluate(x);

		//public Monomial(double ratio, string variable, double degree)
		//{
		//	Ratio = ratio;
		//	this[variable] = degree;
		//}

		//public Monomial(double ratio, double degree, string variable = "x")
		//{
		//	//Ratio = ratio;
		//	//this[variable] = degree;
		//}

		public Monomial(double ratio = 1, double degree = 1)
		{
			Coefficient = ratio;
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

		private static readonly Regex Pattern = new Regex("([+,-]{0,1})([0-9]{0,})[*,]{0,1}([x]{0,1})([^,]{0,1})([0-9,-]{0,})");

		public static Monomial Parse(string p)
		{
			var m = Pattern.Match(p);
			if (m.Groups[1].Value.Length > 1) throw new FormatException();
			if (m.Groups[4].Value.Length == 0 && m.Groups[5].Value.Length > 0) throw new FormatException();
			if (m.Groups[4].Value.Length > 0 && m.Groups[5].Value.Length == 0) throw new FormatException();
			var f = m.Groups[1].Length == 1 ? Double.Parse(m.Groups[1] + "1") : 1;
			var c = m.Groups[2].Length > 0 ? Double.Parse(m.Groups[2].Value) : 1;
			var d = m.Groups[5].Length > 0 ? Double.Parse(m.Groups[5].Value) : 1;
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
	}
}
