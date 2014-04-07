using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ExtraLinq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLINQ.Tests
{
    [TestFixture]
    public class ToDictionaryTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            NameValueCollection collection = null;
            Dictionary<string, string> dictionary = collection.ToDictionary();
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

            Dictionary<string, string> dictionary = collection.ToDictionary();

            dictionary.Should().Equal(new Dictionary<string, string>
            {
                { "a", "1" },
                { "b", "2" },
                { "c", "3" }
            });
        }
    }
}
