using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a given predicate
        /// and returns those values that don't match the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Those values that don't match the given predicate.</returns>
        public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");

            return source.Where(element => !predicate(element));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate
        /// and returns those values that don't match the given predicate.
        /// Each element's index is used in the logic of predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="predicate">
        /// A function to test each element for a condition;
        /// the second parameter of the functions represents the index of the source element.
        /// </param>
        /// <returns>Those values that don't match the given predicate.</returns>
        public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(predicate, "predicate");

            return source.Where((element, index) => !predicate(element, index));
        }
    }
}
