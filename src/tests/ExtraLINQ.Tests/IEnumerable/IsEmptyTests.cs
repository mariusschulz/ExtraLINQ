using System;
using System.Collections.Generic;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsEmptyTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenSequenceIsNull()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.IsEmpty());
        }
    }
}
