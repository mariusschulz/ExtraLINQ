using System;
using System.Collections.Generic;
using ExtraLinq;
using NUnit.Framework;

namespace ExtraLINQ.Tests.Methods
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
