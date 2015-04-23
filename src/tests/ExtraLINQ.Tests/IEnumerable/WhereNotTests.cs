using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class WhereNotTests
    {
        [Fact]
        public static void Eagerly_throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.WhereNot(_ => true));
        }

        [Fact]
        public static void Eagerly_throws_ArgumentNullException_when_predicate_is_null()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.WhereNot(predicate));
        }

        [Fact]
        public static void Returns_all_items_not_matching_the_specified_predicate()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int[] expectedOdds = { 1, 3, 5 };
            Func<int, bool> isEven = n => n % 2 == 0;

            IEnumerable<int> odds = numbers.WhereNot(isEven);

            odds.Should().Equal(expectedOdds);
        }

        [Fact]
        public static void Eagerly_throws_ArgumentNullException_when_sequence_is_null_for_predicate_with_index()
        {
            IEnumerable<char> nullSequence = null;
            Func<char, int, bool> predicate = (index, character) => true;

            Assert.Throws<ArgumentNullException>(() => nullSequence.WhereNot(predicate));
        }

        [Fact]
        public static void Eagerly_throws_ArgumentNullException_when_predicate_with_index_is_null()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.WhereNot(predicate));
        }

        [Fact]
        public static void Returns_all_items_not_matching_the_specified_predicate_with_index()
        {
            int[] numbers = { 2, 3, 5, 7, 11, 13, 17, 19 };
            int[] expectedNumbers = { 2, 3, 7, 11, 17, 19 };

            IEnumerable<int> numbersExceptEveryThird = numbers
                .WhereNot((number, index) => (index + 1) % 3 == 0);

            numbersExceptEveryThird.Should().Equal(expectedNumbers);
        }
    }
}
