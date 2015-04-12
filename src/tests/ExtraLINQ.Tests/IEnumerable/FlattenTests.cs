using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class FlattenTests
    {
        [Fact]
        public static void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            int[][] numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Flatten());
        }

        [Fact]
        public static void FlattensAnArrayOfArraysOfIntegers()
        {
            int[][] numbers =
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5 },
                new[] { 6 }
            };

            IEnumerable<int> flattenedNumbers = numbers.Flatten();

            flattenedNumbers.Should().Equal(1, 2, 3, 4, 5, 6);
        }

        [Fact]
        public static void ReturnsAnEmptySequenceWhenSequenceIsEmpty()
        {
            int[][] numbers = { };

            IEnumerable<int> flattenedNumbers = numbers.Flatten();

            flattenedNumbers.Should().BeEmpty();
        }
    }
}
