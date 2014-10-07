using System;
using System.Collections.Generic;
using ExtraLinq.Tests.Setup;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class WithoutTests
    {
        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Without('c'));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNull()
        {
            IEnumerable<char> letters = "abcd";
            IEnumerable<char> itemsToRemove = null;

            Assert.Throws<ArgumentNullException>(() => letters.Without(itemsToRemove));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithArray()
        {
            IEnumerable<char> letters = "abcd";
            char[] itemsToRemove = null;

            Assert.Throws<ArgumentNullException>(() => letters.Without(itemsToRemove));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNullWithCollection()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Without(new List<char> { 'c' }));
        }

        [Fact]
        public void ReturnsCollectionWithoutSpecifiedItem()
        {
            IEnumerable<char> letters = "abcd";
            const char letterToRemove = 'a';

            letters = letters.Without(letterToRemove);

            letters.Should().NotContain('a');
            letters.Should().HaveCount(3);
        }

        [Fact]
        public void ReturnsCollectionWithoutSpecifiedItems()
        {
            IEnumerable<char> letters = "abcd";

            letters = letters.Without(new[] { 'a', 'c' });

            letters.Should().NotContain('a');
            letters.Should().NotContain('c');
            letters.Should().HaveCount(2);
        }

        [Fact]
        public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItem()
        {
            IEnumerable<char> letters = "abcd";
            const char letterToRemove = 'z';

            letters = letters.Without(letterToRemove);

            letters.Should().HaveCount(4);
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNullWithEqualityComparer()
        {
            IEnumerable<char> nullCollection = null;
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            Assert.Throws<ArgumentNullException>(() => nullCollection.Without(stringLengthEqualityComparer, 'c'));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithEqualityComparer()
        {
            IEnumerable<char> letters = "abcd";
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            Assert.Throws<ArgumentNullException>(() => letters.Without(stringLengthEqualityComparer, null));
        }

        [Fact]
        public void EagerlyThrowsArgumentNullExceptionWhenEqualityComparerIsNull()
        {
            IEnumerable<char> letters = "abcd";
            IEqualityComparer<char> nullEqualityComparer = null;

            Assert.Throws<ArgumentNullException>(() => letters.Without(nullEqualityComparer, 'c'));
        }

        [Fact]
        public void ReturnsCollectionWithoutItemsEqualToPassedItem()
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
        public void DoesNotRemoveItemsThatDoNotMatchThePassedItemButEachOther()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
            const string itemToRemove = "apricot";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            IEnumerable<string> fruitsWithoutItem = fruits.Without(stringLengthEqualityComparer, itemToRemove);
            fruitsWithoutItem.Should().NotContain("apricot");
            fruitsWithoutItem.Should().HaveCount(3);
        }

        [Fact]
        public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItemWithEqualityComparer()
        {
            IEnumerable<string> stringNumbers = new[] { "1", "22", "333", "4444" };
            const string itemToRemove = "55555";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            stringNumbers = stringNumbers.Without(stringLengthEqualityComparer, itemToRemove);

            stringNumbers.Should().HaveCount(4);
        }
    }
}
