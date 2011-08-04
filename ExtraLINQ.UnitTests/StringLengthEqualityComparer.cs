using System.Collections.Generic;

namespace ExtraLINQ.UnitTests
{
    internal class StringLengthEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return x.ToString().Length == y.ToString().Length;
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().Length;
        }
    }
}
