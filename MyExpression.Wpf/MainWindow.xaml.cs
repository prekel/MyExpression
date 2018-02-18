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
			//Graph.Function = x => x * x * x - 2 * x * x - x + 3;
			Graph.Function = x => Math.Sqrt(x);
			Graph.DefinitionArea = new Interval(0, 20);
			Graph.ScaleX = 40;
			Graph.ScaleY = 40;
			Graph.Step = 0.01;
			Graph.Offset = new Point(Double.Parse(OffsetX.Text), Double.Parse(OffsetY.Text));
			Graph.CellsIntervalX = new Interval(-40, 40);
			Graph.CellsIntervalY = new Interval(-40, 40);
			Graph.CellsStepX = 1;
			Graph.CellsStepY = 1;

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
