using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count.</param>
        /// <param name="elementCount">The number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="elementCount"/> elements; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasExactly<TSource>(this IEnumerable<TSource> source, int elementCount)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNegative(elementCount, "elementCount");

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null)
            {
                return sourceCollection.Count == elementCount;
            }

            return HasExactly(source, elementCount, _ => true);
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of elements satisfying the specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying elements.</param>
        /// <param name="elementCount">The number of matching elements the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="elementCount"/> elements satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasExactly<TSource>(this IEnumerable<TSource> source, int elementCount, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");
            ThrowIf.Argument.IsNegative(elementCount, "elementCount");

            ICollection sourceCollection = source as ICollection;

            if (sourceCollection != null && sourceCollection.Count < elementCount)
            {
                // If the collection doesn't even contain as many elements
                // as expected to match the predicate, we can stop here
                return false;
            }

            int matches = 0;

            foreach (var element in source.Where(predicate))
            {
                ++matches;

                if (matches > elementCount)
                {
                    return false;
                }
            }

            return matches == elementCount;
        }
    }
}
