using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class NoneTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.None(alwaysTruePredicate);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            IEnumerable<object> validCollection = new List<string> { string.Empty };
            Func<object, bool> nullPredicate = null;

            validCollection.None(nullPredicate);
        }

        [Test]
        public void ReturnsTrueWhenNoItemMatches()
        {
            IEnumerable<string> validCollection = new List<string> { string.Empty };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

            noItemMatching.Should().BeTrue();
        }

        [Test]
        public void ReturnsFalseWhenAtLeastOneItemMatches()
        {
            IEnumerable<string> validCollection = new List<string> { "Non-empty string" };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

            noItemMatching.Should().BeFalse();
        }
    }
}
