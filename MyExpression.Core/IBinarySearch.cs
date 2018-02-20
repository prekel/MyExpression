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
		double StartLeft { get; }
		double StartRight { get; }
		double Left { get; }
		double Right { get; }
		double Epsilon { get; }
		double Answer { get; }
		double Solve();
	}
}
