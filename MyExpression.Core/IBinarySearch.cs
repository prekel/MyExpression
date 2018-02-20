// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	interface IBinarySearch
	{
		Interval StartInterval { get; }
		Interval Interval { get; }
		double Epsilon { get; }
		double Answer { get; }
		Func<double, double> Function { get; }
		double Solve();
	}
}
