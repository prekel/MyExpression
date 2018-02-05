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
			var m0 = Monomial.Parse("+322x^-232");
			var m1 = Monomial.Parse("-234x^231");
			var m2 = Monomial.Parse("+x^231");
			var m3 = Monomial.Parse("x^231");
			var m4 = Monomial.Parse("-x^231");
			var m5 = Monomial.Parse("-213x^411");
			var m6 = Monomial.Parse("+213x^131");
			var m7 = Monomial.Parse("213x^131");
			var m8 = Monomial.Parse("1");
			var m9 = Monomial.Parse("0");
			var m10 = Monomial.Parse("1231x");
			var m11 = Monomial.Parse("-1231x");
			var m12 = Monomial.Parse("+213x");

			var s = "x^​3-​3*​x^​2-​4*​x+​1";

			//while (true)
			//{
			//	double inp;
			//	if (!double.TryParse(System.Console.ReadLine(), out inp))
			//		break;
			//	var c1 = poly[inp];
			//	System.Console.WriteLine(c1);
			//}
		}
	}
}
