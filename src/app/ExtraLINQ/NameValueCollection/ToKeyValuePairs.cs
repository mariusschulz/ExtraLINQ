using System.Collections.Generic;
using System.Collections.Specialized;

namespace ExtraLinq
{
    public static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Enumerates the specified <see cref="NameValueCollection"/>
        /// as a sequence of key-value pairs of type <see cref="KeyValuePair&lt;TKey,TValue&gt;"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>A sequence of key-value pairs of type <see cref="KeyValuePair&lt;TKey,TValue&gt;"/>.</returns>
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this NameValueCollection collection)
        {
            ThrowIf.Argument.IsNull(collection, "collection");

            return ToKeyValuePairsIterator(collection);
        }

        private static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairsIterator(NameValueCollection collection)
        {
            foreach (string key in collection.Keys)
            {
                yield return new KeyValuePair<string, string>(key, collection[key]);
            }
        }
    }
}
