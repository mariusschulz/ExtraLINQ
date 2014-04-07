using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ExtraLinq
{
    public static class NameValueCollectionExtensions
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            var dictionary = new Dictionary<string, string>(collection.Count);

            foreach (string key in collection.Keys)
            {
                dictionary.Add(key, collection[key]);
            }

            return dictionary;
        }
    }
}
