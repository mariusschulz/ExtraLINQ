using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ExtraLinq;
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
    }
}
