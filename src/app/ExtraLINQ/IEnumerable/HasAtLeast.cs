using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified sequence's element count is equal to or greater than <paramref name="minElementCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="minElementCount">The minimum number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of <paramref name="source"/> is equal to or greater than <paramref name="minElementCount"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAtLeast<TSource>(this IEnumerable<TSource> source, int minElementCount)
        {
            return HasAtLeast(source, minElementCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly <paramref name="minElementCount"/> or more elements satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="minElementCount">The minimum number of elements satisfying the specified condition the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the element count of satisfying elements is equal to or greater than <paramref name="minElementCount"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAtLeast<TSource>(this IEnumerable<TSource> source, int minElementCount, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");
            ThrowIf.Argument.IsNegative(minElementCount, "minElementCount");

            if (minElementCount == 0)
            {
                return true;
            }

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null && sourceCollection.Count < minElementCount)
            {
                // If the collection doesn't even contain as many elements
                // as expected to match the predicate, we can stop here
                return false;
            }

            int matches = 0;

            foreach (TSource element in source.Where(predicate))
            {
                matches++;

                if (matches >= minElementCount)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
