using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public static bool HasAtLeast<TSource>(this IEnumerable<TSource> source, int expectedMinItemCount)
        {
            return HasAtLeast(source, expectedMinItemCount, _ => true);
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
        public static bool HasAtLeast<TSource>(this IEnumerable<TSource> source, int expectedMinItemCount, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");
            ThrowIf.Argument.IsNegative(expectedMinItemCount, "expectedMinItemCount");

            if (expectedMinItemCount == 0)
            {
                return true;
            }

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null && sourceCollection.Count < expectedMinItemCount)
            {
                // If the collection doesn't even contain as many items
                // as expected to match the predicate, we can stop here
                return false;
            }

            int matches = 0;

            foreach (TSource item in source.Where(predicate))
            {
                matches++;

                if (matches >= expectedMinItemCount)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
