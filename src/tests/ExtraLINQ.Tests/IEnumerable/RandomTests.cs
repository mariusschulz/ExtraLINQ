using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class RandomTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Random());
        }

        [Fact]
        public void ReturnsItemContainedWithinCollection()
        {
            IEnumerable<char> letters = "abcde";

            char randomCharacter = letters.Random();

            letters.Should().Contain(randomCharacter);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
        {
            IEnumerable<char> nullCollection = null;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullCollection.Random(random));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenRandomIsNull()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentNullException>(() => letters.Random(null));
        }

        [Fact]
        public void ReturnsItemContainedWithinCollectionWithRandom()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            var random = new Random(arbitrarySeed);
            const char expectedCharacter = 'b';

            char randomCharacter = letters.Random(random);

            randomCharacter.Should().Be(expectedCharacter);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCount()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Random(validItemCount));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegative()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = -5;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.Random(invalidItemCount));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsGreaterThanCollectionCount()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = 100;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.Random(invalidItemCount));
        }

        [Fact]
        public void ReturnsItemsContainedWithinCollection()
        {
            IEnumerable<char> letters = "abcde";

            IEnumerable<char> threeRandomLetters = letters.Random(3);

            foreach (char letter in threeRandomLetters)
            {
                letters.Should().Contain(letter);
            }
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandomAndCount()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullCollection.Random(validItemCount, random));
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegativeWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = -5;
            var random = new Random();

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.Random(negativeElementsCount, random));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenRandomIsNullWithRandomAndCount()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = 2;

            Assert.Throws<ArgumentNullException>(() => letters.Random(negativeElementsCount, null));
        }

        [Fact]
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
