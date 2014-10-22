using System;
using System.Collections.Generic;
using Xunit;

namespace ExtraLinq.Tests
{
    public class PartitionTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<char> nullCollection = null;

            Assert.Throws<ArgumentNullException>(() => nullCollection.Partition(char.IsUpper));
        }
    }
}
