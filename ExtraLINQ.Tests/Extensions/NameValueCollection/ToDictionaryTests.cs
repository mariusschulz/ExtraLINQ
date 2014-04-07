using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ExtraLinq;
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
    }
}
