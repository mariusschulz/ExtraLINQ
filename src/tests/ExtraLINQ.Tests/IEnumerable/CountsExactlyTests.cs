using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class CountsExactlyTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.CountsExactly(1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";

            letters.CountsExactly(-10);
        }

        [Test]
        public void ReturnsTrueWhenActualCountEqualsExpectedCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsExactly(4).Should().BeTrue();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsExactly(4).Should().BeTrue();
        }

        [Test]
        public void ReturnsFalseWhenActualCountDoesNotEqualExpectedCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsExactly(100).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsExactly(100).Should().BeFalse();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.CountsExactly(1, alwaysTruePredicate);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";

            validCollection.CountsExactly(1, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegativeAndPredicateIsValid()
        {
            IEnumerable<char> letters = "abcde";
            Func<char, bool> alwaysTruePredicate = _ => true;

            letters.CountsExactly(-10, alwaysTruePredicate);
        }

        [Test]
        public void ReturnsTrueWhenActualCountEqualsExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.CountsExactly(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.CountsExactly(1, fruit => fruit.StartsWith("b")).Should().BeTrue();
            fruits.CountsExactly(0, fruit => fruit.StartsWith("c")).Should().BeTrue();
        }

        [Test]
        public void ReturnsTrueWhenActualCountDoesNotEqualExpectedCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.CountsExactly(1, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.CountsExactly(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
            fruits.CountsExactly(10, fruit => fruit.StartsWith("c")).Should().BeFalse();
        }
    }
}
