// Copyright (c) 2018 Vladislav Prekel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpression.Core
{
	public class Straight : LinearEquation, IFunctionX, IDerivativable
	{
		public IFunctionX Derivative => new Straight(0, A);

		public Straight() : base(0, 0) { }

		public Straight(double k, double m) : base(k, m) { }

		public Straight(PolynomialEquation p) : base(p) { }

		public double Calculate(double x) => A * x + B;
	}
}
