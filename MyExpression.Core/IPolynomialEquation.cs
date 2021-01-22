// Copyright (c) 2021 Vladislav Prekel


namespace MyExpression.Core
{
    public interface IPolynomialEquation : IEquation
    {
        public double Epsilon { get; }
        public IPolynomial Polynomial { get; }
    }
}
