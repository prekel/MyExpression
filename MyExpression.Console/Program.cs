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
			System.Console.Write("Equation: ");
			var s = System.Console.ReadLine();
			var p = Polynomial.Parse(s);
			var e = new PolynomialEquation(p, 1e-8);
			e.Solve();
			//System.Console.WriteLine(String.Join(" ", e.AllRoots));
			System.Console.WriteLine("   Roots: " + String.Join(" ", e.Roots));
		}
	}
}
