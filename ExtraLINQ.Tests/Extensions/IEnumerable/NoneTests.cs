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
            string[] validCollection = { string.Empty };
            Func<object, bool> nullPredicate = null;

            validCollection.None(nullPredicate);
        }

        [Test]
        public void ReturnsTrueWhenNoItemMatches()
        {
            string[] strings = { string.Empty };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noMatchFound = strings.None(stringLengthGreaterThanZero);

            noMatchFound.Should().BeTrue();
        }

        [Test]
        public void ReturnsFalseWhenAtLeastOneItemMatches()
        {
            string[] strings = { "Non-empty string" };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noMatchFound = strings.None(stringLengthGreaterThanZero);

            noMatchFound.Should().BeFalse();
        }
    }
}
