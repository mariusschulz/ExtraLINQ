using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class WhereNotTests
    {
        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.WhereNot(_ => true));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.WhereNot(predicate));
        }

        [Fact]
        public void ReturnsAllItemsNotMatchingTheSpecifiedPredicate()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int[] expectedOdds = { 1, 3, 5 };
            Func<int, bool> isEven = n => n % 2 == 0;

            IEnumerable<int> odds = numbers.WhereNot(isEven);

            odds.Should().Equal(expectedOdds);
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNullForPredicateWithIndex()
        {
            IEnumerable<char> nullCollection = null;
            Func<char, int, bool> predicate = (index, character) => true;

            Assert.Throws<ArgumentNullException>(() => nullCollection.WhereNot(predicate));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenPredicateWithIndexIsNull()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.WhereNot(predicate));
        }

        [Fact]
        public void ReturnsAllItemsNotMatchingTheSpecifiedPredicateWithIndex()
        {
            int[] numbers = { 2, 3, 5, 7, 11, 13, 17, 19 };
            int[] expectedNumbers = { 2, 3, 7, 11, 17, 19 };

            IEnumerable<int> numbersExceptEveryThird = numbers
                .WhereNot((number, index) => (index + 1) % 3 == 0);

            numbersExceptEveryThird.Should().Equal(expectedNumbers);
        }
    }
}
