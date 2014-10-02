using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class IsEmptyTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.IsEmpty();
        }
    }
}
