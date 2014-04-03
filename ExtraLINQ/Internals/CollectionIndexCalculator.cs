using ExtraLinq.Extensions;

namespace ExtraLinq.Internals
{
    internal static class CollectionIndexCalculator
    {
        public static int CalculateCyclicIndex(int index, int sourceItemCount)
        {
            while (index < 0)
            {
                index += sourceItemCount;
            }

            return index % sourceItemCount;
        }

        public static int CalculateClampIndex(int index, int sourceItemCount)
        {
            return index.Clamp(0, sourceItemCount - 1);
        }
    }
}
