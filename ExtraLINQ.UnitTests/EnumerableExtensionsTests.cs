using System;
using System.Collections;
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
            IEnumerable nullCollection = null;

            nullCollection.IsEmpty();
        }
    }
}
