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
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EagerlyThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<string> nullCollection = null;

            nullCollection.Intersperse("c");
        }

        [Test]
        public void InsertsSeparatorCorrectly()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int[] expectedNumbers = { 1, 0, 2, 0, 3, 0, 4, 0, 5 };

            int[] separatedNumbers = numbers.Intersperse(0).ToArray();

            separatedNumbers.Should().Equal(expectedNumbers);
        }
    }
}
