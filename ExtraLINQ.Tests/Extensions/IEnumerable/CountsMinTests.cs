using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class CountsMinTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.CountsMin(1);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegative()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsMin(-1);
        }

        [Test]
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

        [Test]
        public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCount()
        {
            IEnumerable<char> letters = "abcd";

            letters.CountsMin(5).Should().BeFalse();
            letters.CountsMin(10).Should().BeFalse();

            // Test ICollection.Count early exit strategy
            letters.ToList().CountsMin(5).Should().BeFalse();
            letters.ToList().CountsMin(10).Should().BeFalse();
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.CountsMin(1, alwaysTruePredicate);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<char> validCollection = "abcd";
            Func<char, bool> nullPredicate = null;

            validCollection.CountsMin(1, nullPredicate);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegativeWithPredicate()
        {
            IEnumerable<char> letters = "abcd";
            Func<char, bool> validPredicate = c => c == 'a';

            letters.CountsMin(-1, validPredicate);
        }

        [Test]
        public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            IEnumerable<string> emptyCollection = Enumerable.Empty<string>();

            fruits.CountsMin(1, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.CountsMin(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
            fruits.CountsMin(1, fruit => fruit.StartsWith("b")).Should().BeTrue();

            emptyCollection.CountsMin(0, _ => true).Should().BeTrue();
        }

        [Test]
        public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCountWithPredicate()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

            fruits.CountsMin(3, fruit => fruit.StartsWith("a")).Should().BeFalse();
            fruits.CountsMin(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
        }
    }
}
