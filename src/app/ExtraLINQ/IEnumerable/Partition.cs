using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static string Partition<TSource>(this IEnumerable<TSource> values, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(values, "values");
            ThrowIf.Argument.IsNull(predicate, "predicate");

            throw new NotImplementedException();
        }
    }
}
