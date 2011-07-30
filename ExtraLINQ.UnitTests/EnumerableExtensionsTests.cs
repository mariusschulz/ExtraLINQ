using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtraLINQ.UnitTests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void None_NullArgument_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.IsEmpty();
        }
    }
}
