namespace MyExpression.Core
{
    public interface ILinearEquation  : IEquation
    {
        public double A { get; }
        public double B { get; }

        public double X { get; }
    }
}
