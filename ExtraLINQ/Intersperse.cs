using System;
using System.Collections.Generic;

namespace ExtraLINQ
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns all elements of the specified collection separated by the specified separator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to intersperse the separator into.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>The collection containing the interspersed separator.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> Intersperse<TSource>(this IEnumerable<TSource> source, TSource separator)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            bool isFirst = true;
            foreach (TSource item in source)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    yield return separator;
                }

                yield return item;
            }
        }
    }
}
