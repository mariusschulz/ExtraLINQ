using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ExtraLinq
{
    public static partial class NameValueCollectionExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

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
