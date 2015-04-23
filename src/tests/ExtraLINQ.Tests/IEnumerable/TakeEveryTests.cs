using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class TakeEveryTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.TakeEvery(2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(int.MinValue)]
        public void ThrowsArgumentOutOfRangeExceptionWhenStepIsZeroOrNegative(int invalidStep)
        {
            int[] numbers = { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => numbers.TakeEvery(invalidStep));
        }

        [Fact]
        public static void Returns_every_element_when_step_equals_one()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyNumber = numbers.TakeEvery(1);

            everyNumber.Should().Equal(new[] { 1, 2, 3, 4, 5, 6, 7 });
        }

        [Fact]
        public static void Returns_every_other_element_when_step_equals_two()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyOtherNumber = numbers.TakeEvery(2);

            everyOtherNumber.Should().Equal(new[] { 1, 3, 5, 7 });
        }

        [Fact]
        public static void Returns_the_correct_elements()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> everyThirdNumber = numbers.TakeEvery(3);

            everyThirdNumber.Should().Equal(new[] { 1, 4, 7 });
        }

        [Fact]
        public static void Only_returns_the_first_element_when_step_is_larger_than_the_sequence_length()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            IEnumerable<int> onlyFirstElement = numbers.TakeEvery(10);

            onlyFirstElement.Should().Equal(new[] { 1 });
        }
    }
}
