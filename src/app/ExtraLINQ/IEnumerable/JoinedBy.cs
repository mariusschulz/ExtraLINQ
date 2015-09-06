using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Concatenates all elements of a sequence using the specified separator between each element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="values"/>.</typeparam>
        /// <param name="values">A sequence that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>A string holding the concatenated values.</returns>
        public static string JoinedBy<TSource>(this IEnumerable<TSource> values, string separator)
        {
            ThrowIf.Argument.IsNull(values, "values");
            ThrowIf.Argument.IsNull(separator, "separator");

            return string.Join(separator, values);
        }
    }
}
