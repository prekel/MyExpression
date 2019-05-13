using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpression.Core.Graphs
{
	public abstract class AbstractVertex : List<IVertex>, IVertex
	{
		public int Id { get; set; }
		public IGraph<IVertex> Graph { get; set; }
	}
}
