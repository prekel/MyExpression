// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class Interval
	{
		public double Left { get; set; }

		public double Right { get; set; }

		public Interval(double l, double r)
		{
			Left = l;
			Right = r;
		}

		public override string ToString()
		{
			return $"[{Left}; {Right}]";
		}
	}
}
