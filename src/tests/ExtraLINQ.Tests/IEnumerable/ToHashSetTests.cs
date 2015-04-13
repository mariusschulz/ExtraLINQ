using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class ToHashSetTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.ToHashSet());
        }

        [Fact]
        public void ReturnsAnEmptyHashSetForAnEmptySequence()
        {
            int[] numbers = { };

            var hashSet = numbers.ToHashSet();

            hashSet.Should().BeEmpty();
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
