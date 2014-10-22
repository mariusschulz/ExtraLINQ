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

        [Fact]
        public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
        {
            int[] numbers = { 1, 2, 3 };
            Func<int, bool> nullPredicate = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Partition(nullPredicate));
        }
    }
}
