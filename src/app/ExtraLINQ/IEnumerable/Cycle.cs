using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Turns a finite sequence into a circular one, or equivalently,
        /// repeats the original sequence indefinitely.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to cycle through.</param>
        /// <returns>An infinite sequence cycling through the given sequence.</returns>
        public static IEnumerable<TSource> Cycle<TSource>(this IEnumerable<TSource> source)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return EnumerateForever(source);
        }

        private static IEnumerable<TSource> EnumerateForever<TSource>(IEnumerable<TSource> source)
        {
            var enumeratedItems = new List<TSource>();

            foreach (TSource item in source)
            {
                yield return item;
                enumeratedItems.Add(item);
            }

            if (!enumeratedItems.Any())
            {
                yield break;
            }

            int index = 0;
            while (true)
            {
                yield return enumeratedItems[index];
                index = (index + 1) % enumeratedItems.Count;
            }
        }
    }
}
