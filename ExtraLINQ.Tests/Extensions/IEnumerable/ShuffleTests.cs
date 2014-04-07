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
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Shuffle();
        }

        [Test]
        public void OnlyReturnsItemsContainedWithinCollection()
        {
            IEnumerable<char> letters = "abcde";

            IEnumerable<char> shuffledLetters = letters.Shuffle();

            foreach (char letter in shuffledLetters)
            {
                letters.Should().Contain(letter);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
        {
            IEnumerable<char> nullCollection = null;
            Random randomNumberGenerator = new Random();

            nullCollection.Shuffle(randomNumberGenerator).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";
            Random nullRandomNumberGenerator = null;

            letters.Shuffle(nullRandomNumberGenerator).ToList();
        }

        [Test]
        public void OnlyReturnsItemsContainedWithinCollectionWithRandom()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            Random randomNumberGenerator = new Random(arbitrarySeed);

            IEnumerable<char> shuffledLetters = letters.Shuffle(randomNumberGenerator);
            char[] shuffledLettersArray = shuffledLetters.ToArray();

            shuffledLettersArray.Should().Equal(new[] { 'b', 'a', 'c', 'e', 'd' });
        }
    }
}
