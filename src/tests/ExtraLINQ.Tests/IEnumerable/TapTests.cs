using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ExtraLinq.Tests
{
    public class TapTests
    {
        [Fact]
        public void Throws_ArgumentNullException_when_sequence_is_null()
        {
            IEnumerable<object> nullSequence = null;
            Action<object> doNothing = _ => { };

            Assert.Throws<ArgumentNullException>(() => nullSequence.Tap(doNothing));
        }

        [Fact]
        public void Throws_ArgumentNullException_when_action_is_null()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int> action = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Tap(action));
        }

        [Fact]
        public void Calls_the_specified_action_for_each_element_when_enumerated()
        {
            int[] numbers = { 1, 2, 3 };
            var elementsPassedToAction = new List<int>();
            Action<int> action = elementsPassedToAction.Add;

            numbers.Tap(action).ToList();

            elementsPassedToAction.Should().Equal(numbers);
        }

        [Fact]
        public void Returns_all_elements_of_the_source_sequence_without_modification()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int> action = Console.WriteLine;

            IEnumerable<int> returnedSequence = numbers.Tap(action);

            returnedSequence.Should().Equal(numbers);
        }

        [Fact]
        public void Throws_ArgumentNullException_when_sequence_is_null_for_action_with_index()
        {
            IEnumerable<object> nullSequence = null;
            Action<object, int> doNothing = (element, index) => { };

            Assert.Throws<ArgumentNullException>(() => nullSequence.Tap(doNothing));
        }

        [Fact]
        public void Throws_ArgumentNullException_when_action_with_index_is_null()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int, int> nullAction = null;

            Assert.Throws<ArgumentNullException>(() => numbers.Tap(nullAction));
        }

        [Fact]
        public void Calls_the_specified_action_with_index_for_each_element_when_enumerated()
        {
            int[] numbers = { 3, 4, 5 };
            var elementsPassedToAction = new List<int>();
            var indicesPassedToAction = new List<int>();
            Action<int, int> action = (element, index) =>
            {
                elementsPassedToAction.Add(element);
                indicesPassedToAction.Add(index);
            };

            numbers.Tap(action).ToList();

            elementsPassedToAction.Should().Equal(numbers);
            indicesPassedToAction.Should().Equal(new[] { 0, 1, 2 });
        }

        [Fact]
        public void Returns_all_elements_of_the_source_sequence_without_modification_for_action_with_index()
        {
            int[] numbers = { 1, 2, 3 };
            Action<int, int> action = (element, index) => Console.WriteLine(element);

            IEnumerable<int> returnedSequence = numbers.Tap(action);

            returnedSequence.Should().Equal(numbers);
        }
    }
}
