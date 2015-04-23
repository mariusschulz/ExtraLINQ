using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class HasAtMostTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.HasAtMost(1));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_expected_count_is_negative()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtMost(-10));
        }

        [Fact]
        public static void Returns_true_when_actual_count_is_equal_to_or_lower_than_expected_count()
        {
            IEnumerable<char> letters = "abcd";
            IEnumerable<char> emptySequence = Enumerable.Empty<char>();

            letters.HasAtMost(4).Should().BeTrue();
            letters.HasAtMost(5).Should().BeTrue();
            emptySequence.HasAtMost(0).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtMost(4).Should().BeTrue();
            letters.ToList().HasAtMost(5).Should().BeTrue();
            emptySequence.ToList().HasAtMost(0).Should().BeTrue();
        }

        [Fact]
        public static void Returns_false_when_actual_count_is_greater_than_expected_max_count()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasAtMost(4).Should().BeTrue();
            letters.HasAtMost(5).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtMost(4).Should().BeTrue();
            letters.ToList().HasAtMost(5).Should().BeTrue();
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null_with_predicate()
        {
            IEnumerable<object> nullSequence = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullSequence.HasAtMost(1, alwaysTruePredicate));
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_predicate_is_null()
        {
            IEnumerable<char> letters = "abcd";

            Assert.Throws<ArgumentNullException>(() => letters.HasAtMost(1, null));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_expected_max_count_is_negative()
        {
            IEnumerable<char> letters = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtMost(-1, validPredicate));
        }

        [Fact]
        public static void Returns_true_when_actual_count_is_equal_to_or_lower_than_expected_count_with_predicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            fruits.HasAtMost(2, startsWithLowercasedA).Should().BeTrue();
            fruits.HasAtMost(3, startsWithLowercasedA).Should().BeTrue();
            fruits.HasAtMost(int.MaxValue, startsWithLowercasedA).Should().BeTrue();
        }

        [Fact]
        public static void Returns_false_when_actual_count_higher_than_expected_count_with_predicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            fruits.HasAtMost(0, startsWithLowercasedA).Should().BeFalse();
            fruits.HasAtMost(1, startsWithLowercasedA).Should().BeFalse();
        }
    }
}
