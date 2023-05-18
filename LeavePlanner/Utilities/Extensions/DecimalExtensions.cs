using System.Globalization;

namespace LeavePlanner.Utilities.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// Used for converting to decimal separator
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PrepareDecimalSeparatorForConverting(this string data)
        {
            if (!string.IsNullOrWhiteSpace(data) && data.Contains('.'))
            {
                data = data.Replace(".", ",");
            }
            return data;
        }

        /// <summary>
        /// Used for converting string to decimal 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Decimal number</returns>
        public static decimal ConvertToDecimal(this string data)
        {
            var numInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var decimalNumber = decimal.Parse(data, numInfo);
            return decimalNumber;
        }

        /// <summary>
        /// Used for converting string to decimal 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Nullable decimal number</returns>
        public static decimal? ConvertToNullableDecimal(this string data)
        {
            decimal? decimalNumber = null;


            if (!string.IsNullOrWhiteSpace(data))
            {
                var numInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
                decimalNumber = decimal.Parse(data, numInfo);
            }

            return decimalNumber;
        }


        public static decimal RoundDecimalNumber(this decimal price, int numberOfDecimalPlaces)
        {
            var roundedPrice = decimal
                .Round(price, numberOfDecimalPlaces, MidpointRounding.AwayFromZero);

            return roundedPrice;
        }
    }
}
