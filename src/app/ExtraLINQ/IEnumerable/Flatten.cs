using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
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
