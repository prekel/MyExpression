// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MyExpression.Core;

namespace MyExpression.Wpf
{
	public class GraphableFunction
	{
		public IFunctionX Function { get; set; }
		public FunctionGraph.DrawableFunction GraphFunction { get; set; }
		public string Formula { get; private set; }
		public int Number => ParentList.IndexOf(this);
		public string Type => Function.GetType().Name;
		public IList<GraphableFunction> ParentList { get; set; }

		public GraphableFunction(IFunctionX f, FunctionGraph.DrawableFunction gf, string formula, IList<GraphableFunction> parentList = null)
		{
			Function = f;
			GraphFunction = gf;
			Formula = formula;
			ParentList = parentList;
		}
	}
}
