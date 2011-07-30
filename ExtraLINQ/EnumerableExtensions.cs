using System;
using System.Collections;

namespace ExtraLINQ
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            throw new NotImplementedException();
        }

        public static bool None<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
