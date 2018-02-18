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
			double f1(double x) => x * x * x - 2 * x * x - x + 3;
			double f2(double x) => x * x - 1;
			double f3(double x) => x * x * x - 2 * x * x - x + 3 - x * x + 1;
			if (Graph.Functions.Count == 0)
			{
				Graph.Functions.Add(f1);
				Graph.Functions.Add(f2);
				Graph.Functions.Add(f3);
			}
			//Graph.Functions[0] = f;

			Graph.DefinitionArea = new Interval(-20, 20);
			Graph.Scale = new Point(40, 40);
			Graph.Step = 0.01;
			Graph.Offset = new Point(Double.Parse(OffsetX.Text), Double.Parse(OffsetY.Text));
			Graph.CellsIntervalX = new Interval(-10, 10);
			Graph.CellsIntervalY = new Interval(-10, 10);
			Graph.CellsStep = new Point(40, 40);

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
