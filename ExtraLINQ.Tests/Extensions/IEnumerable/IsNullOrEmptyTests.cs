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
            IEnumerable<string> emptyArray = new string[0];

            bool isNullOrEmpty = emptyArray.IsNullOrEmpty();

            isNullOrEmpty.Should().BeTrue();
        }

        [Test]
        public void ReturnsFalseWhenCollectionContainsElements()
        {
            IEnumerable<string> singleElementArray = new[] { "test" };

            singleElementArray.IsNullOrEmpty().Should().BeFalse();
        }
    }
}
