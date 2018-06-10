// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class Tangent : Straight
	{
		public Tangent(IFunctionX function, double x0, double lim)
		{
			double k, m;
			if (function is IDerivativable p)
			{
				var d = p.Derivative;
				k = d.Calculate(x0);
				m = function.Calculate(x0) - d.Calculate(x0) * x0;
			}
			else
			{
				var fn = function;
				var x1 = x0 + lim;
				var y0 = fn.Calculate(x0);
				var y1 = fn.Calculate(x1);

				k = (y1 - y0) / (x1 - x0);
				m = y0 - k * x0;
			}
			A = k;
			B = m;
		}
	}
}
