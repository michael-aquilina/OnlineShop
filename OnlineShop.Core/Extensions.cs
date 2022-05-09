using System;

namespace OnlineShop.Core
{
    public static class Extensions
    {
        public static bool IsNullOrWhiteSpace(this string toCompare, string toCompareWith)
        {
            return toCompare?.Equals(toCompareWith, StringComparison.InvariantCultureIgnoreCase) ?? false;
        }
        public static bool EqualsIgnoreCase(this string toCompare, string toCompareWith)
        {
            return toCompare?.Equals(toCompareWith, StringComparison.InvariantCultureIgnoreCase) ?? false;
        }
    }
}
