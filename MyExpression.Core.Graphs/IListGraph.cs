using System.Collections;
using System.Collections.Generic;

namespace MyExpression.Core.Graphs
{
	public interface IListGraph<T> : IGraph<T>, IList<T>
		where T : IVertex
	{
		
	}
}