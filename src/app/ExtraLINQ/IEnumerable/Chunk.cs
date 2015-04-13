using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int chunkLength)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsZeroOrNegative(chunkLength, "chunkLength");

            return ChunkIterator(source, chunkLength);
        }

        private static IEnumerable<TSource[]> ChunkIterator<TSource>(IEnumerable<TSource> source, int chunkLength)
        {
            yield break;
        }
    }
}
