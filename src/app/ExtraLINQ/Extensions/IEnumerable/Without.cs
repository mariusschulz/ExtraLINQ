using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
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
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            return Without(source, (IEnumerable<TSource>)items);
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
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> items)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(items, "items");

            return WithoutIterator(source, items, EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="items"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified items from.</param>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <param name="items">The items to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="items"/>.
        /// </returns>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
            IEqualityComparer<TSource> equalityComparer, params TSource[] items)
        {
            return Without(source, equalityComparer, (IEnumerable<TSource>)items);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="items"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified items from.</param>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <param name="items">The items to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="items"/>.
        /// </returns>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
            IEqualityComparer<TSource> equalityComparer, IEnumerable<TSource> items)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(items, "items");
            ThrowIf.Argument.IsNull(equalityComparer, "equalityComparer");

            return WithoutIterator(source, items, equalityComparer);
        }

        private static IEnumerable<TSource> WithoutIterator<TSource>(IEnumerable<TSource> source,
            IEnumerable<TSource> itemsToRemove, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> itemsToRemoveSet = new HashSet<TSource>(itemsToRemove, comparer);

            return source.Where(elem => !itemsToRemoveSet.Contains(elem));
        }
    }
}
