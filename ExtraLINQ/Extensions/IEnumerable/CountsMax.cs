using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified collection's item count is equal to or lower than <paramref name="expectedMaxItemCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMaxItemCount">The maximum number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the item count of <paramref name="source"/> is equal to or lower than <paramref name="expectedMaxItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expectedMaxItemCount"/> is negative.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static bool CountsMax<TSource>(this IEnumerable<TSource> source, int expectedMaxItemCount)
        {
            return CountsMax(source, expectedMaxItemCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified collection contains exactly <paramref name="expectedMaxItemCount"/> or less items satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMaxItemCount">The maximum number of items satisfying the specified condition the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the item count of satisfying items is equal to or less than <paramref name="expectedMaxItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        /// </exception>
        public static bool CountsMax<TSource>(this IEnumerable<TSource> source, int expectedMaxItemCount, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (expectedMaxItemCount < 0)
            {
                throw new ArgumentOutOfRangeException("expectedMaxItemCount", "The expected item count must not be negative.");
            }

            int matchedItemsCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    matchedItemsCount++;
                }

                if (matchedItemsCount > expectedMaxItemCount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
