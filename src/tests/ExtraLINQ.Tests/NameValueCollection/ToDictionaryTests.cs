using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class ToDictionaryTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            NameValueCollection collection = null;

            Assert.Throws<ArgumentNullException>(() => collection.ToDictionary());
        }

        [Fact]
        public void ReturnedDictionaryContainsExactlyTheElementsFromTheNameValueCollection()
        {
            var collection = new NameValueCollection
            {
                { "a", "1" },
                { "b", "2" },
                { "c", "3" }
            };

            Dictionary<string, string> dictionary = collection.ToDictionary();

            dictionary.Should().Equal(new Dictionary<string, string>
            {
                { "a", "1" },
                { "b", "2" },
                { "c", "3" }
            });
        }

        [Fact]
        public void ReturnsAnEmptyDictionaryForAnEmptyNameValueCollection()
        {
            var emptyCollection = new NameValueCollection();

            Dictionary<string, string> dictionary = emptyCollection.ToDictionary();

            dictionary.Should().BeEmpty();
        }
    }
}
