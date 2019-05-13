using System.Collections;
using System.Collections.Generic;

namespace MyExpression.Core.Graphs
{
	public interface IVertex : IList<IVertex>
	{
		int Id { get; set; }
		IGraph<IVertex> Graph { get; set; }
	}
}