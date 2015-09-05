using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified sequence's element count is at most <paramref name="maxElementCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="maxElementCount">The maximum number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of <paramref name="source"/> is equal to or lower than <paramref name="maxElementCount"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAtMost<TSource>(this IEnumerable<TSource> source, int maxElementCount)
        {
            return HasAtMost(source, maxElementCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified sequence contains at most <paramref name="maxElementCount"/> elements satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="maxElementCount">The maximum number of elements satisfying the specified condition the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the element count of satisfying elements is equal to or less than <paramref name="maxElementCount"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAtMost<TSource>(this IEnumerable<TSource> source, int maxElementCount, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");
            ThrowIf.Argument.IsNegative(maxElementCount, "maxElementCount");

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null && sourceCollection.Count <= maxElementCount)
            {
                // If the collection doesn't contain more elements
                // than expected to match the predicate, we can stop here
                return true;
            }

            int matches = 0;

            foreach (TSource element in source.Where(predicate))
            {
                matches++;

                if (matches > maxElementCount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
