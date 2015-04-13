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
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.HasAtLeast(1));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegative()
        {
            IEnumerable<char> letters = "abcd";

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtLeast(-1));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCount()
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
        public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.HasAtLeast(5).Should().BeFalse();
            letters.HasAtLeast(10).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().HasAtLeast(5).Should().BeFalse();
            letters.ToList().HasAtLeast(10).Should().BeFalse();
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.HasAtLeast(1, alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";
            Func<char, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => validCollection.HasAtLeast(1, nullPredicate));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegativeWithPredicate()
        {
            IEnumerable<char> letters = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.HasAtLeast(-1, validPredicate));
        }

        [Fact]
        public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();

            fruits.HasAtLeast(1, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.HasAtLeast(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.HasAtLeast(1, fruit => fruit.StartsWith("b")).Should().BeTrue();

            emptyCollection.HasAtLeast(0, _ => true).Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.HasAtLeast(3, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.HasAtLeast(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
        }
    }
}
