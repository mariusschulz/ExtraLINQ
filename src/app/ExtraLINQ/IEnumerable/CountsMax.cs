using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified collection's item count is at most <paramref name="expectedMaxItemCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMaxItemCount">The maximum number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the item count of <paramref name="source"/> is equal to or lower than <paramref name="expectedMaxItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool CountsMax<TSource>(this IEnumerable<TSource> source, int expectedMaxItemCount)
        {
            return CountsMax(source, expectedMaxItemCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified collection contains at most <paramref name="expectedMaxItemCount"/> items satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMaxItemCount">The maximum number of items satisfying the specified condition the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the item count of satisfying items is equal to or less than <paramref name="expectedMaxItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool CountsMax<TSource>(this IEnumerable<TSource> source, int expectedMaxItemCount, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");
            ThrowIf.Argument.IsNegative(expectedMaxItemCount, "expectedMaxItemCount");

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null && sourceCollection.Count <= expectedMaxItemCount)
            {
                // If the collection doesn't contain more items
                // than expected to match the predicate, we can stop here
                return true;
            }

            int matches = 0;

            foreach (TSource item in source.Where(predicate))
            {
                matches++;

                if (matches > expectedMaxItemCount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
