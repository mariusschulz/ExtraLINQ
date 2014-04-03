using System;
using System.Collections.Generic;
using System.Linq;
using ExtraLinq.Internals;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns a random element from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return an element from.</param>
        /// <returns>
        /// A random element from <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static TSource Random<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var elementsPicker = new CollectionElementsPicker<TSource>(source);

            return elementsPicker.PickRandomElement();
        }

        /// <summary>
        /// Returns the specified number of distinct random elements from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return the elements from.</param>
        /// <param name="randomElementsCount">The number of random elements to return.</param>
        /// <returns>
        /// A collection of distinct random elements from <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>   
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="randomElementsCount"/> is negative or greater than the collection's element count.
        ///   </exception>
        public static IEnumerable<TSource> Random<TSource>(this IEnumerable<TSource> source, int randomElementsCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            // Create array from source for further use to avoid multiple enumeration
            TSource[] sourceArray = source.ToArray();

            bool randomElementsIsOutOfRange = randomElementsCount < 0
                                              || randomElementsCount > sourceArray.Length;

            if (randomElementsIsOutOfRange)
            {
                throw new ArgumentOutOfRangeException("randomElementsCount");
            }

            var elementsPicker = new CollectionElementsPicker<TSource>(sourceArray);

            return elementsPicker.PickRandomElements(randomElementsCount);
        }

        /// <summary>
        /// Returns the specified number of random elements from <paramref name="source"/> using the specified random number generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return an element from.</param>
        /// <param name="randomElementsCount">The number of random elements to return.</param>
        /// <param name="randomNumberGenerator">The random number generator used to select random elements.</param>
        /// <returns>
        /// A collection of random elements from <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="randomNumberGenerator"/> is null.</para>
        ///   </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="randomElementsCount"/> is negative or greater than the collection's element count.
        /// </exception>
        public static IEnumerable<TSource> Random<TSource>(this IEnumerable<TSource> source, int randomElementsCount, Random randomNumberGenerator)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (randomNumberGenerator == null)
            {
                throw new ArgumentNullException("randomNumberGenerator");
            }

            // Create array from source for further use to avoid multiple enumeration
            TSource[] sourceArray = source.ToArray();

            bool randomElementsIsOutOfRange = randomElementsCount < 0
                                              || randomElementsCount > sourceArray.Length;

            if (randomElementsIsOutOfRange)
            {
                throw new ArgumentOutOfRangeException("randomElementsCount");
            }

            var elementsPicker = new CollectionElementsPicker<TSource>(sourceArray);

            return elementsPicker.PickRandomElements(randomElementsCount, randomNumberGenerator);
        }

        /// <summary>
        /// Returns a random element from <paramref name="source"/> using the specified random number generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return an element from.</param>
        /// <param name="randomNumberGenerator">The random number generator used to select a random element.</param>
        /// <returns>
        /// A random element from <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="randomNumberGenerator"/> is null.</para>
        ///   </exception>
        public static TSource Random<TSource>(this IEnumerable<TSource> source, Random randomNumberGenerator)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (randomNumberGenerator == null)
            {
                throw new ArgumentNullException("randomNumberGenerator");
            }

            var elementsPicker = new CollectionElementsPicker<TSource>(source);

            return elementsPicker.PickRandomElement(randomNumberGenerator);
        }
    }
}
