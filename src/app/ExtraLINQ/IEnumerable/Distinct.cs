using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> valueSelector)
        {
            return source;
        }
    }
}
