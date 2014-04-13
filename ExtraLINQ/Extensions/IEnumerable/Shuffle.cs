using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Enumerates the specified input sequence and returns a new sequence
        /// which contains all input elements in random order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to shuffle.</param>
        /// <returns>A shuffled sequence containing the elements of <paramref name="source"/>.</returns>
        /// <remarks>
        /// This method uses the Fisher-Yates shuffle.
        /// </remarks>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return ShuffleIterator(source, _random);
        }

        /// <summary>
        /// Enumerates the specified input sequence and returns a new sequence
        /// which contains all input elements in random order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to shuffle.</param>
        /// <param name="random">The random number generator used for shuffling.</param>
        /// <returns>A shuffled sequence containing the elements of <paramref name="source"/>.</returns>
        /// <remarks>
        /// This method uses the Fisher-Yates shuffle.
        /// </remarks>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source, Random random)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(random, "random");

            return ShuffleIterator(source, random);
        }

        private static IEnumerable<TSource> ShuffleIterator<TSource>(IEnumerable<TSource> source, Random random)
        {
            TSource[] items = source.ToArray();

            for (int n = 0; n < items.Length; n++)
            {
                int k = random.Next(n, items.Length);
                yield return items[k];

                items[k] = items[n];
            }
        }
    }
}
