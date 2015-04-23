using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class JoinedByTests
    {
        [Fact]
        public static void Eagerly_throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.JoinedBy(""));
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_separator_is_null()
        {
            string[] items = { "one", "two", "three" };

            Assert.Throws<ArgumentNullException>(() => items.JoinedBy(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(",")]
        [InlineData("    ")]
        public void ReturnsAnEmptyStringForAnEmptySequence(string separator)
        {
            string[] strings = new string[0];

            string joined = strings.JoinedBy(separator);

            joined.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("")]
        [InlineData(",")]
        [InlineData("    ")]
        public void ReturnsTheSingleItemForASequenceWithJustOneItem(string separator)
        {
            string[] strings = { "The One Ring" };

            string joined = strings.JoinedBy(separator);

            joined.Should().Be("The One Ring");
        }

        [Fact]
        public static void Returns_a_string_containing_all_items_joined_by_the_given_separator()
        {
            string[] strings = { "The", "One", "Ring" };

            string joined = strings.JoinedBy(" ");

            joined.Should().Be("The One Ring");
        }
    }
}
