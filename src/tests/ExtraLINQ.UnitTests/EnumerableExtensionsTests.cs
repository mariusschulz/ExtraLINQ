using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace ExtraLinq.UnitTests
{
    public class EnumerableExtensionsTests
    {
        [TestClass]
        public class TheCountsExactlyMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.CountsExactly(1);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
            {
                IEnumerable<char> letters = "abcde";

                letters.CountsExactly(-10);
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountEqualsExpectedCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsExactly(4).ShouldBeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsExactly(4).ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenActualCountDoesNotEqualExpectedCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsExactly(100).ShouldBeFalse();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsExactly(100).ShouldBeFalse();
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.CountsExactly(1, alwaysTruePredicate);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<char> validCollection = "abcd";

                validCollection.CountsExactly(1, null);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegativeAndPredicateIsValid()
            {
                IEnumerable<char> letters = "abcde";
                Func<char, bool> alwaysTruePredicate = _ => true;

                letters.CountsExactly(-10, alwaysTruePredicate);
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountEqualsExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

                fruits.CountsExactly(2, fruit => fruit.StartsWith("a")).ShouldBeTrue();
                fruits.CountsExactly(1, fruit => fruit.StartsWith("b")).ShouldBeTrue();
                fruits.CountsExactly(0, fruit => fruit.StartsWith("c")).ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountDoesNotEqualExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

                fruits.CountsExactly(1, fruit => fruit.StartsWith("a")).ShouldBeFalse();
                fruits.CountsExactly(2, fruit => fruit.StartsWith("b")).ShouldBeFalse();
                fruits.CountsExactly(10, fruit => fruit.StartsWith("c")).ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheCountsMaxMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.CountsMax(1);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedCountIsNegative()
            {
                IEnumerable<char> letters = "abcde";

                letters.CountsMax(-10);
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCount()
            {
                IEnumerable<char> letters = "abcd";
                IEnumerable<char> emptyCollection = Enumerable.Empty<char>();

                letters.CountsMax(4).ShouldBeTrue();
                letters.CountsMax(5).ShouldBeTrue();
                emptyCollection.CountsMax(0).ShouldBeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMax(4).ShouldBeTrue();
                letters.ToList().CountsMax(5).ShouldBeTrue();
                emptyCollection.ToList().CountsMax(0).ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenActualCountIsGreaterThanExpectedMaxCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMax(4).ShouldBeTrue();
                letters.CountsMax(5).ShouldBeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMax(4).ShouldBeTrue();
                letters.ToList().CountsMax(5).ShouldBeTrue();
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.CountsMax(1, alwaysTruePredicate);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<char> validCollection = "abcd";

                validCollection.CountsMax(1, null);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMaxCountIsNegative()
            {
                IEnumerable<char> validCollection = "abcd";
                Func<char, bool> validPredicate = c => c == 'a';

                validCollection.CountsMax(-1, validPredicate);
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountIsEqualToOrLowerThanExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
                Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

                fruits.CountsMax(2, startsWithLowercasedA).ShouldBeTrue();
                fruits.CountsMax(3, startsWithLowercasedA).ShouldBeTrue();
                fruits.CountsMax(int.MaxValue, startsWithLowercasedA).ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenActualCountHigherThanExpectedCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
                Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

                fruits.CountsMax(0, startsWithLowercasedA).ShouldBeFalse();
                fruits.CountsMax(1, startsWithLowercasedA).ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheCountsMinMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.CountsMin(1);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegative()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMin(-1);
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMin(0).ShouldBeTrue();
                letters.CountsMin(2).ShouldBeTrue();
                letters.CountsMin(4).ShouldBeTrue();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMin(0).ShouldBeTrue();
                letters.ToList().CountsMin(2).ShouldBeTrue();
                letters.ToList().CountsMin(4).ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCount()
            {
                IEnumerable<char> letters = "abcd";

                letters.CountsMin(5).ShouldBeFalse();
                letters.CountsMin(10).ShouldBeFalse();

                // Test ICollection.Count early exit strategy
                letters.ToList().CountsMin(5).ShouldBeFalse();
                letters.ToList().CountsMin(10).ShouldBeFalse();
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithPredicate()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.CountsMin(1, alwaysTruePredicate);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<char> validCollection = "abcd";
                Func<char, bool> nullPredicate = null;

                validCollection.CountsMin(1, nullPredicate);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenExpectedMinCountIsNegativeWithPredicate()
            {
                IEnumerable<char> letters = "abcd";
                Func<char, bool> validPredicate = c => c == 'a';

                letters.CountsMin(-1, validPredicate);
            }

            [TestMethod]
            public void ReturnsTrueWhenActualCountIsGreaterThanOrEqualToExpectedMinCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
                IEnumerable<string> emptyCollection = Enumerable.Empty<string>();

                fruits.CountsMin(1, fruit => fruit.StartsWith("a")).ShouldBeTrue();
                fruits.CountsMin(2, fruit => fruit.StartsWith("a")).ShouldBeTrue();
                fruits.CountsMin(1, fruit => fruit.StartsWith("b")).ShouldBeTrue();

                emptyCollection.CountsMin(0, _ => true).ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenActualCountIsLowerThanExpectedMinCountWithPredicate()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };

                fruits.CountsMin(3, fruit => fruit.StartsWith("a")).ShouldBeFalse();
                fruits.CountsMin(2, fruit => fruit.StartsWith("b")).ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheElementAtMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.ElementAt(1, IndexingStrategy.Regular);
            }

            [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenCollectionIsEmptyWithClampStrategy()
            {
                IEnumerable<char> letters = new char[0];

                letters.ElementAt(0, IndexingStrategy.Clamp);
            }

            [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenCollectionIsEmptyWithCyclicStrategy()
            {
                IEnumerable<char> letters = new char[0];

                letters.ElementAt(0, IndexingStrategy.Cyclic);
            }

            [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenCollectionIsEmptyWithRegularStrategy()
            {
                IEnumerable<char> letters = new char[0];

                letters.ElementAt(0, IndexingStrategy.Regular);
            }

            [TestMethod]
            public void ReturnsFirstElementWhenIndexIsNegativeWithClampStrategy()
            {
                IEnumerable<char> letters = "abcd";

                char characterForNegativeIndex = letters.ElementAt(-10, IndexingStrategy.Clamp);

                characterForNegativeIndex.ShouldEqual('a');
            }

            [TestMethod]
            public void ReturnsLastElementWhenIndexIsGreaterThanCountWithClampStrategy()
            {
                IEnumerable<char> letters = "abcd";

                char characterForNegativeIndex = letters.ElementAt(100, IndexingStrategy.Clamp);

                characterForNegativeIndex.ShouldEqual('d');
            }

            [TestMethod]
            public void ReturnsCorrectElementWithCyclicStrategy()
            {
                IEnumerable<char> letters = "abcd";

                letters.ElementAt(4, IndexingStrategy.Cyclic).ShouldEqual('a');
                letters.ElementAt(8, IndexingStrategy.Cyclic).ShouldEqual('a');
                letters.ElementAt(12, IndexingStrategy.Cyclic).ShouldEqual('a');

                letters.ElementAt(5, IndexingStrategy.Cyclic).ShouldEqual('b');
                letters.ElementAt(9, IndexingStrategy.Cyclic).ShouldEqual('b');
                letters.ElementAt(13, IndexingStrategy.Cyclic).ShouldEqual('b');

                letters.ElementAt(6, IndexingStrategy.Cyclic).ShouldEqual('c');
                letters.ElementAt(10, IndexingStrategy.Cyclic).ShouldEqual('c');
                letters.ElementAt(14, IndexingStrategy.Cyclic).ShouldEqual('c');

                letters.ElementAt(7, IndexingStrategy.Cyclic).ShouldEqual('d');
                letters.ElementAt(11, IndexingStrategy.Cyclic).ShouldEqual('d');
                letters.ElementAt(15, IndexingStrategy.Cyclic).ShouldEqual('d');

                // Int32.MaxValue = 2,147,483,647 and 2,147,483,647 % 4 = 3 ==> 'd'
                letters.ElementAt(Int32.MaxValue, IndexingStrategy.Cyclic).ShouldEqual('d');
            }
        }

        [TestClass]
        public class TheIntersperseMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<string> nullCollection = null;

                nullCollection.Intersperse("c").ToArray();
            }

            [TestMethod]
            public void InsertsSeparatorCorrectly()
            {
                int[] numbers = new[] { 1, 2, 3, 4, 5 };
                int[] expectedNumbers = new[] { 1, 0, 2, 0, 3, 0, 4, 0, 5 };

                int[] separatedNumbers = numbers.Intersperse(0).ToArray();

                separatedNumbers.ShouldEqual(expectedNumbers);
            }
        }

        [TestClass]
        public class TheIsEmptyMethod
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                nullCollection.IsEmpty();
            }
        }

        [TestClass]
        public class TheIsNullOrEmptyMethod
        {
            [TestMethod]
            public void ReturnsTrueWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;

                bool isNullOrEmpty = nullCollection.IsNullOrEmpty();

                isNullOrEmpty.ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsTrueWhenCollectionIsEmpty()
            {
                IEnumerable<string> emptyCollection = new string[0];

                bool isNullOrEmpty = emptyCollection.IsNullOrEmpty();

                isNullOrEmpty.ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenCollectionContainsElements()
            {
                IEnumerable<string> collectionContainingOneElements = new[] { "test" };

                collectionContainingOneElements.IsNullOrEmpty().ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheNoneMethod
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<object> nullCollection = null;
                Func<object, bool> alwaysTruePredicate = _ => true;

                nullCollection.None(alwaysTruePredicate);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullExceptionWhenPredicateIsNull()
            {
                IEnumerable<object> validCollection = new List<string> { string.Empty };
                Func<object, bool> nullPredicate = null;

                validCollection.None(nullPredicate);
            }

            [TestMethod]
            public void ReturnsTrueWhenNoItemMatches()
            {
                IEnumerable<string> validCollection = new List<string> { string.Empty };
                Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

                bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

                noItemMatching.ShouldBeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenAtLeastOneItemMatches()
            {
                IEnumerable<string> validCollection = new List<string> { "Non-empty string" };
                Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

                bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

                noItemMatching.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheRandomMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Random();
            }

            [TestMethod]
            public void ReturnsItemContainedWithinCollection()
            {
                IEnumerable<char> letters = "abcde";

                char randomCharacter = letters.Random();

                letters.ShouldContain(randomCharacter);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
            {
                IEnumerable<char> nullCollection = null;
                Random randomNumberGenerator = new Random();

                nullCollection.Random(randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenRandomIsNull()
            {
                IEnumerable<char> letters = "abcde";
                Random nullRandomNumberGenerator = null;

                letters.Random(nullRandomNumberGenerator);
            }

            [TestMethod]
            public void ReturnsItemContainedWithinCollectionWithRandom()
            {
                IEnumerable<char> letters = "abcde";
                const int arbitrarySeed = 1337;
                Random randomNumberGenerator = new Random(arbitrarySeed);
                const char expectedCharacter = 'b';

                char randomCharacter = letters.Random(randomNumberGenerator);

                randomCharacter.ShouldEqual(expectedCharacter);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCount()
            {
                IEnumerable<char> nullCollection = null;
                const int validItemCount = 0;

                nullCollection.Random(validItemCount);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegative()
            {
                IEnumerable<char> letters = "abcde";
                const int invalidItemCount = -5;

                letters.Random(invalidItemCount);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenCountIsGreaterThanCollectionCount()
            {
                IEnumerable<char> letters = "abcde";
                const int invalidItemCount = 100;

                letters.Random(invalidItemCount);
            }

            [TestMethod]
            public void ReturnsItemsContainedWithinCollection()
            {
                IEnumerable<char> letters = "abcde";

                IEnumerable<char> threeRandomLetters = letters.Random(3);

                foreach (char letter in threeRandomLetters)
                {
                    letters.ShouldContain(letter);
                }
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandomAndCount()
            {
                IEnumerable<char> nullCollection = null;
                const int validItemCount = 0;
                Random randomNumberGenerator = new Random();

                nullCollection.Random(validItemCount, randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [TestMethod]
            public void ThrowsArgumentOutOfRangeExceptionWhenCountIsNegativeWithRandomAndCount()
            {
                IEnumerable<char> letters = "abcde";
                const int negativeElementsCount = -5;
                Random randomNumberGenerator = new Random();

                letters.Random(negativeElementsCount, randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenRandomIsNullWithRandomAndCount()
            {
                IEnumerable<char> letters = "abcde";
                const int negativeElementsCount = 2;
                Random nullRandomNumberGenerator = null;

                letters.Random(negativeElementsCount, nullRandomNumberGenerator);
            }

            [TestMethod]
            public void ReturnsItemsContainedWithinCollectionWithRandomAndCount()
            {
                IEnumerable<char> letters = "abcde";
                const int arbitrarySeed = 1337;
                Random randomNumberGenerator = new Random(arbitrarySeed);

                IEnumerable<char> threeRandomCharacters = letters.Random(3, randomNumberGenerator);
                char[] threeRandomCharactersArray = threeRandomCharacters.ToArray();

                threeRandomCharactersArray.ShouldEqual(new[] { 'b', 'a', 'c' });
            }
        }

        [TestClass]
        public class TheShuffleMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Shuffle();
            }

            [TestMethod]
            public void OnlyReturnsItemsContainedWithinCollection()
            {
                IEnumerable<char> letters = "abcde";

                IEnumerable<char> shuffledLetters = letters.Shuffle();

                foreach (char letter in shuffledLetters)
                {
                    letters.ShouldContain(letter);
                }
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithRandom()
            {
                IEnumerable<char> nullCollection = null;
                Random randomNumberGenerator = new Random();

                nullCollection.Shuffle(randomNumberGenerator);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenRandomIsNull()
            {
                IEnumerable<char> letters = "abcde";
                Random nullRandomNumberGenerator = null;

                letters.Shuffle(nullRandomNumberGenerator);
            }

            [TestMethod]
            public void OnlyReturnsItemsContainedWithinCollectionWithRandom()
            {
                IEnumerable<char> letters = "abcde";
                const int arbitrarySeed = 1337;
                Random randomNumberGenerator = new Random(arbitrarySeed);

                IEnumerable<char> shuffledLetters = letters.Shuffle(randomNumberGenerator);
                char[] shuffledLettersArray = shuffledLetters.ToArray();

                shuffledLettersArray.ShouldEqual(new[] { 'b', 'a', 'c', 'e', 'd' });
            }
        }

        [TestClass]
        public class TheWithoutMethod
        {
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNull()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Without('c');
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNull()
            {
                IEnumerable<char> letters = "abcd";
                IEnumerable<char> itemsToRemove = null;

                letters.Without(itemsToRemove);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithArray()
            {
                IEnumerable<char> letters = "abcd";
                char[] itemsToRemove = null;

                letters.Without(itemsToRemove);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithCollection()
            {
                IEnumerable<char> nullCollection = null;

                nullCollection.Without(new List<char> { 'c' });
            }

            [TestMethod]
            public void ReturnsCollectionWithoutSpecifiedItem()
            {
                IEnumerable<char> letters = "abcd";
                const char letterToRemove = 'a';

                letters = letters.Without(letterToRemove);

                letters.ShouldNotContain('a');
                letters.Count().ShouldEqual(3);
            }

            [TestMethod]
            public void ReturnsCollectionWithoutSpecifiedItems()
            {
                IEnumerable<char> letters = "abcd";

                letters = letters.Without(new[] { 'a', 'c' });

                letters.ShouldNotContain('a');
                letters.ShouldNotContain('c');
                letters.Count().ShouldEqual(2);
            }

            [TestMethod]
            public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItem()
            {
                IEnumerable<char> letters = "abcd";
                const char letterToRemove = 'z';

                letters = letters.Without(letterToRemove);

                letters.Count().ShouldEqual(4);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenCollectionIsNullWithEqualityComparer()
            {
                IEnumerable<char> nullCollection = null;
                IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

                nullCollection.Without(stringLengthEqualityComparer, 'c');
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenItemsToRemoveCollectionIsNullWithEqualityComparer()
            {
                IEnumerable<char> letters = "abcd";
                IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

                letters.Without(stringLengthEqualityComparer, null);
            }

            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void ThrowsArgumentNullExceptionWhenEqualityComparerIsNull()
            {
                IEnumerable<char> letters = "abcd";
                IEqualityComparer<char> nullEqualityComparer = null;

                letters.Without(nullEqualityComparer, 'c');
            }

            [TestMethod]
            public void ReturnsCollectionWithoutItemsEqualToPassedItem()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
                const string itemToRemove = "banana";
                IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

                fruits = fruits.Without(stringLengthEqualityComparer, itemToRemove);

                fruits.ShouldNotContain("banana");
                fruits.ShouldNotContain("cherry");
                fruits.Count().ShouldEqual(2);
            }

            [TestMethod]
            public void DoesNotRemoveItemsThatDoNotMatchThePassedItemButEachOther()
            {
                IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
                const string itemToRemove = "apricot";
                IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

                IEnumerable<string> fruitsWithoutItem = fruits.Without(stringLengthEqualityComparer, itemToRemove);
                fruitsWithoutItem.ShouldNotContain("apricot");
                fruitsWithoutItem.Count().ShouldEqual(3);
            }

            [TestMethod]
            public void ReturnsUnmodifiedCollectionWhenCollectionDoesNotContainItemWithEqualityComparer()
            {
                IEnumerable<string> stringNumbers = new[] { "1", "22", "333", "4444" };
                const string itemToRemove = "55555";
                IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

                stringNumbers = stringNumbers.Without(stringLengthEqualityComparer, itemToRemove);

                stringNumbers.Count().ShouldEqual(4);
            }
        }
    }
}
