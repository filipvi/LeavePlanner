using System.Globalization;

namespace LeavePlanner.Utilities.Extensions
{
    public static class ConvertToDateTimeExtensions
    {
        /// <summary>
        /// Convert string to datetime
        /// </summary>
        /// <param name="data"> pass data in string format</param>
        public static DateTime StringToDateTime(this string data)
        {
            string[] formats = new[] { "dd.MM.yyyy", "dd.MM.yyyy." };

            return DateTime.ParseExact(data, formats, CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Convert datetime to local datetime, removes date difference in local machine and server
        /// </summary>
        /// <param name="data"> pass DateTime </param>
        public static DateTime DateTimeToLocal(this DateTime data)
        {
            DateTime serverTime = data; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
        }
    }
}
