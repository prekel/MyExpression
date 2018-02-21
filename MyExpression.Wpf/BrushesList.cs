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
using System.Reflection;

using MyExpression.Core;

namespace MyExpression.Wpf
{
	public class BrushesList : ComboBox
	{
		private static List<(string, SolidColorBrush)> Brushes { get; set; } = new List<(string, SolidColorBrush)>(141);
		static BrushesList()
		{
			var t = typeof(Brushes);
			var p = t.GetProperties();
			foreach (var i in p)
			{
				Brushes.Add((i.Name, (SolidColorBrush)i.GetValue(null)));
			}
		}
		public BrushesList() : base()
		{
		}
	}
}
