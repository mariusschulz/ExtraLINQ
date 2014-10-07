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
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Shuffle());
        }

        [Fact]
        public void OnlyReturnsItemsContainedWithinCollection()
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
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
        {
            IEnumerable<char> nullCollection = null;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullCollection.Shuffle(random));
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
