using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
    public class Monomial
    {
        public double Ratio { get; set; }
        public double Degree { get; set; }
        public string Var { get; set; }

        public double Evaluate(double x)
        {
            return Ratio * Math.Pow(x, Degree);
        }

        public double this[double x]
        {
            get { return Evaluate(x); }
        }

        public Monomial(double ratio = 1, double degree = 1, string variable = "x")
        {
            Ratio = ratio;
            Degree = degree;
            Var = variable;
        }

        public Monomial Derivative
        {
            get { return new Monomial(Ratio * Degree, Degree - 1); }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}^{2}", Ratio, Var, Degree);
        }
    }
}
