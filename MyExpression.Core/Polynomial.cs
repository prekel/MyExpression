using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class Polynomial : SortedList<Monomial, bool>
    {
        public double Evaluate(double x)
        {
            //var c = 0d;
            //foreach (var i in this)
            //{
            //    c += i.Evaluate(x);
            //}
            return Keys.Sum(m => m.Evaluate(x));
            //return c;
        }

        public double this[double x]
        {
            get { return Evaluate(x); }
        }

		public int Rank
		{
			get{
				return Keys.Max(m => m.Count);
			}
		}

        public Polynomial Derivative
        {
            get 
            {
                var d = new Polynomial();
                foreach (var i in Keys)
                {
                    d.Add(i.Derivative, true);
                }
                return d;
            }
        }

        public override string ToString()
        {
            var s = "";
            foreach (var i in Keys)
            {
                if (i.Ratio > 0 && s != "")
                {
                    s += "+";
                }
                s += i.ToString();
            }
            return s;
        }

		public SquareEquation ToSquareEquation(string x = "x")
		{
			if (Count > 3 || Rank > 1)
				throw new InvalidOperationException();
			return new SquareEquation(Keys[0].Ratio, Keys[1].Ratio, Keys[2].Ratio);
		}
    }
}
