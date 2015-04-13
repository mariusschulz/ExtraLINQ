using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the given sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }
    }
}
