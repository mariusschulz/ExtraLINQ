using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class ShuffleTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Shuffle();
        }

        [Test]
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

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
        {
            IEnumerable<char> nullCollection = null;
            var random = new Random();

            nullCollection.Shuffle(random).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EagerlyThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";

            letters.Shuffle(null).ToList();
        }

        [Test]
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
