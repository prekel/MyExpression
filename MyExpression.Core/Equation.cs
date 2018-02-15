// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	interface IEquation
	{
		IList<double> AllRoots { get; }
		IList<double> Roots { get; }
	}
}
