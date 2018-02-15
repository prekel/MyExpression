// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core.Tests
{
	public class MyRandom : Random
	{
		public MyRandom() : base()
		{

		}

		public MyRandom(int seed) : base(seed)
		{

		}

		public int NextSign() => Next() % 2 == 0 ? -1 : 1;

		public bool NextBool() => Next() % 2 == 0 ? true : false;
	}
}
