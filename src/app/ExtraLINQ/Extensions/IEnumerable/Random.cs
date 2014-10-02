using System;
using System.Collections.Generic;
using System.Linq;

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
            ThrowIf.Argument.IsNull(source, "source");

            return PickRandomElement(source, _random);
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

            return PickRandomElements(sourceArray, randomElementsCount);
        }

        /// <summary>
        /// Returns a random element from <paramref name="source"/> using the specified random number generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return an element from.</param>
        /// <param name="random">The random number generator used to select a random element.</param>
        /// <returns>
        /// A random element from <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="random"/> is null.</para>
        ///   </exception>
        public static TSource Random<TSource>(this IEnumerable<TSource> source, Random random)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(random, "random");

            return PickRandomElement(source, random);
        }

        /// <summary>
        /// Returns the specified number of random elements from <paramref name="source"/> using the specified random number generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to return an element from.</param>
        /// <param name="randomElementsCount">The number of random elements to return.</param>
        /// <param name="random">The random number generator used to select random elements.</param>
        /// <returns>
        /// A collection of random elements from <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="random"/> is null.</para>
        ///   </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="randomElementsCount"/> is negative or greater than the collection's element count.
        /// </exception>
        public static IEnumerable<TSource> Random<TSource>(this IEnumerable<TSource> source, int randomElementsCount, Random random)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(random, "random");

            // Create array from source for further use to avoid multiple enumeration
            TSource[] sourceArray = source.ToArray();

            bool randomElementsIsOutOfRange = randomElementsCount < 0
                || randomElementsCount > sourceArray.Length;

            if (randomElementsIsOutOfRange)
            {
                throw new ArgumentOutOfRangeException("randomElementsCount");
            }

            return PickRandomElements(sourceArray, randomElementsCount, random);
        }

        private static TSource PickRandomElement<TSource>(IEnumerable<TSource> source, Random random)
        {
            var sourceArray = source as TSource[] ?? source.ToArray();
            int randomIndex = random.Next(sourceArray.Length);

            return sourceArray[randomIndex];
        }

        private static IEnumerable<TSource> PickRandomElements<TSource>(IEnumerable<TSource> source, int randomElementsCount)
        {
            return PickRandomElements(source, randomElementsCount, _random);
        }

        private static IEnumerable<TSource> PickRandomElements<TSource>(IEnumerable<TSource> source, int randomElementsCount, Random random)
        {
            return ShuffleCollection(source, random).Take(randomElementsCount);
        }

        public static IEnumerable<TSource> ShuffleCollection<TSource>(IEnumerable<TSource> source, Random random)
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
