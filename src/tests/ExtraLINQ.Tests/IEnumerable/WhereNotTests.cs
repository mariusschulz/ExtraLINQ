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

            Assert.Throws<ArgumentNullException>(() => numbers.WhereNot(null));
        }

        [Fact]
        public void ReturnsAllItemsNotMatchingTheSpecifiedPredicate()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            int[] expectedOdds = { 1, 3, 5 };

            numbers.WhereNot(n => n % 2 == 0).Should().Equal(expectedOdds);
        }
    }
}
