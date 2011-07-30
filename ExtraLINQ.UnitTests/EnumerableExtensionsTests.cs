using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtraLINQ.UnitTests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region IsEmpty()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsEmpty_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.IsEmpty();
        }

        #endregion

        #region

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void None_NullCollectionNullPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> nullPredicate = null;

            nullCollection.None(nullPredicate);
        }

        #endregion
    }
}
