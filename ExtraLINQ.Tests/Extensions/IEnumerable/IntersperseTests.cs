using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    [TestFixture]
    public class IntersperseTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<string> nullCollection = null;

            nullCollection.Intersperse("c").ToArray();
        }

        [Test]
        public void InsertsSeparatorCorrectly()
        {
            int[] numbers = new[] { 1, 2, 3, 4, 5 };
            int[] expectedNumbers = new[] { 1, 0, 2, 0, 3, 0, 4, 0, 5 };

            int[] separatedNumbers = numbers.Intersperse(0).ToArray();

            separatedNumbers.Should().Equal(expectedNumbers);
        }
    }
}
