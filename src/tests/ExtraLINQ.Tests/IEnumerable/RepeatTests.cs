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
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Repeat(1));
        }

        [Fact]
        public void EagerlyThrowsArgumentOutOfRangeExceptionWhenCountIsNegative()
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

            repeatedSequence.Should().HaveCount(0);
        }

        [Fact]
        public void ReturnsAnEmptySequenceWhenCountIsZero()
        {
            int[] numbers = { 1, 2, 3 };

            var repeatedSequence = numbers.Repeat(0);

            repeatedSequence.Should().HaveCount(0);
        }

        [Fact]
        public void ReturnsAnIdenticalSequenceWhenCountEqualsOne()
        {
            int[] numbers = { 1, 2, 3 };

            var identicalSequence = numbers.Repeat(1);

            identicalSequence.Should().Equal(numbers);
        }

        [Fact]
        public void RepeatsASingleItemSequenceWhenCountIsGreaterThanOne()
        {
            string[] sheldonsGreeting = { "Penny!" };

            var sheldonsOcdGreeting = sheldonsGreeting.Repeat(3);

            var expectedGreeting = sheldonsGreeting
                .Concat(sheldonsGreeting)
                .Concat(sheldonsGreeting);

            sheldonsOcdGreeting.Should().Equal(expectedGreeting);
        }

        [Fact]
        public void RepeatsASequenceWithMultipleItemsWhenCountIsGreaterThanOne()
        {
            string[] eatingSounds = { "om", "nom", "nom" };

            var repeatedEeatingSounds = eatingSounds.Repeat(2);

            repeatedEeatingSounds.Should().Equal(new[] { "om", "nom", "nom", "om", "nom", "nom" });
        }

        [Fact]
        public void RepeatsTheGivenSequenceLazily()
        {
            IEnumerable<int> infiniteSequence = new[] { 1, 2, 3 }.Cycle();

            var repeatedSequence = infiniteSequence.Repeat(2048);
            var firstFiveItems = repeatedSequence.Take(5);

            firstFiveItems.Should().Equal(new[] { 1, 2, 3, 1, 2 });
        }
    }
}
