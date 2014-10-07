using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class CountsExactlyTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.CountsExactly(1));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.CountsExactly(-10));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountEqualsExpectedCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsExactly(4).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsExactly(4).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountDoesNotEqualExpectedCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsExactly(100).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsExactly(100).Should().BeFalse();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.CountsExactly(1, alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";

            Assert.Throws<ArgumentNullException>(() => validCollection.CountsExactly(1, null));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegativeAndPredicateIsValid()
        {
            IEnumerable<char> letters = "abcde";
            Func<char, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.CountsExactly(-10, alwaysTruePredicate));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountEqualsExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.CountsExactly(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.CountsExactly(1, fruit => fruit.StartsWith("b")).Should().BeTrue();
            fruits.CountsExactly(0, fruit => fruit.StartsWith("c")).Should().BeTrue();
        }

        [Fact]
        public void ReturnsTrueWhenActualCountDoesNotEqualExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.CountsExactly(1, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.CountsExactly(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
            fruits.CountsExactly(10, fruit => fruit.StartsWith("c")).Should().BeFalse();
        }
    }
}
