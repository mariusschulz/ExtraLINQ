using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class RandomTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Random();
        }

        [Test]
        public void ReturnsItemContainedWithinCollection()
        {
            IEnumerable<char> letters = "abcde";

            char randomCharacter = letters.Random();

            letters.Should().Contain(randomCharacter);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
        {
            IEnumerable<char> nullCollection = null;
            Random randomNumberGenerator = new Random();

            nullCollection.Random(randomNumberGenerator);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";
            Random nullRandomNumberGenerator = null;

            letters.Random(nullRandomNumberGenerator);
        }

        [Test]
        public void ReturnsItemContainedWithinCollectionWithRandom()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            Random randomNumberGenerator = new Random(arbitrarySeed);
            const char expectedCharacter = 'b';

            char randomCharacter = letters.Random(randomNumberGenerator);

            randomCharacter.Should().Be(expectedCharacter);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCount()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;

            nullCollection.Random(validItemCount);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = -5;

            letters.Random(invalidItemCount);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsGreaterThanCollectionCount()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = 100;

            letters.Random(invalidItemCount);
        }

        [Test]
        public void ReturnsItemsContainedWithinCollection()
        {
            IEnumerable<char> letters = "abcde";

            IEnumerable<char> threeRandomLetters = letters.Random(3);

            foreach (char letter in threeRandomLetters)
            {
                letters.Should().Contain(letter);
            }
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandomAndCount()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;
            Random randomNumberGenerator = new Random();

            nullCollection.Random(validItemCount, randomNumberGenerator);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegativeWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = -5;
            Random randomNumberGenerator = new Random();

            letters.Random(negativeElementsCount, randomNumberGenerator);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenRandomIsNullWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = 2;
            Random nullRandomNumberGenerator = null;

            letters.Random(negativeElementsCount, nullRandomNumberGenerator);
        }

        [Test]
        public void ReturnsItemsContainedWithinCollectionWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            Random randomNumberGenerator = new Random(arbitrarySeed);

            IEnumerable<char> threeRandomCharacters = letters.Random(3, randomNumberGenerator);
            char[] threeRandomCharactersArray = threeRandomCharacters.ToArray();

            threeRandomCharactersArray.Should().Equal(new[] { 'b', 'a', 'c' });
        }
    }
}
