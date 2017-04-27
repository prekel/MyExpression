using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class Polynomial : List<Monomial>
    {
        public double Evaluate(double x)
        {
            //var c = 0d;
            //foreach (var i in this)
            //{
            //    c += i.Evaluate(x);
            //}
            return this.Sum(m => m.Evaluate(x));
            //return c;
        }

        public double this[double x]
        {
            get { return Evaluate(x); }
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
                if (i.Ratio > 0 && s != "")
                {
                    s += "+";
                }
                s += i.ToString();
            }
            return s;
        }
    }
}
