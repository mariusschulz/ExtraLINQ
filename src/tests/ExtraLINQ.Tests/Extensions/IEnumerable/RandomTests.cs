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
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
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

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
        {
            IEnumerable<char> nullCollection = null;
            var random = new Random();

            nullCollection.Random(random);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";

            letters.Random(null);
        }

        [Test]
        public void ReturnsItemContainedWithinCollectionWithRandom()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            var random = new Random(arbitrarySeed);
            const char expectedCharacter = 'b';

            char randomCharacter = letters.Random(random);

            randomCharacter.Should().Be(expectedCharacter);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCount()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;

            nullCollection.Random(validItemCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = -5;

            letters.Random(invalidItemCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
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

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandomAndCount()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;
            var random = new Random();

            nullCollection.Random(validItemCount, random);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegativeWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = -5;
            var random = new Random();

            letters.Random(negativeElementsCount, random);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenRandomIsNullWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = 2;

            letters.Random(negativeElementsCount, null);
        }

        [Test]
        public void ReturnsItemsContainedWithinCollectionWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            var random = new Random(arbitrarySeed);

            IEnumerable<char> threeRandomCharacters = letters.Random(3, random);
            char[] threeRandomCharactersArray = threeRandomCharacters.ToArray();

            threeRandomCharactersArray.Should().Equal(new[] { 'b', 'a', 'c' });
        }
    }
}
