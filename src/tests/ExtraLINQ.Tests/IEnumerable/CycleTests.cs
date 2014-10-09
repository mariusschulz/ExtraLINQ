using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class CycleTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Cycle());
        }

        [Fact]
        public void ReturnsAnEmptySequenceWhenPassedAnEmptySequence()
        {
            int[] numbers = new int[0];

            int[] cycledNumbers = numbers.Cycle().ToArray();

            cycledNumbers.Should().HaveCount(0);
        }

        [Fact]
        public void CyclesThroughASequenceWithASingleItem()
        {
            int[] singleTen = { 10 };

            int[] threeTens = singleTen.Cycle().Take(3).ToArray();

            threeTens.Should().Equal(new[] { 10, 10, 10 });
        }

        [Fact]
        public void CyclesThroughASequenceWithMultipleItems()
        {
            int[] bits = { 0, 1 };

            int[] alternatingBits = bits.Cycle().Take(5).ToArray();

            alternatingBits.Should().Equal(new[] { 0, 1, 0, 1, 0 });
        }
    }
}
