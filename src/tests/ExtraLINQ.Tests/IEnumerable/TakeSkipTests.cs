using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class TakeSkipTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.TakeSkip(1, 1));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_take_is_negative()
        {
            string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };

            Assert.Throws<ArgumentOutOfRangeException>(() => hobbits.TakeSkip(-1, 1));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_skip_is_negative()
        {
            string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };

            Assert.Throws<ArgumentOutOfRangeException>(() => hobbits.TakeSkip(1, -1));
        }

        [Fact]
        public static void Returns_an_empty_sequence_when_sequence_is_empty()
        {
            IEnumerable<char> emptySequence = Enumerable.Empty<char>();

            var takenSkippedSequence = emptySequence.TakeSkip(1, 0);

            takenSkippedSequence.Should().BeEmpty();
        }

        [Fact]
        public static void Returns_an_empty_sequence_when_take_is_0()
        {
            string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };

            var selectedHobbits = hobbits.TakeSkip(0, 1);

            selectedHobbits.Should().BeEmpty();
        }

        [Fact]
        public static void Returns_every_element_when_take_is_1_and_skip_is_0()
        {
            string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };

            var allHobbits = hobbits.TakeSkip(1, 0);

            allHobbits.Should().Equal(hobbits);
        }

        [Theory]
        [InlineData(1, 1, new[] { "Frodo", "Merry" })]
        [InlineData(1, 2, new[] { "Frodo", "Pippin" })]
        [InlineData(1, 4, new[] { "Frodo" })]
        [InlineData(2, 1, new[] { "Frodo", "Sam", "Pippin" })]
        [InlineData(2, 2, new[] { "Frodo", "Sam" })]
        [InlineData(2, 3, new[] { "Frodo", "Sam" })]
        [InlineData(3, 1, new[] { "Frodo", "Sam", "Merry" })]
        [InlineData(3, 2, new[] { "Frodo", "Sam", "Merry" })]
        [InlineData(3, 3, new[] { "Frodo", "Sam", "Merry" })]
        public static void Returns_the_correct_elements(int take, int skip, string[] expectedHobbits)
        {
            string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };
            var selectedHobbits = hobbits.TakeSkip(take, skip);

            selectedHobbits.Should().Equal(expectedHobbits);
        }
    }
}
