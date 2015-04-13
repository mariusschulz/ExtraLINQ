using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

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

        [Fact]
        public void ReturnsAnEmptySequenceWhenPassedAnEmptySequence()
        {
            int[] numbers = new int[0];

            IEnumerable<int[]> chunks = numbers.Chunk(2);

            chunks.Should().BeEmpty();
        }
    }
}
