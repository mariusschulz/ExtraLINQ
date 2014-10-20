using System;
using System.Collections.Generic;
using Xunit;

namespace ExtraLinq.Tests
{
    public class ToHashSetTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.ToHashSet());
        }
    }
}
