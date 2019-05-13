// Copyright (c) 2019 Vladislav Prekel

namespace MyExpression.Core.Graphs
{
	public class Edge
	{
		public Vertex U { get; }
		public Vertex V { get; }

		public Edge(Vertex u, Vertex v)
		{
			U = u;
			V = v;
		}
	}
}