using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IntersperseTests
    {
        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<string> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Intersperse("c"));
        }

        [Fact]
        public void Inserts_separator_correctly()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int[] expectedNumbers = { 1, 0, 2, 0, 3, 0, 4, 0, 5 };

            int[] separatedNumbers = numbers.Intersperse(0).ToArray();

            separatedNumbers.Should().Equal(expectedNumbers);
        }
    }
}
