using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Splits the given sequence into chunks of the given size.
        /// If the sequence length isn't evenly divisible by the chunk size,
        /// the last chunk will contain all remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="chunkSize">The number of elements per chunk.</param>
        /// <returns>The chunked sequence.</returns>
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
