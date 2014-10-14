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
            var passedArguments = new List<int>();
            Action<int> action = passedArguments.Add;

            numbers.Pipe(action).ToList();

            passedArguments.Should().Equal(numbers);
        }

        [Fact]
        public void ReturnsAllElementsOfTheSourceSequenceWithoutModification()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int> action = Console.WriteLine;

            IEnumerable<int> returnedSequence = numbers.Pipe(action);

            returnedSequence.Should().Equal(numbers);
        }
    }
}
