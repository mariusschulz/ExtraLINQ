using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return !source.Any();
        }

        public static bool None<TSource>(this IEnumerable source, Func<TSource, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
