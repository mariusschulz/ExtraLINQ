using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of items.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count.</param>
        /// <param name="expectedItemCount">The number of items the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasExactly<TSource>(this IEnumerable<TSource> source, int expectedItemCount)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNegative(expectedItemCount, "expectedItemCount");

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null)
            {
                return sourceCollection.Count == expectedItemCount;
            }

            return HasExactly(source, expectedItemCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of items satisfying the specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying items.</param>
        /// <param name="expectedItemCount">The number of matching items the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasExactly<TSource>(this IEnumerable<TSource> source, int expectedItemCount, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");
            ThrowIf.Argument.IsNegative(expectedItemCount, "expectedItemCount");

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null && sourceCollection.Count < expectedItemCount)
            {
                // If the collection doesn't even contain as many items
                // as expected to match the predicate, we can stop here
                return false;
            }

            int matches = 0;

            foreach (var item in source.Where(predicate))
            {
                ++matches;

                if (matches > expectedItemCount)
                {
                    return false;
                }
            }

            return matches == expectedItemCount;
        }
    }
}
