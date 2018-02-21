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
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
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

				Graph.ClearAll();
				Graph.ResetTranslateTransform();
				Graph.DrawCells();
				Graph.DrawAxis();

				CountLabel.Content = Graph.Count;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}

		private void DrawButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Graph.DrawFunction();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var da = new Interval(Double.Parse(DefinitionAreaLeft.Text), Double.Parse(DefinitionAreaRight.Text));
				Func<double, double> f;
				try
				{
					var p = Core.Polynomial.Parse(Polynomial.Text);
					f = p.Calculate;
				}
				catch
				{
					var ev = new CodeDomEval(Polynomial.Text);
					f = ev.Eval;
				}
				Graph.Add(f, da, GraphBrushComboBox.SelectedBrush);
				CountLabel.Content = Graph.Count;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Graph.Clear();
				CountLabel.Content = Graph.Count;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace, ex.Message);
			}
		}
	}
}
