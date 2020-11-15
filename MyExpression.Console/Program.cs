using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

using MyExpression.Core;

namespace MyExpression.Console
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var arglist = new List<string>(args);

			var eps = 1e-6;
			if (arglist.Contains("-eps"))
			{
				eps = Double.Parse(arglist[arglist.IndexOf("-eps") + 1]);
			}

			var peqstr = arglist.Last();
			var peq = new PolynomialEquation(Polynomial.Parse(peqstr), eps);
			peq.Solve();
			var r = arglist.Contains("--allroots") || arglist.Contains("-a") ? peq.AllRoots : peq.Roots;
			foreach (var i in r)
			{
				WriteLine(i);
			}
			

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

			//var f1 = new CodeDomEval("pow(x, 3) - 2 * pow(x, 2) - x + 2");
			//var f2 = new CodeDomEval("sin(x)");
			//var a1 = f1.Calculate(3);
			//var a2 = f2.Calculate(Math.PI / 2);

			//var e = new FunctionEquation(Math.Sin, new Interval(-5, 5), eps: 1e-15);
			//e.Solve();
			//var r = e.AllRoots;
		}
	}
}
