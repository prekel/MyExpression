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
	public class FunctionGraph : Canvas
	{
		public Interval Interval { get; set; }

		public double Step { get; set; }

		public double ScaleX { get; set; }
		public double ScaleY { get; set; }

		public Func<double, double> Function { get; set; }

		public void DrawAxis()
		{
			Children.Clear();
			var tg1 = (TransformGroup)RenderTransform;
			var tt = new TranslateTransform((int)(ActualWidth / 2) + 0.5, -(int)(ActualHeight / 2) + 0.5);
			if (tg1.Children.Count == 3) tg1.Children.Add(tt);
			else tg1.Children[3] = tt;
			var l1 = new Line
			{
				X1 = 0,
				X2 = 0,
				Y1 = -ActualHeight / 2,
				Y2 = ActualHeight / 2,
				Stroke = Brushes.Black,
				StrokeThickness = 1
			};
			var l2 = new Line
			{
				X1 = -ActualWidth / 2,
				X2 = ActualWidth / 2,
				Y1 = 0,
				Y2 = 0,
				Stroke = Brushes.Black,
				StrokeThickness = 1
			};
			Children.Add(l1);
			Children.Add(l2);
		}

		public void DrawFunction()
		{
			//var l1 = new Line
			//{
			//	X1 = 0,
			//	Y1 = 500,
			//	X2 = 500,
			//	Y2 = 0,
			//	Stroke = Brushes.DarkMagenta,
			//	StrokeThickness = 1
			//};
			var l2 = new Line
			{
				X1 = 0,
				Y1 = 0,
				X2 = 100,
				Y2 = 200,
				Stroke = Brushes.DarkMagenta,
				StrokeThickness = 1
			};

			//Children.Add(l1);
			Children.Add(l2);
		}
	}
}
