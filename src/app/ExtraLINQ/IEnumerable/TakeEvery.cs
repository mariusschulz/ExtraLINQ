using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> TakeEvery<TSource>(this IEnumerable<TSource> source, int step)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsZeroOrNegative(step, "step");

            return source.Where((item, index) => index % step == 0);
        }
    }
}
