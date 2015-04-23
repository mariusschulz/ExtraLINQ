using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsNullOrEmptyTests
    {
        [Fact]
        public void Returns_true_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            bool isNullOrEmpty = nullSequence.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Fact]
        public void Returns_true_when_sequence_is_empty()
        {
            IEnumerable<string> emptyArray = new string[0];

            bool isNullOrEmpty = emptyArray.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Fact]
        public void Returns_false_when_sequence_contains_elements()
        {
            IEnumerable<string> singleElementArray = new[] { "test" };

            singleElementArray.IsNullOrEmpty().Should().BeFalse();
        }
    }
}
