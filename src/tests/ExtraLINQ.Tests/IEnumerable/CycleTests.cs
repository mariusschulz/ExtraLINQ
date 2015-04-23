using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class CycleTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Cycle());
        }

        [Fact]
        public static void Returns_an_empty_sequence_when_passed_an_empty_sequence()
        {
            int[] numbers = new int[0];

            int[] cycledNumbers = numbers.Cycle().ToArray();

            cycledNumbers.Should().BeEmpty();
        }

        [Fact]
        public static void Cycles_through_a_sequence_with_a_single_item()
        {
            int[] singleTen = { 10 };

            int[] threeTens = singleTen.Cycle().Take(3).ToArray();

            threeTens.Should().Equal(new[] { 10, 10, 10 });
        }

        [Fact]
        public static void Cycles_through_a_sequence_with_multiple_items()
        {
            int[] bits = { 0, 1 };

            int[] alternatingBits = bits.Cycle().Take(5).ToArray();

            alternatingBits.Should().Equal(new[] { 0, 1, 0, 1, 0 });
        }
    }
}
