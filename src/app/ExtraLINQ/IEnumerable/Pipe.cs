using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Passes every element of the sequence to the specified action and returns it afterwards.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="action">The action to call for each element.</param>
        /// <returns>All elements of the source sequence.</returns>
        public static IEnumerable<TSource> Pipe<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(action, "action");

            return PipeImplementation(source, action);
        }

        private static IEnumerable<TSource> PipeImplementation<TSource>(IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
            {
                action(element);

                yield return element;
            }
        }

        /// <summary>
        /// Passes every element of the sequence to the specified action and returns it afterwards.
        /// The action additionally receives the index of each element as an argument.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="action">The action to call for each element.</param>
        /// <returns>All elements of the source sequence.</returns>
        public static IEnumerable<TSource> Pipe<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(action, "action");

            return PipeImplementation(source, action);
        }

        private static IEnumerable<TSource> PipeImplementation<TSource>(IEnumerable<TSource> source, Action<TSource, int> action)
        {
            int index = 0;

            foreach (var element in source)
            {
                action(element, index);
                ++index;

                yield return element;
            }
        }
    }
}
