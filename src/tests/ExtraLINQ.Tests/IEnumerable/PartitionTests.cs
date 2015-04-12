using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class PartitionTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Partition(char.IsUpper));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Partition(nullPredicate));
        }

        [Fact]
        public void ReturnsTwoEmptySequencesForAnEmptySequence()
        {
            int[] numbers = { };
            Func<int, bool> isEven = x => x % 2 == 0;

            var evenAndOddNumbers = numbers.Partition(isEven);

            evenAndOddNumbers.Matches.Should().HaveCount(0);
            evenAndOddNumbers.Mismatches.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(new[] { 1 }, new int[0], new[] { 1 })]
        [InlineData(new[] { 1, -2 }, new[] { -2 }, new[] { 1 })]
        [InlineData(new[] { 0, 0, 0 }, new[] { 0, 0, 0 }, new int[0])]
        [InlineData(new[] { 1, 3, 5 }, new int[0], new[] { 1, 3, 5 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 2, 4 }, new[] { 1, 3, 5 })]
        public void CorrectlyPartitionsTheGivenSequenceOfNumbersIntoEvensAndOdds(int[] numbers, int[] expectedEvens, int[] expectedOdds)
        {
            Func<int, bool> isEven = x => x % 2 == 0;

            var evenAndOddNumbers = numbers.Partition(isEven);

            evenAndOddNumbers.Matches.Should().Equal(expectedEvens);
            evenAndOddNumbers.Mismatches.Should().Equal(expectedOdds);
        }
    }
}
