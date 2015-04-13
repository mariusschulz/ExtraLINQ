using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsZeroOrNegative(chunkSize, "chunkSize");

            return ChunkIterator(source, chunkSize);
        }

        private static IEnumerable<TSource[]> ChunkIterator<TSource>(IEnumerable<TSource> source, int chunkSize)
        {
            TSource[] currentChunk = null;
            int currentIndex = 0;

            foreach (var element in source)
            {
                currentChunk = currentChunk ?? new TSource[chunkSize];
                currentChunk[currentIndex++] = element;

                if (currentIndex == chunkSize)
                {
                    yield return currentChunk;
                    currentIndex = 0;
                    currentChunk = null;
                }
            }

            // Do we have an incomplete chunk of remaining elements?
            if (currentChunk != null)
            {
                // This last chunk is incomplete, otherwise it would've been returned already.
                // Thus, we have to create a new, shorter array to hold the remaining elements.
                var lastChunk = new TSource[currentIndex];
                Array.Copy(currentChunk, lastChunk, currentIndex);

                yield return lastChunk;
            }
        }
    }
}
