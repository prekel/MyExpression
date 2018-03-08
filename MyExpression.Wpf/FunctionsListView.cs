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
	public class FunctionsListView : ListView, IEnumerable<IFunctionX>
	{
		public FunctionsListView() : base()
		{
			var gv = new GridView();
			gv.Columns.Add(new GridViewColumn() { Header = "№", DisplayMemberBinding = new Binding("Number") });
			gv.Columns.Add(new GridViewColumn() { Header = "Формула", DisplayMemberBinding= new Binding("Formula")});
			gv.Columns.Add(new GridViewColumn() { Header = "Тип", DisplayMemberBinding = new Binding("Type") });
			View = gv;
			SizeChanged += FunctionsListView_SizeChanged;
			Loaded += FunctionsListView_Loaded;
		}

		private void FunctionsListView_Loaded(object sender, RoutedEventArgs e)
		{
			var gv = (GridView)View;
			var a = 20;
			var b = 95;
			gv.Columns[0].Width = a;
			gv.Columns[2].Width = b;
			gv.Columns[1].Width = ActualWidth - a - b - 10;
		}

		private void FunctionsListView_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			var gv = (GridView)View;
			gv.Columns[1].Width += e.NewSize.Width - e.PreviousSize.Width;
		}

		public IEnumerator<IFunctionX> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
