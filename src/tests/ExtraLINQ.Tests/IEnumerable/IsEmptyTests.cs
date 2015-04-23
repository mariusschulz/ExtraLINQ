using System;
using System.Collections.Generic;
using Xunit;

namespace ExtraLinq.Tests
{
    public class IsEmptyTests
    {
        [Fact]
        public static void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence.IsEmpty());
        }
    }
}
