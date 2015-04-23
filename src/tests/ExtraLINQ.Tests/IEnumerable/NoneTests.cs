using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class NoneTests
    {
        [Fact]
        public void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            Assert.Throws<ArgumentNullException>(() => nullSequence.None(alwaysTruePredicate));
        }

        [Fact]
        public void Throws_ArgumentNullException_when_predicate_is_null()
        {
            string[] strings = { string.Empty };
            Func<object, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => strings.None(nullPredicate));
        }

        [Fact]
        public void Returns_true_when_no_item_matches()
        {
            string[] strings = { string.Empty };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noMatchFound = strings.None(stringLengthGreaterThanZero);

            noMatchFound.Should().BeTrue();
        }

        [Fact]
        public void Returns_false_when_at_least_one_item_matches()
        {
            string[] strings = { "Non-empty string" };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noMatchFound = strings.None(stringLengthGreaterThanZero);

            noMatchFound.Should().BeFalse();
        }
    }
}
