using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
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
                    bool hasNext = enumerator.MoveNext();

                    if (!hasNext)
                    {
                        yield break;
                    }

                    yield return enumerator.Current;
                }

                for (int i = 0; i < skip; i++)
                {
                    bool hasNext = enumerator.MoveNext();

                    if (!hasNext)
                    {
                        yield break;
                    }
                }
            }
        }
    }
}
