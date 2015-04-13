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
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.HasExactly(1));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasExactly(-10));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountEqualsExpectedCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasExactly(4).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasExactly(4).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountDoesNotEqualExpectedCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasExactly(100).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasExactly(100).Should().BeFalse();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.HasExactly(1, alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";

            Assert.Throws<ArgumentNullException>(() => validCollection.HasExactly(1, null));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegativeAndPredicateIsValid()
        {
            IEnumerable<char> letters = "abcde";
            Func<char, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasExactly(-10, alwaysTruePredicate));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountEqualsExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.HasExactly(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.HasExactly(1, fruit => fruit.StartsWith("b")).Should().BeTrue();
            fruits.HasExactly(0, fruit => fruit.StartsWith("c")).Should().BeTrue();
        }

        [Fact]
        public void ReturnsTrueWhenActualCountDoesNotEqualExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.HasExactly(1, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.HasExactly(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
            fruits.HasExactly(10, fruit => fruit.StartsWith("c")).Should().BeFalse();
        }
    }
}
