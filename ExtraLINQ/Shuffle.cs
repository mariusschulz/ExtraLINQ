using System;
using System.Collections.Generic;
using ExtraLinq.Internals;

namespace ExtraLINQ
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Shuffles and returns the specified collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return an element from.</param>
        /// <returns>A shuffled collection containing the elements of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var shuffler = new CollectionShuffler<TSource>(source);

            return shuffler.ShuffleCollection();
        }

        /// <summary>
        /// Shuffles the specified source using the specified random number generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to shuffle.</param>
        /// <param name="randomNumberGenerator">The random number generator used to shuffle the specified collection.</param>
        /// <returns>A shuffled collection containing the elements of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="randomNumberGenerator"/> is null.</para>
        ///   </exception>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source, Random randomNumberGenerator)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (randomNumberGenerator == null)
            {
                throw new ArgumentNullException("randomNumberGenerator");
            }

            var shuffler = new CollectionShuffler<TSource>(source);

            return shuffler.ShuffleCollection(randomNumberGenerator);
        }
    }
}
