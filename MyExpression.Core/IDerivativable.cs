// Copyright (c) 2021 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public interface IDerivativable
	{
		IFunctionX Derivative { get; }
		//double Calculate(double x);
	}
}
