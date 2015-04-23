using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class SkipEveryTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.SkipEvery(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]
        public void ThrowsArgumentOutOfRangeExceptionWhenStepIsZeroOrNegative(int invalidStep)
        {
            int[] numbers = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => numbers.SkipEvery(invalidStep));
        }

        [Fact]
        public static void Returns_an_empty_sequence_when_step_equals_one()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerable<int> everyNumber = numbers.SkipEvery(1);

            everyNumber.Should().BeEmpty();
        }

        [Fact]
        public static void Returns_every_other_element_when_step_equals_two()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerable<int> everyOtherNumber = numbers.SkipEvery(2);

            everyOtherNumber.Should().Equal(new[] { 1, 3, 5 });
        }

        [Fact]
        public static void Skips_elements_correctly()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyOtherNumber = numbers.SkipEvery(3);

            everyOtherNumber.Should().Equal(new[] { 1, 2, 4, 5, 7 });
        }

        [Fact]
        public static void Returns_all_elements_when_step_is_larger_than_the_sequence_length()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerable<int> fullSequence = numbers.SkipEvery(10);

            fullSequence.Should().Equal(new[] { 1, 2, 3, 4, 5 });
        }
    }
}
