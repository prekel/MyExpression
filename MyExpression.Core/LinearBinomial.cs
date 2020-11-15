// Copyright (c) 2018 Vladislav Prekel

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	/// <summary>
	/// Линейный двучлен x - x0
	/// </summary>
	public class LinearBinomial : IFunctionX, IDerivativable
	{
		/// <summary>
		/// Свободный член
		/// </summary>
		public double X0 { get; set; }

		/// <summary>
		/// Создаёт линейный двучлен
		/// </summary>
		/// <param name="x0">Свободный член</param>
		public LinearBinomial(double x0 = 0)
		{
			X0 = x0;
		}

		public double Calculate(double x)
		{
			return x - X0;
		}

		public IFunctionX Derivative => new Straight(0, 1);
		
		public static Polynomial operator *(Polynomial p, LinearBinomial l)
		{
			return new Monomial() * p - l.X0 * p;
		}
		
		public static Polynomial operator *(LinearBinomial l,  Polynomial p)
		{
			return p * l;
		}
		
		public static Polynomial operator *(LinearBinomial a, LinearBinomial b)
		{
			return new Polynomial(1, -(a.X0 + b.X0), a.X0 * b.X0);
		}
	}
}