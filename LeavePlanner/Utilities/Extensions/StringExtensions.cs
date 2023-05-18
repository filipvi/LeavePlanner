using System.Globalization;

namespace LeavePlanner.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string BoolToString(this bool data)
        {
            return data ? "Da" : "Ne";
        }

        /// <summary>
        /// Nullable decimal to string, specify format (decimal spaces)
        /// </summary>
        public static string DecimalToString(this decimal? data, string format)
        {
            if (!data.HasValue)
                return string.Empty;

            return data.Value.ToString(format);
        }

        /// <summary>
        /// Decimal to string, specify format (decimal spaces)
        /// </summary>
        public static string DecimalToString(this decimal data, string format)
        {
            return data.ToString(format);
        }

        /// <summary>
        /// Decimal to string as currency number, specify format (decimal spaces)
        /// </summary>
        public static string DecimalToStringForPrice(this decimal data, string currency)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.CurrencySymbol = currency;
            culture.NumberFormat.CurrencyPositivePattern = 2;

            return string.Format(culture, "{0:C2}", data);
        }

        /// <summary>
        /// Nullable dateTime to string
        /// </summary>
        public static string DateToString(this DateTime? data)
        {
            if (!data.HasValue)
                return string.Empty;

            string format = "dd.MM.yyyy";

            return data.Value.ToString(format);
        }

        /// <summary>
        /// DateTime to string
        /// </summary>
        public static string DateToString(this DateTime data)
        {
            string format = "dd.MM.yyyy";

            return data.ToString(format);
        }

        /// <summary>
        /// Nullable int to string
        /// </summary>
        public static string NumberToString(this int? data)
        {
            if (!data.HasValue)
                return string.Empty;

            return data.Value.ToString();
        }

        public static string GetLastChars(this int data, int numberOfChars)
        {
            var value = data.ToString();

            if (numberOfChars >= value.Length)
            {
                return value;
            }

            return value.Substring(value.Length - numberOfChars);
        }

        public static string TrimValue(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return value.Trim();
        }

        public static DateTime? StringToExactDateTime(this string date)
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                return startDate;
            }
            return null;
        }
    }
}
