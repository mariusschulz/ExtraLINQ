using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="elements"/>.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="elements">The elements to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="elements"/>.
        /// </returns>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, params TSource[] elements)
        {
            return Without(source, (IEnumerable<TSource>)elements);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="elements"/>.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="elements">The elements to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="elements"/>.
        /// </returns>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> elements)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(elements, "elements");

            return WithoutIterator(source, elements, EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="elements"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <param name="elements">The elements to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="elements"/>.
        /// </returns>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
            IEqualityComparer<TSource> equalityComparer, params TSource[] elements)
        {
            return Without(source, equalityComparer, (IEnumerable<TSource>)elements);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="elements"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="equalityComparer">The equality comparer to use.</param>
        /// <param name="elements">The elements to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="elements"/>.
        /// </returns>
        public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source,
            IEqualityComparer<TSource> equalityComparer, IEnumerable<TSource> elements)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(elements, "elements");
            ThrowIf.Argument.IsNull(equalityComparer, "equalityComparer");

            return WithoutIterator(source, elements, equalityComparer);
        }

        private static IEnumerable<TSource> WithoutIterator<TSource>(IEnumerable<TSource> source,
            IEnumerable<TSource> elementsToRemove, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> elementsToRemoveSet = new HashSet<TSource>(elementsToRemove, comparer);

            return source.Where(elem => !elementsToRemoveSet.Contains(elem));
        }
    }
}
