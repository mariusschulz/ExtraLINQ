using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IntersperseTests
    {
        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<string> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Intersperse("c"));
        }

        [Fact]
        public void InsertsSeparatorCorrectly()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int[] expectedNumbers = { 1, 0, 2, 0, 3, 0, 4, 0, 5 };

            int[] separatedNumbers = numbers.Intersperse(0).ToArray();

            separatedNumbers.Should().Equal(expectedNumbers);
        }
    }
}
