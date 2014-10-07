using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class CountsMaxTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.CountsMax(1));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.CountsMax(-10));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCount()
        {
            IEnumerable<char> letters = "abcd";
            IEnumerable<char> emptyCollection = Enumerable.Empty<char>();

            letters.CountsMax(4).Should().BeTrue();
            letters.CountsMax(5).Should().BeTrue();
            emptyCollection.CountsMax(0).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsMax(4).Should().BeTrue();
            letters.ToList().CountsMax(5).Should().BeTrue();
            emptyCollection.ToList().CountsMax(0).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountIsGreaterThanExpectedMaxCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsMax(4).Should().BeTrue();
            letters.CountsMax(5).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsMax(4).Should().BeTrue();
            letters.ToList().CountsMax(5).Should().BeTrue();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.CountsMax(1, alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";

            Assert.Throws<ArgumentNullException>(() => validCollection.CountsMax(1, null));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMaxCountIsNegative()
        {
            IEnumerable<char> validCollection = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            Assert.Throws<ArgumentOutOfRangeException>(() => validCollection.CountsMax(-1, validPredicate));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            fruits.CountsMax(2, startsWithLowercasedA).Should().BeTrue();
            fruits.CountsMax(3, startsWithLowercasedA).Should().BeTrue();
            fruits.CountsMax(int.MaxValue, startsWithLowercasedA).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountHigherThanExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            fruits.CountsMax(0, startsWithLowercasedA).Should().BeFalse();
            fruits.CountsMax(1, startsWithLowercasedA).Should().BeFalse();
        }
    }
}
