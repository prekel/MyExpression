// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core.Tests
{
	public class MyUniqueRandom : MyRandom
	{
		private int CurrentIndex { get; set; }
		private int[] Values { get; set; }

		private void Init(int minvalue, int maxvalue)
		{
			var len = Values.Length;
			for (var i = 0; i < len; i++)
			{
				Values[i] = i - minvalue;
			}

			for (var i = 0; i < len; i++)
			{
				var j = Next(len);
				var num = Values[i];
				Values[i] = Values[j];
				Values[j] = num;
			}
		}

		public MyUniqueRandom(int minvalue, int maxvalue) : base()
		{
			Init(minvalue, maxvalue);
		}

		public MyUniqueRandom(int minvalue, int maxvalue, int seed) : base(seed)
		{
			Init(minvalue, maxvalue);
		}

		public int NextUnique()
		{
			return Values[CurrentIndex++];
		}
	}
}