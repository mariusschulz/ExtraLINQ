using System;
using System.Collections.Generic;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsEmptyTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.IsEmpty());
        }
    }
}
