using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Cycle<TSource>(this IEnumerable<TSource> source)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return EnumerateForever(source);
        }

        private static IEnumerable<TSource> EnumerateForever<TSource>(IEnumerable<TSource> source)
        {
            var enumeratedItems = new List<TSource>();

            foreach (TSource item in source)
            {
                yield return item;
                enumeratedItems.Add(item);
            }

            if (!enumeratedItems.Any())
            {
                yield break;
            }

            int index = 0;
            while (true)
            {
                yield return enumeratedItems[index];
                index = (index + 1) % enumeratedItems.Count;
            }
        }
    }
}
