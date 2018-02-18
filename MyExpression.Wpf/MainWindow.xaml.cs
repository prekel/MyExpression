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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Graph.Functions.Count == 0)
			{
				var p = Core.Polynomial.Parse(Polynomial.Text);
				Graph.Functions.Add(p.Calculate);
			}
			else
			{
				var p = Core.Polynomial.Parse(Polynomial.Text);
				Graph.Functions[0] = p.Calculate;
			}

			Graph.DefinitionArea = new Interval(Double.Parse(DefinitionAreaLeft.Text), Double.Parse(DefinitionAreaRight.Text));
			Graph.Scale = new Point(Double.Parse(ScaleX.Text), Double.Parse(ScaleY.Text));
			Graph.Step = Double.Parse(Step.Text);
			Graph.Offset = new Point(Double.Parse(OffsetX.Text), Double.Parse(OffsetY.Text));
			Graph.CellsIntervalX = new Interval(Double.Parse(CellsIntervalXLeft.Text), Double.Parse(CellsIntervalXRight.Text));
			Graph.CellsIntervalY = new Interval(Double.Parse(CellsIntervalYLeft.Text), Double.Parse(CellsIntervalYRight.Text));
			Graph.CellsStep = new Point(Double.Parse(CellsStepX.Text), Double.Parse(CellsStepY.Text));

			Graph.Clear();
			Graph.ResetTranslateTransform();
			Graph.DrawCells();
			Graph.DrawAxis();
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			Graph.DrawFunction();
		}
	}
}
