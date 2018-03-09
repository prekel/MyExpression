// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class CodeDomEvalEquation : IEquation
	{
		public CodeDomEval Function { get; private set; }
		
		public Interval Interval { get; private set; }
		public double Step { get; private set; }
		public double Epsilon { get; private set; }

		public CodeDomEvalEquation(CodeDomEval function, Interval interval, double step = 1e-3, double eps = 1e-8)
		{
			Function = function;
			Interval = interval;
			Step = step;
			Epsilon = eps;
		}
		
		public IList<double> AllRoots => throw new NotImplementedException();

		public IList<double> Roots => throw new NotImplementedException();

		public void Solve()
		{
			throw new NotImplementedException();
		}
	}
}
