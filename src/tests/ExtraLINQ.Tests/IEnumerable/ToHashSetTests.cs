using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class ToHashSetTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.ToHashSet());
        }

        [Fact]
        public static void Returns_an_empty_hash_set_for_an_empty_sequence()
        {
            int[] numbers = { };

            var hashSet = numbers.ToHashSet();

            hashSet.Should().BeEmpty();
        }

        [Fact]
        public static void The_returned_hash_set_contains_all_distinct_values()
        {
            int[] numbers = { 1, 2, 2, 3, 3, 3 };

            var hashSet = numbers.ToHashSet();

            hashSet.Should().HaveCount(3);
            hashSet.Should().Contain(1);
            hashSet.Should().Contain(2);
            hashSet.Should().Contain(3);
        }
    }
}
