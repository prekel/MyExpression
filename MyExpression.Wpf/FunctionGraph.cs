// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections;
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
	public class FunctionGraph : Canvas, IEnumerable<FunctionGraph.DrawableFunction>
	{
		public Point Offset { get; set; }

		public double Step { get; set; }

		public Point Scale { get; set; }

		private IList<DrawableFunction> Functions { get; set; } = new List<DrawableFunction>(1);

		public class DrawableFunction
		{
			public Func<double, double> Function { get; set; }
			public SolidColorBrush Brush { get; set; } = Brushes.DarkMagenta;
			public Interval DefinitionArea { get; set; }
			public bool IsDrawed { get; set; }
			public DrawableFunction(Func<double, double> f, Interval defarea, SolidColorBrush brush = null)
			{
				Function = f;
				DefinitionArea = new Interval(defarea.Left, defarea.Right);
				if (brush != null) Brush = brush;
			}
		}

		public int Count => Functions.Count;

		public DrawableFunction this[int index]
		{
			get => Functions[index];
			set => Functions[index] = value;
		}

		public void Add(DrawableFunction drawableFunction) => Functions.Add(drawableFunction);

		public void Add(Func<double, double> f, Interval defarea, SolidColorBrush brush = null) => Functions.Add(new DrawableFunction(f, defarea, brush));

		public void ResetTranslateTransform()
		{
			var tg1 = (TransformGroup)RenderTransform;
			var tt = new TranslateTransform((int)(ActualWidth / 2 + Offset.X * Scale.X) + 0.5, -(int)(ActualHeight / 2 + Offset.Y * Scale.Y) + 0.5);
			if (tg1.Children.Count == 3) tg1.Children.Add(tt);
			else tg1.Children[3] = tt;
		}

		public void DrawAxis()
		{
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
			foreach (var f in Functions)
			{
				if (f.IsDrawed) continue;
				f.IsDrawed = true;
				var l = new Polyline
				{
					Stroke = Brushes.DarkMagenta,
					StrokeThickness = 1
				};
				for (var i = f.DefinitionArea.Left; i <= f.DefinitionArea.Right; i += Step)
				{
					var p = new Point(i * Scale.X, f.Function(i) * Scale.Y);
					if (p.Y.Equals(Double.NaN) ||
						p.Y.Equals(Double.PositiveInfinity) ||
						p.Y.Equals(Double.NegativeInfinity) ||
						p.Y >= 1e5 ||
						p.Y <= -1e5)
					{
						if (l.Points.Count > 0)
						{
							Children.Add(l);
							l = new Polyline
							{
								Stroke = Brushes.DarkMagenta,
								StrokeThickness = 1
							};
						}
					}
					else
					{
						l.Points.Add(p);
					}
				}
				Children.Add(l);
			}
		}

		public Point CellsStep { get; set; }

		public Interval CellsIntervalX { get; set; }
		public Interval CellsIntervalY { get; set; }

		public void DrawCells()
		{
			for (var i = CellsStep.X; i <= CellsIntervalX.Right; i += CellsStep.X)
			{
				var l = new Line()
				{
					Stroke = Brushes.LightGray,
					StrokeThickness = 0.5,
					X1 = i * Scale.X,
					X2 = i * Scale.X,
					Y1 = -ActualHeight * 2,
					Y2 = ActualHeight * 2,
				};
				Children.Add(l);
			}
			for (var i = -CellsStep.X; i >= CellsIntervalX.Left; i -= CellsStep.X)
			{
				var l = new Line()
				{
					Stroke = Brushes.LightGray,
					StrokeThickness = 0.5,
					X1 = i * Scale.X,
					X2 = i * Scale.X,
					Y1 = -ActualHeight * 2,
					Y2 = ActualHeight * 2,
				};
				Children.Add(l);
			}
			for (var i = CellsStep.Y; i <= CellsIntervalY.Right; i += CellsStep.Y)
			{
				var l = new Line()
				{
					Stroke = Brushes.LightGray,
					StrokeThickness = 0.5,
					X1 = -ActualWidth * 2,
					X2 = ActualWidth * 2,
					Y1 = i * Scale.Y,
					Y2 = i * Scale.Y,
				};
				Children.Add(l);
			}
			for (var i = -CellsStep.Y; i >= CellsIntervalY.Left; i -= CellsStep.Y)
			{
				var l = new Line()
				{
					Stroke = Brushes.LightGray,
					StrokeThickness = 0.5,
					X1 = -ActualWidth * 2,
					X2 = ActualWidth * 2,
					Y1 = i * Scale.Y,
					Y2 = i * Scale.Y,
				};
				Children.Add(l);
			}
		}

		public void ClearAll()
		{
			Functions.Clear();
			Children.Clear();
		}

		public void Clear()
		{
			for (var i = 0; i < Children.Count; i++)
			{
				if (Children[i] is Polyline) Children.RemoveAt(i);
			}
		}

		public IEnumerator<DrawableFunction> GetEnumerator() => Functions.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => Functions.GetEnumerator();
	}
}
