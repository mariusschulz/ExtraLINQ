using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="items"/>.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified items from.</param>
        /// <param name="items">The items to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="items"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> items)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            return WithoutIterator(source, items, EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="items"/>.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified items from.</param>
        /// <param name="items">The items to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="items"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            return WithoutIterator(source, items, EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="items"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified items from.</param>
        /// <param name="items">The items to remove.</param>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="items"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <para><paramref name="source"/> is null.</para>
        ///   <para>- or - </para>
        ///   <para><paramref name="equalityComparer"/> is null.</para>
        ///   </exception>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
            IEqualityComparer<TSource> equalityComparer, params TSource[] items)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (equalityComparer == null)
            {
                throw new ArgumentNullException("equalityComparer");
            }

            return WithoutIterator(source, items, equalityComparer);
        }

        private static IEnumerable<TSource> WithoutIterator<TSource>(IEnumerable<TSource> source,
            IEnumerable<TSource> itemsToRemove, IEqualityComparer<TSource> comparer)
        {
            List<TSource> itemsToRemoveList = itemsToRemove.ToList();
            Func<TSource, bool> itemDoesNotOccurInRemovalList = item =>
                !itemsToRemoveList.Any(itemToRemove => comparer.Equals(item, itemToRemove));

            return source.Where(itemDoesNotOccurInRemovalList);
        }
    }
}
