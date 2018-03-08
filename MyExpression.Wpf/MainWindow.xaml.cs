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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using MyExpression.Core;

namespace MyExpression.Wpf
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<GraphableFunction> Functions { get; private set; } = new ObservableCollection<GraphableFunction>();
		public GraphableFunction LastFunction { get; private set; }

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
			Graph.DrawFunctions();
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
					Functions.Add(LastFunction = new GraphableFunction(fp, df, Polynomial.Text, Functions));
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
							Dispatcher.Invoke(() => Functions.Add(LastFunction = new GraphableFunction(fp, df, tp.Item1, Functions)));
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
				//var last = LastFunction;
				var last = FunctionsListView.SelectedFunction;
				var p = (Polynomial)last.Function;
				var pe = new PolynomialEquation(p, Double.Parse(SolveEpsilon.Text));
				pe.Solve();
				last.GraphFunction.Roots = new List<double>(pe.Roots);
				last.GraphFunction.RootsBrush = RootsBrushComboBox.SelectedBrush;
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

				var fp = new Tangent(FunctionsListView.SelectedFunction.Function, Double.Parse(TangentX.Text), Double.Parse(TangentLim.Text));

				Func<double, double> f = fp.Calculate;
				Graph.Add(f, da, TangentBrushComboBox.SelectedBrush);
				var df = Graph.Functions.Last();
				Functions.Add(new GraphableFunction(fp, df, fp.ToString(), Functions));

				Graph.DrawFunctions();

				CountLabel.Content = Graph.Count;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}

		private void Graph_Loaded(object sender, RoutedEventArgs e)
		{
			InitGraph();
		}
	}
}
