using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Iterates over the given sequence and repeatedly returns <paramref name="take"/> elements
        /// and skips <paramref name="skip"/> elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to iterate over.</param>
        /// <param name="take">The number of elements to take at each step.</param>
        /// <param name="skip">The number of elements to skip after each step.</param>
        /// <returns>A sequence containing the taken elements.</returns>
        public static IEnumerable<TSource> TakeSkip<TSource>(this IEnumerable<TSource> source, int take, int skip)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNegative(take, "take");
            ThrowIf.Argument.IsNegative(skip, "skip");

            return TakeSkipIterator(source, take, skip);
        }

        private static IEnumerable<TSource> TakeSkipIterator<TSource>(IEnumerable<TSource> source, int take, int skip)
        {
            var enumerator = source.GetEnumerator();

            while (true)
            {
                for (int i = 0; i < take; i++)
                {
                    if (!enumerator.MoveNext())
                        yield break;

                    yield return enumerator.Current;
                }

                for (int i = 0; i < skip; i++)
                {
                    if (!enumerator.MoveNext())
                        yield break;
                }
            }
        }
    }
}
