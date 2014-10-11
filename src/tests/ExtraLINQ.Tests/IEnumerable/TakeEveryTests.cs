using System;
using System.Collections.Generic;
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
    }
}
