using System;

namespace MDLibrary.Utils
{
    public static class DateTimeUtils
    {
        /// <summary>
        /// Function converts Unix time (epoch) to UTC
        /// </summary>
        /// <param name="epochSeconds"></param>
        /// <returns></returns>
        public static DateTime EpochToUTC(int epochSeconds)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime converted = epoch.AddSeconds(epochSeconds);

            return converted;
        }

        /// <summary>
        /// Function returns current UTC time as Unix epoch
        /// </summary>
        /// <returns></returns>
        public static UInt32 GetCurrentEpoch()
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = DateTime.UtcNow - epoch;
            UInt32 epochSeconds = (UInt32)ts.TotalSeconds;

            return epochSeconds;
        }

        /// <summary>
        /// Function converts the specified date to Unix epoch
        /// </summary>
        /// <returns></returns>
        public static UInt32 DateToEpoch(DateTime date)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = date - epoch;
            UInt32 epochSeconds = (UInt32)ts.TotalSeconds;

            return epochSeconds;
        }

        public static DateTime EnsureValidSqlDate(DateTime datetime)
        {
            if (datetime < Convert.ToDateTime("1800-01-01"))
                return Convert.ToDateTime("1800-01-01");
            return datetime;
        }

    }
}
