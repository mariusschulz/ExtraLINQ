using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source, int count)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNegative(count, "count");

            return RepeatImplementation(source, count);
        }

        private static IEnumerable<TSource> RepeatImplementation<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count == 0)
            {
                yield break;
            }

            var collection = source as ICollection<TSource>;

            var itemBuffer = collection == null
                ? new List<TSource>()
                : new List<TSource>(collection.Count);

            foreach (TSource item in source)
            {
                yield return item;

                // We add this item to a local item buffer so that
                // we don't have to enumerate the sequence multiple times
                itemBuffer.Add(item);
            }

            if (itemBuffer.IsEmpty())
            {
                // If the item buffer is empty, so was the source sequence.
                // In this case, we can stop here and simply return an empty sequence.
                yield break;
            }

            // We already returned each item of the sequence once,
            // so take that into account when returning the items repeatedly
            for (int i = 0; i < count - 1; i++)
            {
                foreach (TSource item in itemBuffer)
                {
                    yield return item;
                }
            }
        }
    }
}
