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
using System.Threading;

using MyExpression.Core;

namespace MyExpression.Wpf
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public IList<GraphableFunction> Functions { get; set; } = new List<GraphableFunction>();

		public class GraphableFunction
		{
			public IFunctionX Function { get; set; }
			public FunctionGraph.DrawableFunction GraphFunction { get; set; }

			public GraphableFunction(IFunctionX f, FunctionGraph.DrawableFunction gf)
			{
				Function = f;
				GraphFunction = gf;
			}
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Graph_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			try
			{
				Graph.ReDrawBackground();
				Graph.DrawFunctions();
				Graph.DrawRoots();
			}
			catch
			{

			}
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Graph.Scale = new Point(Double.Parse(ScaleX.Text), Double.Parse(ScaleY.Text));
				Graph.Step = Double.Parse(Step.Text);
				Graph.Offset = new Point(Double.Parse(OffsetX.Text), Double.Parse(OffsetY.Text));
				Graph.CellsIntervalX = new Interval(Double.Parse(CellsIntervalXLeft.Text), Double.Parse(CellsIntervalXRight.Text));
				Graph.CellsIntervalY = new Interval(Double.Parse(CellsIntervalYLeft.Text), Double.Parse(CellsIntervalYRight.Text));
				Graph.CellsStep = new Point(Double.Parse(CellsStepX.Text), Double.Parse(CellsStepY.Text));

				Graph.ReDrawBackground();

				CountLabel.Content = Graph.Count;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void DrawButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (Graph.Children.Count == 0)
				{
					throw new ApplicationException("Непроинициализировано");
				}
				Graph.DrawFunctions();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Cursor = Cursors.Wait;
				var da = new Interval(Double.Parse(DefinitionAreaLeft.Text), Double.Parse(DefinitionAreaRight.Text));
				try
				{
					var fp = Core.Polynomial.Parse(Polynomial.Text);
					Func<double, double> f = fp.Calculate;
					Graph.Add(f, da, GraphBrushComboBox.SelectedBrush);
					var df = Graph.Functions.Last();
					Functions.Add(new GraphableFunction(fp, df));
					CountLabel.Content = Graph.Count;
					Cursor = null;
				}
				catch
				{
					var tpl = (Polynomial.Text, GraphBrushComboBox.SelectedBrush, da);
					var t = new Task((par) =>
					{
						try
						{
							var tp = (Tuple<string, SolidColorBrush, Interval>)par;
							var fp = new CodeDomEval(tp.Item1);
							Func<double, double> f = fp.Calculate;
							Graph.Add(f, tp.Item3, tp.Item2);
							var df = Graph.Functions.Last();
							Functions.Add(new GraphableFunction(fp, df));
							Dispatcher.Invoke(() => CountLabel.Content = Graph.Count);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
						}
						finally
						{
							Dispatcher.Invoke(() => Cursor = null);
						}
					}, tpl.ToTuple());
					t.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Functions.Clear();
				Graph.Clear();
				CountLabel.Content = Functions.Count;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void SolveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var last = Functions.Last();
				var p = (Polynomial)last.Function;
				var pe = new PolynomialEquation(p);
				pe.Solve();
				last.GraphFunction.Roots = new List<double>(pe.Roots);
				Graph.DrawRoots();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}
	}
}
