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
            Random randomNumberGenerator = new Random();

            nullCollection.Shuffle(randomNumberGenerator).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EagerlyThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";
            Random nullRandomNumberGenerator = null;

            letters.Shuffle(nullRandomNumberGenerator).ToList();
        }

        [Test]
        public void UsesTheSpecifiedRandomNumberGenerator()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            Random randomNumberGenerator = new Random(arbitrarySeed);

            IEnumerable<char> shuffledLetters = letters.Shuffle(randomNumberGenerator);

            shuffledLetters.Should().Equal("baced");
        }
    }
}
