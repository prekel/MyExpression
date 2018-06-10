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
using System.Globalization;

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

		private void AddCodeDomEval()
		{
			Cursor = Cursors.Wait;
			var da = new Interval(Double.Parse(DefinitionAreaLeft.Text), Double.Parse(DefinitionAreaRight.Text));
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
					Dispatcher.Invoke(() => Graph.DrawFunctions());
					Dispatcher.Invoke(() => Cursor = null);
				}
			}, tpl.ToTuple());
			t.Start();
		}

		private void AddIFunctionX(IFunctionX fp, string formula)
		{
			Cursor = Cursors.Wait;
			var da = new Interval(Double.Parse(DefinitionAreaLeft.Text), Double.Parse(DefinitionAreaRight.Text));
			Func<double, double> f = fp.Calculate;
			Graph.Add(f, da, GraphBrushComboBox.SelectedBrush);
			var df = Graph.Functions.Last();
			Functions.Add(LastFunction = new GraphableFunction(fp, df, formula, Functions));
			CountLabel.Content = Graph.Count;
			Graph.DrawFunctions();
			Cursor = null;
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var t = (string)((ComboBoxItem)TypeComboBox.SelectedValue).Content;
				if (t == "Auto")
				{
					try
					{
						AddIFunctionX(Core.Polynomial.Parse(Polynomial.Text), Polynomial.Text);
					}
					catch
					{
						AddCodeDomEval();
					}
				}
				if (t == "Polynomial")
				{
					AddIFunctionX(Core.Polynomial.Parse(Polynomial.Text), Polynomial.Text);
				}
				if (t == "CodeDomEval")
				{
					AddCodeDomEval();
				}
				if (t == "Straight")
				{
					var fp = new Straight(Double.Parse(ABox.Text), Double.Parse(BBox.Text));
					AddIFunctionX(fp, fp.ToString());
				}
				if (t == "QuadraticParabola")
				{
					var fp = new QuadraticParabola(Double.Parse(ABox.Text), Double.Parse(BBox.Text), Double.Parse(CBox.Text));
					AddIFunctionX(fp, fp.ToString());
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
				var last = FunctionsListView.SelectedFunction;
				IEquation pe;
				if (last.Function is Polynomial p)
				{
					pe = new PolynomialEquation(p, Double.Parse(SolveEpsilon.Text));
				}
				else if (last.Function is CodeDomEval f)
				{
					var l = Double.Parse(SolveIntervalLeft.Text);
					var r = Double.Parse(SolveIntervalRight.Text);
					var step = Double.Parse(SolveStep.Text);
					var eps = Double.Parse(SolveEpsilon.Text);
					pe = new FunctionEquation(f, new Interval(l, r), step, eps);
				}
				else
				{
					pe = (IEquation)last.Function;
				}
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

		private void EraseButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var last = FunctionsListView.SelectedFunction;
				last.GraphFunction.Roots = null;
				Graph.DrawRoots();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void FunctionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (e.RemovedItems.Count == 1)
				{
					TangentLim.IsEnabled = true;
					SolveButton.IsEnabled = false;
					TangentAddButton.IsEnabled = false;
				}
				if (e.AddedItems.Count == 1)
				{
					TangentAddButton.IsEnabled = true;
					var g = (GraphableFunction)e.AddedItems[0];
					if (g.Function is CodeDomEval)
					{
						TangentLim.IsEnabled = true;
						SolveButton.IsEnabled = true;
						SolveStep.IsEnabled = true;
						SolveIntervalLeft.IsEnabled = true;
						SolveIntervalRight.IsEnabled = true;
						MultipleRootsCheckBox.IsEnabled = false;
						if (SolveEpsilon.Text == "1e-15") SolveEpsilon.Text = "1e-6";
					}
					else
					{
						if (g.Function is Polynomial)
						{
							SolveEpsilon.IsEnabled = true;
						}
						else
						{
							SolveEpsilon.IsEnabled = false;
						}
						TangentLim.IsEnabled = false;
						SolveButton.IsEnabled = true;
						SolveStep.IsEnabled = false;
						SolveIntervalLeft.IsEnabled = false;
						SolveIntervalRight.IsEnabled = false;
						MultipleRootsCheckBox.IsEnabled = true;
						if (SolveEpsilon.Text == "1e-6") SolveEpsilon.Text = "1e-15";
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (ABox == null) return;
				var t = (string)((ComboBoxItem)e.AddedItems[0]).Content;
				if (t == "Auto" || t == "Polynomial" || t == "CodeDomEval")
				{
					Polynomial.IsEnabled = true;
					ABox.IsEnabled = false;
					BBox.IsEnabled = false;
					CBox.IsEnabled = false;
				}
				if (t == "Straight")
				{
					Polynomial.IsEnabled = false;
					ABox.IsEnabled = true;
					BBox.IsEnabled = true;
					CBox.IsEnabled = false;
				}
				if (t == "QuadraticParabola")
				{
					Polynomial.IsEnabled = false;
					ABox.IsEnabled = true;
					BBox.IsEnabled = true;
					CBox.IsEnabled = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
			}
		}
	}
}