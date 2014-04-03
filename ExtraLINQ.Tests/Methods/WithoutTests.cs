using System;
using System.Collections.Generic;
using ExtraLinq;
using ExtraLinq.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLINQ.Tests.Methods
{
    [TestFixture]
    public class WithoutTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Without('c');
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNull()
        {
            IEnumerable<char> letters = "abcd";
            IEnumerable<char> itemsToRemove = null;

            letters.Without(itemsToRemove);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithArray()
        {
            IEnumerable<char> letters = "abcd";
            char[] itemsToRemove = null;

            letters.Without(itemsToRemove);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCollection()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Without(new List<char> { 'c' });
        }

        [Test]
        public void ReturnsCollectionWithoutSpecifiedItem()
        {
            IEnumerable<char> letters = "abcd";
            const char letterToRemove = 'a';

            letters = letters.Without(letterToRemove);

            letters.Should().NotContain('a');
            letters.Should().HaveCount(3);
        }

        [Test]
        public void ReturnsCollectionWithoutSpecifiedItems()
        {
            IEnumerable<char> letters = "abcd";

            letters = letters.Without(new[] { 'a', 'c' });

            letters.Should().NotContain('a');
            letters.Should().NotContain('c');
            letters.Should().HaveCount(2);
        }

        [Test]
        public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItem()
        {
            IEnumerable<char> letters = "abcd";
            const char letterToRemove = 'z';

            letters = letters.Without(letterToRemove);

            letters.Should().HaveCount(4);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithEqualityComparer()
        {
            IEnumerable<char> nullCollection = null;
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            nullCollection.Without(stringLengthEqualityComparer, 'c');
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithEqualityComparer()
        {
            IEnumerable<char> letters = "abcd";
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            letters.Without(stringLengthEqualityComparer, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenEqualityComparerIsNull()
        {
            IEnumerable<char> letters = "abcd";
            IEqualityComparer<char> nullEqualityComparer = null;

            letters.Without(nullEqualityComparer, 'c');
        }

        [Test]
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

        [Test]
        public void DoesNotRemoveItemsThatDoNotMatchThePassedItemButEachOther()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
            const string itemToRemove = "apricot";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            IEnumerable<string> fruitsWithoutItem = fruits.Without(stringLengthEqualityComparer, itemToRemove);
            fruitsWithoutItem.Should().NotContain("apricot");
            fruitsWithoutItem.Should().HaveCount(3);
        }

        [Test]
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
