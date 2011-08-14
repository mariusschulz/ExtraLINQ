using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace ExtraLINQ.UnitTests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region CountsExactly()

        #region CountsExactly<TSource>(IEnumerable<TSource>, int)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsExactly_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.CountsExactly(1);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        [TestMethod]
        public void CountsExactly_NegativeExpectedCount_ThrowsArgumentException()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();

            letters.CountsExactly(-10);
        }

        [TestMethod]
        public void CountsExactly_MatchingActualAndExpectedItemCount_ReturnsTrue()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersCountEquals4 = letters.CountsExactly(4);

            lettersCountEquals4.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsExactly_DifferentActualAndExpectedItemCount_ReturnsTrue()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersCountEquals100 = letters.CountsExactly(100);

            lettersCountEquals100.ShouldBeFalse();
        }

        #endregion

        #region CountsExactly<TSource>(IEnumerable<TSource>, int, Func<TSource, bool>)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsExactly_NullCollectionValidPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.CountsExactly(1, alwaysTruePredicate);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsExactly_ValidCollectionNullPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<char> validCollection = "abcd".ToCharArray();
            Func<char, bool> nullPredicate = null;

            validCollection.CountsExactly(1, nullPredicate);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        [TestMethod]
        public void CountsExactly_NegativeExpectedCountValidPredicate_ThrowsArgumentException()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();
            Func<char, bool> alwaysTruePredicate = _ => true;

            letters.CountsExactly(-10, alwaysTruePredicate);
        }

        [TestMethod]
        public void CountsExactly_CollectionContainingOneMatchingItemExpectingOneMatchingItem_ReturnsTrue()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            bool collectionContainsOneMatchingItem = fruits.CountsExactly(2, startsWithLowercasedA);

            collectionContainsOneMatchingItem.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsExactly_CollectionContainingOneMatchingItemExpectingTwoMatchingItems_ReturnsFalse()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedB = fruit => fruit.StartsWith("b");

            bool collectionContainsOneMatchingItem = fruits.CountsExactly(2, startsWithLowercasedB);

            collectionContainsOneMatchingItem.ShouldBeFalse();
        }

        #endregion

        #endregion

        #region CountsMax()

        #region CountsMax<TSource>(IEnumerable<TSource>, int)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsMax_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.CountsMax(1);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        [TestMethod]
        public void CountsMax_NegativeExpectedMaxItemCount_ThrowsArgumentException()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();

            letters.CountsMax(-10);
        }

        [TestMethod]
        public void CountsMax_ActualItemCountEqualsExpectedMaxItemCount_ReturnsTrue()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersContainsAtMost4Items = letters.CountsMax(4);
            bool lettersContainsAtMost5Items = letters.CountsMax(5);

            lettersContainsAtMost4Items.ShouldBeTrue();
            lettersContainsAtMost5Items.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsMax_ActualItemCountGreaterThanExpectedMinItemCount_ReturnsFalse()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersContainsAtMost2Items = letters.CountsMax(2);

            lettersContainsAtMost2Items.ShouldBeFalse();
        }

        #endregion

        #region CountsMax<TSource>(IEnumerable<TSource>, int, Func<TSource, bool>)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsMax_NullCollectionValidPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.CountsMax(1, alwaysTruePredicate);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsMax_ValidCollectionNullPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<char> validCollection = "abcd".ToCharArray();
            Func<char, bool> nullPredicate = null;

            validCollection.CountsMax(1, nullPredicate);
        }

        [TestMethod]
        public void CountsMax_CollectionContainingOneMatchingItemExpectingAtMostTwoMatchingItems_ReturnsTrue()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            bool collectionContainsAtMostTwoMatchingItems = fruits.CountsMax(2, startsWithLowercasedA);
            bool collectionContainsAtMostThreeMatchingItems = fruits.CountsMax(3, startsWithLowercasedA);

            collectionContainsAtMostTwoMatchingItems.ShouldBeTrue();
            collectionContainsAtMostThreeMatchingItems.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsMax_CollectionContainingTwoMatchingItemsExpectingAtMostOneMatchingItem_ReturnsFalse()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            bool collectionContainsAtMostOneMatchingItem = fruits.CountsMax(1, startsWithLowercasedA);

            collectionContainsAtMostOneMatchingItem.ShouldBeFalse();
        }

        #endregion

        #endregion

        #region CountsMin()

        #region CountsMin<TSource>(IEnumerable<TSource>, int)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsMin_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.CountsMin(1);
        }

        [TestMethod]
        public void CountsMin_ActualItemCountGreaterThanOrEqualsToExpectedMinItemCount_ReturnsTrue()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersContainsAtLeast2Items = letters.CountsMin(2);
            bool lettersContainsAtLeast4Items = letters.CountsMin(4);

            lettersContainsAtLeast2Items.ShouldBeTrue();
            lettersContainsAtLeast4Items.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsMin_ActualItemCountLowerThanExpectedMinItemCount_ReturnsFalse()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersContainsAtLeast10Items = letters.CountsMin(10);

            lettersContainsAtLeast10Items.ShouldBeFalse();
        }

        #endregion

        #region CountsMin<TSource>(IEnumerable<TSource>, int, Func<TSource, bool>)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsMin_NullCollectionValidPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.CountsMin(1, alwaysTruePredicate);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void CountsMin_ValidCollectionNullPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<char> validCollection = "abcd".ToCharArray();
            Func<char, bool> nullPredicate = null;

            validCollection.CountsMin(1, nullPredicate);
        }

        [TestMethod]
        public void CountsMin_CollectionContainingOneMatchingItemExpectingAtLeastOneMatchingItem_ReturnsTrue()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedA = fruit => fruit.StartsWith("a");

            bool collectionContainsAtLeastOneMatchingItem = fruits.CountsMin(1, startsWithLowercasedA);
            bool collectionContainsAtLeastTwoMatchingItems = fruits.CountsMin(2, startsWithLowercasedA);

            collectionContainsAtLeastOneMatchingItem.ShouldBeTrue();
            collectionContainsAtLeastTwoMatchingItems.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsMin_CollectionContainingOneMatchingItemExpectingAtLeastTwoMatchingItems_ReturnsFalse()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedB = fruit => fruit.StartsWith("b");

            bool collectionContainsAtLeastTwoMatchingItems = fruits.CountsMin(2, startsWithLowercasedB);

            collectionContainsAtLeastTwoMatchingItems.ShouldBeFalse();
        }

        #endregion

        #endregion

        #region ElementAt()

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void ElementAt_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.ElementAt(1, IndexingStrategy.Regular);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        [TestMethod]
        public void ElementAt_ClampIndexingStrategyEmptyCollection_ThrowsArgumentOutOfRangeException()
        {
            IEnumerable<char> letters = new char[0];

            letters.ElementAt(0, IndexingStrategy.Clamp);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        [TestMethod]
        public void ElementAt_CyclicIndexingStrategyEmptyCollection_ThrowsArgumentOutOfRangeException()
        {
            IEnumerable<char> letters = new char[0];

            letters.ElementAt(0, IndexingStrategy.Cyclic);
        }

        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
        [TestMethod]
        public void ElementAt_RegularIndexingStrategyEmptyCollection_ThrowsArgumentOutOfRangeException()
        {
            IEnumerable<char> letters = new char[0];

            letters.ElementAt(0, IndexingStrategy.Regular);
        }

        [TestMethod]
        public void ElementAt_ClampStrategyNegativeIndex_ReturnsFirstElement()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            char characterForNegativeIndex = letters.ElementAt(-10, IndexingStrategy.Clamp);

            characterForNegativeIndex.ShouldEqual('a');
        }

        [TestMethod]
        public void ElementAt_ClampStrategyIndexGreaterThanItemCount_ReturnsLastElement()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            char characterForNegativeIndex = letters.ElementAt(100, IndexingStrategy.Clamp);

            characterForNegativeIndex.ShouldEqual('d');
        }

        [TestMethod]
        public void ElementAt_CyclicIndexingStrategyIndexGreaterThanItemCount()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

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
        }

        #endregion

        #region IsEmpty()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsEmpty_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;

            nullCollection.IsEmpty();
        }

        #endregion

        #region IsNullOrEmpty()

        [TestMethod]
        public void IsNullOrEmpty_Null_ReturnsTrue()
        {
            IEnumerable<object> nullCollection = null;

            bool isNullOrEmpty = nullCollection.IsNullOrEmpty();

            isNullOrEmpty.ShouldBeTrue();
        }

        [TestMethod]
        public void IsNullOrEmpty_EmptyCollection_ReturnsTrue()
        {
            IEnumerable<string> emptyCollection = new string[0];

            bool isNullOrEmpty = emptyCollection.IsNullOrEmpty();

            isNullOrEmpty.ShouldBeTrue();
        }

        [TestMethod]
        public void IsNullOrEmpty_CollectionContainingOneElement_ReturnsFalse()
        {
            IEnumerable<string> collectionContainingOneElements = new[] { "test" };

            bool isNullOrEmpty = collectionContainingOneElements.IsNullOrEmpty();

            isNullOrEmpty.ShouldBeFalse();
        }

        #endregion

        #region None()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void None_NullCollectionValidPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<object> nullCollection = null;
            Func<object, bool> alwaysTruePredicate = _ => true;

            nullCollection.None(alwaysTruePredicate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void None_ValidCollectionNullPredicate_ThrowsArgumentNullException()
        {
            IEnumerable<object> validCollection = new List<string> { string.Empty };
            Func<object, bool> nullPredicate = null;

            validCollection.None(nullPredicate);
        }

        [TestMethod]
        public void None_CollectionWithoutMatchingItem_ReturnsTrue()
        {
            IEnumerable<string> validCollection = new List<string> { string.Empty };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

            noItemMatching.ShouldBeTrue();
        }

        [TestMethod]
        public void None_CollectionWithMatchingItem_ReturnsFalse()
        {
            IEnumerable<string> validCollection = new List<string> { "Non-empty string" };
            Func<string, bool> stringLengthGreaterThanZero = item => item.Length > 0;

            bool noItemMatching = validCollection.None(stringLengthGreaterThanZero);

            noItemMatching.ShouldBeFalse();
        }

        #endregion

        #region Random()

        #region Random<TSource>(IEnumerable<TSource>)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Random_NullCollection_ThrowsArgumentNullExeption()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Random();
        }

        [TestMethod]
        public void Random_Empty_ReturnsValidCollectionElement()
        {
            IEnumerable<char> letters = "abcde";

            char randomCharacter = letters.Random();

            letters.ShouldContain(randomCharacter);
        }

        #endregion

        #region Random<TSource>(IEnumerable<TSource>, Random)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Random_NullCollectionValidRandom_ThrowsArgumentNullExeption()
        {
            IEnumerable<char> nullCollection = null;
            Random randomNumberGenerator = new Random();
            
            nullCollection.Random(randomNumberGenerator);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Random_ValidCollectionNullRandom_ThrowsArgumentNullExeption()
        {
            IEnumerable<char> letters = "abcde";
            Random nullRandomNumberGenerator = null;

            letters.Random(nullRandomNumberGenerator);
        }

        [TestMethod]
        public void Random_ValidParameters_ReturnsValidCollectionElement()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();
            Random randomNumberGenerator = new Random();

            char randomCharacter = letters.Random(randomNumberGenerator);

            letters.ShouldContain(randomCharacter);
        }

        #endregion

        #region Random<TSource>(IEnumerable<TSource>, int)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Random_NullCollectionValidItemCount_ThrowsArgumentNullException()
        {
            IEnumerable<char> nullCollection = null;
            const int validItemCount = 0;

            nullCollection.Random(validItemCount);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Random_ValidCollectionNegativeRandomElementsCount_ThrowsArgumentOutOfRangeException()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();
            const int invalidItemCount = -5;

            letters.Random(invalidItemCount);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Random_ValidCollectionTooLargeRandomElementsCount_ThrowsArgumentOutOfRangeException()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();
            const int invalidItemCount = 100;

            letters.Random(invalidItemCount);
        }

        [TestMethod]
        public void Random_ValidParameters_ReturnsValidCollectionItems()
        {
            IEnumerable<char> letters = "abcde".ToCharArray();

            IEnumerable<char> threeRandomLetters = letters.Random(3);

            foreach (char letter in threeRandomLetters)
            {
                letters.ShouldContain(letter);
            }
        }

        #endregion

        #endregion

        #region Without()

        #region Without(IEnumerable<TSource>, TSource)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Without_NullCollection_ThrowsArgumentNullException()
        {
            IEnumerable<char> nullCollection = null;

            nullCollection.Without('c');
        }

        [TestMethod]
        public void Without_ItemThatTheCollectionContains_ReturnsCollectionWithoutItem()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();
            const char letterToRemove = 'a';

            letters = letters.Without(letterToRemove);

            letters.ShouldNotContain('a');
            letters.Count().ShouldEqual(3);
        }

        [TestMethod]
        public void Without_ItemThatTheCollectionDoesNotContain_ReturnsUnmodifiedCollection()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();
            const char letterToRemove = 'z';

            letters = letters.Without(letterToRemove);

            letters.Count().ShouldEqual(4);
        }

        #endregion

        #region Without(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>)

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Without_NullCollectionUsingEqualityComparer_ThrowsArgumentNullException()
        {
            IEnumerable<char> nullCollection = null;
            IEqualityComparer<char> stringLengthEqualityComparer = new StringLengthEqualityComparer<char>();

            nullCollection.Without('c', stringLengthEqualityComparer);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Without_NullEqualityComparer_ThrowsArgumentNullException()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();
            IEqualityComparer<char> nullEqualityComparer = null;

            letters.Without('c', nullEqualityComparer);
        }

        [TestMethod]
        public void Without_StringLengthEqualityComparer_ReturnsCollectionWithoutItemsWithSameStringLength()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
            const string itemToRemove = "banana";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            fruits = fruits.Without(itemToRemove, stringLengthEqualityComparer);

            fruits.ShouldNotContain("banana");
            fruits.ShouldNotContain("cherry");
            fruits.Count().ShouldEqual(2);
        }

        [TestMethod]
        public void Without_StringLengthEqualityComparer_DoesNotRemoveCollectionElementsThatDoNotMatchTheItemToRemoveButEachOther()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana", "cherry" };
            const string itemToRemove = "apricot";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            fruits = fruits.Without(itemToRemove, stringLengthEqualityComparer);

            fruits.ShouldNotContain("apricot");
            fruits.Count().ShouldEqual(3);
        }

        [TestMethod]
        public void Without_ItemThatTheCollectionDoesNotContainAndStringLengthEqualityComparer_ReturnsUnmodifiedCollection()
        {
            IEnumerable<string> stringNumbers = new[] { "1", "22", "333", "4444" };
            const string itemToRemove = "55555";
            IEqualityComparer<string> stringLengthEqualityComparer = new StringLengthEqualityComparer<string>();

            stringNumbers = stringNumbers.Without(itemToRemove, stringLengthEqualityComparer);

            stringNumbers.Count().ShouldEqual(4);
        }

        #endregion

        #endregion
    }
}
