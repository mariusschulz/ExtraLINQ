using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class TakeEveryTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.TakeEvery(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]
        public void ThrowsArgumentOutOfRangeExceptionWhenStepIsZeroOrNegative(int invalidStep)
        {
            int[] numbers = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => numbers.TakeEvery(invalidStep));
        }

        [Fact]
        public void ReturnsEveryElementWhenStepEqualsOne()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyNumber = numbers.TakeEvery(1);

            everyNumber.Should().Equal(new[] { 1, 2, 3, 4, 5, 6, 7 });
        }

        [Fact]
        public void ReturnsEveryOtherElementWhenStepEqualsTwo()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyOtherNumber = numbers.TakeEvery(2);

            everyOtherNumber.Should().Equal(new[] { 1, 3, 5, 7 });
        }

        [Fact]
        public void ReturnsTheCorrectElements()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyThirdNumber = numbers.TakeEvery(3);

            everyThirdNumber.Should().Equal(new[] { 1, 4, 7 });
        }

        [Fact]
        public void OnlyReturnsTheFirstElementWhenStepIsLargerThanTheSequenceLength()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> onlyFirstElement = numbers.TakeEvery(10);

            onlyFirstElement.Should().Equal(new[] { 1 });
        }
    }
}
