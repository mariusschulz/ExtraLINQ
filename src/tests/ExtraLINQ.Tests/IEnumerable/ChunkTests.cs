using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class ChunkTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Chunk(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ThrowsAnArgumentExceptionWhenChunkLengthIsZeroOrNegative(int chunkLength)
        {
            int[] numbers = { 1, 2, 3 };

            Action chunk = () => numbers.Chunk(chunkLength);

            chunk.ShouldThrow<ArgumentException>(because: "the chunk length must be greater than or equal to 1");
        }

        [Fact]
        public void ReturnsAnEmptySequenceWhenPassedAnEmptySequence()
        {
            int[] numbers = new int[0];

            IEnumerable<int[]> chunks = numbers.Chunk(2);

            chunks.Should().BeEmpty();
        }
    }
}
