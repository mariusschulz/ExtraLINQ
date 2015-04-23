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
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Random());
        }

        [Fact]
        public static void Only_returns_element_found_within_sequence()
        {
            IEnumerable<char> letters = "abcde";

            char randomCharacter = letters.Random();

            letters.Should().Contain(randomCharacter);
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null_with_random()
        {
            IEnumerable<char> nullSequence = null;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullSequence.Random(random));
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_random_is_null()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentNullException>(() => letters.Random(null));
        }

        [Fact]
        public static void Only_returns_element_found_within_sequence_with_random()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            var random = new Random(arbitrarySeed);
            const char expectedCharacter = 'b';

            char randomCharacter = letters.Random(random);

            randomCharacter.Should().Be(expectedCharacter);
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null_with_count()
        {
            IEnumerable<char> nullSequence = null;
            const int validItemCount = 0;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Random(validItemCount));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_count_is_negative()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = -5;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.Random(invalidItemCount));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_count_is_greater_than_sequence_count()
        {
            IEnumerable<char> letters = "abcde";
            const int invalidItemCount = 100;

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.Random(invalidItemCount));
        }

        [Fact]
        public static void Only_returns_items_found_within_sequence()
        {
            IEnumerable<char> letters = "abcde";

            IEnumerable<char> threeRandomLetters = letters.Random(3);

            foreach (char letter in threeRandomLetters)
            {
                letters.Should().Contain(letter);
            }
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null_with_random_and_count()
        {
            IEnumerable<char> nullSequence = null;
            const int validItemCount = 0;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullSequence.Random(validItemCount, random));
        }

        [Fact]
        public static void Throws_ArgumentOutOfRangeException_when_count_is_negative_with_random_and_count()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = -5;
            var random = new Random();

            Assert.Throws<ArgumentOutOfRangeException>(() => letters.Random(negativeElementsCount, random));
        }

        [Fact]
        public static void Throws_ArgumentNullException_when_random_is_null_with_random_and_count()
        {
            IEnumerable<char> letters = "abcde";
            const int negativeElementsCount = 2;

            Assert.Throws<ArgumentNullException>(() => letters.Random(negativeElementsCount, null));
        }

        [Fact]
        public static void Only_returns_items_found_within_sequence_with_random_and_count()
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
