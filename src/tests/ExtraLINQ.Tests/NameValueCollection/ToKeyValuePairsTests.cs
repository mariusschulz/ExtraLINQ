using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ExtraLinq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLINQ.Tests
{
    [TestFixture]
    public class ToKeyValuePairsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            NameValueCollection collection = null;
            IEnumerable<KeyValuePair<string, string>> keyValuePairs = collection.ToKeyValuePairs();
        }

        [Test]
        public void ReturnedDictionaryContainsExactlyTheElementsFromTheNameValueCollection()
        {
            var collection = new NameValueCollection
            {
                { "a", "1" },
                { "b", "2" },
                { "c", "3" }
            };

            IEnumerable<KeyValuePair<string, string>> keyValuePairs = collection.ToKeyValuePairs();

            keyValuePairs.Should().Equal(new[]
            {
                new KeyValuePair<string, string>("a", "1"), 
                new KeyValuePair<string, string>("b", "2"), 
                new KeyValuePair<string, string>("c", "3")
            });
        }

        [Test]
        public void ReturnsAnEmptyDictionaryForAnEmptyNameValueCollection()
        {
            var emptyCollection = new NameValueCollection();

            IEnumerable<KeyValuePair<string, string>> keyValuePairs = emptyCollection.ToKeyValuePairs();

            keyValuePairs.Should().HaveCount(0);
        }
    }
}
