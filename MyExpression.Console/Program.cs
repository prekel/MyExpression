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
			//System.Console.Write("Equation: ");
			//var s = System.Console.ReadLine();
			//System.Console.Write(" Epsilon: ");
			//var eps = System.Console.ReadLine();
			//var p = Polynomial.Parse(s);
			//var e = new PolynomialEquation(p, Double.Parse(eps));
			//e.Solve();
			////System.Console.WriteLine(String.Join(" ", e.AllRoots));
			//System.Console.WriteLine("   Roots: " + String.Join(" ", e.Roots));
			//System.Console.ReadKey();

			//var s = System.Console.ReadLine();
			//var evaluator = new CodeDomEval(s);
			//while (true)
			//{
			//	if (!Double.TryParse(System.Console.ReadLine(), out double x)) break;
			//	var result = evaluator.Eval(x);
			//	System.Console.WriteLine(result);
			//}
			//System.Console.ReadKey();

			//System.Console.Write("Equation: ");
			//var s = System.Console.ReadLine();
			//var s = "3x^3-2x^2+x-1";
			//var p = Polynomial.Parse(s);
			//p.Compile();
			//while (true)
			//{
			//	if (!Double.TryParse(System.Console.ReadLine(), out double x)) break;
			//	var result = p.Evaluate(x);
			//	System.Console.WriteLine(result);
			//}

			//var s = "3x^3-2x^2+x-1";
			//var p = Polynomial.Parse(s);
			//var e = new PolynomialEquation(p, 1e-8);
			//e.Solve();
			////System.Console.WriteLine(String.Join(" ", e.AllRoots));
			//System.Console.WriteLine("   Roots: " + String.Join(" ", e.Roots));

			//var f = new CodeDomEval("pow(x, 3) - 2 * pow(x, 2) - x + 2");
			var f = new CodeDomEval("sin(x)");
			var e = new FunctionEquation(Math.Sin, new Interval(-5, 5));
			e.Solve();
			var r = e.AllRoots;
		}
	}
}
