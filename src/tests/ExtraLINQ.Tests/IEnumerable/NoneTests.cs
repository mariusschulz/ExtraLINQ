using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class NoneTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<object> nullSequence = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullSequence.None(alwaysTruePredicate));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            string[] strings = { string.Empty };
            Func<object, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => strings.None(nullPredicate));
        }

        [Fact]
        public void ReturnsTrueWhenNoItemMatches()
        {
            string[] strings = { string.Empty };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noMatchFound = strings.None(stringLengthGreaterThanZero);

            noMatchFound.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenAtLeastOneItemMatches()
        {
            string[] strings = { "Non-empty string" };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noMatchFound = strings.None(stringLengthGreaterThanZero);

            noMatchFound.Should().BeFalse();
        }
    }
}
