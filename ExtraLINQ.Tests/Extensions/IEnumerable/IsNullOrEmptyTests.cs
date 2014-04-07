using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class IsNullOrEmptyTests
    {
        [Test]
        public void ReturnsTrueWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            bool isNullOrEmpty = nullCollection.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Test]
        public void ReturnsTrueWhenCollectionIsEmpty()
        {
            IEnumerable<string> emptyCollection = new string[0];

            bool isNullOrEmpty = emptyCollection.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Test]
        public void ReturnsFalseWhenCollectionContainsElements()
        {
            IEnumerable<string> collectionContainingOneElements = new[] { "test" };

            collectionContainingOneElements.IsNullOrEmpty().Should().BeFalse();
        }
    }
}
