using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class WithoutTests
    {
        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Without('c'));
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_items_to_remove_collection_is_null()
        {
            IEnumerable<char> letters = "abcd";
            IEnumerable<char> itemsToRemove = null;

            Assert.Throws<ArgumentNullException>(() => letters.Without(itemsToRemove));
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_items_to_remove_collection_is_null_with_array()
        {
            IEnumerable<char> letters = "abcd";
            char[] itemsToRemove = null;

            Assert.Throws<ArgumentNullException>(() => letters.Without(itemsToRemove));
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null_with_collection()
        {
            IEnumerable<char> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.Without(new List<char> { 'c' }));
        }

        [Fact]
        public void Returns_sequence_without_specified_item()
        {
            IEnumerable<char> letters = "abcd";
            const char letterToRemove = 'a';

            letters = letters.Without(letterToRemove);

            letters.Should().NotContain('a');
            letters.Should().HaveCount(3);
        }

        [Fact]
        public void Returns_sequence_without_specified_items()
        {
            IEnumerable<char> letters = "abcd";

            letters = letters.Without(new[] { 'a', 'c' });

            letters.Should().NotContain('a');
            letters.Should().NotContain('c');
            letters.Should().HaveCount(2);
        }

        [Fact]
        public void Returns_unmodified_sequence_when_sequence_does_not_contain_any_item_to_remove()
        {
            IEnumerable<char> letters = "abcd";
            const char letterToRemove = 'z';

            letters = letters.Without(letterToRemove);

            letters.Should().HaveCount(4);
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_sequence_is_null_with_equality_comparer()
        {
            IEnumerable<char> nullSequence = null;
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            Assert.Throws<ArgumentNullException>(() => nullSequence.Without(stringLengthEqualityComparer, 'c'));
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_items_to_remove_collection_is_null_with_equality_comparer()
        {
            IEnumerable<char> letters = "abcd";
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            Assert.Throws<ArgumentNullException>(() => letters.Without(stringLengthEqualityComparer, null));
        }

        [Fact]
        public void Eagerly_throws_ArgumentNullException_when_equality_comparer_is_null()
        {
            IEnumerable<char> letters = "abcd";
            IEqualityComparer<char> nullEqualityComparer = null;

            Assert.Throws<ArgumentNullException>(() => letters.Without(nullEqualityComparer, 'c'));
        }

        [Fact]
        public void Returns_sequence_without_items_equal_to_passed_item()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
            const string itemToRemove = "banana";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            fruits = fruits.Without(stringLengthEqualityComparer, itemToRemove);

            fruits.Should().NotContain("banana");
            fruits.Should().NotContain("cherry");
            fruits.Should().HaveCount(2);
        }

        [Fact]
        public void Does_not_remove_items_that_do_not_match_the_passed_item_but_each_other()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
            const string itemToRemove = "apricot";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            IEnumerable<string> fruitsWithoutItem = fruits.Without(stringLengthEqualityComparer, itemToRemove);
            fruitsWithoutItem.Should().NotContain("apricot");
            fruitsWithoutItem.Should().HaveCount(3);
        }

        [Fact]
        public void Returns_unmodified_sequence_when_sequence_does_not_contain_any_item_to_remove_with_equality_comparer()
        {
            IEnumerable<string> stringNumbers = new[] { "1", "22", "333", "4444" };
            const string itemToRemove = "55555";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            stringNumbers = stringNumbers.Without(stringLengthEqualityComparer, itemToRemove);

            stringNumbers.Should().HaveCount(4);
        }
    }
}
