// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	/// <summary>
	/// Уравнение
	/// </summary>
	public interface IEquation
	{
		/// <summary>
		/// Все корни уравнения, включая кратные
		/// </summary>
		IList<double> AllRoots { get; }
		
		/// <summary>
		/// Корни уравнения
		/// </summary>
		IList<double> Roots { get; }
		
		/// <summary>
		/// Решает уравнение
		/// </summary>
		void Solve();
	}
}
