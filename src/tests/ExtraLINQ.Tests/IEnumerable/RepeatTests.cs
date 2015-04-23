using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class RepeatTests
    {
        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Repeat(1));
        }

        [Fact]
        public void Eagerly_throws_ArgumentOutOfRangeException_when_count_is_negative()
        {
            int[] numbers = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => numbers.Repeat(-1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(42)]
        [InlineData(int.MaxValue)]
        public void ReturnsAnEmptySequenceWhenPassedAnEmptySequence(int count)
        {
            var emptySequence = Enumerable.Empty<int>();

            var repeatedSequence = emptySequence.Repeat(count);

            repeatedSequence.Should().BeEmpty();
        }

        [Fact]
        public void Returns_an_empty_sequence_when_count_is_zero()
        {
            int[] numbers = { 1, 2, 3 };

            var repeatedSequence = numbers.Repeat(0);

            repeatedSequence.Should().BeEmpty();
        }

        [Fact]
        public void Returns_an_identical_sequence_when_count_equals_one()
        {
            int[] numbers = { 1, 2, 3 };

            var identicalSequence = numbers.Repeat(1);

            identicalSequence.Should().Equal(numbers);
        }

        [Fact]
        public void Repeats_a_single_item_sequence_when_count_is_greater_than_one()
        {
            string[] sheldonsGreeting = { "Penny!" };

            var sheldonsOcdGreeting = sheldonsGreeting.Repeat(3);

            var expectedGreeting = sheldonsGreeting
                .Concat(sheldonsGreeting)
                .Concat(sheldonsGreeting);

            sheldonsOcdGreeting.Should().Equal(expectedGreeting);
        }

        [Fact]
        public void Repeats_a_sequence_with_multiple_items_when_count_is_greater_than_one()
        {
            string[] eatingSounds = { "om", "nom", "nom" };

            var repeatedEeatingSounds = eatingSounds.Repeat(2);

            repeatedEeatingSounds.Should().Equal(new[] { "om", "nom", "nom", "om", "nom", "nom" });
        }

        [Fact]
        public void Repeats_the_given_sequence_lazily()
        {
            IEnumerable<int> infiniteSequence = new[] { 1, 2, 3 }.Cycle();

            var repeatedSequence = infiniteSequence.Repeat(2048);
            var firstFiveItems = repeatedSequence.Take(5);

            firstFiveItems.Should().Equal(new[] { 1, 2, 3, 1, 2 });
        }
    }
}
