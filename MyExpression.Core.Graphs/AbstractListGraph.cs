using System.Collections.Generic;

namespace MyExpression.Core.Graphs
{
	public class AbstractListGraph<T> : List<T>, IListGraph<T>
		where T : IVertex
	{
		public AbstractListGraph(int cap) : base(cap)
		{

		}
	}
}