using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified collection's item count is equal to or greater than <paramref name="expectedMinItemCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMinItemCount">The minimum number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the item count of <paramref name="source"/> is equal to or greater than <paramref name="expectedMinItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        /// <remarks>
        /// No exception is thrown in case a negative <paramref name="expectedMinItemCount"/> is passed.
        /// </remarks>
        public static bool CountsMin<TSource>(this IEnumerable<TSource> source, int expectedMinItemCount)
        {
            return CountsMin(source, expectedMinItemCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified collection contains exactly <paramref name="expectedMinItemCount"/> or more items satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMinItemCount">The minimum number of items satisfying the specified condition the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the item count of satisfying items is equal to or greater than <paramref name="expectedMinItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        ///   </exception>
        /// <remarks>
        /// No exception is thrown in case a negative <paramref name="expectedMinItemCount"/> is passed.
        /// </remarks>
        public static bool CountsMin<TSource>(this IEnumerable<TSource> source, int expectedMinItemCount, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (expectedMinItemCount < 0)
            {
                throw new ArgumentOutOfRangeException("expectedMinItemCount", "The expected item count must not be negative.");
            }

            if (expectedMinItemCount == 0)
            {
                return true;
            }

            int matchedItemsCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    matchedItemsCount++;
                }

                if (matchedItemsCount == expectedMinItemCount)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
