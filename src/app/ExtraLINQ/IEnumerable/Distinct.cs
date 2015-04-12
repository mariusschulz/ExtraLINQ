using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns distinct elements from the given sequence using the default equality comparer
        /// to compare values projected by <paramref name="projection"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the projected value for each element of the sequence.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="projection">The projection that is applied to each element to retrieve the value which is being compared.</param>
        /// <returns>A sequence of elements whose projected values are distinct.</returns>
        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> projection)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(projection, "projection");

            return DistinctIterator(source, projection, EqualityComparer<TResult>.Default);
        }

        /// <summary>
        /// Returns distinct elements from the given sequence using the specified equality comparer
        /// to compare values projected by <paramref name="projection"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the projected value for each element of the sequence.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="projection">The projection that is applied to each element to retrieve the value which is being compared.</param>
        /// <param name="equalityComparer">The equality comparer to use for comparing the projected values.</param>
        /// <returns>A sequence of elements whose projected values are considered distinct by the specified equality comparer.</returns>
        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> projection, IEqualityComparer<TResult> equalityComparer)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(projection, "projection");
            ThrowIf.Argument.IsNull(equalityComparer, "equalityComparer");

            return DistinctIterator(source, projection, equalityComparer);
        }

        private static IEnumerable<TSource> DistinctIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> projection, IEqualityComparer<TResult> equalityComparer)
        {
            var alreadySeenValues = new HashSet<TResult>(equalityComparer);

            foreach (var element in source)
            {
                var value = projection(element);

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
