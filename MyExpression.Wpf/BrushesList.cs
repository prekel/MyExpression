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

		//public SolidColorBrush SelectedBrush => ((SolidColorBrush)((Rectangle)(((Grid)SelectedItem).Children[0])).Fill);
		public SolidColorBrush SelectedBrush
		{
			get => ((BrushGrid)SelectedItem).Brush;
			set
			{
				var c = 0;
				var dm = 0;
				foreach (var i in Brushes)
				{
					if (i.Item2.Equals(value))
					{
						dm = c;
					}
					c++;
				}
				SelectedIndex = dm;
			}
		}

		public class BrushGrid : Grid
		{
			public SolidColorBrush Brush { get; set; }
			public Rectangle Rectangle { get; set; }
			public Label Label { get; set; }

			public BrushGrid(string colorName, SolidColorBrush brush) : base()
			{
				Brush = brush;
				Rectangle = new Rectangle
				{
					Width = 29,
					Margin = new Thickness(1, 1, 0, 1),
					HorizontalAlignment = HorizontalAlignment.Left,
					Fill = brush,
					Stroke = System.Windows.Media.Brushes.Black
				};

				Label = new Label
				{
					Width = Width - 30,
					Content = colorName,
					Margin = new Thickness(30, -2, 0, 0),
					HorizontalAlignment = HorizontalAlignment.Left
				};

				Width = Width;
				Height = 25;
				Children.Add(Rectangle);
				Children.Add(Label);
			}
		}

		public BrushesList() : base()
		{
			var c = 0;
			var dm = 0;
			foreach (var i in Brushes)
			{
				var g = new BrushGrid(i.Item1, i.Item2);
				Items.Add(g);

				if (i.Item2.Equals(System.Windows.Media.Brushes.DarkMagenta))
				{
					dm = c;
				}
				c++;
			}
			//SelectedIndex = dm;
		}
	}
}
