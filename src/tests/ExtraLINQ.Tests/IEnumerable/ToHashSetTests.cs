using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class ToHashSetTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.ToHashSet());
        }

        [Fact]
        public void ReturnsAnEmptyHashSetForAnEmptySequence()
        {
            int[] numbers = { };

            var hashSet = numbers.ToHashSet();

            hashSet.Should().HaveCount(0);
        }

        [Fact]
        public void TheReturnedHashSetContainsAllDistinctValues()
        {
            int[] numbers = { 1, 2, 2, 3, 3, 3 };

            var hashSet = numbers.ToHashSet();

            hashSet.Should().HaveCount(3);
            hashSet.Should().Contain(1);
            hashSet.Should().Contain(2);
            hashSet.Should().Contain(3);
        }
    }
}
