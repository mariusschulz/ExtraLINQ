using System;
using System.Collections.Generic;
using Xunit;

namespace ExtraLinq.Tests
{
    public class DistinctTests
    {
        [Fact]
        public static void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<int> numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Distinct(n => n % 2));
        }
    }
}
