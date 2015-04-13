using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsNullOrEmptyTests
    {
        [Fact]
        public void ReturnsTrueWhenSequenceIsNull()
        {
            IEnumerable<object> nullSequence = null;

            bool isNullOrEmpty = nullSequence.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Fact]
        public void ReturnsTrueWhenSequenceIsEmpty()
        {
            IEnumerable<string> emptyArray = new string[0];

            bool isNullOrEmpty = emptyArray.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenSequenceContainsElements()
        {
            IEnumerable<string> singleElementArray = new[] { "test" };

            singleElementArray.IsNullOrEmpty().Should().BeFalse();
        }
    }
}
