using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> valueSelector)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return DistinctIterator(source, valueSelector);
        }

        private static IEnumerable<TSource> DistinctIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> valueSelector)
        {
            var alreadySeenValues = new HashSet<TResult>();

            foreach (var element in source)
            {
                var value = valueSelector(element);

                if (alreadySeenValues.Contains(value))
                {
                    continue;
                }

                yield return element;
                alreadySeenValues.Add(value);
            }
        }
    }
}
