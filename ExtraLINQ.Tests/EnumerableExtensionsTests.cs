using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ExtraLinq.Tests
{
    public class EnumerableExtensionsTests
    {
        [TestFixture]
        public class TheCountsExactlyMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.CountsExactly(1);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
            {
                IEnumerable<char> letters = "abcde";

                letters.CountsExactly(-10);
            }

            [Test]
            public void ReturnsTrueWhenActualCountEqualsExpectedCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsExactly(4).Should().BeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsExactly(4).Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenActualCountDoesNotEqualExpectedCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsExactly(100).Should().BeFalse();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsExactly(100).Should().BeFalse();
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.CountsExactly(1, alwaysTruePredicate);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<char> validCollection = "abcd";

                validCollection.CountsExactly(1, null);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegativeAndPredicateIsValid()
            {
                IEnumerable<char> letters = "abcde";
                Func<char, bool> alwaysTruePredicate = _ => true;

                letters.CountsExactly(-10, alwaysTruePredicate);
            }

            [Test]
            public void ReturnsTrueWhenActualCountEqualsExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

                fruits.CountsExactly(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
                fruits.CountsExactly(1, fruit => fruit.StartsWith("b")).Should().BeTrue();
                fruits.CountsExactly(0, fruit => fruit.StartsWith("c")).Should().BeTrue();
            }

            [Test]
            public void ReturnsTrueWhenActualCountDoesNotEqualExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

                fruits.CountsExactly(1, fruit => fruit.StartsWith("a")).Should().BeFalse();
                fruits.CountsExactly(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
                fruits.CountsExactly(10, fruit => fruit.StartsWith("c")).Should().BeFalse();
            }
        }

        [TestFixture]
        public class TheCountsMaxMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.CountsMax(1);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
            {
                IEnumerable<char> letters = "abcde";

                letters.CountsMax(-10);
            }

            [Test]
            public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCount()
            {
                IEnumerable<char> letters = "abcd";
                IEnumerable<char> emptyCollection = Enumerable.Empty<char>();

                letters.CountsMax(4).Should().BeTrue();
                letters.CountsMax(5).Should().BeTrue();
                emptyCollection.CountsMax(0).Should().BeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMax(4).Should().BeTrue();
                letters.ToList().CountsMax(5).Should().BeTrue();
                emptyCollection.ToList().CountsMax(0).Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenActualCountIsGreaterThanExpectedMaxCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMax(4).Should().BeTrue();
                letters.CountsMax(5).Should().BeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMax(4).Should().BeTrue();
                letters.ToList().CountsMax(5).Should().BeTrue();
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.CountsMax(1, alwaysTruePredicate);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<char> validCollection = "abcd";

                validCollection.CountsMax(1, null);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMaxCountIsNegative()
            {
                IEnumerable<char> validCollection = "abcd";
                Func<char, bool> validPredicate = c => c == 'a';

                validCollection.CountsMax(-1, validPredicate);
            }

            [Test]
            public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
                Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

                fruits.CountsMax(2, startsWithLowercasedA).Should().BeTrue();
                fruits.CountsMax(3, startsWithLowercasedA).Should().BeTrue();
                fruits.CountsMax(int.MaxValue, startsWithLowercasedA).Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenActualCountHigherThanExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
                Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

                fruits.CountsMax(0, startsWithLowercasedA).Should().BeFalse();
                fruits.CountsMax(1, startsWithLowercasedA).Should().BeFalse();
            }
        }

        [TestFixture]
        public class TheCountsMinMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.CountsMin(1);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegative()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMin(-1);
            }

            [Test]
            public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMin(0).Should().BeTrue();
                letters.CountsMin(2).Should().BeTrue();
                letters.CountsMin(4).Should().BeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMin(0).Should().BeTrue();
                letters.ToList().CountsMin(2).Should().BeTrue();
                letters.ToList().CountsMin(4).Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMin(5).Should().BeFalse();
                letters.CountsMin(10).Should().BeFalse();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMin(5).Should().BeFalse();
                letters.ToList().CountsMin(10).Should().BeFalse();
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.CountsMin(1, alwaysTruePredicate);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<char> validCollection = "abcd";
                Func<char, bool> nullPredicate = null;

                validCollection.CountsMin(1, nullPredicate);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegativeWithPredicate()
            {
                IEnumerable<char> letters = "abcd";
                Func<char, bool> validPredicate = c => c == 'a';

                letters.CountsMin(-1, validPredicate);
            }

            [Test]
            public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
                IEnumerable<string> emptyCollection = Enumerable.Empty<string>();

                fruits.CountsMin(1, fruit => fruit.StartsWith("a")).Should().BeTrue();
                fruits.CountsMin(2, fruit => fruit.StartsWith("a")).Should().BeTrue();
                fruits.CountsMin(1, fruit => fruit.StartsWith("b")).Should().BeTrue();

                emptyCollection.CountsMin(0, _ => true).Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

                fruits.CountsMin(3, fruit => fruit.StartsWith("a")).Should().BeFalse();
                fruits.CountsMin(2, fruit => fruit.StartsWith("b")).Should().BeFalse();
            }
        }

        [TestFixture]
        public class TheElementAtMethod
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

        [TestFixture]
        public class TheIntersperseMethod
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

        [TestFixture]
        public class TheIsEmptyMethod
        {
            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.IsEmpty();
            }
        }

        [TestFixture]
        public class TheIsNullOrEmptyMethod
        {
            [Test]
            public void ReturnsTrueWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                bool isNullOrEmpty = nullCollection.IsNullOrEmpty();

                isNullOrEmpty.Should().BeTrue();
            }

            [Test]
            public void ReturnsTrueWhenCollectionIsEmpty()
            {
                IEnumerable<string> emptyCollection = new string[0];

                bool isNullOrEmpty = emptyCollection.IsNullOrEmpty();

                isNullOrEmpty.Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenCollectionContainsElements()
            {
                IEnumerable<string> collectionContainingOneElements = new[] { "test" };

                collectionContainingOneElements.IsNullOrEmpty().Should().BeFalse();
            }
        }

        [TestFixture]
        public class TheNoneMethod
        {
            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.None(alwaysTruePredicate);
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<object> validCollection = new List<string> { string.Empty };
                Func<object, bool> nullPredicate = null;

                validCollection.None(nullPredicate);
            }

            [Test]
            public void ReturnsTrueWhenNoItemMatches()
            {
                IEnumerable<string> validCollection = new List<string> { string.Empty };
                Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

                bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

                noItemMatching.Should().BeTrue();
            }

            [Test]
            public void ReturnsFalseWhenAtLeastOneItemMatches()
            {
                IEnumerable<string> validCollection = new List<string> { "Non-empty string" };
                Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

                bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

                noItemMatching.Should().BeFalse();
            }
        }

        [TestFixture]
        public class TheRandomMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Random();
            }

            [Test]
            public void ReturnsItemContainedWithinCollection()
            {
                IEnumerable<char> letters = "abcde";

                char randomCharacter = letters.Random();

                letters.Should().Contain(randomCharacter);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
            {
                IEnumerable<char> nullCollection = null;
                Random randomNumberGenerator = new Random();

                nullCollection.Random(randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenRandomIsNull()
            {
                IEnumerable<char> letters = "abcde";
                Random nullRandomNumberGenerator = null;

                letters.Random(nullRandomNumberGenerator);
            }

            [Test]
            public void ReturnsItemContainedWithinCollectionWithRandom()
            {
                IEnumerable<char> letters = "abcde";
                const int arbitrarySeed = 1337;
                Random randomNumberGenerator = new Random(arbitrarySeed);
                const char expectedCharacter = 'b';

                char randomCharacter = letters.Random(randomNumberGenerator);

                randomCharacter.Should().Be(expectedCharacter);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCount()
            {
                IEnumerable<char> nullCollection = null;
                const int validItemCount = 0;

                nullCollection.Random(validItemCount);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegative()
            {
                IEnumerable<char> letters = "abcde";
                const int invalidItemCount = -5;

                letters.Random(invalidItemCount);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenCountIsGreaterThanCollectionCount()
            {
                IEnumerable<char> letters = "abcde";
                const int invalidItemCount = 100;

                letters.Random(invalidItemCount);
            }

            [Test]
            public void ReturnsItemsContainedWithinCollection()
            {
                IEnumerable<char> letters = "abcde";

                IEnumerable<char> threeRandomLetters = letters.Random(3);

                foreach (char letter in threeRandomLetters)
                {
                    letters.Should().Contain(letter);
                }
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandomAndCount()
            {
                IEnumerable<char> nullCollection = null;
                const int validItemCount = 0;
                Random randomNumberGenerator = new Random();

                nullCollection.Random(validItemCount, randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [Test]
            public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegativeWithRandomAndCount()
            {
                IEnumerable<char> letters = "abcde";
                const int negativeElementsCount = -5;
                Random randomNumberGenerator = new Random();

                letters.Random(negativeElementsCount, randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenRandomIsNullWithRandomAndCount()
            {
                IEnumerable<char> letters = "abcde";
                const int negativeElementsCount = 2;
                Random nullRandomNumberGenerator = null;

                letters.Random(negativeElementsCount, nullRandomNumberGenerator);
            }

            [Test]
            public void ReturnsItemsContainedWithinCollectionWithRandomAndCount()
            {
                IEnumerable<char> letters = "abcde";
                const int arbitrarySeed = 1337;
                Random randomNumberGenerator = new Random(arbitrarySeed);

                IEnumerable<char> threeRandomCharacters = letters.Random(3, randomNumberGenerator);
                char[] threeRandomCharactersArray = threeRandomCharacters.ToArray();

                threeRandomCharactersArray.Should().Equal(new[] { 'b', 'a', 'c' });
            }
        }

        [TestFixture]
        public class TheShuffleMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Shuffle();
            }

            [Test]
            public void OnlyReturnsItemsContainedWithinCollection()
            {
                IEnumerable<char> letters = "abcde";

                IEnumerable<char> shuffledLetters = letters.Shuffle();

                foreach (char letter in shuffledLetters)
                {
                    letters.Should().Contain(letter);
                }
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
            {
                IEnumerable<char> nullCollection = null;
                Random randomNumberGenerator = new Random();

                nullCollection.Shuffle(randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenRandomIsNull()
            {
                IEnumerable<char> letters = "abcde";
                Random nullRandomNumberGenerator = null;

                letters.Shuffle(nullRandomNumberGenerator);
            }

            [Test]
            public void OnlyReturnsItemsContainedWithinCollectionWithRandom()
            {
                IEnumerable<char> letters = "abcde";
                const int arbitrarySeed = 1337;
                Random randomNumberGenerator = new Random(arbitrarySeed);

                IEnumerable<char> shuffledLetters = letters.Shuffle(randomNumberGenerator);
                char[] shuffledLettersArray = shuffledLetters.ToArray();

                shuffledLettersArray.Should().Equal(new[] { 'b', 'a', 'c', 'e', 'd' });
            }
        }

        [TestFixture]
        public class TheWithoutMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Without('c');
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNull()
            {
                IEnumerable<char> letters = "abcd";
                IEnumerable<char> itemsToRemove = null;

                letters.Without(itemsToRemove);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithArray()
            {
                IEnumerable<char> letters = "abcd";
                char[] itemsToRemove = null;

                letters.Without(itemsToRemove);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCollection()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Without(new List<char> { 'c' });
            }

            [Test]
            public void ReturnsCollectionWithoutSpecifiedItem()
            {
                IEnumerable<char> letters = "abcd";
                const char letterToRemove = 'a';

                letters = letters.Without(letterToRemove);

                letters.Should().NotContain('a');
                letters.Count().Should().Be(3);
            }

            [Test]
            public void ReturnsCollectionWithoutSpecifiedItems()
            {
                IEnumerable<char> letters = "abcd";

                letters = letters.Without(new[] { 'a', 'c' });

                letters.Should().NotContain('a');
                letters.Should().NotContain('c');
                letters.Count().Should().Be(2);
            }

            [Test]
            public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItem()
            {
                IEnumerable<char> letters = "abcd";
                const char letterToRemove = 'z';

                letters = letters.Without(letterToRemove);

                letters.Count().Should().Be(4);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithEqualityComparer()
            {
                IEnumerable<char> nullCollection = null;
                IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

                nullCollection.Without(stringLengthEqualityComparer, 'c');
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithEqualityComparer()
            {
                IEnumerable<char> letters = "abcd";
                IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

                letters.Without(stringLengthEqualityComparer, null);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [Test]
            public void ThrowsArgumentNullExceptionWhenEqualityComparerIsNull()
            {
                IEnumerable<char> letters = "abcd";
                IEqualityComparer<char> nullEqualityComparer = null;

                letters.Without(nullEqualityComparer, 'c');
            }

            [Test]
            public void ReturnsCollectionWithoutItemsEqualToPassedItem()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
                const string itemToRemove = "banana";
                IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

                fruits = fruits.Without(stringLengthEqualityComparer, itemToRemove);

                fruits.Should().NotContain("banana");
                fruits.Should().NotContain("cherry");
                fruits.Should().HaveCount(2);
            }

            [Test]
            public void DoesNotRemoveItemsThatDoNotMatchThePassedItemButEachOther()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
                const string itemToRemove = "apricot";
                IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

                IEnumerable<string> fruitsWithoutItem = fruits.Without(stringLengthEqualityComparer, itemToRemove);
                fruitsWithoutItem.Should().NotContain("apricot");
                fruitsWithoutItem.Should().HaveCount(3);
            }

            [Test]
            public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItemWithEqualityComparer()
            {
                IEnumerable<string> stringNumbers = new[] { "1", "22", "333", "4444" };
                const string itemToRemove = "55555";
                IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

                stringNumbers = stringNumbers.Without(stringLengthEqualityComparer, itemToRemove);

                stringNumbers.Count().Should().Be(4);
            }
        }
    }
}
