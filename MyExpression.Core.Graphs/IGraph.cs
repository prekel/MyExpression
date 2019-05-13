using System.Collections;
using System.Collections.Generic;

namespace MyExpression.Core.Graphs
{
	public interface IGraph<T> : IEnumerable<T>
		where T : IVertex
	{

	}
}