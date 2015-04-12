using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class DistinctTests
    {
        [Fact]
        public static void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<int> numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Distinct(n => n % 2));
        }

        [Fact]
        public static void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            int[] numbers = { };
            Func<int, string> predicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Distinct(predicate));
        }

        [Fact]
        public static void ReturnsAnEmptySequenceWhenSequenceIsEmpty()
        {
            int[] numbers = { };

            var distinctNumbers = numbers.Distinct(n => n % 2);

            distinctNumbers.Should().HaveCount(0);
        }

        [Fact]
        public static void ReturnsUnmodifiedSequenceIfSequenceHasOneElement()
        {
            int[] numbers = { 1 };

            var distinctNumbers = numbers.Distinct(n => n % 2);

            distinctNumbers.Should().Equal(1);
        }

        [Fact]
        public static void ReturnsUnmodifiedSequenceIfSequenceHasTwoDifferentElements()
        {
            int[] numbers = { 1, 2 };

            var distinctNumbers = numbers.Distinct(n => n % 2);

            distinctNumbers.Should().Equal(1, 2);
        }

        [Fact]
        public static void ReturnsOnlyElementsConsideredDistinctByPredicate()
        {
            Tuple<int, string>[] digitNames =
            {
                Tuple.Create(1, "One"),
                Tuple.Create(1, "I SHOULDN'T BE HERE"),
                Tuple.Create(2, "Two"),
                Tuple.Create(2, "ME NEITHER"),
                Tuple.Create(2, "ME NEITHER"),
                Tuple.Create(3, "Three")
            };

            var distinctDigitNames = digitNames.Distinct(n => n.Item1);

            distinctDigitNames.Select(t => t.Item2).Should().Equal("One", "Two", "Three");
        }
    }
}
