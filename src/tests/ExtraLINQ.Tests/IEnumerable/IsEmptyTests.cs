using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsEmptyTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.IsEmpty());
        }

        [Fact]
        public static void Returns_true_when_sequence_is_empty()
        {
            IEnumerable<string> emptyArray = new string[0];

            bool isEmpty = emptyArray.IsEmpty();

            isEmpty.Should().BeTrue();
        }

        [Fact]
        public static void Returns_false_when_sequence_is_not_empty()
        {
            int[] numbers = { 1, 2, 3 };

            bool isEmpty = numbers.IsEmpty();

            isEmpty.Should().BeFalse();
        }
    }
}
