using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns all elements of the specified collection separated by the given separator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to intersperse the separator into.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>The collection containing the interspersed separator.</returns>
        /// <example>
        /// <code>
        /// int[] numbers = { 1, 2, 3, 4 };
        /// IEnumerable&lt;int&gt; interspersed = numbers.Intersperse(0);
        /// </code>
        /// The <c>interspersed</c> variable, when iterated over, will yield the sequence 1, 0, 2, 0, 3, 0, 4.
        /// </example>
        public static IEnumerable<TSource> Intersperse<TSource>(this IEnumerable<TSource> source, TSource separator)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return IntersperseIterator(source, separator);
        }

        private static IEnumerable<TSource> IntersperseIterator<TSource>(IEnumerable<TSource> source, TSource separator)
        {
            bool isFirst = true;

            foreach (TSource item in source)
            {
                if (!isFirst)
                {
                    yield return separator;
                }
                else
                {
                    isFirst = false;
                }

                yield return item;
            }
        }
    }
}
