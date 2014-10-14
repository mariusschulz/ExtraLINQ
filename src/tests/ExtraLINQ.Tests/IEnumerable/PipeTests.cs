using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class PipeTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
        {
            IEnumerable<object> nullCollection = null;
            Action<object> doNothing = _ => { };

            Assert.Throws<ArgumentNullException>(() => nullCollection.Pipe(doNothing));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenActionIsNull()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int> action = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Pipe(action));
        }

        [Fact]
        public void CallsTheSpecifiedActionForEachElementWhenEnumerated()
        {
            int[] numbers = { 1, 2, 3 };
            var elementsPassedToAction = new List<int>();
            Action<int> action = elementsPassedToAction.Add;

            numbers.Pipe(action).ToList();

            elementsPassedToAction.Should().Equal(numbers);
        }

        [Fact]
        public void ReturnsAllElementsOfTheSourceSequenceWithoutModification()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int> action = Console.WriteLine;

            IEnumerable<int> returnedSequence = numbers.Pipe(action);

            returnedSequence.Should().Equal(numbers);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenCollectionIsNullForActionWithIndex()
        {
            IEnumerable<object> nullCollection = null;
            Action<object, int> doNothing = (element, index) => { };

            Assert.Throws<ArgumentNullException>(() => nullCollection.Pipe(doNothing));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionWhenActionWithIndexIsNull()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int, int> nullAction = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Pipe(nullAction));
        }

        [Fact]
        public void CallsTheSpecifiedActionWithIndexForEachElementWhenEnumerated()
        {
            int[] numbers = { 3, 4, 5 };
            var elementsPassedToAction = new List<int>();
            var indicesPassedToAction = new List<int>();
            Action<int, int> action = (element, index) =>
            {
                elementsPassedToAction.Add(element);
                indicesPassedToAction.Add(index);
            };

            numbers.Pipe(action).ToList();

            elementsPassedToAction.Should().Equal(numbers);
            indicesPassedToAction.Should().Equal(new[] { 0, 1, 2 });
        }

        [Fact]
        public void ReturnsAllElementsOfTheSourceSequenceWithoutModificationForActionWithIndex()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int, int> action = (element, index) => Console.WriteLine(element);

            IEnumerable<int> returnedSequence = numbers.Pipe(action);

            returnedSequence.Should().Equal(numbers);
        }
    }
}
