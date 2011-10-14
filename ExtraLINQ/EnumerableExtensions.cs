using System;
using System.Collections.Generic;
using System.Linq;
using ExtraLinq.Internals;

namespace ExtraLinq
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
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count.</param>
        /// <param name="expectedItemCount">The number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
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

            int itemCount = 0;
            foreach (TSource item in source)
            {
                itemCount++;

                if (itemCount > expectedItemCount)
                {
                    return false;
                }
            }

            return itemCount == expectedItemCount;
        }

        /// <summary>
        /// Determines whether the specified collection contains exactly the specified number of items satisfying the specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying items.</param>
        /// <param name="expectedItemCount">The number of satisfying items the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="expectedItemCount"/> items satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expectedItemCount"/> is negative.</exception>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        /// </exception>
        public static bool CountsExactly<TSource>(this IEnumerable<TSource> source, int expectedItemCount, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedItemCount < 0)
            {
                throw new ArgumentException("The expected item count must not be negative.", "expectedItemCount");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return source.Count(predicate) == expectedItemCount;
        }

        /// <summary>
        /// Determines whether the specified collection's item count is equal to or lower than <paramref name="expectedMaxItemCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMaxItemCount">The maximum number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the item count of <paramref name="source"/> is equal to or lower than <paramref name="expectedMaxItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expectedMaxItemCount"/> is negative.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static bool CountsMax<TSource>(this IEnumerable<TSource> source, int expectedMaxItemCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (expectedMaxItemCount < 0)
            {
                throw new ArgumentOutOfRangeException("expectedMaxItemCount", "The expected item count must not be negative.");
            }

            int itemCount = 0;
            foreach (TSource item in source)
            {
                itemCount++;

                if (itemCount > expectedMaxItemCount)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified collection contains exactly <paramref name="expectedMaxItemCount"/> or less items satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMaxItemCount">The maximum number of items satisfying the specified condition the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the item count of satisfying items is equal to or less than <paramref name="expectedMaxItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        /// </exception>
        public static bool CountsMax<TSource>(this IEnumerable<TSource> source, int expectedMaxItemCount, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            int matchedItemsCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    matchedItemsCount++;
                }

                if (matchedItemsCount > expectedMaxItemCount)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified collection's item count is equal to or greater than <paramref name="expectedMinItemCount"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMinItemCount">The minimum number of items the specified collection is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the item count of <paramref name="source"/> is equal to or greater than <paramref name="expectedMinItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        /// <remarks>
        /// No exception is thrown in case a negative <paramref name="expectedMinItemCount"/> is passed.
        /// </remarks>
        public static bool CountsMin<TSource>(this IEnumerable<TSource> source, int expectedMinItemCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            
            int itemCount = 0;
            foreach (TSource item in source)
            {
                itemCount++;

                if (itemCount == expectedMinItemCount)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified collection contains exactly <paramref name="expectedMinItemCount"/> or more items satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose items to count.</param>
        /// <param name="expectedMinItemCount">The minimum number of items satisfying the specified condition the specified collection is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the item count of satisfying items is equal to or greater than <paramref name="expectedMinItemCount"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="predicate"/> is null.</para>
        ///   </exception>
        /// <remarks>
        /// No exception is thrown in case a negative <paramref name="expectedMinItemCount"/> is passed.
        /// </remarks>
        public static bool CountsMin<TSource>(this IEnumerable<TSource> source, int expectedMinItemCount, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            int matchedItemsCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    matchedItemsCount++;
                }

                if (matchedItemsCount == expectedMinItemCount)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the item at a specified index in a specified collection using a specified indexing strategy.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/>containing the item.</param>
        /// <param name="index">The item's index.</param>
        /// <param name="indexingStrategy">The <see cref="IndexingStrategy"/> to use.</param>
        /// <returns>The item at the specified index.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index, IndexingStrategy indexingStrategy)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            // We copy source to sourceList to avoid multiple enumerations of source.
            IList<TSource> sourceList = source.ToList();

            if (sourceList.IsEmpty())
            {
                throw new ArgumentException("source must not be empty.", "source");
            }

            switch (indexingStrategy)
            {
                case IndexingStrategy.Cyclic:
                {
                    index = CollectionIndexCalculator.CalculateCyclicIndex(index, sourceList.Count);

                    break;
                }
                case IndexingStrategy.Clamp:
                {
                    index = CollectionIndexCalculator.CalculateClampIndex(index, sourceList.Count);

                    break;
                }
            }

            return sourceList.ElementAt(index);
        }

        /// <summary>
        /// Determines whether the specified collection is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
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
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for null or emptiness.</param>
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

            CollectionElementsPicker<TSource> elementsPicker = new CollectionElementsPicker<TSource>(source);
            TSource randomItem = elementsPicker.PickRandomElement();

            return randomItem;
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

            CollectionElementsPicker<TSource> elementsPicker = new CollectionElementsPicker<TSource>(sourceArray);
            IEnumerable<TSource> randomElements = elementsPicker.PickRandomElements(randomElementsCount);

            return randomElements;
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

            CollectionElementsPicker<TSource> elementsPicker = new CollectionElementsPicker<TSource>(sourceArray);
            IEnumerable<TSource> randomElements = elementsPicker.PickRandomElements(randomElementsCount, randomNumberGenerator);

            return randomElements;
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

            CollectionElementsPicker<TSource> elementsPicker = new CollectionElementsPicker<TSource>(source);
            TSource randomItem = elementsPicker.PickRandomElement(randomNumberGenerator);

            return randomItem;
        }

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

            CollectionShuffler<TSource> shuffler = new CollectionShuffler<TSource>(source);
            IEnumerable<TSource> shuffledCollection = shuffler.ShuffleCollection();

            return shuffledCollection;
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

            CollectionShuffler<TSource> shuffler = new CollectionShuffler<TSource>(source);
            IEnumerable<TSource> shuffledCollection = shuffler.ShuffleCollection(randomNumberGenerator);

            return shuffledCollection;
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> except <paramref name="item"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/>containing the item.</param>
        /// <param name="item">The item to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="item"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.Where(element => !element.Equals(item));
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> except <paramref name="item"/> using the specified equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> containing the item.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="item"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="equalityComparer"/> is null.</para>
        ///   </exception>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, TSource item,
            IEqualityComparer<TSource> equalityComparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (equalityComparer == null)
            {
                throw new ArgumentNullException("equalityComparer");
            }

            return source.Where(element => !equalityComparer.Equals(element, item));
        }
    }
}
