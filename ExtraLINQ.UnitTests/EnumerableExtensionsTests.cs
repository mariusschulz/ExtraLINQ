using System;
using System.Collections.Generic;
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
        public void CountsMin_ActualItemCountEqualsExpectedMinItemCount_ReturnsTrue()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersContainsAtLeast4Items = letters.CountsMin(4);

            lettersContainsAtLeast4Items.ShouldBeTrue();
        }

        [TestMethod]
        public void CountsMin_ActualItemCountGreaterThanExpectedMinItemCount_ReturnsTrue()
        {
            IEnumerable<char> letters = "abcd".ToCharArray();

            bool lettersContainsAtLeast2Items = letters.CountsMin(2);

            lettersContainsAtLeast2Items.ShouldBeTrue();
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
        public void CountsMin_CollectionContainingOneMatchingItemExpectingTwoMatchingItems_ReturnsFalse()
        {
            IEnumerable<string> fruits = new[] { "apple", "apricot", "banana" };
            Func<string, bool> startsWithLowercasedB = fruit => fruit.StartsWith("b");

            bool collectionContainsAtLeastTwoMatchingItems = fruits.CountsMin(2, startsWithLowercasedB);

            collectionContainsAtLeastTwoMatchingItems.ShouldBeFalse();
        }

        #endregion

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
    }
}
