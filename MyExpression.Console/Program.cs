using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyExpression.Core;

namespace MyExpression.Console
{
	public class Program
	{
		private static void Main(string[] args)
		{
			//var poly = new Polynomial { new Monomial(2, 2), new Monomial(-1, 1), new Monomial(-2) };
			//var m0 = Monomial.Parse("+322x^-232");
			//var m1 = Monomial.Parse("-234x^231");
			//var m2 = Monomial.Parse("+x^231");
			//var m3 = Monomial.Parse("x^231");
			//var m4 = Monomial.Parse("-x^231");
			//var m5 = Monomial.Parse("-213x^411");
			//var m6 = Monomial.Parse("+213x^131");
			//var m7 = Monomial.Parse("213x^131");
			//var m8 = Monomial.Parse("1");
			//var m9 = Monomial.Parse("0");
			//var m10 = Monomial.Parse("1231x");
			//var m11 = Monomial.Parse("-1231x");
			//var m12 = Monomial.Parse("+213x");

			//var s = "1x^3-3x^2-4x+1";
			//var p = Polynomial.Parse(s);
			//var d = p.Derivative;

			//var g = p + d;

			//var p1 = Polynomial.Parse("x^2-4x+2");

			//var a = Double.PositiveInfinity;
			//var b = Double.NegativeInfinity;
			//var c1 = a * a;
			//var c2 = a * b;
			//var c3 = b * b;
			//var c4 = a * a;

			//var e = new PolynomialEquation(p);
			//e.Solve();

			//while (true)
			//{
			//	double inp;
			//	if (!double.TryParse(System.Console.ReadLine(), out inp))
			//		break;
			//	var c11 = p.Evaluate(inp);
			//	System.Console.WriteLine(c11);
			//}
			
			var s = System.Console.ReadLine();
			var p = Polynomial.Parse(s);
			var e = new PolynomialEquation(p, 1e-8);
			e.Solve();
			System.Console.WriteLine(String.Join(" ", e.AllRoots));
			System.Console.WriteLine(String.Join(" ", e.Roots));
		}
	}
}
