using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static string Partition<TSource>(this IEnumerable<TSource> values, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(values, "values");

            throw new NotImplementedException();
        }
    }
}
