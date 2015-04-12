using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> valueSelector)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(valueSelector, "valueSelector");

            return DistinctIterator(source, valueSelector, EqualityComparer<TResult>.Default);
        }

        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> valueSelector, IEqualityComparer<TResult> equalityComparer)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(valueSelector, "valueSelector");
            ThrowIf.Argument.IsNull(equalityComparer, "equalityComparer");

            return DistinctIterator(source, valueSelector, equalityComparer);
        }

        private static IEnumerable<TSource> DistinctIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> valueSelector, IEqualityComparer<TResult> equalityComparer)
        {
            var alreadySeenValues = new HashSet<TResult>(equalityComparer);

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
