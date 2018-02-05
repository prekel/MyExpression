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
			var poly = new Polynomial { new Monomial(2, 2), new Monomial(-1, 1), new Monomial(-2) };
			while (true)
			{
				double inp;
				if (!double.TryParse(System.Console.ReadLine(), out inp))
					break;
				var c1 = poly[inp];
				System.Console.WriteLine(c1);
			}
		}
	}
}
