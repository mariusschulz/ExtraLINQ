using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class HasAtLeastTests
    {
        [Fact]
        public void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.HasAtLeast(1));
        }

        [Fact]
        public void Throws_ArgumentOutOfRangeException_when_expected_min_count_is_negative()
        {
            IEnumerable<char> letters = "abcd";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtLeast(-1));
        }

        [Fact]
        public void Returns_true_when_actual_count_is_greater_than_or_equal_to_expected_min_count()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasAtLeast(0).Should().BeTrue();
            letters.HasAtLeast(2).Should().BeTrue();
            letters.HasAtLeast(4).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtLeast(0).Should().BeTrue();
            letters.ToList().HasAtLeast(2).Should().BeTrue();
            letters.ToList().HasAtLeast(4).Should().BeTrue();
        }

        [Fact]
        public void Returns_false_when_actual_count_is_lower_than_expected_min_count()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasAtLeast(5).Should().BeFalse();
            letters.HasAtLeast(10).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtLeast(5).Should().BeFalse();
            letters.ToList().HasAtLeast(10).Should().BeFalse();
        }

        [Fact]
        public void Throws_ArgumentNullException_when_sequence_is_null_with_predicate()
        {
            IEnumerable<object> nullSequence = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullSequence.HasAtLeast(1, alwaysTruePredicate));
        }

        [Fact]
        public void Throws_ArgumentNullException_when_predicate_is_null()
        {
            IEnumerable<char> letters = "abcd";
            Func<char, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => letters.HasAtLeast(1, nullPredicate));
        }

        [Fact]
        public void Throws_ArgumentOutOfRangeException_when_expected_min_count_is_negative_with_predicate()
        {
            IEnumerable<char> letters = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtLeast(-1, validPredicate));
        }

        [Fact]
        public void Returns_true_when_actual_count_is_greater_than_or_equal_to_expected_min_count_with_predicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            IEnumerable<string> emptySequence = Enumerable.Empty<string>();

            fruits.HasAtLeast(1, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.HasAtLeast(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.HasAtLeast(1, fruit => fruit.StartsWith("b")).Should().BeTrue();

            emptySequence.HasAtLeast(0, _ => true).Should().BeTrue();
        }

        [Fact]
        public void Returns_false_when_actual_count_is_lower_than_expected_min_count_with_predicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.HasAtLeast(3, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.HasAtLeast(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
        }
    }
}
