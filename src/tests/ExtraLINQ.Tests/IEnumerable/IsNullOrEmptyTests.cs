using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsNullOrEmptyTests
    {
        [Fact]
        public void ReturnsTrueWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            bool isNullOrEmpty = nullCollection.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Fact]
        public void ReturnsTrueWhenCollectionIsEmpty()
        {
            IEnumerable<string> emptyArray = new string[0];

            bool isNullOrEmpty = emptyArray.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenCollectionContainsElements()
        {
            IEnumerable<string> singleElementArray = new[] { "test" };

            singleElementArray.IsNullOrEmpty().Should().BeFalse();
        }
    }
}
