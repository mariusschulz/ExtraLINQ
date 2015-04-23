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
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Shuffle());
        }

        [Fact]
        public void Only_returns_items_found_within_sequence()
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
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null_with_random()
        {
            IEnumerable<char> nullSequence = null;
            var random = new Random();

            Assert.Throws<ArgumentNullException>(() => nullSequence.Shuffle(random));
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_random_is_null()
        {
            IEnumerable<char> letters = "abcde";

            Assert.Throws<ArgumentNullException>(() => letters.Shuffle(null));
        }

        [Fact]
        public void Uses_the_specified_random_number_generator()
        {
            IEnumerable<char> letters = "abcde";
            const int arbitrarySeed = 1337;
            var random = new Random(arbitrarySeed);

            IEnumerable<char> shuffledLetters = letters.Shuffle(random);

            shuffledLetters.Should().Equal("baced");
        }
    }
}
