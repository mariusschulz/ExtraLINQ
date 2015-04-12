using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns a flattened sequence that contains the concatenation of all the nested sequences' elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of sequences to be flattened.</param>
        /// <returns>The concatenation of all the nested sequences' elements.</returns>
        public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return FlattenIterator(source);
        }

        private static IEnumerable<TSource> FlattenIterator<TSource>(IEnumerable<IEnumerable<TSource>> source)
        {
            foreach (IEnumerable<TSource> array in source)
            {
                foreach (TSource element in array)
                {
                    yield return element;
                }
            }
        }
    }
}
