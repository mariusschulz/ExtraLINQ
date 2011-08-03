using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ
{
    /// <summary>
    /// Provides handy extension methods for collections.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the specified collection contains exactly the specified number of items.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{TSource}"/> to count.</param>
        /// <param name="expectedItemCount">The number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items; otherwise, <c>false</c>.
        /// </returns>
        public static bool CountsExactly<TSource>(this IEnumerable<TSource> source, int expectedItemCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedItemCount < 0)
            {
                throw new ArgumentException("The expected item count must not be negative.", "expectedItemCount");
            }

            return source.Count() == expectedItemCount;
        }

        /// <summary>
        /// Determines whether the specified collection is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return !source.Any();
        }

        /// <summary>
        /// Determines whether the specified collection is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{TSource}"/> to check for null or emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                return true;
            }

            return !source.Any();
        }

        /// <summary>
        /// Determines whether none of the elements of a collection satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.IEnumerable{TSource}"/> to check for matches.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if no element satisfies the specified condition; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        /// </exception>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return !source.Any(predicate);
        }
    }
}
