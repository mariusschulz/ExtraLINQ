using System;
using Xunit;

namespace ExtraLinq.Tests
{
    public class FlattenTests
    {
        [Fact]
        public static void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            int[][] numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Flatten());
        }
    }
}
