
namespace ExtraLinq.Extensions
{
    internal static class IntegerExtensions
    {
        public static int Clamp(this int value, int lowerBorder, int upperBorder)
        {
            if (value < lowerBorder)
            {
                return lowerBorder;
            }

            if (value > upperBorder)
            {
                return upperBorder;
            }

            return value;
        }
    }
}
