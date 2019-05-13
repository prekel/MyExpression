// Copyright (c) 2019 Vladislav Prekel

using System;
using System.Collections.Generic;

namespace MyExpression.Core.Graphs
{
	public class Graph : AbstractListGraph<Vertex>
	{
		public Graph(int cap) : base(cap)
		{
		}
	}
}
