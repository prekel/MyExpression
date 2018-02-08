using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	interface IEquation
	{
		IList<double> Roots { get; }
	}
}
