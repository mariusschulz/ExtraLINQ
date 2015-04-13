using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class ChunkTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Chunk(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ThrowsAnArgumentExceptionWhenChunkSizeIsZeroOrNegative(int chunkSize)
        {
            int[] numbers = { 1, 2, 3 };

            Action chunk = () => numbers.Chunk(chunkSize);

            chunk.ShouldThrow<ArgumentException>(because: "the chunk size must be greater than or equal to 1");
        }

        [Fact]
        public void ReturnsAnEmptySequenceWhenPassedAnEmptySequence()
        {
            int[] numbers = new int[0];

            IEnumerable<int[]> chunks = numbers.Chunk(2);

            chunks.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void ReturnsASingleChunkIfTheSequenceOnlyHasOneElement(int chunkSize)
        {
            int[] numbers = { 42 };

            int[][] chunks = numbers.Chunk(chunkSize).ToArray();

            chunks.Should().HaveCount(1);
            chunks.First().Should().Equal(42);
        }

        [Fact]
        public void CorrectlySplitsASequenceIntoSingleElementChunks()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };
            int[][] expectedChunks =
            {
                new[] { 1 },
                new[] { 2 },
                new[] { 3 },
                new[] { 4 },
                new[] { 5 },
                new[] { 6 },
                new[] { 7 }
            };

            int[][] actualChunks = numbers.Chunk(1).ToArray();

            actualChunks.Should().HaveSameCount(expectedChunks);

            for (int i = 0; i < actualChunks.Length; i++)
            {
                int[] actualChunk = actualChunks[i];
                int[] expectedChunk = expectedChunks[i];

                actualChunk.Should().Equal(expectedChunk);
            }
        }

        [Fact]
        public void CorrectlySplitsASequenceIntoTwoElementChunks()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };
            int[][] expectedChunks =
            {
                new[] { 1, 2 },
                new[] { 3, 4 },
                new[] { 5, 6 },
                new[] { 7 }
            };

            int[][] actualChunks = numbers.Chunk(2).ToArray();

            actualChunks.Should().HaveSameCount(expectedChunks);

            for (int i = 0; i < actualChunks.Length; i++)
            {
                int[] actualChunk = actualChunks[i];
                int[] expectedChunk = expectedChunks[i];

                actualChunk.Should().Equal(expectedChunk);
            }
        }
    }
}
