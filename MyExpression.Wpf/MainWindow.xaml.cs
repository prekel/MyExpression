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
		public IList<GraphableFunction> Functions { get; private set; } = new List<GraphableFunction>();
		public GraphableFunction LastFunction { get; private set; }

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
			InitGraph();
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

		private void InitGraph()
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

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			InitGraph();
		}

		//private void DrawButton_Click(object sender, RoutedEventArgs e)
		//{
		//	try
		//	{
		//		if (Graph.Children.Count == 0)
		//		{
		//			throw new ApplicationException("Непроинициализировано");
		//		}
		//		Graph.DrawFunctions();
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
		//	}
		//}

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
					Functions.Add(LastFunction = new GraphableFunction(fp, df));
					CountLabel.Content = Graph.Count;
					Cursor = null;
					Graph.DrawFunctions();
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
							Functions.Add(LastFunction = new GraphableFunction(fp, df));
							Dispatcher.Invoke(() => CountLabel.Content = Graph.Count);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
						}
						finally
						{
							Dispatcher.Invoke(() => Cursor = null);
							Dispatcher.Invoke(() => Graph.DrawFunctions());
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
				LastFunction = null;
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
				//var last = Functions.Last();
				var last = LastFunction;
				var p = (Polynomial)last.Function;
				var pe = new PolynomialEquation(p, Double.Parse(SolveEpsilon.Text));
				pe.Solve();
				last.GraphFunction.Roots = new List<double>(pe.Roots);
				RootsTextBox.Text = String.Join("\n", MultipleRootsCheckBox.IsChecked.Value ? pe.AllRoots : pe.Roots);
				Graph.DrawRoots();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void TangentAddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var da = new Interval(Double.Parse(DefinitionAreaLeft.Text), Double.Parse(DefinitionAreaRight.Text));

				double k, m;
				if (LastFunction.Function is Polynomial p)
				{
					var d = p.Derivative;
					var x0 = Double.Parse(TangentX.Text);
					k = d.Calculate(x0);
					m = p.Calculate(x0) - d.Calculate(x0) * x0;
				}
				else
				{
					var fn = LastFunction.Function;
					var x0 = Double.Parse(TangentX.Text);
					var x1 = x0 + Double.Parse(TangentLim.Text);
					var y0 = fn.Calculate(x0);
					var y1 = fn.Calculate(x1);

					k = (y1 - y0) / (x1 - x0);
					m = y0 - k * x0;
				}

				var fp = new Polynomial(k, m);

				Func<double, double> f = fp.Calculate;
				Graph.Add(f, da, TangentBrushComboBox.SelectedBrush);
				var df = Graph.Functions.Last();
				Functions.Add(new GraphableFunction(fp, df));

				Graph.DrawFunctions();

				CountLabel.Content = Graph.Count;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}
	}
}
