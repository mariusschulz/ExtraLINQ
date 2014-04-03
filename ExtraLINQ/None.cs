using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether none of the elements of a collection satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for matches.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if no element satisfies the specified condition; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
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
