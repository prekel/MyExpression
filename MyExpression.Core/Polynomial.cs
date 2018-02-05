using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	//[System.Diagnostics.DebuggerDisplay("{ToString()}")]
	public class Polynomial : MySortedList<Monomial>
	{
		public double Evaluate(double x)
		{
			//var c = 0d;
			//foreach (var i in this)
			//{
			//	c += i.Evaluate(x);
			//}
			return this.Sum(m => m.Evaluate(x));
			//return c;
		}

		public double this[double x] => Evaluate(x);

		//public int Rank => this.Max(m => m.Count);

		public Polynomial() {
			
		}

		public Polynomial Derivative
		{
			get 
			{
				var d = new Polynomial();
				foreach (var i in this)
				{
					d.Add(i.Derivative);
				}
				return d;
			}
		}

		public override string ToString()
		{
			var s = "";
			foreach (var i in this)
			{
				if (i.Coefficient > 0 && s != "")
				{
					s += "+";
				}
				s += i.ToString();
			}
			return s;
		}

		public SquareEquation ToSquareEquation()
		{
			if (Count > 3)
				throw new InvalidOperationException();
			return new SquareEquation(base[0].Coefficient, base[1].Coefficient, base[2].Coefficient);
		}
	}
}
