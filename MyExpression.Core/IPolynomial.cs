// Copyright (c) 2021 Vladislav Prekel

using System.Collections.Generic;

namespace MyExpression.Core
{
    public interface IPolynomial : IFunctionX, IDerivativable, IEnumerable<IMonomial>
    {
        public IMonomial this[int degree] { get; }
    }
}
