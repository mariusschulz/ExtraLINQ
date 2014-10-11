using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class SkipEveryTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.SkipEvery(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]
        public void ThrowsArgumentOutOfRangeExceptionWhenStepIsZeroOrNegative(int invalidStep)
        {
            int[] numbers = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => numbers.SkipEvery(invalidStep));
        }

        [Fact]
        public void ReturnsAnEmptySequenceWhenStepEqualsOne()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerable<int> everyNumber = numbers.SkipEvery(1);

            everyNumber.Should().BeEmpty();
        }

        [Fact]
        public void ReturnsEveryOtherElementWhenStepEqualsTwo()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerable<int> everyOtherNumber = numbers.SkipEvery(2);

            everyOtherNumber.Should().Equal(new[] { 1, 3, 5 });
        }

        [Fact]
        public void SkipsElementsCorrectly()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyOtherNumber = numbers.SkipEvery(3);

            everyOtherNumber.Should().Equal(new[] { 1, 2, 4, 5, 7 });
        }

        [Fact]
        public void ReturnsAllElementsWhenStepIsLargerThanTheSequenceLength()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerable<int> fullSequence = numbers.SkipEvery(10);

            fullSequence.Should().Equal(new[] { 1, 2, 3, 4, 5 });
        }
    }
}
