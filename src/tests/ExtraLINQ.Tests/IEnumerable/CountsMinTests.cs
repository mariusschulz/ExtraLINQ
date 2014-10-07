using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class CountsMinTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.CountsMin(1));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegative()
        {
            IEnumerable<char> letters = "abcd";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.CountsMin(-1));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsMin(0).Should().BeTrue();
            letters.CountsMin(2).Should().BeTrue();
            letters.CountsMin(4).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsMin(0).Should().BeTrue();
            letters.ToList().CountsMin(2).Should().BeTrue();
            letters.ToList().CountsMin(4).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsMin(5).Should().BeFalse();
            letters.CountsMin(10).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsMin(5).Should().BeFalse();
            letters.ToList().CountsMin(10).Should().BeFalse();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.CountsMin(1, alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";
            Func<char, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => validCollection.CountsMin(1, nullPredicate));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegativeWithPredicate()
        {
            IEnumerable<char> letters = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.CountsMin(-1, validPredicate));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();

            fruits.CountsMin(1, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.CountsMin(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.CountsMin(1, fruit => fruit.StartsWith("b")).Should().BeTrue();

            emptyCollection.CountsMin(0, _ => true).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.CountsMin(3, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.CountsMin(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
        }
    }
}
