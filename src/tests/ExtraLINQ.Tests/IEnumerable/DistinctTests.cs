using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class DistinctTests
    {
        public class WithoutEqualityComparer
        {
            [Fact]
            public static void Throws_ArgumentNullException_when_sequence_is_null()
            {
                IEnumerable<int> numbers = null;

                Assert.Throws<ArgumentNullException>(() => numbers.Distinct(n => n % 2));
            }

            [Fact]
            public static void Throws_ArgumentNullException_when_value_selector_is_null()
            {
                int[] numbers = { };
                Func<int, string> valueSelector = null;

                Assert.Throws<ArgumentNullException>(() => numbers.Distinct(valueSelector));
            }

            [Fact]
            public static void Returns_an_empty_sequence_when_sequence_is_empty()
            {
                int[] numbers = { };

                var distinctNumbers = numbers.Distinct(n => n % 2);

                distinctNumbers.Should().BeEmpty();
            }

            [Fact]
            public static void Returns_unmodified_sequence_if_sequence_has_one_element()
            {
                int[] numbers = { 1 };

                var distinctNumbers = numbers.Distinct(n => n % 2);

                distinctNumbers.Should().Equal(1);
            }

            [Fact]
            public static void Returns_unmodified_sequence_if_sequence_has_two_different_elements()
            {
                int[] numbers = { 1, 2 };

                var distinctNumbers = numbers.Distinct(n => n % 2);

                distinctNumbers.Should().Equal(1, 2);
            }

            [Fact]
            public static void Returns_only_tuples_whose_selected_value_is_considered_distinct()
            {
                Tuple<int, string>[] digitNames =
                {
                    Tuple.Create(1, "One"),
                    Tuple.Create(1, "I SHOULDN'T BE HERE"),
                    Tuple.Create(2, "Two"),
                    Tuple.Create(2, "ME NEITHER"),
                    Tuple.Create(2, "ME NEITHER"),
                    Tuple.Create(3, "Three")
                };

                var distinctDigitNames = digitNames.Distinct(n => n.Item1);

                distinctDigitNames.Select(t => t.Item2).Should().Equal("One", "Two", "Three");
            }

            [Fact]
            public static void Returns_only_strings_whose_selected_value_is_considered_distinct()
            {
                string[] spellingsOfJavaScript = { "JavaScript", "Javascript", "javascript" };

                var distinctSpellings = spellingsOfJavaScript.Distinct(n => n.ToLower());

                distinctSpellings.Should().Equal("JavaScript");
            }
        }

        public class WithEqualityComparer
        {
            [Fact]
            public static void Throws_ArgumentNullException_when_sequence_is_null()
            {
                IEnumerable<int> numbers = null;
                Func<int, int> valueSelector = n => n % 2;
                IEqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;

                Assert.Throws<ArgumentNullException>(() => numbers.Distinct(valueSelector, equalityComparer));
            }

            [Fact]
            public static void Throws_ArgumentNullException_when_value_selector_is_null()
            {
                int[] numbers = { };
                Func<int, int> valueSelector = null;
                IEqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;

                Assert.Throws<ArgumentNullException>(() => numbers.Distinct(valueSelector, equalityComparer));
            }

            [Fact]
            public static void Throws_ArgumentNullException_when_equality_comparer_is_null()
            {
                int[] numbers = { };
                Func<int, int> valueSelector = n => n % 2;
                IEqualityComparer<int> equalityComparer = null;

                Assert.Throws<ArgumentNullException>(() => numbers.Distinct(valueSelector, equalityComparer));
            }

            [Fact]
            public static void Returns_an_empty_sequence_when_sequence_is_empty()
            {
                int[] numbers = { };
                Func<int, int> valueSelector = n => n % 2;
                IEqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;

                var distinctNumbers = numbers.Distinct(valueSelector, equalityComparer);

                distinctNumbers.Should().BeEmpty();
            }

            [Fact]
            public static void Returns_unmodified_sequence_if_sequence_has_one_element()
            {
                int[] numbers = { 1 };
                Func<int, int> valueSelector = n => n % 2;
                IEqualityComparer<int> equalityComparer = EqualityComparer<int>.Default;

                var distinctNumbers = numbers.Distinct(valueSelector, equalityComparer);

                distinctNumbers.Should().Equal(1);
            }

            [Fact]
            public static void Returns_only_tuples_whose_selected_string_values_are_considered_distinct()
            {
                Tuple<int, string>[] spellingsOfJavaScript =
                {
                    Tuple.Create(1, "JavaScript"),
                    Tuple.Create(2, "Javascript"),
                    Tuple.Create(3, "javascript")
                };

                var distinctSpellings = spellingsOfJavaScript.Distinct(tuple => tuple.Item2, StringComparer.InvariantCultureIgnoreCase);

                var singleTuple = distinctSpellings.Single();
                singleTuple.Item1.Should().Be(1);
                singleTuple.Item2.Should().Be("JavaScript");
            }
        }
    }
}
