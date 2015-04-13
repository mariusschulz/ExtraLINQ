using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class ShuffleTests
    {
        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Shuffle());
        }

        [Fact]
        public void OnlyReturnsItemsFoundWithinSequence()
        {
            char[] letters = "abcde".ToCharArray();

            IEnumerable<char> shuffledLetters = letters.Shuffle().ToArray();

            foreach (char letter in shuffledLetters)
            {
                letters.Should().Contain(letter);
            }

            shuffledLetters.Should().HaveCount(letters.Length);
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenSequenceIsNullWithRandom()
        {
            IEnumerable<char> nullSequence = null;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullSequence.Shuffle(random));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentNullException>(() => letters.Shuffle(null));
        }

        [Fact]
        public void UsesTheSpecifiedRandomNumberGenerator()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            var random = new Random(arbitrarySeed);

            IEnumerable<char> shuffledLetters = letters.Shuffle(random);

            shuffledLetters.Should().Equal("baced");
        }
    }
}
