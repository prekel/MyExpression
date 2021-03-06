﻿// Copyright (c) 2018 Vladislav Prekel

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
	public class FunctionsListView : ListView, IEnumerable<GraphableFunction>
	{
		public IList<GraphableFunction> Functions 
		{
			get => (IList<GraphableFunction>)ItemsSource;
			set => ItemsSource = value;
		}

		public GraphableFunction SelectedFunction
		{
			get => (GraphableFunction)SelectedItem;
			set => SelectedItem = value;
		}

		public FunctionsListView() : base()
		{
			var gv = new GridView();
			gv.Columns.Add(new GridViewColumn() { Header = "№", DisplayMemberBinding = new Binding("Number") });
			gv.Columns.Add(new GridViewColumn() { Header = "Формула", DisplayMemberBinding= new Binding("Formula")});
			gv.Columns.Add(new GridViewColumn() { Header = "Тип", DisplayMemberBinding = new Binding("Type") });
			View = gv;
			SelectionMode = SelectionMode.Single;
			SizeChanged += FunctionsListView_SizeChanged;
			Loaded += FunctionsListView_Loaded;
			MouseDoubleClick += (s, ea) => SelectedFunction = null;
		}

		public double MinFormulaColumnWidth { get; set; } = 100;

		private void FunctionsListView_Loaded(object sender, RoutedEventArgs e)
		{
			var gv = (GridView)View;
			var a = 0;
			var b = 90;
			gv.Columns[0].Width = a;
			gv.Columns[2].Width = b;
			gv.Columns[1].Width = Math.Max(ActualWidth - a - b - 10, MinFormulaColumnWidth);
		}

		private void FunctionsListView_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			var gv = (GridView)View;
			var inc = e.NewSize.Width - e.PreviousSize.Width;
			if (gv.Columns[1].Width + inc >= MinFormulaColumnWidth || inc > 0)
			{
				gv.Columns[1].Width += inc;
			}
		}

		public IEnumerator<GraphableFunction> GetEnumerator()
		{
			return Functions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Functions.GetEnumerator();
		}
	}
}
