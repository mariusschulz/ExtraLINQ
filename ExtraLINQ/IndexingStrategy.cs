using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    /// <summary>
    /// Represents the indexing strategy that
    /// <see cref="EnumerableExtensions.ElementAt{T}(IEnumerable{T}, int, IndexingStrategy)"/>
    /// uses to return an item from a collection.
    /// </summary>
    public enum IndexingStrategy
    {
        /// <summary>
        /// Clamps the index to the collection's lower and upper border.
        /// </summary>
        Clamp,

        /// <summary>
        /// Treats the collection as a cyclic one and returns the item at the position <c>index % itemCount</c>.
        /// </summary>
        Cyclic,

        /// <summary>
        /// Represents the lookup strategy used by
        /// <see cref="Enumerable.ElementAt{T}(IEnumerable{T}, int)" />.
        /// </summary>
        Regular
    }
}
