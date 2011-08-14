
namespace ExtraLinq.Internals
{
    internal static class MathHelper
    {
        public static int Clamp(int value, int lowerBorder, int upperBorder)
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
