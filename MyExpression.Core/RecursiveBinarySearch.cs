// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class RecursiveBinarySearch : IBinarySearch
	{
		public double StartLeft { get; private set; }
		public double StartRight { get; private set; }
		public double Left { get; private set; }
		public double Right { get; private set; }
		public double Epsilon { get; private set; }
		public double Answer { get; private set; }

		public RecursiveBinarySearch(double l, double r, double eps)
		{
			StartLeft = l;
			Left = l;
			StartRight = r;
			Right = r;
			Epsilon = eps;
		}

		public double Solve()
		{
			throw new NotImplementedException();
		}
	}
}
