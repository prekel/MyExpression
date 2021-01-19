// Copyright (c) 2021 Vladislav Prekel

namespace MyExpression.Core
{
    public interface IInterval
    {
        public double Left { get; }

        public double Right { get; }

        public bool IsInInterval(double x);
    }
}
