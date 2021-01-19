using System;

namespace MyExpression.Core
{
    public interface IMonomial : IFunctionX, IDerivativable
    {
        public double Coefficient { get; }

        public double Degree { get; }
    }
}
