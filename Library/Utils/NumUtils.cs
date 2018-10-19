using System;

namespace MDLibrary.Utils
{
    public static class NumUtils
    {
        /// <summary>
        /// Returns the value guaranteed to sit within the specified boundaries.
        /// </summary>
        public static T BoundValue<T>(T value, T minValue, T maxValue) where T : IComparable
        {
            if (minValue.CompareTo(value) > 0)
            {
                return minValue;
            }

            if (value.CompareTo(maxValue) > 0)
            {
                return maxValue;
            }

            return value;
        }

        public static int ConvertDoubleToInt(double authDefaultAccountLockoutTimeMins, int defaultval)
        {
            try
            {
                return Convert.ToInt32(authDefaultAccountLockoutTimeMins);
            }
            catch (Exception)
            {
                return defaultval;
            }
        }
    }
}
