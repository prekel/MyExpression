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
			foreach (var i in Brushes)
			{
				var r = new Rectangle
				{
					Width = 30,
					Margin = new Thickness(5, 5, 0, 5),
					HorizontalAlignment = HorizontalAlignment.Left,
					Fill = i.Item2,
					Stroke = System.Windows.Media.Brushes.Black
				};

				var l = new Label
				{
					Content = i.Item1,
					Margin = new Thickness(40, 0, 0, 0),
					HorizontalAlignment = HorizontalAlignment.Left
				};

				var g = new Grid
				{
					Width = Width,
					Height = 30
				};
				g.Children.Add(r);
				g.Children.Add(l);

				Items.Add(g);
			}
		}
	}
}
