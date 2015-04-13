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
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.HasAtMost(1));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtMost(-10));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCount()
        {
            IEnumerable<char> letters = "abcd";
            IEnumerable<char> emptyCollection = Enumerable.Empty<char>();

            letters.HasAtMost(4).Should().BeTrue();
            letters.HasAtMost(5).Should().BeTrue();
            emptyCollection.HasAtMost(0).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtMost(4).Should().BeTrue();
            letters.ToList().HasAtMost(5).Should().BeTrue();
            emptyCollection.ToList().HasAtMost(0).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountIsGreaterThanExpectedMaxCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasAtMost(4).Should().BeTrue();
            letters.HasAtMost(5).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtMost(4).Should().BeTrue();
            letters.ToList().HasAtMost(5).Should().BeTrue();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.HasAtMost(1, alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";

            Assert.Throws<ArgumentNullException>(() => validCollection.HasAtMost(1, null));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMaxCountIsNegative()
        {
            IEnumerable<char> validCollection = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            Assert.Throws<ArgumentOutOfRangeException>(() => validCollection.HasAtMost(-1, validPredicate));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            fruits.HasAtMost(2, startsWithLowercasedA).Should().BeTrue();
            fruits.HasAtMost(3, startsWithLowercasedA).Should().BeTrue();
            fruits.HasAtMost(int.MaxValue, startsWithLowercasedA).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountHigherThanExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            fruits.HasAtMost(0, startsWithLowercasedA).Should().BeFalse();
            fruits.HasAtMost(1, startsWithLowercasedA).Should().BeFalse();
        }
    }
}
