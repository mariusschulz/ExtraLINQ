using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class HasExactlyTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.HasExactly(1));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_expected_count_is_negative()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasExactly(-10));
        }

        [Fact]
        public static void Returns_true_when_actual_count_equals_expected_count()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasExactly(4).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasExactly(4).Should().BeTrue();
        }

        [Fact]
        public static void Returns_false_when_actual_count_does_not_equal_expected_count()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasExactly(100).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasExactly(100).Should().BeFalse();
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null_with_predicate()
        {
            IEnumerable<object> nullSequence = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullSequence.HasExactly(1, alwaysTruePredicate));
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_predicate_is_null()
        {
            IEnumerable<char> characters = "abcd";

            Assert.Throws<ArgumentNullException>(() => characters.HasExactly(1, null));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_expected_count_is_negative_and_predicate_is_valid()
        {
            IEnumerable<char> letters = "abcde";
            Func<char, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasExactly(-10, alwaysTruePredicate));
        }

        [Fact]
        public static void Returns_true_when_actual_count_equals_expected_count_with_predicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.HasExactly(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.HasExactly(1, fruit => fruit.StartsWith("b")).Should().BeTrue();
            fruits.HasExactly(0, fruit => fruit.StartsWith("c")).Should().BeTrue();
        }

        [Fact]
        public static void Returns_true_when_actual_count_does_not_equal_expected_count_with_predicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.HasExactly(1, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.HasExactly(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
            fruits.HasExactly(10, fruit => fruit.StartsWith("c")).Should().BeFalse();
        }
    }
}
