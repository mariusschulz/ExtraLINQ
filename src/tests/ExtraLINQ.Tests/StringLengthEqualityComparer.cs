using System.Collections.Generic;

namespace ExtraLinq.Tests
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
