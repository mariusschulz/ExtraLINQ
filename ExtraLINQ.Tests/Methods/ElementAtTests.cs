using System;
using System.Collections.Generic;
using ExtraLinq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLINQ.Tests.Methods
{
    [TestFixture]
    public class ElementAtTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [Test]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.ElementAt(1, IndexingStrategy.Regular);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenCollectionIsEmptyWithClampStrategy()
        {
            IEnumerable<char> letters = new char[0];

            letters.ElementAt(0, IndexingStrategy.Clamp);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenCollectionIsEmptyWithCyclicStrategy()
        {
            IEnumerable<char> letters = new char[0];

            letters.ElementAt(0, IndexingStrategy.Cyclic);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void ThrowsArgumentOutOfRangeExceptionWhenCollectionIsEmptyWithRegularStrategy()
        {
            IEnumerable<char> letters = new char[0];

            letters.ElementAt(0, IndexingStrategy.Regular);
        }

        [Test]
        public void ReturnsFirstElementWhenIndexIsNegativeWithClampStrategy()
        {
            IEnumerable<char> letters = "abcd";

            char characterForNegativeIndex = letters.ElementAt(-10, IndexingStrategy.Clamp);

            characterForNegativeIndex.Should().Be('a');
        }

        [Test]
        public void ReturnsLastElementWhenIndexIsGreaterThanCountWithClampStrategy()
        {
            IEnumerable<char> letters = "abcd";

            char characterForNegativeIndex = letters.ElementAt(100, IndexingStrategy.Clamp);

            characterForNegativeIndex.Should().Be('d');
        }

        [Test]
        public void ReturnsCorrectElementWithCyclicStrategy()
        {
            IEnumerable<char> letters = "abcd";

            letters.ElementAt(4, IndexingStrategy.Cyclic).Should().Be('a');
            letters.ElementAt(8, IndexingStrategy.Cyclic).Should().Be('a');
            letters.ElementAt(12, IndexingStrategy.Cyclic).Should().Be('a');

            letters.ElementAt(5, IndexingStrategy.Cyclic).Should().Be('b');
            letters.ElementAt(9, IndexingStrategy.Cyclic).Should().Be('b');
            letters.ElementAt(13, IndexingStrategy.Cyclic).Should().Be('b');

            letters.ElementAt(6, IndexingStrategy.Cyclic).Should().Be('c');
            letters.ElementAt(10, IndexingStrategy.Cyclic).Should().Be('c');
            letters.ElementAt(14, IndexingStrategy.Cyclic).Should().Be('c');

            letters.ElementAt(7, IndexingStrategy.Cyclic).Should().Be('d');
            letters.ElementAt(11, IndexingStrategy.Cyclic).Should().Be('d');
            letters.ElementAt(15, IndexingStrategy.Cyclic).Should().Be('d');

            // Int32.MaxValue = 2,147,483,647 and 2,147,483,647 % 4 = 3 ==> 'd'
            letters.ElementAt(Int32.MaxValue, IndexingStrategy.Cyclic).Should().Be('d');
        }
    }
}
