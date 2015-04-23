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
        public static void Throws_ArgumentNullException_when_collection_is_null()
        {
            NameValueCollection collection = null;

            Assert.Throws<ArgumentNullException>(() => collection.ToDictionary());
        }

        [Fact]
        public static void Returned_dictionary_contains_exactly_the_elements_from_the_name_value_collection()
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
        public static void Returns_an_empty_dictionary_for_an_empty_name_value_collection()
        {
            var emptyCollection = new NameValueCollection();

            Dictionary<string, string> dictionary = emptyCollection.ToDictionary();

            dictionary.Should().BeEmpty();
        }
    }
}
