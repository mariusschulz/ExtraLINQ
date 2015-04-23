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
        public void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Partition(char.IsUpper));
        }

        [Fact]
        public void Throws_ArgumentNullException_when_predicate_is_null()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Partition(nullPredicate));
        }

        [Fact]
        public void Returns_two_empty_sequences_for_an_empty_sequence()
        {
            int[] numbers = { };
            Func<int, bool> isEven = x => x % 2 == 0;

            var evenAndOddNumbers = numbers.Partition(isEven);

            evenAndOddNumbers.Matches.Should().BeEmpty();
            evenAndOddNumbers.Mismatches.Should().BeEmpty();
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
