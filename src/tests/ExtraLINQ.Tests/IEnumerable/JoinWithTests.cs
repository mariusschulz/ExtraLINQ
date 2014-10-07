using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace ExtraLinq.Tests
{
    public class JoinWithTests
    {
        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.JoinWith(""));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenSeparatorIsNull()
        {
            string[] items = { "one", "two", "three" };

            Assert.Throws<ArgumentNullException>(() => items.JoinWith(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(",")]
        [InlineData("    ")]
        public void ReturnsAnEmptyStringForAnEmptySequence(string separator)
        {
            string[] strings = new string[0];

            string joined = strings.JoinWith(separator);

            joined.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("")]
        [InlineData(",")]
        [InlineData("    ")]
        public void ReturnsTheSingleItemForASequenceWithJustOneItem(string separator)
        {
            string[] strings = { "The One Ring" };

            string joined = strings.JoinWith(separator);

            joined.Should().Be("The One Ring");
        }

        [Fact]
        public void ReturnsAStringContainingAllItemsJoinedByTheGivenSeparator()
        {
            string[] strings = { "The", "One", "Ring" };

            string joined = strings.JoinWith(" ");

            joined.Should().Be("The One Ring");
        }
    }
}
