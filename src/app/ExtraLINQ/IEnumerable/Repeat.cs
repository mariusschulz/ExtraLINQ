using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Repeats the given sequence <paramref name="count"/> times.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to repeat.</param>
        /// <param name="count">The number of times to repeat the sequence.</param>
        /// <returns>A repeated version of the original sequence.</returns>
        public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source, int count)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNegative(count, "count");

            return RepeatIterator(source, count);
        }

        private static IEnumerable<TSource> RepeatIterator<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count == 0)
            {
                yield break;
            }

            var collection = source as ICollection<TSource>;

            var elementBuffer = collection == null
                ? new List<TSource>()
                : new List<TSource>(collection.Count);

            foreach (TSource element in source)
            {
                yield return element;

                // We add this element to a local element buffer so that
                // we don't have to enumerate the sequence multiple times
                elementBuffer.Add(element);
            }

            if (elementBuffer.IsEmpty())
            {
                // If the element buffer is empty, so was the source sequence.
                // In this case, we can stop here and simply return an empty sequence.
                yield break;
            }

            // We already returned each element of the sequence once,
            // so take that into account when returning the elements repeatedly
            for (int i = 0; i < count - 1; i++)
            {
                foreach (TSource element in elementBuffer)
                {
                    yield return element;
                }
            }
        }
    }
}
