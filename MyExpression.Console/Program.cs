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
			//var p = Polynomial.Parse(s);
			//var e = new PolynomialEquation(p, 1e-8);
			//e.Solve();
			////System.Console.WriteLine(String.Join(" ", e.AllRoots));
			//System.Console.WriteLine("   Roots: " + String.Join(" ", e.Roots));

			var s = System.Console.ReadLine();
			var evaluator = new CodeDomEval(s.Replace("x", "args[0]"));
			//FunctionCompiler evaluator = new FunctionCompiler("args[0] * 4 - Math.Sin(args[1])");
			var args1 = new double[] { Double.Parse(System.Console.ReadLine()) };
			double? result = evaluator.Eval(new object[] { args1 });
			System.Console.WriteLine(result);
		}
	}
}
